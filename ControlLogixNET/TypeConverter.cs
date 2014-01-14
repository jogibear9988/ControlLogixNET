using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlLogixNET
{
    internal static class TypeConverter
    {
        public static int[] GetInt32Array(byte[] sourceArray, int offset, int length)
        {
            int skipSize = 4;
            List<int> retVals = new List<int>();

            if (sourceArray.Length < offset + length)
                throw new IndexOutOfRangeException("Offset and length must refer to a position in the array");

            for (int i = offset; i < offset + length; i += skipSize)
            {
                retVals.Add(BitConverter.ToInt32(sourceArray, i));
            }

            return retVals.ToArray();
        }

        public static long[] GetInt64Array(byte[] sourceArray, int offset, int length)
        {
            int skipSize = 8;
            List<long> retVals = new List<long>();

            if (sourceArray.Length < offset + length)
                throw new IndexOutOfRangeException("Offset and length must refer to a position in the array");

            for (int i = offset; i < offset + length; i += skipSize)
            {
                retVals.Add(BitConverter.ToInt64(sourceArray, i));
            }

            return retVals.ToArray();
        }

        public static short[] GetShortArray(byte[] sourceArray, int offset, int length)
        {
            int skipSize = 2;
            List<short> retVals = new List<short>();

            if (sourceArray.Length < offset + length)
                throw new IndexOutOfRangeException("Offset and length must refer to a position in the array");

            for (int i = offset; i < offset + length; i += skipSize)
            {
                retVals.Add(BitConverter.ToInt16(sourceArray, i));
            }

            return retVals.ToArray();
        }

        public static float[] GetFloatArray(byte[] sourceArray, int offset, int length)
        {
            int skipSize = 4;
            List<float> retVals = new List<float>();

            if (sourceArray.Length < offset + length)
                throw new IndexOutOfRangeException("Offset and length must refer to a position in the array");

            for (int i = offset; i < offset + length; i += skipSize)
            {
                retVals.Add(BitConverter.ToSingle(sourceArray, i));
            }

            return retVals.ToArray();
        }

        public static byte[] GetByteArray(byte[] sourceArray, int offset, int length)
        {
            byte[] temp = new byte[length];
            Buffer.BlockCopy(sourceArray, (int)offset, temp, 0, length);
            return temp;
        }

        public static bool[] GetBoolArray(int[] sourceArray)
        {
            List<bool> retVal = new List<bool>();
            for (int i = 0; i < sourceArray.Length; i++)
            {
                for (int b = 0; b < 32; b++)
                {
                    int mask = 1 << b;
                    if ((sourceArray[i] & mask) == mask)
                        retVal.Add(true);
                    else
                        retVal.Add(false);
                }
            }
            return retVal.ToArray();
        }

        public static int[] GetBoolArray(bool[] sourceArray)
        {
            List<int> retVal = new List<int>();
            int current = 0;
            int bit = 0;

            for (int i = 0; i < sourceArray.Length; i++)
            {
                if (sourceArray[i])
                {
                    current &= 0x01 << bit;
                }

                bit++;

                if (bit > 31)
                {
                    retVal.Add(current);
                    current = 0;
                    bit = 0;
                }
            }

            return retVal.ToArray();
        }
		
#if MONO
		public static byte[] GetBytes(int[] intArray)
		{
			return GetBytes(intArray, 0);		
		}
#endif
#if MONO
		public static byte[] GetBytes(int[] intArray, int sizeInBytes)
#else
        public static byte[] GetBytes(int[] intArray, int sizeInBytes = 0)
#endif
		{
            if (sizeInBytes == 0)
                sizeInBytes = intArray.Length * 4;

            List<byte> retVal = new List<byte>();
            for (int i = 0; i < sizeInBytes; i++)
            {
                if (i * 4 < intArray.Length)
                {
                    retVal.AddRange(BitConverter.GetBytes(intArray[i / 4]));
                    i += 3;
                }
                else
                    retVal.Add(0);
            }

            return retVal.ToArray();
        }
		
#if MONO
		public static byte[] GetBytes(short[] shortArray)
		{
			return GetBytes(shortArray, 0);		
		}
#endif
#if MONO
		public static byte[] GetBytes(short[] shortArray, int sizeInBytes)
#else
        public static byte[] GetBytes(short[] shortArray, int sizeInBytes = 0)
#endif
		{
            if (sizeInBytes == 0)
                sizeInBytes = shortArray.Length * 2;

            List<byte> retVal = new List<byte>();
            for (int i = 0; i < sizeInBytes; i++)
            {
                if (i * 2 < shortArray.Length)
                {
                    retVal.AddRange(BitConverter.GetBytes(shortArray[i / 2]));
                    i += 1;
                }
                else
                    retVal.Add(0);
            }

            return retVal.ToArray();
        }
		
#if MONO
		public static byte[] GetBytes(long[] longArray)
		{
			return GetBytes(longArray, 0);		
		}
#endif
#if MONO
		public static byte[] GetBytes(long[] longArray, int sizeInBytes)
#else
        public static byte[] GetBytes(long[] longArray, int sizeInBytes = 0)
#endif
		{
            if (sizeInBytes == 0)
                sizeInBytes = longArray.Length * 8;

            List<byte> retVal = new List<byte>();
            for (int i = 0; i < sizeInBytes; i++)
            {
                if (i * 8 < longArray.Length)
                {
                    retVal.AddRange(BitConverter.GetBytes(longArray[i / 4]));
                    i += 7;
                }
                else
                    retVal.Add(0);
            }

            return retVal.ToArray();
        }
		
#if MONO
		public static byte[] GetBytes(float[] floatArray)
		{
			return GetBytes(floatArray, 0);		
		}
#endif
#if MONO
		public static byte[] GetBytes(float[] floatArray, int sizeInBytes)
#else
        public static byte[] GetBytes(float[] floatArray, int sizeInBytes = 0)
#endif
		{
            if (sizeInBytes == 0)
                sizeInBytes = floatArray.Length * 4;

            List<byte> retVal = new List<byte>();
            for (int i = 0; i < sizeInBytes; i++)
            {
                if (i * 4 < floatArray.Length)
                {
                    retVal.AddRange(BitConverter.GetBytes(floatArray[i / 4]));
                    i += 3;
                }
                else
                    retVal.Add(0);
            }

            return retVal.ToArray();
        }
    }
}
