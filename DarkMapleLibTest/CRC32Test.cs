using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkMapleLib
{
    /// <summary>
    /// This class tests CRC32.cs
    /// </summary>
    [TestClass]
    public class CRC32Test
    {
        [TestMethod]
        public void TestCRC32()
        {
            byte[] testData = new byte[] { 9, 1, 2, 3, 4, 5, 6, 7, 8 };
            Assert.AreEqual((uint)0x14D9FD2, CRC32.MapleCRC(testData), "The MapleCRC32 gave back incorrect results");
        }
    }
}
