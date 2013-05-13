using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;
using System.IO;

namespace NetSecSET.Security
{
    class MD5
    {
        public string getMD5(String input)
        {
            byte[] data = Encoding.UTF8.GetBytes(input);

            //int dataByteCount = Encoding.UTF8.GetByteCount(input);
            //if(dataByteCount % 512 > 0)
            do
            {
                using(MemoryStream ms = new MemoryStream())
                {
                    ms.Write(data, 0, 1);
                }
            }
            while((Encoding.UTF8.GetByteCount(input) % 512) > 0);

            

            



            MD5 md5hash = new MD5.Create();
            byte[] hashData = md5hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        }
    }
    
}
