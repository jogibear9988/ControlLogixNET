using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlLogixNET
{
    internal static class SequenceNumberGenerator
    {

        private static object _lockObject;
        private static ushort _sequenceNum;


        public static ushort SequenceNumber
        {
            get
            {
                lock (_lockObject)
                {
                    if (_sequenceNum == ushort.MaxValue)
                        _sequenceNum = ushort.MinValue;
                    _sequenceNum++;
                    return _sequenceNum;
                }
            }
        }

        static SequenceNumberGenerator()
        {
            _lockObject = new object();
        }

    }
}
