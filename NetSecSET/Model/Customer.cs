using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSecSET.Model
{
    class Customer
    {
        public UInt32 createDualSignature(int publicKey, int privateKey)
        {
            // get both the OI and PI from file
            OrderInfo OI = Util.loadOI(Util.m_OIFileName);
            PaymentInfo PI = Util.loadPI(Util.m_PIFileName);

            // create the hashs for both files
            UInt16 PIMD = createPIMDHash(PI);
            UInt16 OIMD = createOIMDHash(OI);

            UInt32 combinedHash = (UInt32) PIMD + OIMD;

            UInt32 POMD = createPOMD(combinedHash);

            UInt32 DS = encrypt(POMD, privateKey);

            return POMD;
        }

        public UInt16 createPIMDHash(PaymentInfo PI)
        {
            return 2;
        }

        public UInt16 createOIMDHash(OrderInfo OI)
        {
            return 2;
        }

        public UInt32 createPOMD(UInt32 POMD)
        {
            return 2;
        }

        public UInt32 encrypt(UInt32 POMD, int Key)
        {
            return 2;
        }
    }
}
