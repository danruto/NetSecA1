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

        public void writeOI(int pnumber, string pname, DateTime date, string custname, string custaddress, int custnumber)
        {
            // OI parameters
            string content = "Product Number: " + pnumber;
            content += "\n Product Name: " + pname;
            content += "\n Order Date: " + Convert.ToString(date);
            content += "\n Customer Name: " + custname;
            content += "\n Customer address: " + custaddress;
            content += "\n Customer Contact: " + custnumber;
            File.WriteAllText(@m_OIFileName, content); // write info to txt file
        }

        public string getContent()
        {
            if (File.Exists(@m_OIFileName))
                return File.ReadAllText(m_OIFileName);
            return "";
        }

    }
}
