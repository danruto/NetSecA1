﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace NetSecSET.Model
{
    class Util
    {
        // enabling files to be called on
        public static string m_TAG = "Util";
        public static string m_OIFileName = "OI.txt";
        public static string m_PIFileName = "PI.txt";
        public static string m_OIEFileName = "OIE.txt";
        public static string m_PIEFileName = "PIE.txt";
        public static string m_DualSignatureFileName = "DualSignature.txt";
        public static string m_CustCertFileName = "CustomerCertificate.txt";
        public static string m_MerchantCertFileName = "MerchantCertificate.txt";
        public static string m_BankCertFileName = "BankCertificate.txt";
        public static string m_LogFileName = "NetsecLog.log";
        private static Semaphore logSem = new Semaphore(1, 1);

        /**********************************************************************************
         * IO Functions
         */

        public static void writeText(string fileName, string msg)
        {
            File.WriteAllText(@fileName, msg);
        }

        public static void writeText(string fileName, string msg, string logMsg)
        {
            Log(m_TAG, logMsg);
            File.WriteAllText(@fileName, msg);
        }

        public static string readText(string fileName)
        {
            if (File.Exists(@fileName))
                return File.ReadAllText(@fileName);
            return "";
        }

        public static void writeBytes(string fileName, byte[] msg)
        {
            File.WriteAllBytes(@fileName, msg);
        }

        public static void writeBytes(string fileName, byte[] msg, string logMsg)
        {
            Log(m_TAG, logMsg);
            File.WriteAllBytes(@fileName, msg);
        }

        public static byte[] readBytes(string fileName)
        {
            if (File.Exists(@fileName))
                return File.ReadAllBytes(@fileName);
            return new byte[0];
        }

        /**********************************************************************************
         * Logging Functions
         */
        public static void Log(string tag, string msg)
        {
            string[] s = new string[5];
            s[0] = "\r\nLog Entry : ";
            s[1] = "\n " + DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToLongDateString();
            s[2] = "\n Tag: " + tag;
            s[3] = "\n Message :" + msg;
            s[4] = "\n-------------------------------";

            lock (logSem)
            {
                File.AppendAllLines(@m_LogFileName, s, Encoding.UTF8);
            }
        }

        public static string getLog()
        {
            lock (logSem)
            {
                if (File.Exists(@m_LogFileName))
                {
                    return File.ReadAllText(@m_LogFileName);
                    /*string log = "\n";
                    foreach (string s in lines)
                    { log += s + "\n"; }
                    return log;*/
                }
                return "";
            }
        }

        public static void ClearLog()
        {
            lock (logSem)
            {
                if (File.Exists(@m_LogFileName))
                    File.WriteAllText(@m_LogFileName, "");
                if (File.Exists(@m_OIFileName))
                    File.WriteAllText(@m_OIFileName, "");
                if (File.Exists(@m_PIFileName))
                    File.WriteAllText(@m_PIFileName, "");
            }
        }

        #region oldCode

        public static void ClearLog2()
        {
            string sSource;
            string sLog;

            sSource = "NetSecSET";
            sLog = "Application";
            try
            {
                if (EventLog.SourceExists(sSource))
                    EventLog.DeleteEventSource(sSource);
            }
            catch (Exception) { }
        }

        public static void Log2(string tag, string msg)
        {
            // Run eventcreate /ID 1 /L APPLICATION /T INFORMATION /SO NetSecSET /D “Registering” as administrator so the log will work
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

                // Write an informational entry to the event log   
                EventLog.WriteEntry(sSource, s);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }


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
            // save Certificate
            XmlSerializer s = new XmlSerializer(typeof(Certificate));
            TextWriter tw = new StreamWriter(@fileName);
            s.Serialize(tw, cert);
            Log(m_TAG, "saving Certificate");
            tw.Close();
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
            // save OI 
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
            // save PI
            XmlSerializer s = new XmlSerializer(typeof(PaymentInfo));
            TextWriter tw = new StreamWriter(@fileName);
            s.Serialize(tw, PI);
            Log(m_TAG, "saving Payment Information");
            tw.Close();
        }

        #endregion

    }
}
