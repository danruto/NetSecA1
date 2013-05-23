using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSecSET.Security;
using NetSecSET.Model;

using System.Security.Cryptography;

namespace NetSecSET.Model
{
    class Customer
    {
        private string m_TAG = "Customer";
        private Bernstein m_Hash;
        private Certificate m_Certificate;
        private RSACryptoServiceProvider RSAProvider;
        public Key publicKey { get; set; }
        public Key privateKey { get; set; }

        public Customer(Key publicKey, Key privateKey)        
        {
            setup(publicKey, privateKey);
        }

        public void setup(Key publicKey, Key privateKey)
        {
            m_Hash = new Bernstein();
            RSAProvider = new RSACryptoServiceProvider();
            this.publicKey = publicKey;
            this.privateKey = privateKey;

            createCertificate();
        }

        public void createCertificate()
        {
            m_Certificate = new Certificate(Certificate.t_CertificateType.CustomerCertificate, RSAProvider);
        }

        public UInt32 createDualSignature()
        {
            Util.Log(m_TAG, "creating dual signature...");

            // get both the OI and PI from file
            OrderInfo OI = Util.loadOI(Util.m_OIFileName);
            PaymentInfo PI = Util.loadPI(Util.m_PIFileName);

            // create the hashs for both files
            UInt32 PIMD = createPIMDHash(PI);
            UInt32 OIMD = createOIMDHash(OI);

            UInt32 combinedHash = PIMD + OIMD;

            UInt32 POMD = createPOMD(combinedHash);

            UInt32 DS = RSASec.encrypt(POMD, RSAProvider);

            return DS;
            //RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            //byte[] encryptedData = RSA.Encrypt(POMD, false);
            //return encryptedData;
            
        }

        public UInt32 createPIMDHash(PaymentInfo PI)
        {
            Util.Log(m_TAG, "creating PIMD hash");
            return m_Hash.getHash(PI.getContent());
        }

        public UInt32 createOIMDHash(OrderInfo OI)
        {
            Util.Log(m_TAG, "creating OIMD hash");
            return m_Hash.getHash(OI.getContent());
        }

        public UInt32 createPOMD(UInt32 combinedHash)
        {
            Util.Log(m_TAG, "creating POMD");
            return m_Hash.getHash(combinedHash + "");
        }

        
    }

    
}
