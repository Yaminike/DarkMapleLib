using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DarkMapleLib
{
    /// <summary>
    /// This class tests Cipher.cs FastAES.cs and InitializationVector.cs using combined tests
    /// </summary>
    [TestClass]
    public class CipherTest
    {
        Cipher testCipher = new Cipher(1);

        [TestMethod]
        public void TestGetPacketLength()
        {
            byte[] testData = new byte[4];
            Assert.AreEqual(testCipher.GetPacketLength(testData), 0, "PacketLength is not working correctly");

            testData = new byte[4] { 20, 10, 20, 10};
            Assert.AreEqual(testCipher.GetPacketLength(testData), 0, "PacketLength is not working correctly");

            testData = new byte[4] { 10, 10, 20, 10 };
            Assert.AreEqual(testCipher.GetPacketLength(testData), 30, "PacketLength is not working correctly");

            testData = new byte[4] { 20, 10, 10, 10 };
            Assert.AreEqual(testCipher.GetPacketLength(testData), 30, "PacketLength is not working correctly");

            testData = new byte[4] { 10, 20, 10, 10 };
            Assert.AreEqual(testCipher.GetPacketLength(testData), 7680, "PacketLength is not working correctly");

            testData = new byte[4] { 10, 10, 10, 20 };
            Assert.AreEqual(testCipher.GetPacketLength(testData), 7680, "PacketLength is not working correctly");
        }

        [TestMethod]
        public void TestEncrypt()
        {
            byte[] testData = new byte[10];
            byte[] testResult = new byte[14] { 172, 186, 166, 186, 74, 210, 98, 206, 114, 67, 136, 22, 159, 150 };
            testCipher.SetIV(0xBAADF00D);
            DarkMapleLib.Cipher.EncryptResult cipherResult = testCipher.Encrypt(ref testData, false);

            Assert.AreEqual(cipherResult.ToSend, false, "Something went wrong in the VectorPing check");
            Assert.AreEqual(cipherResult.LOIV, 0x9D21, "Something went wrong in shifting the cryptoIV");
            Assert.AreEqual(testResult.Length, testData.Length, "Encrypted data length is incorrect");
            
            int i = 0;
            foreach (byte test in testResult)
            {
                Assert.AreEqual(testData[i++], test, "Encrypted data mismatch");
            }

            testCipher.Encrypt(ref testData, false);
            cipherResult = testCipher.Encrypt(ref testData, false);
            Assert.AreEqual(cipherResult.ToSend, true, "Something went wrong in the VectorPing check");
            Assert.AreEqual(cipherResult.LOIV, 0x30EC, "Something went wrong in shifting the cryptoIV");
            Assert.AreEqual(22, testData.Length, "Encrypted data length is incorrect");
        }

        [TestMethod]
        public void TestDecrypt()
        {
            byte[] testData = new byte[14] { 172, 186, 166, 186, 74, 210, 98, 206, 114, 67, 136, 22, 159, 150 };
            byte[] testResult = new byte[10];
            testCipher.SetIV(0xBAADF00D);
            testCipher.Decrypt(ref testData);

            Assert.AreEqual(testResult.Length, testData.Length, "Decrypted data length is incorrect");

            int i = 0;
            foreach (byte test in testResult)
            {
                Assert.AreEqual(testData[i++], test, "Decrypted data mismatch");
            }
        }
    }
}
