using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIPNET.CIP;

namespace ControlLogixNET
{
    internal class LogixRead
    {
        public CIPType DataType { get; set; }
        public int VarCount { get; set; }
        public int TotalSize { get; set; }
        public int ElementSize { get; set; }
        public uint Mask { get; set; }
    }
}
