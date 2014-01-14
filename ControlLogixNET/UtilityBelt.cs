using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlLogixNET
{
    internal static class UtilityBelt
    {
        /// <summary>
        /// Compares 2 arrays. The arrays must be the same size...
        /// </summary>
        /// <param name="array1">First array</param>
        /// <param name="array2">Second array</param>
        /// <returns>True if they are equal, false if they are not</returns>
        public static bool CompareArrays(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
                return false;

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                    return false;
            }

            return true;
        }
		
#if MONO
		public static bool CompareArrays(byte[] sourceArray, byte[] compareArray, uint offset)
		{
			return CompareArrays(sourceArray, compareArray, offset, 0);
		}
#endif 
		
#if MONO
		public static bool CompareArrays(byte[] sourceArray, byte[] compareArray, uint offset, uint len)
#else
        /// <summary>
        /// Compares a source array at the specified offset to the compare array
        /// </summary>
        /// <param name="sourceArray">Source array</param>
        /// <param name="compareArray">Compare array</param>
        /// <param name="offset">Position to start compare in the source array</param>
        /// <returns>True if they are equal, false if not</returns>
        public static bool CompareArrays(byte[] sourceArray, byte[] compareArray, uint offset, uint len = 0)
#endif
		{
            int compPos = 0;
            if (len == 0)
                len = (uint)compareArray.Length;
            for (uint i = offset; i < len; i++)
            {
                if (sourceArray[i] != compareArray[compPos])
                    return false;
                compPos++;
            }
            return true;
        }

    }
}
