using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSecSET.Model;

using System.Security.Cryptography;

namespace NetSecSET.Model
{
    public static class RSASec
    {
        private static string m_TAG = "RSA";
        // private static RSACryptoServiceProvider RSAprovider = new RSACryptoServiceProvider();
  
        /*public static RSACryptoServiceProvider getRSA()
        {
            return RSAprovider;
        }*/

        public static UInt32 encrypt (string msg)
        {
            return 0;
        }a

        // data encryption 
        public static UInt32 encrypt(UInt32 msg, RSACryptoServiceProvider RSAprovider)
        {
            byte[] toEnc = BitConverter.GetBytes(msg);
            byte[] encryptedData = RSAprovider.Encrypt(toEnc, false);
            return BitConverter.ToUInt32(encryptedData, 0);
        }

        public static UInt32 decrypt(UInt32 msg, RSACryptoServiceProvider RSAProvider)
        {
            byte[] toDec = BitConverter.GetBytes(msg);
            byte[] decryptedData = RSAProvider.Decrypt(toDec, false);
            return BitConverter.ToUInt32(decryptedData, 0);
        }

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
            // double c = (Math.Pow(hash, privateKey.k) % privateKey.n);
            // RSA c = m^e mod n; where (e,n) are values for public key
            double c = ((value % n) + n) % n;

            return c;
        }

        public static double encrypt(UInt32 POMD, int e, int n)
        {
            Util.Log(m_TAG, "encrypt(): POMD");
            double c = (Math.Pow(POMD, e) % n);
            return c;
        }

        // encrypt-decrypt-encrypt
        public static double decrypt(double digitalSignature, Key publicKey)
        {
            Util.Log(m_TAG, "decrypt(): digitalSignature");
            // double m = (Math.Pow(digitalSignature, publicKey.k) % publicKey.n);
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
