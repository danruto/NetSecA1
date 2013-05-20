using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSecSET.Model;
using NetSecSET.Security;

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

        public Certificate(t_CertificateType ct, Key publicKey, Key privateKey)
        {
            switch (ct)
            {
                case t_CertificateType.BankCertificate:
                    createBankCertificate(publicKey, privateKey);
                    break;
                case t_CertificateType.MerchantCertificate:
                    createMerchantCertificate(publicKey, privateKey);
                    break;
                case t_CertificateType.CustomerCertificate:
                    createCustomerCertificate(publicKey, privateKey);
                    break;
                default:
                    break;
            }
        }

        public void createBankCertificate(Key publicKey, Key privateKey)
        {
            /*byte[] tmp = Signature.createDigitalSignature(m_BankCertificateName, privateKey);
            string digitalSig = Encoding.UTF8.GetString(tmp, 0, tmp.Length);

            string content = "Name: " + "Bank Certificate" +
                             "\nPublicKey: (" + publicKey.k + ", " + publicKey.n + ")" +
                             "\nDigitalSignature: " + digitalSig;

            Util.saveCertificateText(Util.m_BankCertFileName, content);*/
        }

        public void createMerchantCertificate(Key publicKey, Key privateKey)
        {
            /*byte[] tmp = Signature.createDigitalSignature(m_MerchantCertificateName, privateKey);
            string digitalSig = Encoding.UTF8.GetString(tmp, 0, tmp.Length);

            string content = "Name: " + "Merchant Certificate" +
                             "\nPublicKey: (" + publicKey.k + ", " + publicKey.n + ")" +
                             "\nDigitalSignature: " + digitalSig;

            Util.saveCertificateText(Util.m_MerchantCertFileName, content);*/
        }

        public void createCustomerCertificate(Key publicKey, Key privateKey)
        {
            double tmp = Signature.createDigitalSignature(m_CustomerCertificateName, privateKey);
            double tmp2 = RSASec.decrypt(tmp, publicKey);
            Bernstein hash = new Bernstein();
            UInt32 hashValue = hash.getHash(m_CustomerCertificateName);
            //ASCIIEncoding enc = new ASCIIEncoding();

            //string digitalSig = Encoding.UTF8.GetString(tmp, 0, tmp.Length);
            string content = "Name: " + "Customer Certificate" +
                             "\nPublicKey: (" + publicKey.k + ", " + publicKey.n + ")" +
                             "\nDigitalSignature: " + tmp +
                             "\naHash: " + hashValue + 
                             "\nDecDigitalSig: " + tmp2;

            Util.saveCertificateText(Util.m_CustCertFileName, content);
        }


    }
}
