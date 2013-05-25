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

        public void writePI(string cardnumber, string cvvnum, double payment)
        {
            // PI parameters
            string content = "Credit Card Number: " + cardnumber;
            content += "\nCVV Number: " + cvvnum;
            content += "\nPayment Amount: " + String.Format("{0:0.##}", payment);
            File.WriteAllText(@m_PIFileName, content); // write to txt file
        }

        public string getContent()
        {
            if (File.Exists(@m_PIFileName))
                return File.ReadAllText(@m_PIFileName);
            return "";
        }

    }
}
