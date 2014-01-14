using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlLogixNET
{
    /// <summary>
    /// State of the ControlLogix Processor
    /// </summary>
    public enum ProcessorState
    {
        /// <summary>
        /// Solid Red (Power-Up)
        /// </summary>
        SolidRed = 0,
        /// <summary>
        /// Firmware Update Mode
        /// </summary>
        FirmwareUpdate = 1,
        /// <summary>
        /// Communication Fault
        /// </summary>
        CommunicationFault = 2,
        /// <summary>
        /// Awaiting Connection
        /// </summary>
        AwaitingConnection = 3,
        /// <summary>
        /// Bad Configuration
        /// </summary>
        ConfigurationBad = 4,
        /// <summary>
        /// Major Fault
        /// </summary>
        MajorFault = 5,
        /// <summary>
        /// Connected
        /// </summary>
        Connected = 6,
        /// <summary>
        /// Program Mode
        /// </summary>
        ProgramMode = 7
    }
}
