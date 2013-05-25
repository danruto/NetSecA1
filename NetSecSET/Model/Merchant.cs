using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSecSET.Model;
using NetSecSET.Security;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using ArpanTECH;

namespace NetSecSET.Model
{
    class Merchant
    {
        // create hashes+keys for Merchant certificate
        private string m_TAG = "Merchant";
        private Bernstein m_Hash = new Bernstein();
        private RSAx RSAProvider;
        private Certificate m_Certificate;
        private string decryptedMsg;
        public Key publicKey { get; set; }
        public Key privateKey { get; set; }
        private string m_privateKey;
        private string m_publicKey;

        public Merchant(Key publicKey, Key privateKey)
        {
            this.publicKey = publicKey;
            this.privateKey = privateKey;
            //RSAProvider = new RSACryptoServiceProvider();
            createCertificate();
        }

        public Merchant(int keyLength)
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
            m_Certificate = new Certificate(Certificate.t_CertificateType.MerchantCertificate, RSAProvider, m_publicKey);
        }

        /*public void decrypt()
        {
            // Check the customer certificate first
            string customerCert = Util.loadCertificateText(Util.m_CustCertFileName);
            //http://rubular.com/
            // find public key
            //Match match = Regex.Match(customerCert, @"\<RSAKeyValue\>\<Modulus\>([A-Za-z0-9\-\=\/\\]+)=\<\/Modulus\>\<Exponent\>([A-Za-z0-9\-\=\/\\])\</Exponent\>\<\/RSAKeyValue\>", RegexOptions.IgnoreCase);
            //<RSAKeyValue><Modulus>
            Match match = Regex.Match(customerCert, @"(<RSAKeyValue>\S+<\/Exponent>)");
            if (match.Success)
            {
                string key = match.Groups[1].Value;
                Util.Log(m_TAG, key);
            }
            
            //int DS, OrderInfo OI, int PIMD
            Bernstein hash = new Bernstein();
            UInt32 DS = Util.loadDualSignature();
            string PI = Util.loadPI(Util.m_PIFileName);
            UInt32 PIMD = hash.getHash(PI);
            string OI = Util.loadOI(Util.m_OIFileName);
            UInt32 POMD = hash.getHash(hash.getHash(OI) + PIMD + "");
            //UInt32 decryptedDS = RSASec.decrypt(DS, RSAProvider);

            if (DS.Equals(POMD))
                Util.Log(m_TAG, "merchant: decrypt() successful");
            else
                Util.Log(m_TAG, "merchant: decrypt() unsuccessful");

        }*/

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
                string PI = Util.loadPI(Util.m_PIFileName);
                UInt32 PIMD = hash.getHash(PI);
                string OI = Util.loadOI(Util.m_OIFileName);
                string POMD = hash.getHash(hash.getHash(OI) + PIMD + "") + "";

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
            catch (Exception ex) { Util.Log(m_TAG, "Merchant: Error in decryption" + ex.ToString()); }
        }

        // encrypting and decrypting OI, PIMD and OIMD
        public string hashOI(string OI)
        {
            
            return Convert.ToString(m_Hash.getHash(OI));
        }

        public string concatenateString(string OI, int PIMD)
        {
            return hashOI(OI) + PIMD;
        }

        public string hashOI_PIMD(string OI, int PIMD)
        {
            return Convert.ToString(m_Hash.getHash(concatenateString(OI, PIMD)));
        }

        public bool verifyDS(string PI, int OIMD)
        {
            return decryptedMsg == hashOI_PIMD(PI, OIMD);
        }

    }
}
