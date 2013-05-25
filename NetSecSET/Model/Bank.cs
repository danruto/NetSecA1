using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSecSET.Model;
using NetSecSET.Security;
using System.Security.Cryptography;
using ArpanTECH;
using System.Text.RegularExpressions;

namespace NetSecSET.Model
{
    class Bank
    {

        //calling hash and RSA for certificates, public and private keys
        private string m_TAG = "Bank";
        private Bernstein m_Hash = new Bernstein();
        private RSAx RSAProvider;
        private Certificate m_Certificate;
        private string decryptedMsg;
        public Key publicKey { get; set; }
        public Key privateKey { get; set; }
        private string m_privateKey;
        private string m_publicKey;


        //assigning public and private keys to certificates for encryption/decryption
        public Bank(Key publicKey, Key privateKey)
        {
            this.publicKey = publicKey;
            this.privateKey = privateKey;  
            //RSAProvider = new RSACryptoServiceProvider();
            createCertificate();
        }

        public Bank(int keyLength)
        {
            RSACryptoServiceProvider csp = new RSACryptoServiceProvider(keyLength);
            // Include both Private and Public key
            m_privateKey = csp.ToXmlString(true).Replace("><", ">\r\n<");
            m_publicKey = csp.ToXmlString(false).Replace("><", ">\r\n<");
            RSAProvider = new RSAx(m_privateKey, keyLength);

            m_Hash = new Bernstein();
            createCertificate();
        }

        private void createCertificate()
        {
            m_Certificate = new Certificate(Certificate.t_CertificateType.BankCertificate, RSAProvider, m_publicKey);
        }

        // DS, PI, OIMD
        public void decrypt()
        {
            // Load the customer certificate
            string customerCert = Util.loadCertificateText(Util.m_CustCertFileName);
            string key = "";

            Match match = Regex.Match(customerCert, @"(<RSAKeyValue>\S+)");
            if (match.Success)
            {
                key = match.Groups[1].Value;//.Replace("><", ">\r\n<");
                Util.Log(m_TAG, "Customer Public Key found:\n" + key);
            }

            RSAx custPublicRSA = new RSAx(key, 1024);

            Bernstein hash = new Bernstein();

            try
            {
                string DS = Util.loadDualSignature();
                //byte[] dualSignatureBytes = Convert.FromBase64String(DS);
                byte[] dualSignatureBytes = Util.loadDualSignatureBytes();
                string OI = Util.loadPI(Util.m_OIFileName);
                UInt32 OIMD = hash.getHash(OI);
                string PI = Util.loadOI(Util.m_PIFileName);
                string POMD = hash.getHash(hash.getHash(PI) + OIMD + "") + "";

                // Use public key for decryption
                custPublicRSA.RSAxHashAlgorithm = RSAxParameters.RSAxHashAlgorithm.SHA1;
                byte[] decryptedDualSignatureBytes = custPublicRSA.Decrypt(dualSignatureBytes, false, true);
                string POMDDecrypted = Encoding.UTF8.GetString(decryptedDualSignatureBytes);

                if (POMDDecrypted.Equals(POMD))
                    Util.Log(m_TAG, "Decryption Succeeded");
                else
                {
                    Util.Log(m_TAG, "POMD do not match!");
                    Util.Log(m_TAG, "DS: " + DS);
                }
            }
            catch (Exception ex) { Util.Log(m_TAG, "Bank: Error in decryption" + ex.ToString()); }
        }

        public string hashOI(string PI)
        {
            return Convert.ToString(m_Hash.getHash(PI));
        }

        public string concatenateString(string PI, int OIMD)
        {
            return hashOI(PI) + OIMD;
        }

        public string hashPI_OIMD(string PI, int OIMD)
        {
            return Convert.ToString(m_Hash.getHash(concatenateString(PI, OIMD)));
        }

        public bool verifyDS(string PI, int OIMD)
        {
            return decryptedMsg == hashPI_OIMD(PI, OIMD);
        }

    }
}
