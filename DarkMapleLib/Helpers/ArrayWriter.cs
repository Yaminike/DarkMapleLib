using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkMapleLib.Helpers
{
    /// <summary>
    /// Class to handle writing data to an byte array
    /// </summary>
    /// <remarks>
    /// Created by Yaminike, aka Minike, aka 0minike0
    /// </remarks>
    public class ArrayWriter
    {
        /// <summary>
        /// Buffer holding the packet data
        /// </summary>
        private byte[] Buffer { get; set; }

        /// <summary>
        /// Length of the packet
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// The position to start reading on
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Creates a new instance of a ArrayWriter
        /// </summary>
        public ArrayWriter()
        {
            this.Buffer = new byte[0x50];
        }

        /// <summary>
        /// Prevents the buffer being to small
        /// </summary>
        private void EnsureCapacity(int length)
        {
            if (Position + length < this.Buffer.Length) return; //Return as quikly as posible
            byte[] newBuffer = new byte[this.Buffer.Length + 0x50];
            System.Buffer.BlockCopy(this.Buffer, 0, newBuffer, 0, this.Buffer.Length);
            this.Buffer = newBuffer;
            EnsureCapacity(length);
        }

        /// <summary>
        /// Writes bytes to the buffer
        /// </summary>
        public unsafe void WriteBytes(byte[] bytes)
        {
            int length = bytes.Length;
            EnsureCapacity(length);

            fixed (byte* pBuffer = this.Buffer)
            {
                for (int i = 0; i < length; i++)
                    *(pBuffer + this.Position + i) = bytes[i];
            }

            Length += length;
            Position += length;
        }

        /// <summary>
        /// Sets bytes in the buffer
        /// </summary>
        public unsafe void SetBytes(byte[] bytes, int position)
        {
            fixed (byte* pBuffer = this.Buffer)
            {
                for (int i = 0; i < bytes.Length; i++)
                    *(pBuffer + position + i) = bytes[i];
            }
        }

        /// <summary>
        /// Writes a bool to the buffer
        /// </summary>
        public unsafe void WriteBool(bool value)
        {
            WriteByte((byte)(value ? 1 : 0));
        }

        /// <summary>
        /// Sets a bool in the buffer
        /// </summary>
        public unsafe void SetBool(bool value, int position)
        {
            SetByte((byte)(value ? 1 : 0), position);
        }

        /// <summary>
        /// Writes a signed byte to the buffer
        /// </summary>
        public unsafe void WriteSByte(sbyte value)
        {
            int length = 1;
            EnsureCapacity(length);

            fixed (byte* pBuffer = this.Buffer)
                *(sbyte*)(pBuffer + this.Position) = value;

            Length += length;
            Position += length;
        }

        /// <summary>
        /// Sets a signed byte in the buffer
        /// </summary>
        public unsafe void SetSByte(sbyte value, int position)
        {
            fixed (byte* pBuffer = this.Buffer)
                *(sbyte*)(pBuffer + position) = value;
        }

        /// <summary>
        /// Writes a unsigned byte to the buffer
        /// </summary>
        public unsafe void WriteByte(byte value)
        {
            int length = 1;
            EnsureCapacity(length);

            fixed (byte* pBuffer = this.Buffer)
                *(pBuffer + this.Position) = value;

            Length += length;
            Position += length;
        }

        /// <summary>
        /// Sets a unsigned byte in the buffer
        /// </summary>
        public unsafe void SetByte(byte value, int position)
        {
            fixed (byte* pBuffer = this.Buffer)
                *(pBuffer + position) = value;
        }

        /// <summary>
        /// Writes a signed short to the buffer
        /// </summary>
        public unsafe void WriteShort(short value)
        {
            int length = 2;
            EnsureCapacity(length);

            fixed (byte* pBuffer = this.Buffer)
                *(short*)(pBuffer + this.Position) = value;

            Length += length;
            Position += length;
        }

        /// <summary>
        /// Sets a signed short in the buffer
        /// </summary>
        public unsafe void SetShort(short value, int position)
        {
            fixed (byte* pBuffer = this.Buffer)
                *(short*)(pBuffer + position) = value;
        }

        /// <summary>
        /// Writes a unsigned short to the buffer
        /// </summary>
        public unsafe void WriteUShort(ushort value)
        {
            int length = 2;
            EnsureCapacity(length);

            fixed (byte* pBuffer = this.Buffer)
                *(ushort*)(pBuffer + this.Position) = value;

            Length += length;
            Position += length;
        }

        /// <summary>
        /// Sets a unsigned short in the buffer
        /// </summary>
        public unsafe void SetUShort(ushort value, int position)
        {
            fixed (byte* pBuffer = this.Buffer)
                *(ushort*)(pBuffer + position) = value;
        }

        /// <summary>
        /// Writes a signed int to the buffer
        /// </summary>
        public unsafe void WriteInt(int value)
        {
            int length = 4;
            EnsureCapacity(length);

            fixed (byte* pBuffer = this.Buffer)
                *(int*)(pBuffer + this.Position) = value;

            Length += length;
            Position += length;
        }

        /// <summary>
        /// Sets a signed int in the buffer
        /// </summary>
        public unsafe void SetInt(int value, int position)
        {
            fixed (byte* pBuffer = this.Buffer)
                *(int*)(pBuffer + position) = value;
        }

        /// <summary>
        /// Writes a unsigned int to the buffer
        /// </summary>
        public unsafe void WriteUInt(uint value)
        {
            int length = 4;
            EnsureCapacity(length);

            fixed (byte* pBuffer = this.Buffer)
                *(uint*)(pBuffer + this.Position) = value;

            Length += length;
            Position += length;
        }

        /// <summary>
        /// Sets a unsigned int in the buffer
        /// </summary>
        public unsafe void SetUInt(uint value, int position)
        {
            fixed (byte* pBuffer = this.Buffer)
                *(uint*)(pBuffer + position) = value;
        }

        /// <summary>
        /// Writes a signed long to the buffer
        /// </summary>
        public unsafe void WriteLong(long value)
        {
            int length = 8;
            EnsureCapacity(length);

            fixed (byte* pBuffer = this.Buffer)
                *(long*)(pBuffer + this.Position) = value;

            Length += length;
            Position += length;
        }

        /// <summary>
        /// Sets a signed long in the buffer
        /// </summary>
        public unsafe void SetLong(long value, int position)
        {
            fixed (byte* pBuffer = this.Buffer)
                *(long*)(pBuffer + position) = value;
        }

        /// <summary>
        /// Writes a unsigned long to the buffer
        /// </summary>
        public unsafe void WriteULong(ulong value)
        {
            int length = 8;
            EnsureCapacity(length);

            fixed (byte* pBuffer = this.Buffer)
                *(ulong*)(pBuffer + this.Position) = value;

            Length += length;
            Position += length;
        }

        /// <summary>
        /// Sets a unsigned long in the buffer
        /// </summary>
        public unsafe void SetULong(ulong value, int position)
        {
            fixed (byte* pBuffer = this.Buffer)
                *(ulong*)(pBuffer + position) = value;
        }

        /// <summary>
        /// Writes a number of empty bytes to the buffer
        /// </summary>
        /// <param name="count">Number of empty (zero) bytes to write</param>
        public void WriteZeroBytes(int count)
        {
            WriteBytes(new byte[count]);
        }

        /// <summary>
        /// Write a string as maplestring to the buffer
        /// </summary>
        /// <param name="mString">String to write</param>
        public void WriteMapleString(string mString)
        {
            if (String.IsNullOrWhiteSpace(mString) || mString.Length == 0)
            {
                WriteZeroBytes(2);
                return;
            }
            byte[] bytes = Encoding.UTF8.GetBytes(mString);
            WriteUShort((ushort)bytes.Length);
            WriteBytes(bytes);
        }

        /// <summary>
        /// Creates an byte array of the current ArrayWriter
        /// </summary>
        /// <param name="direct">If true, returns a direct reference of the buffer</param>
        public byte[] ToArray(bool direct = false)
        {
            if (direct)
                return this.Buffer;
            else
            {
                byte[] toRet = new byte[this.Length];
                System.Buffer.BlockCopy(this.Buffer, 0, toRet, 0, this.Length);
                return toRet;
            }
        }
    }
}
