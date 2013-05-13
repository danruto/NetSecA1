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
        Key publicKey = new Key();
        Key privateKey = new Key();

        public Main()
        {
            InitializeComponent();
            logBox.ScrollBars = ScrollBars.Vertical;

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
            int d = 0;
            double dTemp;

            for (int k = 1; k < 9; k++)
            {
                dTemp = ((k * (p - 1) * (q - 1)) + 1f) / e;
                // check if decimals are = 00

                if (dTemp % 1 == 0)
                {
                    d = Convert.ToInt32(dTemp);
                    break;
                }
            }

            createKeys(p, q, e, d);
            displayKeys();

            Customer cust = new Customer();
            cust.setup(publicKey, privateKey);

            Merchant merch = new Merchant();
            Bank bank = new Bank();
            Certificate cert = new Certificate();
        }

        private void createKeys(int p, int q, int e, int d)
        {
            publicKey.k = e;
            privateKey.k = d;
            publicKey.n = p * q;
            privateKey.n = p * q;
        }

        private void displayKeys()
        {
            enTextBox.Text = "(" + publicKey.k + "," + " " + publicKey.n + ")";
            dnTextBox.Text = "(" + privateKey.k + "," + " " + privateKey.n + ")";
        }

        private void showLog()
        {
            EventLog eventLog = new EventLog();

            eventLog.Log = "Application";

            eventLog.Source = "NetSecSET";

            string s = "";

            foreach (EventLogEntry log in eventLog.Entries)
            {
               s = Convert.ToBase64String(log.Data);
            }

            //logBox.Text = s;
            logBox.Invoke((MethodInvoker)(() => logBox.Text = s));
        }

    }
}
