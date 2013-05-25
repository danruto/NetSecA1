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
        private bool dataVerifed;

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
            dataVerifed = false;
            m_Hash = new Bernstein();
            createCertificate();
        }

        private void createCertificate()
        {
            m_Certificate = new Certificate(Certificate.t_CertificateType.BankCertificate, RSAProvider, m_publicKey);
        }

        // DS, PI, OIMD
        public void verifyPayment()
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
                byte[] dualSignatureBytes = Util.loadDualSignatureBytes();
                string OIStr = Util.loadOIMD(Util.m_OIEFileName);
                UInt32 OIMD = Convert.ToUInt32(OIStr);
                string PI = Util.loadOI(Util.m_PIFileName);
                string POMD = hash.getHash(hash.getHash(PI) + OIMD + "") + "";

                // Use public key for decryption
                custPublicRSA.RSAxHashAlgorithm = RSAxParameters.RSAxHashAlgorithm.SHA1;
                byte[] decryptedDualSignatureBytes = custPublicRSA.Decrypt(dualSignatureBytes, false, true);
                string POMDDecrypted = Encoding.UTF8.GetString(decryptedDualSignatureBytes);

                if (POMDDecrypted.Equals(POMD))
                {
                    dataVerifed = true;
                    Util.Log(m_TAG, "Decryption Succeeded");
                }
                else
                {
                    dataVerifed = false;
                    Util.Log(m_TAG, "POMD do not match!");
                    Util.Log(m_TAG, "DS: " + DS);
                }
            }
            catch (Exception ex) { Util.Log(m_TAG, "Bank: Error in decryption. Keys do not match"); dataVerifed = false; }
        }

        public string getCost()
        {
            string cost = "";
            if (dataVerifed)
            {
                try
                {
                    string PI = Util.loadPI(Util.m_PIFileName);

                    Match match = Regex.Match(PI, @"(Payment Amount: [0-9]+)");
                    if (match.Success)
                    {
                        cost = match.Groups[1].Value.Replace("Payment Amount: ", "");
                        // Util.Log(m_TAG, "Merchant: Get customer number");
                    }
                }
                catch (Exception) { }
            }
            return cost;
        }

        public string getCVV()
        {
            string ccv = "";
            if (dataVerifed)
            {
                try
                {
                    string PI = Util.loadPI(Util.m_PIFileName);

                    Match match = Regex.Match(PI, @"(CVV Number: [0-9]+)");
                    if (match.Success)
                    {
                        ccv = match.Groups[1].Value.Replace("CVV Number: ", "");
                        // Util.Log(m_TAG, "Merchant: Get customer number");
                    }
                }
                catch (Exception) { }
            }
            return ccv;
        }

        public string getCCNb()
        {
            string ccNb = "";
            if (dataVerifed)
            {
                try
                {
                    string PI = Util.loadPI(Util.m_PIFileName);

                    Match match = Regex.Match(PI, @"(Credit Card Number: [0-9]+)");
                    if (match.Success)
                    {
                        ccNb = match.Groups[1].Value.Replace("Credit Card Number: ", "");
                        // Util.Log(m_TAG, "Merchant: Get customer number");
                    }
                }
                catch (Exception) { }
            }
            return ccNb;
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
