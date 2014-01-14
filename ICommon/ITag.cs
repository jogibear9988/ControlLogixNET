using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICommon
{
    /// <summary>
    /// Interface for Tag objects
    /// </summary>
    public interface ITag
    {
        /// <summary>
        /// Gets or Sets a value determining if the tag is active on the controller
        /// </summary>
        bool Enabled { get; set; }
        /// <summary>
        /// Gets or the device to which this tag belongs
        /// </summary>
        IDevice Device { get; }
        /// <summary>
        /// Gets the DataType code for the tag
        /// </summary>
        ushort DataType { get; }
        /// <summary>
        /// Gets or Sets an object available to the user for any use
        /// </summary>
        object UserData { get; set; }
        /// <summary>
        /// Gets or Sets the address of the tag
        /// </summary>
        string Address { get; }
        /// <summary>
        /// Gets the last error associated with the tag
        /// </summary>
        string LastError { get; }
        /// <summary>
        /// Gets the last error number associated with the tag
        /// </summary>
        int LastErrorNumber { get; }
        /// <summary>
        /// Gets the quality of the tag
        /// </summary>
        TagQuality Quality { get; }
        /// <summary>
        /// Gets the timestamp of the last successful operation on the tag
        /// </summary>
        DateTime TimeStamp { get; }

        /// <summary>
        /// Raised when the value of the tag is updated
        /// </summary>
        event TagValueUpdateEventHandler TagValueUpdated;
        /// <summary>
        /// Raised when the quality of the tag has changed
        /// </summary>
        event TagQualityChangedEventHandler TagQualityChanged;
    }

    //Support Classes

    /// <summary>
    /// Event delegate for the TagValueUpdateEvent
    /// </summary>
    /// <param name="sender">ITag raising the update</param>
    /// <param name="e">Event arguments</param>
    public delegate void TagValueUpdateEventHandler(ITag sender, TagValueUpdateEventArgs e);
    /// <summary>
    /// Event arguments for <see cref="TagValueUpdateEventHandler"/>
    /// </summary>
    public class TagValueUpdateEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the Tag associated with the Update event
        /// </summary>
        public ITag Tag { get; internal set; }

        /// <summary>
        /// Creates a new TagValueUpdateEventArgs class
        /// </summary>
        /// <param name="tag">Tag associated with the update</param>
        public TagValueUpdateEventArgs(ITag tag)
        {
            Tag = tag;
        }
    }

    /// <summary>
    /// Event delegate for the TagQualityChanged
    /// </summary>
    /// <param name="sender">ITag raising the event</param>
    /// <param name="e">Event arguments</param>
    public delegate void TagQualityChangedEventHandler(ITag sender, TagQualityChangedEventArgs e);
    /// <summary>
    /// Event arguments for <see cref="TagQualityChangedEventHandler"/>
    /// </summary>
    public class TagQualityChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the Tag associated with the Update event
        /// </summary>
        public ITag Tag { get; internal set; }

        /// <summary>
        /// Creates a new TagValueUpdateEventArgs class
        /// </summary>
        /// <param name="tag">Tag associated with the update</param>
        public TagQualityChangedEventArgs(ITag tag)
        {
            Tag = tag;
        }
    }
}
