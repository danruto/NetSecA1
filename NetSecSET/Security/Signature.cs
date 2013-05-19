using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSecSET.Model;

namespace NetSecSET.Security
{
    class Signature
    {
        public static double createDigitalSignature(string msg, Key privateKey)
        {
            byte[] digitalSignature = new byte[8]; //64-bit
            Bernstein hash = new Bernstein();
            UInt32 hashValue = hash.getHash(msg);

            double cipher = RSA.encrypt(hashValue, privateKey);
            //digitalSignature = BitConverter.GetBytes(cipher);

            return cipher;
        }
    }
}
