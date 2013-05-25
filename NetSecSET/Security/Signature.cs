using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSecSET.Model;

using System.Security.Cryptography;
using ArpanTECH;

namespace NetSecSET.Security
{
    class Signature
    {
        private const string m_TAG = "Signature";
        public static byte[] createDigitalSignature(string msg, RSACryptoServiceProvider RSA)
        {
            byte[] digitalSignature = new byte[8]; //64-bit
            byte[] encryptedData;
            Bernstein hash = new Bernstein();
            byte[] hashValue = hash.getBytes(hash.getHash(msg));

            //double cipher = RSASec.encrypt(hashValue, privateKey);
            encryptedData = RSA.Encrypt(hashValue, false);

            //digitalSignature = BitConverter.GetBytes(cipher);

            return encryptedData;
        }

        public static UInt32 createDigitalSignature(UInt32 msg, RSACryptoServiceProvider RSAprovider)
        {
            Bernstein hash = new Bernstein();
            Util.Log(m_TAG, "creating digital signature");
            return RSASec.encrypt(msg, RSAprovider);
        }

        public static UInt32 createDigitalSignature(UInt32 msg, RSAx RSAProvider)
        {
            Bernstein hash = new Bernstein();
            Util.Log(m_TAG, "creating digital signature");

            // Encrypt using PRIVATE key with OAEP off
            return BitConverter.ToUInt32(RSAProvider.Encrypt(BitConverter.GetBytes(msg), true, false), 0);
        }
    }
}
