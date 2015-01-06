using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkMapleLib
{
    /// <summary>
    /// Initialization vector used by the Cipher class
    /// </summary>
    /// <remarks>
    /// Created by Yaminike, aka Minike, aka 0minike0
    /// </remarks>
    public class InitializationVector
    {
        /// <summary>
        /// IV Container
        /// </summary>
        public UInt32 Value = 0;

        /// <summary>
        /// Gets the HIWORD from the current container
        /// </summary>
        public UInt16 HIWORD
        {
            get
            {
                return unchecked((UInt16)(Value >> 16));
            }
        }

        /// <summary>
        /// Gets the LOWORD from the current container
        /// </summary>
        public UInt16 LOWORD
        {
            get
            {
                return (UInt16)Value;
            }
        }

        /// <summary>
        /// Creates a IV instance using <paramref name="vector"/>
        /// </summary>
        /// <param name="vector">Initialization vector</param>
        public InitializationVector(UInt32 vector)
        {
            Value = vector;
        }

        /// <summary>
        /// Shuffles the current IV to the next vector using the shuffle table
        /// </summary>
        public unsafe void Shuffle()
        {
            UInt32 Key = Constants.DefaultKey;
            UInt32* pKey = &Key;
            fixed (UInt32* pIV = &Value)
            {
                fixed (byte* pShuffle = Constants.Shuffle)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        *((byte*)pKey + 0) += (byte)(*(pShuffle + *((byte*)pKey + 1)) - *((byte*)pIV + i));
                        *((byte*)pKey + 1) -= (byte)(*((byte*)pKey + 2) ^ *(pShuffle + *((byte*)pIV + i)));
                        *((byte*)pKey + 2) ^= (byte)(*((byte*)pIV + i) + *(pShuffle + *((byte*)pKey + 3)));
                        *((byte*)pKey + 3) = (byte)(*((byte*)pKey + 3) - *(byte*)pKey + *(pShuffle + *((byte*)pIV + i)));

                        *(uint*)pKey = (*(uint*)pKey << 3) | (*(uint*)pKey >> (32 - 3));
                    }
                }
            }

            Value = Key;
        }

        /// <summary>
        /// Checks if the current IV matches the requirements of being in a need to be pushed to the server
        /// </summary>
        /// <returns>Bool if match</returns>
        public bool CheckIV()
        {
            return LOWORD % 0x1F == 0;
        }
    }
}
