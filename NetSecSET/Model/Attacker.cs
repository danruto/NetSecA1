using ArpanTECH;
using NetSecSET.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NetSecSET.Model
{
    class Attacker
    {
        private string m_privateKey;
        private string m_publicKey;
        private Bernstein m_Hash;
        private RSAx RSAProvider;
        private const string m_TAG = "Attacker";

        public Attacker(int keyLength)
        {
            RSACryptoServiceProvider csp = new RSACryptoServiceProvider(keyLength);
            // Include both Private and Public key
            m_privateKey = csp.ToXmlString(true);//.Replace("><", ">\r\n<");
            m_publicKey = csp.ToXmlString(false);//.Replace("><", ">\r\n<");
            RSAProvider = new RSAx(m_privateKey, keyLength);

            m_Hash = new Bernstein();
        }

        public byte[] createDualSignature()
        {
            Util.Log(m_TAG, "Attacker: creating dual signature...");

            // obtain both the OI and PI from file
            string OI = Util.loadOI(Util.m_OIFileName);
            string PI = Util.loadPI(Util.m_PIFileName);

            // create the hashes for both files
            UInt32 PIMD = m_Hash.getHash(PI);
            UInt32 OIMD = m_Hash.getHash(OI);

            writePIMD(PIMD + "");
            writeOIMD(OIMD + "");

            UInt32 combinedHash = PIMD + OIMD;

            //byte[] POMD = BitConverter.GetBytes(createPOMD(combinedHash));
            string POMDstr = m_Hash.getHash(combinedHash + "") + "";
            byte[] POMD = Encoding.UTF8.GetBytes(POMDstr);

            //UInt32 DS = RSASec.encrypt(POMD, RSAProvider);
            // Encrypt using Private Key
            RSAProvider.RSAxHashAlgorithm = RSAxParameters.RSAxHashAlgorithm.SHA1;
            byte[] dualSignatureBytes = RSAProvider.Encrypt(POMD, true, true);

            return dualSignatureBytes;
        }

        public void writeOIMD(string OI)
        {
            UInt32 OIMD = m_Hash.getHash(OI);
            Util.Log(m_TAG, "Attacker: creating OIMD");
            File.WriteAllText(Util.m_OIEFileName, OI + "");
        }

        public void writePIMD(string PI)
        {
            UInt32 PIMD = m_Hash.getHash(PI);
            Util.Log(m_TAG, "Attacker: creating PIMD");
            File.WriteAllText(Util.m_PIEFileName, PI + "");
        }
    }
}
