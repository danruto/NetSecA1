using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSecSET.Model;
using NetSecSET.Security;
using System.Security.Cryptography;

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

        private string m_BankCertificateName = "Bank";
        private string m_MerchantCertificateName = "Merchant";
        private string m_CustomerCertificateName = "Customer";



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

        public void createBankCertificate(RSACryptoServiceProvider RSAProvider)
        {
            Bernstein hash = new Bernstein();
            UInt32 hashValue = hash.getHash(m_BankCertificateName);
            UInt32 digitalSignature = Signature.createDigitalSignature(hashValue, RSAProvider);
            string content = "Name: " + "Customer Certificate" +
                             //"\nPublicKey: (" + publicKey.k + ", " + publicKey.n + ")" +
                             "\nPublic Key:\n" + RSAProvider.ToXmlString(false) +
                             "\nDigitalSignature: " + digitalSignature +
                             "\naHash: " + hashValue;

            Util.saveCertificateText(Util.m_BankCertFileName, content);
        }

        public void createMerchantCertificate(RSACryptoServiceProvider RSAProvider)
        {
            Bernstein hash = new Bernstein();
            UInt32 hashValue = hash.getHash(m_MerchantCertificateName);
            UInt32 digitalSignature = Signature.createDigitalSignature(hashValue, RSAProvider);
            string content = "Name: " + "Customer Certificate" +
                             //"\nPublicKey: (" + publicKey.k + ", " + publicKey.n + ")" +
                             "\nPublic Key:\n" + RSAProvider.ToXmlString(false) +
                             "\nDigitalSignature: " + digitalSignature +
                             "\naHash: " + hashValue;

            Util.saveCertificateText(Util.m_MerchantCertFileName, content);
        }

        public void createCustomerCertificate(RSACryptoServiceProvider RSAProvider)
        {
            /*byte[] tmp = Signature.createDigitalSignature(m_CustomerCertificateName, RSASec.getRSA());
            double tmp2 = RSASec.decrypt(tmp, publicKey);
            byte[] tmp2 = RSASec.getRSA().Decrypt(tmp, false);
            
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] hashBytes = BitConverter.GetBytes(hashValue);

            byte[] dSigTemp = RSASec.getRSA().Encrypt(hashBytes, false);
            UInt32 digitalSig1 = BitConverter.ToUInt32(dSigTemp, 0);

            byte[] dSigDec = RSASec.getRSA().Decrypt(dSigTemp, false);
            UInt32 digitalSig2 = BitConverter.ToUInt32(dSigDec, 0);*/

            Bernstein hash = new Bernstein();
            UInt32 hashValue = hash.getHash(m_CustomerCertificateName);
            UInt32 digitalSignature = Signature.createDigitalSignature(hashValue, RSAProvider);
            string content = "Name: " + "Customer Certificate" +
                //"\nPublicKey: (" + publicKey.k + ", " + publicKey.n + ")" +
                             "\nPublic Key:\n" + RSAProvider.ToXmlString(false) +
                             "\nDigitalSignature: " + digitalSignature +
                             "\naHash: " + hashValue;

            Util.saveCertificateText(Util.m_CustCertFileName, content);
        }


    }
}
