using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICommon;

namespace ControlLogixNET
{
    /// <summary>
    /// Represents a ControlLogix Tag
    /// </summary>
    public abstract class LogixTag : ITag
    {

        #region Fields

        private bool _enabled;
        private IDevice _parent;
        private ushort _dataType;
        private object _userData;
        private string _address;
        //private object _value;
        private string _lastError;
        private int _lastErrorNumber;
        private TagQuality _tagQuality;
        private DateTime _timeStamp;
        private ushort _elements = 1;

        //private object _pendingWriteValue;

        private ReadDataServiceRequest _readRequest;
        private WriteDataServiceRequest _writeRequest;
        private ushort _dim1 = 0;
        private ushort _dim2 = 0;
        private ushort _dim3 = 0;

        #endregion

        #region Properties

        ///<summary>The Enabled property turns on/off any updates to the tag.</summary>
        ///<remarks>
        ///<para>Setting the Enabled property to falls will disable any updates to the tag. When the property is disabled,
        ///the tag will no longer recieve updates from the controller. This does not mean that the tag will be removed from
        ///the controller update routine, it just means any updates will be ignored.</para>
        ///<para>When this property is false, it will not raise any events and any writes to the Value property will not have
        ///any effect (will not be cached).</para>
        ///</remarks>
        ///<example>
        ///This example shows how to disable the tag from receiving/sending any updates.
        ///<code lang="cs">
        ///public static class Program
        ///{
        ///     public static void Main(string[] args)
        ///     {
        ///         LogixTag myTag = new LogixTag("sample");
        ///         myTag.Enabled = false;
        ///     }
        ///}
        ///</code>
        ///</example>
        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = false;
            }
        }

        ///<summary>Gets the <see cref="ControlLogixNET.LogixProcessor"/> that this <see cref="ControlLogixNET.LogixTag"/> belongs to.</summary>
        ///<remarks>
        ///<para>This provides a reference from the <see cref="LogixTag"/> to the <see cref="LogixProcessor"/> that the tag belongs to. This
        ///property is automatically set when a tag is added to a processor. By design, any <see cref="LogixTag"/> can only belong to a single
        ///<see cref="LogixProcessor"/> at any time. Adding a <see cref="LogixTag"/> to another <see cref="LogixProcessor"/> will result in the
        ///tag being removed from one <see cref="LogixProcessor"/> before being added to the other. </para>
        ///</remarks>
        ///<example>
        ///This example shows how to get the processor from a <see cref="LogixTag"/>.
        ///<code lang="cs">
        ///public static class Program
        ///{
        ///     public static void Main(string[] args)
        ///     {
        ///         LogixTag myTag = new LogixTag("sample");
        ///         LogixProcessor tagProcessor = myTag.Device as LogixProcessor;
        ///     }
        ///}
        ///</code>
        ///</example>
        public IDevice Device
        {
            get { return _parent; }
            internal set { _parent = value; }
        }

        ///<summary>Gets the <see cref="EIPNET.CIP.CIPType"/> associated with the tag.</summary>
        ///<remarks>
        ///<para>The data type of the tag is automatically determined on the first read of the tag. For this reason, the
        ///tag cannot be written until it has been read at least once.</para>
        ///</remarks>
        ///<example>
        ///This example shows how to obtain the data type code of the tag and convert it to a <see cref="EIPNET.CIP.CIPType"/>.
        ///<code lang="cs">
        ///public static class Program
        ///{
        ///     public static void Main(string[] args)
        ///     {
        ///         LogixTag myTag = new LogixTag("sample");
        ///
        ///         //Here the tag is added to the processor, the processor
        ///         //will automatically read the tag before adding it to
        ///         //the optimized packet collection to verify that the
        ///         //tag exists and to obtain the data type.
        ///
        ///         EIPNET.CIP.CIPType tagType = (EIPNET.CIP.CIPType)myTag.DataType;
        ///     }
        ///}
        ///</code>
        ///</example>
        public ushort DataType
        {
            get { return _dataType; }
        }

        ///<summary>Gets or Sets any object to be associated with the tag.</summary>
        ///<remarks>This property is available to the user to store any data. The data stays with the tag
        ///in the system and is not modified outside of user code.</remarks>
        ///<example>
        ///This example shows how to store and retrieve data from the <see cref="UserData"/>.
        ///<code lang="cs">
        ///public static class Program
        ///{
        ///     public static void Main(string[] args)
        ///     {
        ///         LogixTag myTag = new LogixTag("sample");
        ///
        ///         myTag.UserData = "Stored string";
        ///         string storedString = myTag.UserData as string;
        ///
        ///     }
        ///}
        ///</code>
        ///</example>
        public object UserData
        {
            get
            {
                return _userData;
            }
            set
            {
                _userData = value;
            }
        }

        ///<summary>Gets the address (tag name) associated with the tag.</summary>
        ///<remarks>
        ///<para>
        ///When the tag is added to a device, the tag is read to automatically check for
        ///the existance of the tag and the tag's data type. If the tag does not exist on the
        ///processor, it will raise an error and be removed from the optimized packets.
        ///</para>
        ///</remarks>
        ///<example>
        ///This example shows how to obtain the address property after the tag has been created.
        ///<code lang="cs">
        ///public static class Program
        ///{
        ///     public static void Main(string[] args)
        ///     {
        ///         LogixTag myTag = new LogixTag("sample");
        ///
        ///         string tagName = myTag.Address;         //tagName now contains "sample"
        ///     }
        ///}
        ///</code>
        ///</example>
        public string Address
        {
            get { return _address; }
        }

        ///<summary>Gets a description of the last error associated with this tag.</summary>
        ///<remarks>
        ///<para>
        ///The error descriptions are obtained by analyzing the CIP error code and obtaining the
        ///description from the EIPNET library. The error descriptions are localized to the 
        ///current UI culture on the computer. The currently supported languages are EN and FR.
        ///</para>
        ///</remarks>
        ///<example>
        ///The following example shows how to read the <see cref="LastErrorProperty"/> of a tag.
        ///<code lang="cs">
        ///public static class Program
        ///{
        ///     public static void Main(string[] args)
        ///     {
        ///         LogixProcessor myPLC = new LogixProcessor("192.168.1.10", new byte[] { 1 });
        ///         myPLC.Connect();
        ///
        ///         LogixTag myTag = new LogixTag("sample", myPLC);
        ///         myTag.Value = 100;
        ///
        ///         myPLC.WriteTag(myTag);
        ///
        ///         if (myTag.LastErrorNumber != 0)
        ///         {
        ///             Console.WriteLine("Error Occurred: " + myTag.LastError);
        ///         }
        ///
        ///     }
        ///}
        ///</code>
        ///</example>
        public string LastError
        {
            get { return _lastError; }
            internal set { _lastError = value; }
        }

        ///<summary>Gets the last error number associated with this tag.</summary>
        ///<remarks>
        ///<para>
        ///The last error number is the CIP general status associated with the last operation
        ///on the tag. If this number is anything other than 0, it indicates an error has
        ///occurred. A text description of the error can be viewed by using the <see cref="LastError"/>
        ///property. The description text is localized.
        ///</para>
        ///</remarks>
        ///<example>
        ///The following example shows how to use the LastErrorNumber property.
        ///<code lang="cs">
        ///public static class Program
        ///{
        ///     public static void Main(string[] args)
        ///     {
        ///         LogixProcessor myPLC = new LogixProcessor("192.168.1.10", new byte[] { 1 });
        ///         myPLC.Connect();
        ///
        ///         LogixTag myTag = new LogixTag("sample", myPLC);
        ///         myTag.Value = 100;
        ///
        ///         myPLC.WriteTag(myTag);
        ///
        ///         if (myTag.LastErrorNumber != 0)
        ///         {
        ///             Console.WriteLine("Error Occurred: " + myTag.LastError);
        ///         }
        ///
        ///     }
        ///}
        ///</code>
        ///</example>
        public int LastErrorNumber
        {
            get { return _lastErrorNumber; }
            internal set { _lastErrorNumber = value; }
        }

        ///<summary>Gets the <see cref="ICommon.TagQuality"/> of the tag.</summary>
        ///<remarks>
        ///<para>
        ///The tag quality is initially <see cref="ICommon.TagQuality.Unknown"/> until the tag
        ///is read the first time (when it is added to a processor). If the tag can be successfully read,
        ///the quality becomes <see cref="ICommon.TagQuality.Good"/>, otherwise the quality is
        ///<see cref="ICommon.TagQuality.Bad"/>.
        ///</para>
        ///</remarks>
        public TagQuality Quality
        {
            get { return _tagQuality; }
        }

        ///<summary>Gets the timestamp of the most recent operation on the tag.</summary>
        ///<remarks>
        ///<para>
        ///The timestamp is initially set to <see cref="DateTime.MinValue"/>. The timestamp is the
        ///most recent operation (successful or otherwise) that happened on the tag.
        ///</para>
        ///</remarks>
        public DateTime TimeStamp
        {
            get { return _timeStamp; }
        }

        /// <summary>Gets the number of elements read for this tag</summary>
        /// <remarks>The number of elements is typically the size of the array to retrieve.</remarks>
        public ushort Elements
        {
            get { return _elements; }
        }

        /// <summary>
        /// Gets the Logix type of the tag
        /// </summary>
        public abstract LogixType.LogixTypes LogixType { get; }

        /// <summary>
        /// Gets or Sets the value of the tag as an object
        /// </summary>
        public abstract object ValueAsObject { get; set; }

        #endregion

        #region Internal Properties

        internal ushort Dim1 { get { return _dim1; } }
        internal ushort Dim2 { get { return _dim2; } }
        internal ushort Dim3 { get { return _dim3; } }
        internal ushort StructHandle { get; set; }
        internal LogixProcessor Processor { get { return (LogixProcessor)_parent; } }
        internal LogixTagInfo TagInfo { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// Event raised when the tag value is updated
        /// </summary>
        public event TagValueUpdateEventHandler TagValueUpdated;

        /// <summary>
        /// Event raised when the tag quality changes
        /// </summary>
        public event TagQualityChangedEventHandler TagQualityChanged;

        #endregion

        #region Construction / Deconstruction

        /// <summary>
        /// Creates a new <see cref="LogixTag"/> with the specified TagAddress.
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) of the item on the Processor.</param>
        internal LogixTag(string TagAddress)
        {
            _enabled = true;
            _parent = null;
            _dataType = 0x00;
            _address = TagAddress;
            _lastError = string.Empty;
            _lastErrorNumber = 0;
            _tagQuality = TagQuality.Unknown;
            _timeStamp = DateTime.MinValue;
        }

        /// <summary>
        /// Creates a new <see cref="LogixTag"/> with the specified TagAddress and adds it to the <see cref="T:LogixProcessor"/>.
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) of the item on the Processor.</param>
        /// <param name="Processor">Processor to add this <c>LogixTag</c> to.</param>
        internal LogixTag(string TagAddress, LogixProcessor Processor)
            : this(TagAddress)
        {
            _parent = Processor;
            //Processor.AddTag(this);

            //Everything else is automatically set by the processor
        }
		
#if MONO
	
		internal LogixTag(string TagAddress, LogixProcessor Processor, ushort ElementCount)
			: this(TagAddress, Processor, ElementCount, null)
		{
		}
		
#endif
		
#if MONO
		internal LogixTag(string TagAddress, LogixProcessor Processor, ushort ElementCount, object InitData)
#else
        /// <summary>
        /// Creates a new <see cref="LogixTag"/> with the specified TagAddress and adds it to the <see cref="T:LogixProcessor"/>.
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) of the item on the Processor.</param>
        /// <param name="Processor">Processor to add this <c>LogixTag</c> to.</param>
        /// <param name="ElementCount"></param>
        internal LogixTag(string TagAddress, LogixProcessor Processor, ushort ElementCount, object InitData = null)
#endif
		{
            _enabled = true;
            _parent = Processor;
            _dataType = 0x00;
            _address = TagAddress;
            _lastError = string.Empty;
            _lastErrorNumber = 0;
            _tagQuality = TagQuality.Unknown;
            _timeStamp = DateTime.MinValue;
            _elements = ElementCount;
            Initialize(InitData);
            //Processor.AddTag(this);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the dimensions of a multiple dimension array
        /// </summary>
        /// <param name="Dimension1">First Dimension</param>
        /// <param name="Dimension2">Second Dimension</param>
        public void SetMultipleDimensions(ushort Dimension1, ushort Dimension2)
        {
            _dim1 = Dimension1;
            _dim2 = Dimension2;
        }

        /// <summary>
        /// Sets the dimensions of a multiple dimension array
        /// </summary>
        /// <param name="Dimension1">First Dimension</param>
        /// <param name="Dimension2">Second Dimension</param>
        /// <param name="Dimension3">Third Dimension</param>
        public void SetMultipleDimensions(ushort Dimension1, ushort Dimension2, ushort Dimension3)
        {
            _dim1 = Dimension1;
            _dim2 = Dimension2;
            _dim3 = Dimension3;
        }

        #endregion

        #region Internal Abstract Methods
		
#if MONO
		internal void OnDataUpdated(byte[] data)
		{
			OnDataUpdated(data, 0);
		}
#endif
#if MONO
		internal abstract void OnDataUpdated(byte[] data, uint byteOffset);
#else
        /// <summary>
        /// Called when new data is obtained through a tag that is not an atomic type
        /// </summary>
        /// <remarks>This is used to create custom data types in the processor. To
        /// create a custom type, first inherit the ControlLogixNET.LogixTag class
        /// and add your custom data type fields. Then override the OnDataUpdated
        /// function to decode the data as required. The data returned in the array
        /// is presented exactly as it is read from the processor. The arrangement
        /// of data in the class depends on how the type is defined in the custom
        /// data type.</remarks>
        /// <param name="data">Array of data returned by the processor.</param>
        /// <param name="byteOffset">Offset that this byte array starts in the entire object</param>
        internal abstract bool OnDataUpdated(byte[] data, uint byteOffset = 0);
#endif
        /// <summary>
        /// Called when the tag needs to be written to the processor.
        /// </summary>
        /// <remarks>This is used to create custom data types in the processor. To
        /// create a custom type, first inherit the ControlLogixNET.Logix class
        /// and add your custom data type fields. Then override the OnDataUpdated
        /// and OnDataWrite functions. The data returned by the array is the packed
        /// data to be written to the PLC. The format of the data depends on how
        /// the data type is set up in the processor.</remarks>
        /// <returns>Custom data type packed into an array.</returns>
        internal abstract byte[] OnDataWrite();

        /// <summary>
        /// Returns True if there is a cached pending write
        /// </summary>
        /// <returns>True if there is a pending write</returns>
        internal abstract bool HasPendingWrite();

        /// <summary>
        /// Clears the pending write, this happens after the
        /// request has been successfully processed by the
        /// PLC.
        /// </summary>
        internal abstract void ClearPendingWrite();
		
#if MONO
		internal void Initialize()
		{
			Initialize(null);	
		}
#endif
#if MONO
		internal abstract void Initialize(object InitData);
#else
        /// <summary>
        /// Initializes internal data structures before the first read
        /// </summary>
        internal abstract void Initialize(object InitData = null);
#endif
		
        /// <summary>
        /// Returns the write data and the header for the tag
        /// </summary>
        /// <param name="header">[Out] Header to be sent with each packet</param>
        /// <param name="byteAlignment">[Out] Alignment that the bytes must be set on</param>
        /// <returns></returns>
        internal abstract byte[] GetWriteData(out int byteAlignment);

        #endregion

        #region Internal Methods

        internal void SetTagError(byte[] value)
        {
            TagQuality oldQuality = _tagQuality;

            switch (value[0])
            {
                case 0x04:
                    _lastError = Resources.ErrorStrings.MalformedIOI;
                    _lastErrorNumber = (int)LogixErrors.MalformedIOI;
                    _tagQuality = TagQuality.Bad;
                    break;
                case 0x05:
                    _lastError = Resources.ErrorStrings.ItemNotFound;
                    _lastErrorNumber = (int)LogixErrors.ItemNotFound;
                    _tagQuality = TagQuality.Bad;
                    break;
                case 0x0A:
                    _lastError = Resources.ErrorStrings.AttributeError;
                    _lastErrorNumber = (int)LogixErrors.AttributeError;
                    _tagQuality = TagQuality.Bad;
                    break;
                case 0x13:
                    _lastError = Resources.ErrorStrings.NotEnoughData;
                    _lastErrorNumber = (int)LogixErrors.NotEnoughData;
                    _tagQuality = TagQuality.Bad;
                    break;
                case 0x1C:
                    _lastError = Resources.ErrorStrings.InsufficientAttributes;
                    _lastErrorNumber = (int)LogixErrors.InsufficientAttributes;
                    _tagQuality = TagQuality.Bad;
                    break;
                case 0x26:
                    _lastError = Resources.ErrorStrings.InvalidIOILength;
                    _lastErrorNumber = (int)LogixErrors.InvalidIOILength;
                    _tagQuality = TagQuality.Bad;
                    break;
                case 0xFF:
                    if (value.Length != 4)
                    {
                        _lastError = Resources.ErrorStrings.Unknown;
                        _lastErrorNumber = (int)LogixErrors.Unknown;
                        _tagQuality = TagQuality.Bad;
                    }
                    else
                    {
                        switch (value[2])
                        {
                            case 0x05:
                                _lastError = Resources.ErrorStrings.AccessBeyondObject;
                                _lastErrorNumber = (int)LogixErrors.AccessBeyondObject;
                                _tagQuality = TagQuality.Bad;
                                break;
                            case 0x07:
                                _lastError = Resources.ErrorStrings.AbbreviatedTypeError;
                                _lastErrorNumber = (int)LogixErrors.AbbreviatedTypeError;
                                _tagQuality = TagQuality.Bad;
                                break;
                            case 0x04:
                                _lastError = Resources.ErrorStrings.BeginOffsetError;
                                _lastErrorNumber = (int)LogixErrors.BeginOffsetError;
                                _tagQuality = TagQuality.Bad;
                                break;
                            default:
                                _lastError = Resources.ErrorStrings.Unknown;
                                _lastErrorNumber = (int)LogixErrors.Unknown;
                                _tagQuality = TagQuality.Bad;
                                break;
                        }
                    }
                    break;
                case 0x06:
                case 0x00:
                    _lastErrorNumber = 0x00;
                    _lastError = string.Empty;
                    _tagQuality = TagQuality.Good;
                    break;
                default:
                    _lastError = Resources.ErrorStrings.Unknown;
                    _lastErrorNumber = (int)LogixErrors.Unknown;
                    _tagQuality = TagQuality.Bad;
                    break;
            }

            if (_tagQuality != oldQuality)
                RaiseTagQualityChanged(this);
        }
		
#if MONO
		internal void UpdateValue(byte[] value)
		{
			UpdateValue(value, 0);
		}
#endif
#if MONO
		internal bool UpdateValue(byte[] value, uint byteOffset)
#else
        internal bool UpdateValue(byte[] value, uint byteOffset = 0)
#endif
		{
            if (!Enabled)
                return false;

            //The structure is always a 2 byte type code followed by the
            //actual data...
            ushort typeCode = BitConverter.ToUInt16(value, 0);

            if (_dataType == 0)
            {
                //Setting the type code for the first time...
                _dataType = typeCode;
            }
            else
            {
                if (_dataType != typeCode)
                {
                    _tagQuality = TagQuality.Bad;
                    _lastError = Resources.ErrorStrings.TypeMismatch + "0x" + typeCode.ToString("X2");
                    _lastErrorNumber = (int)LogixErrors.TypeMismatch;
                    throw new Exception(Resources.ErrorStrings.TypeMismatch + "0x" + typeCode.ToString("X2"));
                }
            }
            byte[] temp = new byte[value.Length - 2];

            Buffer.BlockCopy(value, 2, temp, 0, temp.Length);

            _timeStamp = DateTime.Now;

            return OnDataUpdated(temp, byteOffset);
        }

        internal void GenerateRequests()
        {
            int rSize = 0;
            _readRequest = LogixServices.BuildLogixReadDataRequest(_address, _elements, out rSize);
            _writeRequest = LogixServices.BuildLogixWriteDataRequest(_address, _dataType, _elements, new byte[] { });

        }

        internal WriteDataServiceRequest GetWriteRequest()
        {
            _writeRequest.Data = OnDataWrite();
            return _writeRequest;
        }

        internal byte[] GetWriteData()
        {
            int align = 0;
            return GetWriteData(out align);
        }

        internal void ClearWrite()
        {
            ClearPendingWrite();
        }

        internal void RaiseTagDataChanged(LogixTag sender)
        {
            if (TagValueUpdated != null)
                TagValueUpdated(sender, new TagValueUpdateEventArgs(sender));
        }

        internal void RaiseTagQualityChanged(LogixTag sender)
        {
            if (TagQualityChanged != null)
                TagQualityChanged(sender, new TagQualityChangedEventArgs(sender));
        }

        #endregion

        #region Private Methods



        #endregion

    }
}
