using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;

namespace NetSecSET.Model
{
    class Util
    {
        public static string m_TAG = "Util";
        public static string m_OIFileName = "OI.txt";
        public static string m_PIFileName = "PI.txt";
        public static string m_CustCertFileName = "CustomerCertificate.txt";
        public static string m_MerchantCertFileName = "MerchantCertificate.txt";
        public static string m_BankCertFileName = "BankCertificate.txt";

        /**********************************************************************************
         * Certificate Functions
         */ 
        public static Certificate loadCertificate(string fileName)
        {
            Certificate cert = null;

            if (File.Exists(fileName))
            {
                XmlSerializer s = new XmlSerializer(typeof(Certificate));
                TextReader tr = new StreamReader(@fileName);
                cert = (Certificate)s.Deserialize(tr);
                Log(m_TAG, "loading Certificate");
                tr.Close();
            }

            return cert;
        }

        public static void saveCertificate(string fileName, Certificate cert)
        {
            XmlSerializer s = new XmlSerializer(typeof(Certificate));
            TextWriter tw = new StreamWriter(@fileName);
            s.Serialize(tw, cert);
            Log(m_TAG, "saving Ceritifcate");
            tw.Close();
        }

        public static string loadCertificateText(string fileName)
        {
            return File.ReadAllText(fileName);
        }

        public static void saveCertificateText(string fileName, string msg)
        {
            File.WriteAllText(fileName, msg);
        }

        /**********************************************************************************
         * OI Functions
         */
        public static OrderInfo loadOI(string fileName)
        {
            OrderInfo OI = null;

            if (File.Exists(fileName))
            {
                XmlSerializer s = new XmlSerializer(typeof(OrderInfo));
                TextReader tr = new StreamReader(@fileName);
                OI = (OrderInfo)s.Deserialize(tr);
                Log(m_TAG, "loading Order Information");
                tr.Close();
            }

            return OI;
        }

        public static void saveOI(string fileName, OrderInfo OI)
        {
            XmlSerializer s = new XmlSerializer(typeof(OrderInfo));
            TextWriter tw = new StreamWriter(@fileName);
            s.Serialize(tw, OI);
            Log(m_TAG, "saving Order Information");
            tw.Close();
        }

        /**********************************************************************************
         * PI Functions
         */
        public static PaymentInfo loadPI(string fileName)
        {
            PaymentInfo PI = null;

            if (File.Exists(fileName))
            {
                XmlSerializer s = new XmlSerializer(typeof(PaymentInfo));
                TextReader tr = new StreamReader(@fileName);
                PI = (PaymentInfo)s.Deserialize(tr);
                Log(m_TAG, "loading Payment Information");
                tr.Close();
            }

            return PI;
        }

        public static void savePI(string fileName, PaymentInfo PI)
        {
            XmlSerializer s = new XmlSerializer(typeof(PaymentInfo));
            TextWriter tw = new StreamWriter(@fileName);
            s.Serialize(tw, PI);
            Log(m_TAG, "saving Payment Information");
            tw.Close();
        }

        /**********************************************************************************
         * Logging Functions
         */
        public static void Log(string tag, string msg)
        {
            string s = "";
            s += "\r\nLog Entry : ";
            s += "\n " + DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToLongDateString();
            s += "\n Tag: " + tag;
            s += "\n Message :" + msg;
            s += "\n-------------------------------";

            string sSource;
            string sLog;

            sSource = "NetSecSET";
            sLog = "Application";

            try
            {

                if (!EventLog.SourceExists(sSource))
                    EventLog.CreateEventSource(sSource, sLog);

                // Write an informational entry to the event log.    
                EventLog.WriteEntry(sSource, s);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            
        }

        

    }
}
