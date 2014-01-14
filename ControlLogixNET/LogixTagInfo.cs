using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlLogixNET
{
    /// <summary>
    /// Holds information about a LogixTag in the processor
    /// </summary>
    public class LogixTagInfo
    {
        /// <summary>
        /// Gets the memory address of the tag in the LogixProcessor
        /// </summary>
        internal uint MemoryAddress { get; set; }
        /// <summary>
        /// Gets the type information
        /// </summary>
        public ushort FullTypeInfo { get; internal set; }
        /// <summary>
        /// Gets the CIP data type for the tag, or the structure handle
        /// </summary>
        public ushort DataType { get; internal set; }
        /// <summary>
        /// Gets the number of dimensions for array tags
        /// </summary>
        public ushort Dimensions { get; internal set; }
        /// <summary>
        /// Gets true if the tag is a structure, false if it is atomic
        /// </summary>
        public bool IsStructure { get; internal set; }
        /// <summary>
        /// Gets the name of the tag on the <see cref="LogixProcessor"/>
        /// </summary>
        public string TagName { get; internal set; }
        /// <summary>
        /// Gets the first dimension size for array tags.
        /// </summary>
        public uint Dimension1Size { get; internal set; }
        /// <summary>
        /// Gets the second dimension size for array tags. In order for
        /// this to be valid, <see cref="Dimension1Size"/> must be greater
        /// than zero.
        /// </summary>
        public uint Dimension2Size { get; internal set; }
        /// <summary>
        /// Gets the third dimension size for array tags. In order for
        /// this to be valid, <see cref="Dimension2Size"/> must be greater
        /// than zero.
        /// </summary>
        public uint Dimension3Size { get; internal set; }

        internal LogixTagInfo() { }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(TagName);

            if (Dimensions > 0)
            {
                sb.Append("[");
                sb.Append(Dimension1Size);
                if (Dimensions > 1)
                {
                    sb.Append(",");
                    sb.Append(Dimension2Size);
                }

                if (Dimensions > 2)
                {
                    sb.Append(",");
                    sb.Append(Dimension3Size);
                }
                sb.Append("]");
            }

            sb.Append((IsStructure ? " (Structure)" : "(Not a Structure)"));
            sb.Append(" Memory Address: " + MemoryAddress.ToString("X8"));

            return sb.ToString();
        }
    }
}
