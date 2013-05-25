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

        public void writeOI(int pnumber, string pname, DateTime date, string custname, string custaddress, string custnumber)
        {
            // OI parameters
            string content = "Product Number: " + pnumber;
            content += "\nProduct Name: " + pname;
            content += "\nOrder Date: " + Convert.ToString(date);
            content += "\nCustomer Name: " + custname;
            content += "\nCustomer address: " + custaddress;
            content += "\nCustomer Contact: " + custnumber;
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
