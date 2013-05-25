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
        private const string m_TAG = "Merchant";
        private Bernstein m_Hash = new Bernstein();
        private RSAx RSAProvider;
        private Certificate m_Certificate;

        public Key publicKey { get; set; }
        public Key privateKey { get; set; }

        private string m_privateKey;
        private string m_publicKey;

        private bool dataVerified;

        public Merchant(Key publicKey, Key privateKey)
        {
            this.publicKey = publicKey;
            this.privateKey = privateKey;
            createCertificate();
        }

        public Merchant(int keyLength)
        {
            RSACryptoServiceProvider csp = new RSACryptoServiceProvider(keyLength);
            // Include both Private and Public key
            m_privateKey = csp.ToXmlString(true).Replace("><", ">\r\n<");
            m_publicKey = csp.ToXmlString(false).Replace("><", ">\r\n<");
            RSAProvider = new RSAx(m_privateKey, keyLength);
            dataVerified = false;
            m_Hash = new Bernstein();
            createCertificate();
        }

        private void createCertificate()
        {
            m_Certificate = new Certificate(Certificate.t_CertificateType.MerchantCertificate, RSAProvider, m_publicKey);
        }

        // DS, OI, PIMD
        public void verifyOrder()
        {
            // Load the customer certificate
            string customerCert = Util.readText(Util.m_CustCertFileName);
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
                byte[] dualSignatureBytes = Util.readBytes(Util.m_DualSignatureFileName);
                string PIMDString = Util.readText(Util.m_PIEFileName);
                UInt32 PIMD = Convert.ToUInt32(PIMDString);
                string OI = Util.readText(Util.m_OIFileName);
                string POMD = hash.getHash(hash.getHash(OI) + PIMD + "") + "";

                // Use public key for decryption
                custPublicRSA.RSAxHashAlgorithm = RSAxParameters.RSAxHashAlgorithm.SHA1;
                byte[] decryptedDualSignatureBytes = custPublicRSA.Decrypt(dualSignatureBytes, false, true);
                string POMDDecrypted = Encoding.UTF8.GetString(decryptedDualSignatureBytes);

                if (POMDDecrypted.Equals(POMD))
                {
                    dataVerified = true;
                    Util.Log(m_TAG, "Decryption Succeeded");
                }
                else
                {
                    dataVerified = false;
                    Util.Log(m_TAG, "POMD do not match!");
                }
            }
            catch (Exception) { Util.Log(m_TAG, "Merchant: Error in decryption. Keys do not match"); dataVerified = false; }
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

        public string getProductNumber()
        {
            string productNumber = "";
            if (dataVerified)
            {
                try
                {
                    string OI = Util.readText(Util.m_OIFileName);
                    Match match = Regex.Match(OI, @"(Product Number: [0-9]+)");
                    if (match.Success)
                    {
                        productNumber = match.Groups[1].Value.Replace("Product Number: ", "");
                        //Util.Log(m_TAG, "Merchant: Get product number");
                    }
                }
                catch (Exception) { }
            }
            return productNumber;
        }

        public string getProductName()
        {
            string productName = "";
            if (dataVerified)
            {
                try
                {
                    string OI = Util.readText(Util.m_OIFileName);

                    Match match = Regex.Match(OI, @"(Product Name: [a-zA-Z]+)");
                    if (match.Success)
                    {
                        productName = match.Groups[1].Value.Replace("Product Name: ", "");
                        //Util.Log(m_TAG, "Merchant: Get product name");
                    }
                }
                catch (Exception) { }
            }
            return productName;
        }

        public string getCustomerName()
        {
            string customerName = "";
            if (dataVerified)
            {
                try
                {
                    string OI = Util.readText(Util.m_OIFileName);

                    Match match = Regex.Match(OI, @"(Customer Name: [a-zA-Z]+ [a-zA-Z]+)");
                    if (match.Success)
                    {
                        customerName = match.Groups[1].Value.Replace("Customer Name: ", "");
                        //Util.Log(m_TAG, "Merchant: Get customer name");
                    }
                }
                catch (Exception) { }
            }
            return customerName;
        }

        public string getAddress()
        {
            string address = "";
            if (dataVerified)
            {
                try
                {
                    string OI = Util.readText(Util.m_OIFileName);

                    Match match = Regex.Match(OI, @"(Customer address: [0-9]+ [a-zA-Z]+[a-zA-Z]+ [a-zA-Z]+, [a-zA-Z]+)");
                    if (match.Success)
                    {
                        address = match.Groups[1].Value.Replace("Customer address: ", "");
                        //Util.Log(m_TAG, "Merchant: Get address");
                    }
                }
                catch (Exception) { }
            }
            return address;
        }

        public string getCustomerNumber()
        {
            string customerNumber = "";
            if (dataVerified)
            {
                try
                {
                    string OI = Util.readText(Util.m_OIFileName);

                    Match match = Regex.Match(OI, @"(Customer Contact: [0-9]+)");
                    if (match.Success)
                    {
                        customerNumber = match.Groups[1].Value.Replace("Customer Contact: ", "");
                        // Util.Log(m_TAG, "Merchant: Get customer number");
                    }
                }
                catch (Exception) { }
            }
            return customerNumber;
        }

    }
}
