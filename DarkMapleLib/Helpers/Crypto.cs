using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkMapleLib.Helpers
{
    /// <summary>
    /// Helper class for Cipher related functionality
    /// </summary>
    /// <remarks>
    /// Alonevampire can SMD
    /// Created by Yaminike, aka Minike, aka 0minike0
    /// </remarks>
    public class Crypto
    {
        /// <summary>
        /// Packet crypto
        /// </summary>
        private Cipher RecvCipher { get; set; }

        /// <summary>
        /// Packet crypto
        /// </summary>
        private Cipher SendCipher { get; set; }

        /// <summary>
        /// General locker for adding data
        /// </summary>
        private Object AddLocker = new Object();

        /// <summary>
        /// Waiting state
        /// </summary>
        private bool IsWaiting = true;

        /// <summary>
        /// Data buffer
        /// </summary>
        private byte[] buffer = new byte[4096];

        /// <summary>
        /// Current data in buffer
        /// </summary>
        private int AvailableData = 0;

        /// <summary>
        /// Amount of data to wait on
        /// </summary>
        private int WaitForData = 0;

        /// <summary>
        /// Callback for when a packet is finished
        /// </summary>
        public delegate void CallPacketFinished(byte[] packet);

        /// <summary>
        /// Event called when a packet has been handled by the crypto
        /// </summary>
        public event CallPacketFinished PacketFinished;

        /// <summary>
        /// Callback for when a handshake is finished
        /// </summary>
        public delegate void CallHandshakeFinished(uint SIV, uint RIV);

        /// <summary>
        /// Event called when a handshake has been handled by the crypto
        /// </summary>
        public event CallHandshakeFinished HandshakeFinished;

        /// <summary>
        /// Creates a new instance of Crypto
        /// </summary>
        /// <param name="GameVersion">The current MapleStory version</param>
        public Crypto(ushort GameVersion)
        {
            RecvCipher = new Cipher(GameVersion);
            SendCipher = new Cipher(GameVersion);
        }

        /// <summary>
        /// Adds data to the buffer to await decryption
        /// </summary>
        public void AddData(byte[] data)
        {
            int length = data.Length;
            lock (AddLocker)
            {
                if (buffer.Length < length + AvailableData)
                    Array.Resize<byte>(ref buffer, length + AvailableData);

                Buffer.BlockCopy(data, 0, this.buffer, this.AvailableData, length);
                this.AvailableData += length;
            }
            if (WaitForData != 0)
            {
                if (WaitForData <= AvailableData)
                {
                    int w = WaitForData - 2;
                    if (RecvCipher.Handshaken)
                        w -= 2;

                    WaitForData = 0;
                    WaitMore(w);
                }
            }
            if (IsWaiting)
                Wait();
        }

        /// <summary>
        /// Sets the Recv and Send Vectors for the ciphers
        /// </summary>
        public void SetVectors(uint SIV, uint RIV)
        {
            SendCipher.SetIV(SIV);
            RecvCipher.SetIV(RIV);
        }

        /// <summary>
        /// Checks if there is enough data to read, Or waits if there isn't.
        /// </summary>
        private void Wait()
        {
            if (!IsWaiting)
                IsWaiting = true;

            if (AvailableData >= 4)
            {
                IsWaiting = false;
                GetHeader();
            }
        }

        /// <summary>
        /// Second step of the wait sequence
        /// </summary>
        private void WaitMore(int length)
        {
            int add = RecvCipher.Handshaken ? 4 : 2;

            if (AvailableData < (length + add))
            {
                WaitForData = length + add;
                return;
            }

            byte[] data;

            data = new byte[length + add];
            Buffer.BlockCopy(buffer, 0, data, 0, data.Length);
            Buffer.BlockCopy(buffer, length + add, buffer, 0, buffer.Length - (length + add));
            AvailableData -= (length + add);

            Decrypt(data.ToArray());
        }

        /// <summary>
        /// Decrypts the packet data
        /// </summary>
        private void Decrypt(byte[] data)
        {
            if (!RecvCipher.Handshaken)
            {
                RecvCipher.RecvHandshake(ref data);
                ArrayReader pr = new ArrayReader(data);
                pr.ReadShort(); //Version
                pr.ReadMapleString(); //Sub Version
                uint siv = pr.ReadUInt();
                uint riv = pr.ReadUInt();
                SendCipher.SetIV(siv);
                RecvCipher.SetIV(riv);

                if (HandshakeFinished != null)
                    HandshakeFinished(siv, riv);
            }
            else
            {
                RecvCipher.Decrypt(ref data);
                if (data.Length == 0) return;

                if (PacketFinished != null)
                    PacketFinished(data);
            }
            Wait();
        }

        /// <summary>
        /// Gets the packet header from the current packet.
        /// </summary>
        private void GetHeader()
        {
            if (!RecvCipher.Handshaken)
                WaitMore(BitConverter.ToInt16(buffer, 0));
            else
            {
                int packetLength = RecvCipher.GetPacketLength(buffer);
                WaitMore(packetLength);
            }
        }

        /// <summary>
        /// Encrypts packet data
        /// </summary>
        /// <returns>
        /// True when the IV matches the requirements of being in a need to be pushed to the server 
        /// </returns>
        public Cipher.EncryptResult Encrypt(ref byte[] data, bool toClient = false)
        {
            return SendCipher.Encrypt(ref data, toClient);
        }
    }
}
