using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlLogixNET
{
    /// <summary>
    /// Raised when an invalid response was detected from the processor
    /// </summary>
    public class InvalidProcessorResponseException : Exception
    {
        /// <summary>
        /// Creates a new InvalidProcessorResponseException
        /// </summary>
        /// <param name="Description">Description of the exception</param>
        internal InvalidProcessorResponseException(string Description)
            : base(Description)
        {

        }
    }

    /// <summary>
    /// Raised when the dimensions have not been set before accessing a
    /// multi-dimensional tag.
    /// </summary>
    public class DimensionsNotSetException : Exception
    {
        /// <summary>
        /// Creates a new DimensionsNotSetException
        /// </summary>
        /// <param name="Description">Description of the exception</param>
        internal DimensionsNotSetException(string Description)
            : base(Description)
        {

        }
    }

    /// <summary>
    /// Raised when trying to add a group to a <see cref="LogixProcessor"/> that
    /// already exists.
    /// </summary>
    public class GroupAlreadyExistsException : Exception
    {
        internal GroupAlreadyExistsException(string Description)
            : base(Description)
        {

        }
    }

    /// <summary>
    /// Raised when trying to retrieve a group that doesn't exist on the processor
    /// </summary>
    public class GroupNotFoundException : Exception
    {
        internal GroupNotFoundException(string Description)
            : base(Description)
        {

        }
    }

    /// <summary>
    /// Raised when trying to address a field in a UDT that does not exist.
    /// </summary>
    public class UDTMemberNotFoundException : Exception
    {
        internal UDTMemberNotFoundException(string Description)
            : base(Description)
        {

        }
    }

    /// <summary>
    /// Raised when the specified type cannot be converted to the correct type.
    /// </summary>
    public class TypeConversionException : Exception
    {
        internal TypeConversionException(string Description)
            : base(Description)
        {

        }
    }

    /// <summary>
    /// Raised when an array length is not of the required size
    /// </summary>
    public class ArrayLengthException : Exception
    {
        internal ArrayLengthException(string Description)
            : base(Description) { }
    }
}
