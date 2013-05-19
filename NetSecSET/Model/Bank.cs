using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSecSET.Model;
using NetSecSET.Security;

namespace NetSecSET.Model
{
    class Bank
    {

        private string m_TAG = "Bank";
        private Bernstein hash = new Bernstein();
        private string decryptedMsg;

        public void decrypt(int DS, PaymentInfo PI, int OIMD)
        {
        }

        public string hashOI(string PI)
        {

            return Convert.ToString(hash.getHash(PI));
        }

        public string concatenateString(string PI, int OIMD)
        {
            return hashOI(PI) + OIMD;
        }

        public string hashPI_OIMD(string PI, int OIMD)
        {
            return Convert.ToString(hash.getHash(concatenateString(PI, OIMD)));
        }

        public bool verifyDS(string PI, int OIMD)
        {
            if (decryptedMsg == hashPI_OIMD(PI, OIMD))
                return true;
            else return false;
        }

    }
}
