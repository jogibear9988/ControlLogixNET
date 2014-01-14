using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlLogixNET
{
    /// <summary>
    /// Various Logix Errors
    /// </summary>
    public enum LogixErrors
    {
        /// <summary>
        /// Session could not be established with the communications link
        /// </summary>
        SessionNotEstablished = -1,
        /// <summary>
        /// Session could not be registered with the communications link
        /// </summary>
        SessionNotRegistered = -2,
        /// <summary>
        /// CIP is not supported on the target device
        /// </summary>
        CIPNotSupported = -3,
        /// <summary>
        /// Processor could not be connected
        /// </summary>
        ProcessorNotConnected = -4,
        /// <summary>
        /// Incorrect Argument Tag Type
        /// </summary>
        IncorrectArgTagType = -5,
        /// <summary>
        /// Tag was not found on the processor
        /// </summary>
        TagNotFound = -6,
        /// <summary>
        /// The data returned by the processor does not match the tag type
        /// </summary>
        TypeMismatch = -7,
        /// <summary>
        /// The group that you are trying to add to the processor already exists
        /// </summary>
        GroupExists = -8,
        /// <summary>
        /// The specified group was not found
        /// </summary>
        GroupNotFound = -9,
        /// <summary>
        /// The processor received an invalid response size
        /// </summary>
        InvalidResponseSize = -10,
        /// <summary>
        /// The specified value could not be converted to the required type
        /// </summary>
        TypeConversionError = -11,
        /// <summary>
        /// The specified User Defined Type member was not found in the structure
        /// </summary>
        UDTMemberNotFound = -12,
        /// <summary>
        /// The IOI path could not be deciphered or the matching item was not found.
        /// </summary>
        MalformedIOI = -13,
        /// <summary>
        /// The item referenced could not be found on the processor
        /// </summary>
        ItemNotFound = -14,
        /// <summary>
        /// An error occurred trying to process one of the attributes
        /// </summary>
        AttributeError = -15,
        /// <summary>
        /// Not enough data was sent to the processor to execute the command
        /// </summary>
        NotEnoughData = -16,
        /// <summary>
        /// An insufficient number of attributes were supplied compared to the attribute count.
        /// </summary>
        InsufficientAttributes = -17,
        /// <summary>
        /// The IOI word length did not match the amount of IOI which was processed
        /// </summary>
        InvalidIOILength = -18,
        /// <summary>
        /// An attempt was made to access data beyond the end of the object
        /// </summary>
        AccessBeyondObject = -19,
        /// <summary>
        /// The abbreviated type does not match the data type of the object
        /// </summary>
        AbbreviatedTypeError = -20,
        /// <summary>
        /// The beginning offset was beyond the end of the template
        /// </summary>
        BeginOffsetError = -21,
        /// <summary>
        /// A socket exception has occurred
        /// </summary>
        SocketError = -22,
        /// <summary>
        /// An unknown error has occurred
        /// </summary>
        Unknown = -100
    }
}
