using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlLogixNET
{
    internal class CommonDataServiceReply
    {
        public byte Service { get; set; }
        public byte Reserved { get; set; }
        public ushort Status { get; set; }
    }
}
