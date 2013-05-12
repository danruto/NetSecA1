using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSecSET.Model
{
    class Customer
    {
        private string m_TAG = "Customer";

        public double createDualSignature(Key publicKey, Key privateKey)
        {
            // get both the OI and PI from file
            OrderInfo OI = Util.loadOI(Util.m_OIFileName);
            PaymentInfo PI = Util.loadPI(Util.m_PIFileName);

            // create the hashs for both files
            UInt16 PIMD = createPIMDHash(PI);
            UInt16 OIMD = createOIMDHash(OI);

            UInt32 combinedHash = (UInt32) PIMD + OIMD;

            UInt32 POMD = createPOMD(combinedHash);

            double DS = RSA.encrypt(POMD, privateKey.k, privateKey.n);

            return DS;
        }

        public UInt16 createPIMDHash(PaymentInfo PI)
        {
            Util.Log(m_TAG, "creating PIMD hash");
            return 2;
        }

        public UInt16 createOIMDHash(OrderInfo OI)
        {
            Util.Log(m_TAG, "creating OIMD hash");
            return 2;
        }

        public UInt32 createPOMD(UInt32 POMD)
        {
            Util.Log(m_TAG, "creating POMD");
            return 2;
        }

        
    }

    
}
