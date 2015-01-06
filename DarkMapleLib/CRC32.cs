using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkMapleLib
{
    /// <summary>
    /// General CRC32 used by MapleStory for hashing purposes
    /// </summary>
    /// <remarks>
    /// Created by Yaminike, aka Minike, aka 0minike0
    /// </remarks>
    public static class CRC32
    {
        /// <summary>
        /// Hashes <paramref name="data"/> using the MapleStory CRC32 method
        /// </summary>
        /// <param name="data">Data to hash</param>
        /// <returns>Hash result</returns>
        public static UInt32 MapleCRC(byte[] data)
        {
            UInt32 result = 0;
            for (int i = 0; i < data.Length; i++)
                result = Constants.Crc32Table[data[i] ^ (result >> 24)] ^ (result << 8);
            return result;
        }
    }
}
