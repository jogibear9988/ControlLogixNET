using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlLogixNET
{
    internal class WriteDataServiceRequest
    {
        public byte Service { get; set; }
        public byte PathSize { get; set; }
        public byte[] Path { get; set; }
        public ushort DataType { get; set; }
        public ushort StructHandle { get; set; }
        public ushort Elements { get; set; }
        public byte[] Data { get; set; }
        public bool IsFragmented { get; set; }
        public uint Offset { get; set; }

        public int Size { get { return 6 + (2 * PathSize) + (Data == null ? 0 : Data.Length) + (IsFragmented ? 4 : 0) + (StructHandle == 0 ? 0 : 2); } }

        public byte[] Pack()
        {
            byte[] retVal = new byte[Size];
            retVal[0] = Service;
            retVal[1] = PathSize;
            Buffer.BlockCopy(Path, 0, retVal, 2, Path.Length);
            int offset = 2 + (2 * PathSize);
            Buffer.BlockCopy(BitConverter.GetBytes(DataType), 0, retVal, offset, 2);
            offset += 2;
            if (StructHandle != 0)
            {
                Buffer.BlockCopy(BitConverter.GetBytes(StructHandle), 0, retVal, offset, 2);
                offset += 2;
            }
            Buffer.BlockCopy(BitConverter.GetBytes(Elements), 0, retVal, offset, 2);
            offset += 2;
            if (IsFragmented)
            {
                Buffer.BlockCopy(BitConverter.GetBytes(Offset), 0, retVal, offset, 4);
                offset += 4;
            }
            if (Data != null)
                Buffer.BlockCopy(Data, 0, retVal, offset, Data.Length);

            return retVal;
        }
    }
}
