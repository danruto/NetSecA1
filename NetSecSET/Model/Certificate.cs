using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSecSET.Model;

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

        public Certificate(t_CertificateType ct, Key publicKey)
        {
            switch (ct)
            {
                case t_CertificateType.BankCertificate:
                    createBankCertificate(publicKey);
                    break;
                case t_CertificateType.MerchantCertificate:
                    createMerchantCertificate();
                    break;
                case t_CertificateType.CustomerCertificate:
                    createCustomerCertificate();
                    break;
                default:
                    break;
            }
        }

        public void createBankCertificate(Key publicKey)
        {
            string content = "Name: " + "Certificate" +
                             "\nPublicKey: (" + publicKey.k + ", " + publicKey.n + ")" +
                             "\nDigitalSignature: " + "";
        }

        public void createMerchantCertificate()
        {
        }

        public void createCustomerCertificate()
        {
        }


    }
}
