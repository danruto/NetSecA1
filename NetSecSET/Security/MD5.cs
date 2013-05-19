using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using System.Security.Cryptography;
//using System.IO;

namespace NetSecSET.Security
{
    class MD5
    {
        //md5 specifications http://tools.ietf.org/html/rfc1321
        
        //bernstein constants
        const uint INITIAL_VALUE = 5381;
        const uint M = 33;

        //bernstein hash algorithm
        //uses additions instead of XOR
        public string getHash(string input)
        {
            uint hash = INITIAL_VALUE;
            byte[] bytes = getBytes(input);

            for (int i = 0; i < bytes.Length; ++i)
            {
                hash = M * hash + bytes[i];
            }
            return hash;


            //byte[] bytes = Encoding.UTF8.GetBytes(input);
            ////int dataByteCount = Encoding.UTF8.GetByteCount(input);
            ////if(dataByteCount % 512 > 0)
            //do
            //{
            //    using (MemoryStream ms = new MemoryStream())
            //    {
            //        ms.Write(data, 0, 1);
            //    }
            //}
            //while ((Encoding.UTF8.GetByteCount(input) % 512) > 0);

            //MD5 md5hash = new MD5.Create();
            //byte[] hashData = md5hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        }

        public byte[] getBytes(string input)
        {
            byte[] bytes = new byte[input.Length * sizeof(char)];
            System.Buffer.BlockCopy(input.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}
