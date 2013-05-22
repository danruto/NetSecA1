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
            MerchantCertificate = 1,
            CustomerCertificate = 1
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
            byte[] tmp = Signature.createDigitalSignature(m_CustomerCertificateName, RSASec.getRSA());
            //double tmp2 = RSASec.decrypt(tmp, publicKey);
            byte[] tmp2 = RSASec.getRSA().Decrypt(tmp, false);
            Bernstein hash = new Bernstein();
            UInt32 hashValue = hash.getHash(m_CustomerCertificateName);
            ASCIIEncoding enc = new ASCIIEncoding();

            //string digitalSig1 = RSASec.getRSA().GetString(tmp, 0, tmp.Length);
            //string digitalSig2 = enc.GetString(tmp2, 0, tmp2.Length);

            byte[] digitalSig1 = RSASec.getRSA().SignHash(BitConverter.GetBytes(hashValue), CryptoConfig.MapNameToOID("Bernstein"));
            //byte[] digitalSig2 = 
            string content = "Name: " + "Customer Certificate" +
                             "\nPublicKey: (" + publicKey.k + ", " + publicKey.n + ")" +
                             "\nDigitalSignature: " + digitalSig1 +
                             "\naHash: " + hashValue + 
                             "\nDecDigitalSig: " + "digitalSig2";

            Util.saveCertificateText(Util.m_CustCertFileName, content);
        }


    }
}
