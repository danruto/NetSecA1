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
        public static byte[] createDigitalSignature(string msg, Key privateKey)
        {
            byte[] z = new byte[2];
            Bernstein hash = new Bernstein();
            hash.getHash(msg);

            RSA.encrypt(msg, privateKey);
            return z;
        }
    }
}
