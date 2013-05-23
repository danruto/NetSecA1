using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSecSET.Model;
using NetSecSET.Security;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace NetSecSET.Model
{
    class Merchant
    {
        private string m_TAG = "Merchant";
        private Bernstein hash = new Bernstein();
        private RSACryptoServiceProvider RSAProvider;
        private Certificate m_Certificate;
        private string decryptedMsg;
        public Key publicKey { get; set; }
        public Key privateKey { get; set; }

        public Merchant(Key publicKey, Key privateKey)
        {
            this.publicKey = publicKey;
            this.privateKey = privateKey;
            RSAProvider = new RSACryptoServiceProvider();
            createCertificate();
        }

        private void createCertificate()
        {
            m_Certificate = new Certificate(Certificate.t_CertificateType.MerchantCertificate, RSAProvider);
        }

        public void decrypt()
        {
            // Check the customer certificate first
            string customerCert = Util.loadCertificateText(Util.m_CustCertFileName);
            //http://rubular.com/
            // find public key
            //Match match = Regex.Match(customerCert, @"\<RSAKeyValue\>\<Modulus\>([A-Za-z0-9\-\=\/\\]+)=\<\/Modulus\>\<Exponent\>([A-Za-z0-9\-\=\/\\])\</Exponent\>\<\/RSAKeyValue\>", RegexOptions.IgnoreCase);
            //<RSAKeyValue><Modulus>
            Match match = Regex.Match(customerCert, @"(<RSAKeyValue>\S+<\/Exponent>)");
            if (match.Success)
            {
                string key = match.Groups[1].Value;
                Util.Log(m_TAG, key);
            }
            //string custPublicKey = 
            //int DS, OrderInfo OI, int PIMD
            Bernstein hash = new Bernstein();
            UInt32 DS = Util.loadDualSignature();
            string PI = Util.loadPI(Util.m_PIFileName);
            UInt32 PIMD = hash.getHash(PI);
            string OI = Util.loadOI(Util.m_OIFileName);
            UInt32 POMD = hash.getHash(hash.getHash(OI) + PIMD + "");
            //UInt32 decryptedDS = RSASec.decrypt(DS, RSAProvider);

            if (DS.Equals(POMD))
                Util.Log(m_TAG, "merchant: decrypt() successful");
            else
                Util.Log(m_TAG, "merchant: decrypt() unsuccessful");

        }

        public string hashOI(string OI)
        {
            
            return Convert.ToString(hash.getHash(OI));
        }

        public string concatenateString(string OI, int PIMD)
        {
            return hashOI(OI) + PIMD;
        }

        public string hashOI_PIMD(string OI, int PIMD)
        {
            return Convert.ToString(hash.getHash(concatenateString(OI, PIMD)));
        }

        public bool verifyDS(string PI, int OIMD)
        {
            return decryptedMsg == hashOI_PIMD(PI, OIMD);
        }

    }
}
