using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSecSET.Model;
using NetSecSET.Security;

namespace NetSecSET.Model
{
    class Merchant
    {
        private string m_TAG = "Merchant";
        private Bernstein hash = new Bernstein();
        private string decryptedMsg;

        public void decrypt(int DS, OrderInfo OI, int PIMD)
        {


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
            if (decryptedMsg == hashOI_PIMD(PI, OIMD))
                return true;
            else return false;
        }

    }
}
