using NetSecSET.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetSecSET
{
    public partial class SETGui : Form
    {
        private Customer m_customer;
        private Merchant m_merchant;
        private Bank m_bank;
        private Thread logThread;

        public SETGui()
        {
            InitializeComponent();
        }

        private void SETGui_Load(object sender, EventArgs e)
        {
            // Add a scrollbar to the logViewer
            logBox.ScrollBars = ScrollBars.Vertical;
            
            Util.ClearLog();
            // Logging thread
            logThread = new Thread(new ThreadStart(showLog));
            logThread.Start();
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

        private void showLog()
        {
            /*EventLog eventLog = new EventLog();

            eventLog.Log = "Application";

            eventLog.Source = "NetSecSET";

            string s = "";
            try
            {
                foreach (EventLogEntry log in eventLog.Entries)
                {
                    if (log.Source == "NetSecSET")
                        s += log.Message.ToString();
                }
            }
            catch (Exception ex)
            {
            }*/

            //logBox.Text = s;
            while (true)
            {
                logBox.Invoke((MethodInvoker)(() => logBox.Text = Util.getLog()));
                Thread.Sleep(50);
            }
        }       

        private void startBut_Click(object sender, EventArgs e)
        {
            createCustKeyPair();
            createMerchKeyPair();
            createBankKeyPair();
            showKeyPairs();
        }

        private void SETGui_FormClosed(object sender, FormClosedEventArgs e)
        {
            logThread.Abort();
        }

        private void custMakePaymentBut_Click(object sender, EventArgs e)
        {
            /*
            custCostLbl.Text;
            custCCTextBox;
            custCVVTextBox;
            */

            OrderInfo OI = new OrderInfo();
            OI.writeOI(Convert.ToInt32(custProductNoLbl.Text), custProductNameLbl.Text, DateTime.Now, custNameTextBox.Text, custAddressTextBox.Text, Convert.ToInt32(custContactNoTextBox.Text));

            PaymentInfo PI = new PaymentInfo();
            PI.writePI(Convert.ToInt32(custCCTextBox.Text), Convert.ToInt32(custCVVTextBox.Text), Convert.ToDouble(custCostLbl.Text));

            UInt32 dualSignature = m_customer.createDualSignature();
            Util.WriteDualSignature(dualSignature);

            startDecryption();
        }

        private void startDecryption()
        {
            m_merchant.decrypt();
            //m_bank.decrypt();
        }

        private void createCustKeyPair()
        {
            Key publicKey = new Key();
            Key privateKey = new Key();

            int p = Convert.ToInt32(cpTextBox.Text.ToString());
            int q = Convert.ToInt32(cqTextBox.Text.ToString());
            // e < p-1, q-1
            int e = Convert.ToInt32(ceTextBox.Text.ToString());

            // 
            int d = createDKey(p, q, e);
            int n = p * q;

            publicKey.k = e;
            publicKey.n = n;
            privateKey.k = d;
            privateKey.n = n;

            m_customer = new Customer(publicKey, privateKey);
        }

        private void createMerchKeyPair()
        {
            Key publicKey = new Key();
            Key privateKey = new Key();
            int p = Convert.ToInt32(mpTextBox.Text.ToString());
            int q = Convert.ToInt32(mqTextBox.Text.ToString());
            // e < p-1, q-1
            int e = Convert.ToInt32(meTextBox.Text.ToString());

            // 
            int d = createDKey(p, q, e);
            int n = p * q;

            publicKey.k = e;
            publicKey.n = n;
            privateKey.k = d;
            privateKey.n = n;

            m_merchant = new Merchant(publicKey, privateKey);
        }

        private void createBankKeyPair()
        {
            Key publicKey = new Key();
            Key privateKey = new Key();
            int p = Convert.ToInt32(bpTextBox.Text.ToString());
            int q = Convert.ToInt32(bqTextBox.Text.ToString());
            // e < p-1, q-1
            int e = Convert.ToInt32(beTextBox.Text.ToString());

            // 
            int d = createDKey(p, q, e);
            int n = p * q;

            publicKey.k = e;
            publicKey.n = n;
            privateKey.k = d;
            privateKey.n = n;

            m_bank = new Bank(publicKey, privateKey);
        }

        private void showKeyPairs()
        {
            cenTextBox.Text = "(" + m_customer.publicKey.k + "," + m_customer.publicKey.n + ")";
            cdnTextBox.Text = "(" + m_customer.privateKey.k + "," + m_customer.privateKey.n + ")";

            menTextBox.Text = "(" + m_merchant.publicKey.k + "," + m_merchant.publicKey.n + ")";
            mdnTextBox.Text = "(" + m_merchant.privateKey.k + "," + m_merchant.privateKey.n + ")";

            benTextBox.Text = "(" + m_bank.publicKey.k + "," + m_bank.publicKey.n + ")";
            bdnTextBox.Text = "(" + m_bank.privateKey.k + "," + m_bank.privateKey.n + ")";
        }
    }
}
