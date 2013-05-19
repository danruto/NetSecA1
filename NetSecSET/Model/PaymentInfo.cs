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
        public static string m_PIFileName = "PI.txt";


        public static string readPI()
        {
            return File.ReadAllText(m_PIFileName);
        }

        public void writePI(int cardnumber, int cvvnum, double payment)
        {
            string content = "Credit Card Number: " + cardnumber ;
            content += "\n CVV Number: " + cvvnum;
            content += "\n Payment Amount: " + payment;
            File.WriteAllText(m_PIFileName, content);
        }

    }
}
