using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlLogixNET
{
    internal class GetStructAttribsRequest
    {
        //Bytes 6 and 7 are the instance number
        byte[] request = new byte[] 
            { 0x03, 0x03, 0x20, 0x6C, 0x25, 0x00, 0x00, 0x00, 0x04, 0x00, 0x04, 0x00, 0x03, 0x00,
              0x02, 0x00, 0x01, 0x00 };

        public byte RequestSize { get { return (byte)request.Length; } }

        public GetStructAttribsRequest(ushort StructureId)
        {
            request[7] = (byte)(StructureId & 0x00FF);
            request[6] = (byte)((StructureId & 0xFF00) >> 8);
        }

        public byte[] Pack()
        {
            return request;
        }
    }

    internal class GetStructAttribsReply
    {
        public uint TemplateSize { get; internal set; }
        public ushort MemorySize { get; internal set; }
        public ushort MemberCount { get; internal set; }
        public ushort Handle { get; internal set; }

        public GetStructAttribsReply(byte[] data)
        {
            TemplateSize = BitConverter.ToUInt32(data, 6);
            MemorySize = BitConverter.ToUInt16(data, 14);
            MemberCount = BitConverter.ToUInt16(data, 20);
            Handle = BitConverter.ToUInt16(data, 26);
        }
    }
}
