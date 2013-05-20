using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using NetSecSET.Model;
using System.Diagnostics;

namespace NetSecSET
{
    public partial class Main : Form
    {
        private Key m_publicKey = new Key();
        private Key m_privateKey = new Key();

        public Main()
        {
            InitializeComponent();


            // Add a scrollbar to the logViewer
            logBox.ScrollBars = ScrollBars.Vertical;

            // Logging thread
            Thread t = new Thread(new ThreadStart(showLog));
            t.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void startBut_Click(object sender, EventArgs eArg)
        {
            int p = Convert.ToInt32(pValue.Text.ToString());
            int q = Convert.ToInt32(qValue.Text.ToString());
            // e < p-1, q-1
            int e = Convert.ToInt32(eValue.Text.ToString());

            // 
            int d = createDKey(p, q, e);

            saveKeys(p, q, e, d);
            displayKeys();

            Customer cust = new Customer(m_publicKey, m_privateKey);

            Merchant merch = new Merchant();
            Bank bank = new Bank();
        }

        private int createDKey(int p, int q, int e)
        {
            double dTemp;
            int d = 0;

            for (int k = 1; k < 9; k++)
            {
                dTemp = ((k * (p - 1) * (q - 1)) + 1f) / e;
                // check if decimals are = 00

                if (dTemp % 1 == 0)
                {
                    d = Convert.ToInt32(dTemp);
                    return d;
                }
            }

            return d;
        }

        private void saveKeys(int p, int q, int e, int d)
        {
            m_publicKey.k = e;
            m_privateKey.k = d;
            m_publicKey.n = p * q;
            m_privateKey.n = p * q;
        }

        private void displayKeys()
        {
            enTextBox.Text = "(" + m_publicKey.k + "," + " " + m_publicKey.n + ")";
            dnTextBox.Text = "(" + m_privateKey.k + "," + " " + m_privateKey.n + ")";
        }

        private void showLog()
        {
            EventLog eventLog = new EventLog();

            eventLog.Log = "Application";

            eventLog.Source = "NetSecSET";

            string s = "";

            foreach (EventLogEntry log in eventLog.Entries)
            {
                if (log.Source == "NetSecSET")
                    s += log.Message.ToString();
            }

            //logBox.Text = s;
            logBox.Invoke((MethodInvoker)(() => logBox.Text = s));
        }

    }
}
