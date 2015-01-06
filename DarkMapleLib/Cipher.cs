using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkMapleLib
{
    /// <summary>
    /// Cipher class used for encrypting and decrypting maple packet data
    /// </summary>
    /// <remarks>
    /// Alonevampire can SMD
    /// Created by Yaminike, aka Minike, aka 0minike0
    /// </remarks>
    public class Cipher
    {
        /// <summary>
        /// Transformer to transform blocks
        /// </summary>
        private FastAES Transformer { get; set; }

        /// <summary>
        /// General locker to prevent multithreading
        /// </summary>
        private volatile Object Locker = new Object();

        /// <summary>
        /// Vector to use in the MapleCrypto
        /// </summary>
        internal InitializationVector MapleIV { get; set; }

        /// <summary>
        /// Gameversion of the current <see cref="Cipher"/> instance
        /// </summary>
        public UInt16 GameVersion { get; private set; }

        /// <summary>
        /// Bool stating if the current instance received its handshake
        /// </summary>
        public bool Handshaken { get; set; }

        /// <summary>
        /// Creates a new instance of the Cipher
        /// </summary>
        /// <param name="CurrentGameVersion">The current MapleStory version</param>
        public Cipher(UInt16 CurrentGameVersion)
        {
            this.Handshaken = false;
            this.GameVersion = CurrentGameVersion;
            this.Transformer = new FastAES(this.ExpandKey());
        }

        /// <summary>
        /// Manually sets the vector for the current instance
        /// </summary>
        public void SetIV(UInt32 vector)
        {
            this.MapleIV = new InitializationVector(vector);
            this.Handshaken = true;
        }

        /// <summary>
        /// Decrypts a maple packet contained in <paramref name="data"/>
        /// </summary>
        /// <param name="data">Data to decrypt</param>
        public void Decrypt(ref byte[] data)
        {
            if (!Handshaken || MapleIV == null) return;
            int length = GetPacketLength(data);

            byte[] ret = new byte[length];
            Buffer.BlockCopy(data, 4, ret, 0, length);

            lock (Locker)
            {
                Transform(ret);
            }

            DecryptData(ret);
            data = ret;
        }

        /// <summary>
        /// Encrypts data using the current instance
        /// </summary>
        /// <param name="data">Data to encrypt</param>
        /// <param name="toClient">Direction of the the data</param>
        /// <returns>
        /// True when the IV matches the requirements of being in a need to be pushed to the server 
        /// </returns>
        public EncryptResult Encrypt(ref byte[] data, bool toClient)
        {
            if (!Handshaken || MapleIV == null) return null;
            byte[] ret = new byte[data.Length + 4];
            if (toClient)
                WriteHeaderToClient(ret);
            else
                WriteHeaderToServer(ret);

            bool toSend = false;
            EncryptData(data);

            lock (Locker)
            {
                Transform(data);
                toSend = MapleIV.CheckIV();
            }

            Buffer.BlockCopy(data, 0, ret, 4, data.Length);
            data = ret;

            return new EncryptResult()
            {
                LOIV = MapleIV.LOWORD,
                ToSend = toSend
            };
        }

        /// <summary>
        /// Handles an handshake for the current instance
        /// </summary>
        /// <param name="data"></param>
        public void RecvHandshake(ref byte[] data)
        {
            ushort length = BitConverter.ToUInt16(data, 0);
            byte[] ret = new byte[length];
            Buffer.BlockCopy(data, 2, ret, 0, ret.Length);
            data = ret;
        }

        /// <summary>
        /// Gets the length of <paramref name="data"/>
        /// </summary>
        /// <param name="data">Data to check</param>
        /// <returns>Length of <paramref name="data"/></returns>
        public unsafe int GetPacketLength(byte[] data)
        {
            fixed (byte* pData = data)
            {
                return *(ushort*)pData ^ *((ushort*)pData + 1);
            }
        }

        /// <summary>
        /// Creates a packet header for outgoing data
        /// </summary>
        private unsafe void WriteHeaderToServer(byte[] data)
        {
            fixed (byte* pData = data)
            {
                *(ushort*)pData = (ushort)(GameVersion ^ MapleIV.HIWORD);
                *((ushort*)pData + 1) = (ushort)(*(ushort*)pData ^ (data.Length - 4));
            }
        }

        /// <summary>
        /// Creates a packet header for incoming data
        /// </summary>
        private unsafe void WriteHeaderToClient(byte[] data)
        {
            fixed (byte* pData = data)
            {
                *(ushort*)pData = (ushort)(-(GameVersion + 1) ^ MapleIV.HIWORD);
                *((ushort*)pData + 1) = (ushort)(*(ushort*)pData ^ (data.Length - 4));
            }
        }

        /// <summary>
        /// Expands the key we store as long
        /// </summary>
        /// <returns>The expanded key</returns>
        private byte[] ExpandKey()
        {
            byte[] Expand = BitConverter.GetBytes(Constants.Key).Reverse().ToArray();
            byte[] Key = new byte[Expand.Length * 4];
            for (int i = 0; i < Expand.Length; i++)
                Key[i * 4] = Expand[i];
            return Key;
        }

        /// <summary>
        /// Performs Maplestory's AES algo
        /// </summary>
        private void Transform(byte[] buffer)
        {
            int remaining = buffer.Length;
            int length = 0x5B0;
            int start = 0;
            int index;
            byte[] realIV = new byte[sizeof(int) * 4];
            byte[] IVBytes = BitConverter.GetBytes(MapleIV.Value);
            while (remaining > 0)
            {
                for (index = 0; index < realIV.Length; ++index)
                    realIV[index] = IVBytes[index % 4];

                if (remaining < length) length = remaining;
                for (index = start; index < (start + length); ++index)
                {
                    if (((index - start) % realIV.Length) == 0)
                        Transformer.TransformBlock(realIV);

                    buffer[index] ^= realIV[(index - start) % realIV.Length];
                }
                start += length;
                remaining -= length;
                length = 0x5B4;
            }
            MapleIV.Shuffle();
        }

        /// <summary>
        /// Decrypts <paramref name="buffer"/> using the custom maple crypto
        /// </summary>
        private void DecryptData(byte[] buffer)
        {
            int length = buffer.Length;
            byte xorKey, save, len, temp;
            int i;
            for (int passes = 0; passes < 3; passes++)
            {
                xorKey = 0;
                save = 0;
                len = (byte)(length & 0xFF);
                for (i = length - 1; i >= 0; --i)
                {
                    temp = (byte)(ROL(buffer[i], 3) ^ 0x13);
                    save = temp;
                    temp = ROR((byte)((xorKey ^ temp) - len), 4);
                    xorKey = save;
                    buffer[i] = temp;
                    --len;
                }

                xorKey = 0;
                len = (byte)(length & 0xFF);
                for (i = 0; i < length; ++i)
                {
                    temp = ROL((byte)(~(buffer[i] - 0x48)), len & 0xFF);
                    save = temp;
                    temp = ROR((byte)((xorKey ^ temp) - len), 3);
                    xorKey = save;
                    buffer[i] = temp;
                    --len;
                }
            }
        }

        /// <summary>
        /// Encrypts <paramref name="buffer"/> using the custom maple crypto
        /// </summary>
        private void EncryptData(byte[] buffer)
        {
            int length = buffer.Length;
            byte xorKey, len, temp;
            int i;
            for (int passes = 0; passes < 3; passes++)
            {
                xorKey = 0;
                len = (byte)(length & 0xFF);
                for (i = 0; i < length; i++)
                {
                    temp = (byte)((ROL(buffer[i], 3) + len) ^ xorKey);
                    xorKey = temp;
                    temp = (byte)(((~ROR(temp, len & 0xFF)) & 0xFF) + 0x48);
                    buffer[i] = temp;
                    len--;
                }
                xorKey = 0;
                len = (byte)(length & 0xFF);
                for (i = length - 1; i >= 0; i--)
                {
                    temp = (byte)(xorKey ^ (len + ROL(buffer[i], 4)));
                    xorKey = temp;
                    temp = ROR((byte)(temp ^ 0x13), 3);
                    buffer[i] = temp;
                    len--;
                }
            }
        }

        /// <summary>
        /// Bitwise shift left
        /// </summary>
        private byte ROL(byte b, int count)
        {
            int tmp = b << (count & 7);
            return unchecked((byte)(tmp | (tmp >> 8)));
        }

        /// <summary>
        /// Bitwise shift right
        /// </summary>
        private byte ROR(byte b, int count)
        {
            int tmp = b << (8 - (count & 7));
            return unchecked((byte)(tmp | (tmp >> 8)));
        }

        public class EncryptResult
        {
            public bool ToSend { get; set; }
            public UInt16 LOIV { get; set; }
        }
    }
}
