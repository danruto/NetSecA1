using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSecSET.Model;
using NetSecSET.Security;
using System.Security.Cryptography;

using ArpanTECH;

namespace NetSecSET
{
    class Certificate
    {
        public enum t_CertificateType
        {
            BankCertificate = 1,
            MerchantCertificate,
            CustomerCertificate
        };

        private const string m_BankCertificateName = "Bank";  
        private const string m_MerchantCertificateName = "Merchant";
        private const string m_CustomerCertificateName = "Customer";

        public Certificate(t_CertificateType ct, RSAx RSAProvider, string publicKey)
        {
            switch (ct)
            {
                case t_CertificateType.BankCertificate:
                    createBankCertificate(RSAProvider, publicKey);
                    break;
                case t_CertificateType.MerchantCertificate:
                    createMerchantCertificate(RSAProvider, publicKey);
                    break;
                case t_CertificateType.CustomerCertificate:
                    createCustomerCertificate(RSAProvider, publicKey);
                    break;
                default:
                    break;
            }
        }

        //creating Bank certificate 
        public void createBankCertificate(RSAx RSAProvider, string publicKey)
        {
            Bernstein hash = new Bernstein(); //new Bernstein hash

            string content = "Name: " + "Bank Certificate" +
                             "\nCA: Network Security";

            UInt32 hashValue = hash.getHash(m_BankCertificateName);
            UInt32 digitalSignature = Signature.createDigitalSignature(hashValue, RSAProvider);

            content +=      //"\nPublicKey: (" + publicKey.k + ", " + publicKey.n + ")" +
                             "\nDigitalSignature: " + digitalSignature +
                             "\naHash: " + hashValue +
                             "\nPublic Key: " + publicKey;

            Util.writeText(Util.m_BankCertFileName, content);
        }

        //creating Merchant certificate
        public void createMerchantCertificate(RSAx RSAProvider, string publicKey)
        {
            //creating Bernstein hash function
            Bernstein hash = new Bernstein();

            string content = "Name: " + "Merchant Certificate" +
                             "\nCA: Network Security";

            //hash value for Merchant certificate
            UInt32 hashValue = hash.getHash(m_MerchantCertificateName);
            //creating digital signature, applying hash value and RSA
            UInt32 digitalSignature = Signature.createDigitalSignature(hashValue, RSAProvider);

            content +=      //"\nPublicKey: (" + publicKey.k + ", " + publicKey.n + ")" +
                             "\nDigitalSignature: " + digitalSignature +
                             "\naHash: " + hashValue +
                             "\nPublic Key: " + publicKey;

            Util.writeText(Util.m_MerchantCertFileName, content);
        }

        //creating Customer certificate
        public void createCustomerCertificate(RSAx RSAProvider, string publicKey)
        {
            //creating Bernstein hash to apply to certificates
            Bernstein hash = new Bernstein();

            string content = "Name: " + "Customer Certificate" +
                             "\nCA: Network Security";

            UInt32 hashValue = hash.getHash(content);
            UInt32 digitalSignature = Signature.createDigitalSignature(hashValue, RSAProvider);

            content +=      //"\nPublicKey: (" + publicKey.k + ", " + publicKey.n + ")" +
                             "\nDigitalSignature: " + digitalSignature +
                             "\naHash: " + hashValue +
                             "\nPublic Key: " + publicKey;



            Util.writeText(Util.m_CustCertFileName, content);
        }

        #region oldCode

        public Certificate(t_CertificateType ct, RSACryptoServiceProvider RSAProvider)
        {
            switch (ct)
            {
                case t_CertificateType.BankCertificate:
                    createBankCertificate(RSAProvider);
                    break;
                case t_CertificateType.MerchantCertificate:
                    createMerchantCertificate(RSAProvider);
                    break;
                case t_CertificateType.CustomerCertificate:
                    createCustomerCertificate(RSAProvider);
                    break;
                default:
                    break;
            }
        }

        //creating Bank certificate 
        public void createBankCertificate(RSACryptoServiceProvider RSAProvider)
        {
            Bernstein hash = new Bernstein(); //new Bernstein hash
            UInt32 hashValue = hash.getHash(m_BankCertificateName);
            UInt32 digitalSignature = Signature.createDigitalSignature(hashValue, RSAProvider);
            string content = "Name: " + "Customer Certificate" +
                             //"\nPublicKey: (" + publicKey.k + ", " + publicKey.n + ")" +
                             "\nPublic Key:\n" + RSAProvider.ToXmlString(false) +
                             "\nDigitalSignature: " + digitalSignature +
                             "\naHash: " + hashValue;

            Util.writeText(Util.m_BankCertFileName, content);
        }

       //creating Merchant certificate
       public void createMerchantCertificate(RSACryptoServiceProvider RSAProvider)
        {
            //creating Bernstein hash function
            Bernstein hash = new Bernstein(); 
            //hash value for Merchant certificate
            UInt32 hashValue = hash.getHash(m_MerchantCertificateName); 
            //creating digital signature, applying hash value and RSA
            UInt32 digitalSignature = Signature.createDigitalSignature(hashValue, RSAProvider); 
            string content = "Name: " + "Customer Certificate" +
                             //"\nPublicKey: (" + publicKey.k + ", " + publicKey.n + ")" +
                             "\nPublic Key:\n" + RSAProvider.ToXmlString(false) +
                             "\nDigitalSignature: " + digitalSignature +
                             "\naHash: " + hashValue;

            Util.writeText(Util.m_MerchantCertFileName, content);
        }

        //creating Customer certificate
        public void createCustomerCertificate(RSACryptoServiceProvider RSAProvider)
        {
            //creating Bernstein hash to apply to certificates
            Bernstein hash = new Bernstein();
            //obtaining hash value for Customer certificate
            UInt32 hashValue = hash.getHash(m_CustomerCertificateName);
            UInt32 digitalSignature = Signature.createDigitalSignature(hashValue, RSAProvider);
            string content = "Name: " + "Customer Certificate" +
                //"\nPublicKey: (" + publicKey.k + ", " + publicKey.n + ")" +
                             "\nPublic Key:\n" + RSAProvider.ToXmlString(false) +
                             "\nDigitalSignature: " + digitalSignature +
                             "\naHash: " + hashValue;

            Util.writeText(Util.m_CustCertFileName, content);
        }

        #endregion
    }
}
