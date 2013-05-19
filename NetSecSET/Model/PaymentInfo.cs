using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NetSecSET.Model
{
    class PaymentInfo
    {
        public static string m_TAG = "PaymentInfo";
        public static string m_PIFileName = "OI.txt";


        public static string readPI()
        {
            return File.ReadAllText(m_PIFileName);
        }

        public static void writePI(string msg)
        {
            File.WriteAllText(m_PIFileName, msg);
        }

    }
}
