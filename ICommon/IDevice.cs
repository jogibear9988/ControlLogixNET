using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICommon
{
    /// <summary>
    /// Represents a device which has tag data
    /// </summary>
    public interface IDevice : IDisposable
    {
        /// <summary>
        /// Gets the last error code
        /// </summary>
        int ErrorCode { get; }
        /// <summary>
        /// Gets the last error string
        /// </summary>
        string ErrorString { get; }
        /// <summary>
        /// Gets or Sets an object for use by the user
        /// </summary>
        object UserData { get; set; }
        /// <summary>
        /// Gets the version information
        /// </summary>
        Version Version { get; }

        /// <summary>
        /// Connects to the device
        /// </summary>
        /// <returns>True if connected</returns>
        bool Connect();
        /// <summary>
        /// Disconnects from the device
        /// </summary>
        /// <returns>True if disconnected</returns>
        bool Disconnect();

        /// <summary>
        /// Reads a particular tag from the PLC
        /// </summary>
        /// <param name="tag">Tag to read</param>
        /// <returns>True if the tag was read successfully</returns>
        bool ReadTag(ITag tag);
        /// <summary>
        /// Writes a particular tag to the PLC
        /// </summary>
        /// <param name="tag">Tag to write</param>
        /// <returns>True if the tag was written successfully</returns>
        bool WriteTag(ITag tag);

    }
}
