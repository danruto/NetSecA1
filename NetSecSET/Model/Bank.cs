using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSecSET.Model;
using NetSecSET.Security;
using System.Security.Cryptography;

namespace NetSecSET.Model
{
    class Bank
    {

        private string m_TAG = "Bank";
        private Bernstein hash = new Bernstein();
        private RSACryptoServiceProvider RSAProvider;
        private Certificate m_Certificate;
        private string decryptedMsg;
        public Key publicKey { get; set; }
        public Key privateKey { get; set; }


        public Bank(Key publicKey, Key privateKey)
        {
            this.publicKey = publicKey;
            this.privateKey = privateKey;
            RSAProvider = new RSACryptoServiceProvider();
            createCertificate();
        }

        private void createCertificate()
        {
            m_Certificate = new Certificate(Certificate.t_CertificateType.BankCertificate, RSAProvider);
        }

        public void decrypt()
        {
            //int DS, PaymentInfo PI, int OIMD
        }

        public string hashOI(string PI)
        {
            return Convert.ToString(hash.getHash(PI));
        }

        public string concatenateString(string PI, int OIMD)
        {
            return hashOI(PI) + OIMD;
        }

        public string hashPI_OIMD(string PI, int OIMD)
        {
            return Convert.ToString(hash.getHash(concatenateString(PI, OIMD)));
        }

        public bool verifyDS(string PI, int OIMD)
        {
            return decryptedMsg == hashPI_OIMD(PI, OIMD);
        }

    }
}
