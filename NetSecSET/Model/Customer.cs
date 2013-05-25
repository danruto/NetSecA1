using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSecSET.Security;
using NetSecSET.Model;

using System.Security.Cryptography;
using ArpanTECH;
using System.IO;
using System.Numerics;

namespace NetSecSET.Model
{
    class Customer
    {
        private const string m_TAG = "Customer";
        private Bernstein m_Hash;
        private Certificate m_Certificate;
        private RSAx RSAProvider;
        private string m_privateKey;
        private string m_publicKey;

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
            string OI = Util.readText(Util.m_OIFileName);
            string PI = Util.readText(Util.m_PIFileName);

            // create the hashes for both files
            UInt32 PIMD = createPIMDHash(PI);
            UInt32 OIMD = createOIMDHash(OI);

            writePIMD(PIMD);
            writeOIMD(OIMD);

            UInt32 combinedHash = PIMD + OIMD;

            string POMDstr = createPOMD(combinedHash) + "";
            byte[] POMD = Encoding.UTF8.GetBytes(POMDstr);

            // Encrypt using Private Key
            RSAProvider.RSAxHashAlgorithm = RSAxParameters.RSAxHashAlgorithm.SHA1;
            byte[] dualSignatureBytes = RSAProvider.Encrypt(POMD, true, true);

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

        public void writeOIMD(UInt32 OI)
        {
            Util.writeText(Util.m_OIEFileName, OI + "");
        }

        public void writePIMD(UInt32 PI)
        {
            Util.writeText(Util.m_PIEFileName, PI + "");
        }

        #region oldCode

        public Customer(Key publicKey, Key privateKey)
        {

        }

        public void writeEncryptedOI(string OI)
        {
            byte[] toEnc = Encoding.UTF8.GetBytes(OI);
            byte[] encOI = RSAProvider.Encrypt(toEnc, true, true);
            Util.Log(m_TAG, "Customer: Writing encrypted OI");
            File.WriteAllBytes(@Util.m_OIEFileName, encOI);
        }

        public void writeEncryptedPI(string PI)
        {
            byte[] toEnc = Encoding.UTF8.GetBytes(PI);
            byte[] encPI = RSAProvider.Encrypt(toEnc, true, true);
            Util.Log(m_TAG, "Customer: Writing encrypted PI");
            File.WriteAllBytes(@Util.m_PIEFileName, encPI);
        }

        #endregion
    }

    
}
