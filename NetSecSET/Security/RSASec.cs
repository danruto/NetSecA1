using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSecSET.Model;

namespace NetSecSET.Model
{
    public static class RSASec
    {
        private static string m_TAG = "RSA";

        public static double encrypt(string hash, Key privateKey)
        {
            Util.Log(m_TAG, "encrypt(): hash");
            double hashValue = Convert.ToInt32(hash);
            double c = (Math.Pow(hashValue, privateKey.k) % privateKey.n);
            return c;
        }

        public static double encrypt(UInt32 hash, Key privateKey)
        {
            Util.Log(m_TAG, "encrypt(): hash INT32");
            int n = privateKey.n;
            double value = Math.Pow(hash, privateKey.k);
            //double c = (Math.Pow(hash, privateKey.k) % privateKey.n);
            // c = m^e mod n;
            double c = ((value % n) + n) % n;

            return c;
        }

        public static double encrypt(UInt32 POMD, int e, int n)
        {
            Util.Log(m_TAG, "encrypt(): POMD");
            double c = (Math.Pow(POMD, e) % n);
            return c;
        }

        public static double decrypt(double digitalSignature, Key publicKey)
        {
            Util.Log(m_TAG, "decrypt(): digitalSignature");
            //double m = (Math.Pow(digitalSignature, publicKey.k) % publicKey.n);
            double value = Math.Pow(digitalSignature, publicKey.k);
            int n = publicKey.n;
            double m = ((value % n) + n) % n;
            return m;
        }

        public static double decrypt(double c, int d, int n)
        {
            Util.Log(m_TAG, "decrypt(): c");
            double m = (Math.Pow(c, d) % n);
            return m;
        }

        static double MathMod(double a, int b)
        {
            return (Math.Abs(a * b) + a) % b;
        }

        static long mod(int a, int n)
        {
            long result = a % n;
            if ((a < 0 && n > 0) || (a > 0 && n < 0))
                result += n;
            return result;
        }
    }
}
