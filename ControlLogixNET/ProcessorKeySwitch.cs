using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlLogixNET
{
    /// <summary>
    /// Position of the Processor Key Switch
    /// </summary>
    public enum ProcessorKeySwitch
    {
        /// <summary>
        /// Unknown Position
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Run Mode
        /// </summary>
        RunMode = 1,
        /// <summary>
        /// Program Mode
        /// </summary>
        ProgramMode = 2,
        /// <summary>
        /// Remote Mode
        /// </summary>
        RemoteMode = 3
    }
}
