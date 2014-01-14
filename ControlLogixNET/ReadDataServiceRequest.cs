using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlLogixNET
{
    internal class ReadDataServiceRequest
    {
        public byte Service { get; set; }
        public byte PathSize { get; set; }
        public byte[] Path { get; set; }
        public ushort Elements { get; set; }
        public uint DataOffset { get; set; }
        public bool IsFragmented { get; set; }
        public byte[] AddtlData { get; set; }

        public int Size { get { return 2 + (PathSize * 2) + 2 + (IsFragmented ? 4 : 0) + (AddtlData == null ? 0 : AddtlData.Length); } }

        public byte[] Pack()
        {
            byte[] retVal = new byte[Size];
            if (AddtlData != null)
                retVal = new byte[Size - 2];
            retVal[0] = Service;
            retVal[1] = (byte)(PathSize);
            Buffer.BlockCopy(Path, 0, retVal, 2, Path.Length);
            int offset = 2 + (2 * PathSize);

            if (AddtlData != null)
            {
                Buffer.BlockCopy(AddtlData, 0, retVal, offset, AddtlData.Length);
                offset += AddtlData.Length;
            }
            else
            {
                if (IsFragmented)
                {
                    Buffer.BlockCopy(BitConverter.GetBytes(Elements), 0, retVal, offset, 2);
                    offset += 2;
                    Buffer.BlockCopy(BitConverter.GetBytes(DataOffset), 0, retVal, offset, 4);
                    offset += 4;
                    //Buffer.BlockCopy(BitConverter.GetBytes(Elements), 0, retVal, retVal.Length - 6, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(DataOffset), 0, retVal, retVal.Length - 4, 4);
                }
                else
                {
                    Buffer.BlockCopy(BitConverter.GetBytes(Elements), 0, retVal, offset, 2);
                    offset += 2;
                    //Buffer.BlockCopy(BitConverter.GetBytes(Elements), 0, retVal, retVal.Length - 2, 2);
                }
            }

            return retVal;
        }
    }
}
