using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSecSET.Security
{
    class Bernstein
    {
        //md5 specifications http://tools.ietf.org/html/rfc1321
        
        //bernstein constants
        const UInt32 INITIAL_VALUE = 5875;
        const UInt32 M = 33;

        //bernstein hash algorithm
        //uses additions instead of XOR
        public UInt32 getHash(string input)
        {
            UInt32 hash = INITIAL_VALUE;
            byte[] bytes = getBytes(input);

            for (int i = 0; i < bytes.Length; ++i)
            {
                hash += M + bytes[i];
            }
            return hash;
        }

        public byte[] getBytes(UInt32 hash)
        {
            return getBytes(hash + "");
        }

        public byte[] getBytes(string input)
        {
            byte[] bytes = new byte[input.Length * sizeof(char)];
            System.Buffer.BlockCopy(input.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}
