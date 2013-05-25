using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSecSET.Security;
using NetSecSET.Model;

using System.Security.Cryptography;
using ArpanTECH;

namespace NetSecSET.Model
{
    class Customer
    {
        private string m_TAG = "Customer";
        private Bernstein m_Hash;
        private Certificate m_Certificate;
        private RSAx RSAProvider;
        private string m_privateKey;
        private string m_publicKey;

        public Customer(Key publicKey, Key privateKey)        
        {
            //setup(publicKey, privateKey);
        }

        public Customer(int keyLength)
        {
            RSACryptoServiceProvider csp = new RSACryptoServiceProvider(keyLength);
            // Include both Private and Public key
            m_privateKey = csp.ToXmlString(true);//.Replace("><", ">\r\n<");
            m_publicKey = csp.ToXmlString(false);//.Replace("><", ">\r\n<");
            RSAProvider = new RSAx(m_privateKey, keyLength);

            m_Hash = new Bernstein();
            createCertificate();
        }

        public void createCertificate()
        {
            m_Certificate = new Certificate(Certificate.t_CertificateType.CustomerCertificate, RSAProvider, m_publicKey);
        }

        public byte[] createDualSignature()
        {
            Util.Log(m_TAG, "creating dual signature...");

            // obtain both the OI and PI from file
            string OI = Util.loadOI(Util.m_OIFileName);
            string PI = Util.loadPI(Util.m_PIFileName);

            // create the hashes for both files
            UInt32 PIMD = createPIMDHash(PI);
            UInt32 OIMD = createOIMDHash(OI);

            UInt32 combinedHash = PIMD + OIMD;

            //byte[] POMD = BitConverter.GetBytes(createPOMD(combinedHash));
            string POMDstr = createPOMD(combinedHash) + "";
            byte[] POMD = Encoding.UTF8.GetBytes(POMDstr);

            //UInt32 DS = RSASec.encrypt(POMD, RSAProvider);
            // Encrypt using Private Key
            RSAProvider.RSAxHashAlgorithm = RSAxParameters.RSAxHashAlgorithm.SHA1;
            byte[] dualSignatureBytes = RSAProvider.Encrypt(POMD, true, true);
            string fk2 = Convert.ToBase64String(dualSignatureBytes);
            byte[] fk3 = Encoding.UTF8.GetBytes(fk2);

           /* try
            {
                byte[] fk = RSAProvider.Decrypt(dualSignatureBytes, false, true);
                string fk4 = Encoding.UTF8.GetString(fk);
                Util.Log(m_TAG, "cDS(): WORKS!");
            }
            catch (Exception ex) { Util.Log(m_TAG, "cDS(): Error, \n" + ex.ToString()); };*/
            return dualSignatureBytes;
        }

        public UInt32 createPIMDHash(string PI)
        {
            Util.Log(m_TAG, "creating PIMD hash");
            return m_Hash.getHash(PI);
        }

        public UInt32 createOIMDHash(string OI)
        {
            Util.Log(m_TAG, "creating OIMD hash");
            return m_Hash.getHash(OI);
        }

        public UInt32 createPOMD(UInt32 combinedHash)
        {
            Util.Log(m_TAG, "creating POMD");
            return m_Hash.getHash(combinedHash + "");
        }


        
    }

    
}
