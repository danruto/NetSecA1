using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NetSecSET.Model
{
    class OrderInfo
    {
        public static string m_TAG = "OrderInfo";
        public static string m_OIFileName = "OI.txt";

      

         public static string readOI()
        {
            return File.ReadAllText(m_OIFileName);
        }

        public static void writeOI(string msg)
        {
            File.WriteAllText(m_OIFileName, msg);
        }

    }
}
