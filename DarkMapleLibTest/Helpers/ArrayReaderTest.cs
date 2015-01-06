using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkMapleLib.Helpers
{
    /// <summary>
    /// This class tests ArrayReader.cs
    /// </summary>
    [TestClass]
    public class ArrayReaderTest
    {
        [TestMethod]
        public void TestReadTypes()
        {
            byte[] buffer = new byte[100];
            ArrayReader pr = new ArrayReader(buffer);
            Assert.AreEqual(false, pr.ReadBool());
            Assert.AreEqual((sbyte)0, pr.ReadSByte());
            Assert.AreEqual((byte)0, pr.ReadByte());
            Assert.AreEqual((short)0, pr.ReadShort());
            Assert.AreEqual((ushort)0, pr.ReadUShort());
            Assert.AreEqual((int)0, pr.ReadInt());
            Assert.AreEqual((uint)0, pr.ReadUInt());
            Assert.AreEqual((long)0, pr.ReadLong());
            Assert.AreEqual((ulong)0, pr.ReadULong());
            Assert.AreEqual("..", pr.ReadString(2));
            Assert.AreEqual(String.Empty, pr.ReadMapleString());
            
            Assert.AreEqual(65, pr.Available);
            Assert.AreEqual(35, pr.Position);
            Assert.AreEqual(buffer.ToArray().Length, pr.Position + pr.Available);
        }
    }
}
