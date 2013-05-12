using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSecSET.Model
{
    public static class RSA
    {
        private static string m_TAG = "RSA";

        public static double encrypt(UInt32 POMD, int e, int n)
        {
            Util.Log(m_TAG, "encrypting...");
            double c = (Math.Pow(POMD, e) % n);
            return c;
        }

        public static double decrypt(double c, int d, int n)
        {
            Util.Log(m_TAG, "decrypting...");
            double m = (Math.Pow(c, d) % n);
            return m;
        }
    }
}
