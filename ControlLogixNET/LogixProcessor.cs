using System;
using System.Collections.Generic;
using System.Text;
using ICommon;
using EIPNET.EIP;
using System.Diagnostics;
using EIPNET;
using EIPNET.CIP;
using System.Threading;
using System.Net.Sockets;

namespace ControlLogixNET
{
    /// <summary>
    /// Interface to a ControlLogix Processor
    /// </summary>
    public sealed class LogixProcessor : IDevice
    {

        #region Fields

        private object _userData;
        private int _errorCode;
        private string _errorString;

        private byte[] _path;
        private string _ipAddress;
        private SessionInfo _session;

        private SortedDictionary<string, LogixTag> _tags;

        private ProcessorState _state = ProcessorState.SolidRed;
        private ProcessorKeySwitch _keySw = ProcessorKeySwitch.Unknown;
        private ProcessorFaultState _faultState = ProcessorFaultState.None;

        private ushort _vendorId;
        private ushort _deviceType;
        private ushort _productCode;
        private byte _majorRevision;
        private byte _minorRevision;
        private uint _serialNumber;
        private string _productName;

        private object _lockObject = new object();

        private Timer _stateTimer;
        private Thread _stateThread;
        private bool _stateUpdateEnabled = true;
        private bool _stateEventsEnabled = false;

        private Timer _autoUpdateTimer;
        private Thread _autoUpdateThread;
        private bool _autoUpdateEnabled = false;
        private int _autoUpdateTime = 0;

        private Dictionary<string, LogixTagGroup> _tagGroups;

        private DateTime _lastOperationTime = DateTime.Now;
        private bool _initializing = false;

        private List<LogixTagInfo> _tagInfo;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the most recent error code, see <see cref="LogixErrors"/>
        /// </summary>
        public int ErrorCode
        {
            get { return _errorCode; }
        }

        /// <summary>
        /// Gets a text description of the most recent error in a localized language if available
        /// </summary>
        public string ErrorString
        {
            get { return _errorString; }
        }

        /// <summary>
        /// Gets or Sets any data available to the user to store with the LogixProcessor
        /// </summary>
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

        /// <summary>
        /// Gets the version information
        /// </summary>
        public Version Version
        {
            get
            {
                return System.Reflection.Assembly.GetAssembly(typeof(LogixProcessor)).GetName().Version;
            }
        }

        /// <summary>
        /// Gets the ProcessorState, see <see cref="ProcessorState"/>
        /// </summary>
        public ProcessorState ProcessorState { get { return _state; } }

        /// <summary>
        /// Gets the Key Switch Position, see <see cref="ProcessorKeySwitch"/>
        /// </summary>
        public ProcessorKeySwitch KeySwitchPosition { get { return _keySw; } }

        /// <summary>
        /// Gets the Fault State, see <see cref="ProcessorFaultState"/>
        /// </summary>
        public ProcessorFaultState FaultState { get { return _faultState; } }

        /// <summary>
        /// Gets the VendorId (should be 0x01 for Allen-Bradley)
        /// </summary>
        public ushort VendorId { get { return _vendorId; } }

        /// <summary>
        /// Gets the device type (should be 0x0E for PLC)
        /// </summary>
        public ushort DeviceType { get { return _deviceType; } }

        /// <summary>
        /// Gets the Product Code
        /// </summary>
        public ushort ProductCode { get { return _productCode; } }

        /// <summary>
        /// Gets the Major Revision of the Processor
        /// </summary>
        public byte MajorRevision { get { return _majorRevision; } }

        /// <summary>
        /// Gets the Minor Revision of the Processor
        /// </summary>
        public byte MinorRevision { get { return _minorRevision; } }

        /// <summary>
        /// Gets the Serial Number of the Processor
        /// </summary>
        public uint SerialNumber { get { return _serialNumber; } }

        /// <summary>
        /// Gets the Product Name
        /// </summary>
        public string ProductName { get { return _productName; } }
        
        /// <summary>
        /// Gets the Host Name or IP Address associated with this processor
        /// </summary>
        public string HostNameOrIp { get { return _ipAddress; } }

        /// <summary>
        /// Gets a <see cref="bool"/> indicating if the AutoUpdate feature is enabled
        /// </summary>
        /// <remarks>
        /// <para>To enable the AutoUpdate feature, use <see cref="EnableAutoUpdate"/>. To
        /// disable the AutoUpdate feature, use <see cref="DisableAutoUpdate"/>.</para>
        /// </remarks>
        public bool AutoUpdateEnabled
        {
            get { return _autoUpdateEnabled; }
        }

        /// <summary>
        /// Gets the AutoUpdate interval time in Milliseconds
        /// </summary>
        /// <remarks>
        /// <para>To change the time between updates, call <see cref="EnableAutoUpdate"/>
        /// with the new interval time. You can call this function without disabling
        /// the auto update feature first.</para>
        /// </remarks>
        public int AutoUpdateTimeMs
        {
            get { return _autoUpdateTime; }
        }

        /// <summary>
        /// Gets the SessionInfo object (for internal use only)
        /// </summary>
        internal SessionInfo SessionInfo { get { return _session; } }

        /// <summary>
        /// Gets the path to the processor
        /// </summary>
        internal byte[] Path { get { return _path; } }

        internal object SyncRoot { get { return _lockObject; } }

        #endregion

        #region Events

        /// <summary>
        /// Raised when the processor state changes
        /// </summary>
        public event LogixProcessorStateChangedEvent ProcessorStateChanged;
        /// <summary>
        /// Raised when the fault state changes
        /// </summary>
        public event LogixFaultStateChangedEvent FaultStateChanged;
        /// <summary>
        /// Raised when the key switch position changes
        /// </summary>
        public event LogixKeyPositionChangedEvent KeySwitchChanged;

        #endregion

        #region Construction / Deconstruction

        /// <summary>
        /// Creates a new LogixProcessor
        /// </summary>
        /// <param name="HostNameOrIP">Host Name or IP Address</param>
        /// <param name="ProcessorPath">Path to the processor</param>
        public LogixProcessor(string HostNameOrIP, byte[] ProcessorPath)
        {
            _tagGroups = new Dictionary<string, LogixTagGroup>();

            _ipAddress = HostNameOrIP;
            _path = new byte[ProcessorPath.Length + 1];
            _path[0] = 1;
            Buffer.BlockCopy(ProcessorPath, 0, _path, 1, ProcessorPath.Length);

            _tags = new SortedDictionary<string, LogixTag>();
        }

        /// <summary>
        /// Disposes the object, closing the connection if necessary
        /// </summary>
        public void Dispose()
        {
            Disconnect();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Connects to the PLC
        /// </summary>
        /// <returns>True if the connection was successfully made</returns>
        public bool Connect()
        {
            try
            {
                _initializing = true;
                _session = SessionManager.CreateAndRegister(_ipAddress, 0xAF12, System.Text.ASCIIEncoding.ASCII.GetBytes("6DSYSTEM"));

                if (!_session.Connected)
                {
                    Trace.WriteLine(Resources.ErrorStrings.SessionNotEstablished);
                    _errorCode = (int)LogixErrors.SessionNotEstablished;
                    _errorString = Resources.ErrorStrings.SessionNotEstablished;
                    _initializing = false;
                    return false;
                }

                if (!_session.Registered)
                {
                    Trace.WriteLine(Resources.ErrorStrings.SessionNotRegistered + " : " + _session.LastSessionErrorString);
                    _errorCode = (int)LogixErrors.SessionNotRegistered;
                    _errorString = Resources.ErrorStrings.SessionNotRegistered + " : " + _session.LastSessionErrorString;
                    _initializing = false;
                    return false;
                }

                Trace.WriteLine(Resources.ErrorStrings.SessionRegistered);

                if (!SessionManager.VerifyCIP(_session))
                {
                    Trace.WriteLine(Resources.ErrorStrings.CIPNotSupported);
                    _errorCode = (int)LogixErrors.CIPNotSupported;
                    _errorString = Resources.ErrorStrings.CIPNotSupported;
                    SessionManager.UnRegister(_session);
                    _initializing = false;
                    return false;
                }

                Random rnd = new Random();
                _session.SetConnectionParameters((ushort)rnd.Next(0, ushort.MaxValue),
                    1800000, (uint)rnd.Next(0, int.MaxValue), 0xFACE, 0xEFFEC);
                _session.MillisecondTimeout = 2000;

                if (!ConnectionManager.ConnectOverControlNet(_session, _path, 0))//0x43F4))
                {
                    _errorCode = (int)LogixErrors.ProcessorNotConnected;
                    _errorString = Resources.ErrorStrings.ProcessorNotConnected;
                    Trace.WriteLine(Resources.ErrorStrings.ProcessorNotConnected);
                    SessionManager.UnRegister(_session);
                    _initializing = false;
                    return false;
                }

                //The connection times out after some time, so we have to make sure to
                //get an event when that happens so we can reconnect...
                _lastOperationTime = DateTime.Now;

                _session.OnSocketError = new EIPNET.EIP.SocketErrorCallback(SocketErrorCallback);
                _session.OnPreOperation = new EIPNET.EIP.PreOperationCallback(PreOperationCallback);
                _session.OnPostOperation = new EIPNET.EIP.PostOperationCallback(PostOperationCallback);

                _stateEventsEnabled = false;
                UpdateState();
                _stateEventsEnabled = true;

                //_stateTimer = new Timer(new TimerCallback(UpdateStateTimer), null, 1000, 1000);
                _stateThread = new Thread(new ThreadStart(StateUpdateThread));
                _stateThread.IsBackground = true;
                _stateThread.Name = _ipAddress + " State Update Thread";
                _stateThread.Start();

                _initializing = false;
                return true;
            }
            catch (Exception e)
            {
                _initializing = false;
                return false;
            }
        }

        /// <summary>
        /// Disconnects from the PLC
        /// </summary>
        /// <returns>True if the connection was successfully closed</returns>
        public bool Disconnect()
        {
            //lock (_lockObject)
            {
                //Stop the state timer...
                if (_stateUpdateEnabled)
                {
                    _stateUpdateEnabled = false;
                    try
                    {
                        _stateThread.Abort();
                        _stateThread = null;
                    }
                    catch { }
                }

                DisableAutoUpdate();

                if (_session != null && _session.Connected)
                {
                    ConnectionManager.ForwardClose(_session, _path);
                    SessionManager.UnRegister(_session);
                }

            }

            return true;
        }

        /// <summary>
        /// Reads a single tag
        /// </summary>
        /// <param name="tag">Tag to read</param>
        /// <returns>True if the read was successful</returns>
        /// <remarks>This is not the preferred method of updating tag information. If you have
        /// multiple tags that you want to update, use the <see cref="UpdateGroup"/> method.</remarks>
        public bool ReadTag(ITag tag)
        {
            lock (_lockObject)
            {
                LogixTag lgxTag = tag as LogixTag;

                if (lgxTag == null)
                    throw new ArgumentException(Resources.ErrorStrings.IncorrectArgTagType, "tag");

                ReadDataServiceReply lgxRead = LogixServices.ReadLogixData(_session, lgxTag.Address, (ushort)lgxTag.Elements);

                if (lgxRead == null || lgxRead.Data == null)
                {
                    if (lgxRead != null)
                        lgxTag.SetTagError(lgxRead.ByteStatus);

                    lgxTag.LastError = Resources.ErrorStrings.TagNotFound + _ipAddress;
                    lgxTag.LastErrorNumber = (int)LogixErrors.TagNotFound;
                    return false;
                }

                lgxTag.SetTagError(lgxRead.ByteStatus);
                CIPType tagType = (CIPType)lgxRead.DataType;
                byte[] temp = new byte[lgxRead.Data.Length + 2];
                Buffer.BlockCopy(BitConverter.GetBytes(lgxRead.DataType), 0, temp, 0, 2);
                Buffer.BlockCopy(lgxRead.Data, 0, temp, 2, lgxRead.Data.Length);

                lgxTag.UpdateValue(temp);

                uint offset = (uint)lgxRead.Data.Length;

                if (lgxRead.Status == 0x06)
                {
                    //We are going to have to request more data...
                    while (lgxRead.Status == 0x06)
                    {
                        lgxRead = LogixServices.ReadLogixDataFragmented(_session, lgxTag.Address, (ushort)lgxTag.Elements, offset);
                        lgxTag.SetTagError(lgxRead.ByteStatus);

                        tagType = (CIPType)lgxRead.DataType;
                        temp = new byte[lgxRead.Data.Length + 2];
                        Buffer.BlockCopy(BitConverter.GetBytes(lgxRead.DataType), 0, temp, 0, 2);
                        Buffer.BlockCopy(lgxRead.Data, 0, temp, 2, lgxRead.Data.Length);

                        lgxTag.UpdateValue(temp, offset);

                        offset += (uint)lgxRead.Data.Length;
                    }
                }
            }       //End Lock

            return true;
        }

        /// <summary>
        /// Writes a single tag
        /// </summary>
        /// <param name="tag">Tag to write</param>
        /// <returns>True if the write was successful</returns>
        /// <remarks>This is not the preferred method of updating tag information. If you have
        /// multiple tags that you want to update, use the <see cref="UpdateGroups"/> method.</remarks>
        public bool WriteTag(ITag tag)
        {
            lock (_lockObject)
            {
                LogixTag lgxTag = tag as LogixTag;

                if (lgxTag == null)
                    throw new ArgumentException(Resources.ErrorStrings.IncorrectArgTagType, "tag");

                WriteDataServiceReply lgxWrite = LogixServices.WriteLogixData(_session, lgxTag.Address, lgxTag.DataType, (ushort)lgxTag.Elements, lgxTag.GetWriteData(), lgxTag.StructHandle);

                if (lgxWrite == null)
                    return false;

                if (lgxWrite.Status == 0x00)
                {
                    lgxTag.ClearPendingWrite();
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Writes a tag to the PLC then reads it back
        /// </summary>
        /// <param name="tag">Tag to write</param>
        /// <returns>True if both the read and write were successful</returns>
        public bool WriteRead(ITag tag)
        {
            bool retVal = true;
            retVal &= WriteTag(tag);
            retVal &= ReadTag(tag);
            return retVal;
        }

        /// <summary>
        /// Updates (reads/writes) all the tags in the active groups
        /// </summary>
        public void UpdateGroups()
        {
            lock (_lockObject)
            {
                foreach (LogixTagGroup group in _tagGroups.Values)
                {
                    if (group.Enabled)
                        group.UpdateGroup();
                }
            }
        }

        /// <summary>
        /// Creates a LogixTagGroup on this processor
        /// </summary>
        /// <param name="GroupName">Name of the group to create</param>
        /// <returns><see cref="LogixTagGroup"/> with the specified group name</returns>
        /// <exception cref="GroupAlreadyExistsException">Raised when trying to add a group that already exists</exception>
        public LogixTagGroup CreateTagGroup(string GroupName)
        {
            //First verify that the group doesn't exist
            if (_tagGroups.ContainsKey(GroupName))
                throw new GroupAlreadyExistsException(Resources.ErrorStrings.GroupExists);

            LogixTagGroup newGroup = new LogixTagGroup(this, GroupName);
            _tagGroups.Add(GroupName, newGroup);

            return newGroup;
        }

        /// <summary>
        /// Sets the processor to be in the RUN mode
        /// </summary>
        /// <remarks>The keyswitch must be in the REM position for this to work.</remarks>
        public void SetRunMode()
        {
            //lock (_lockObject)
            {
                CommonPacket cpf = new CommonPacket();
                cpf.AddressItem = CommonPacketItem.GetConnectedAddressItem(_session.ConnectionParameters.O2T_CID);

                byte[] request = new byte[] { 0x06, 0x02, 0x20, 0x8E, 0x24, 0x01 };

                cpf.DataItem = CommonPacketItem.GetConnectedDataItem(request, SequenceNumberGenerator.SequenceNumber);

                _session.SendUnitData(cpf.AddressItem, cpf.DataItem);
            }
        }

        /// <summary>
        /// Sets the processor to be in the PROGRAM mode
        /// </summary>
        /// <remarks>The keyswitch must be in the REM position for this to work.</remarks>
        public void SetProgramMode()
        {
            //lock (_lockObject)
            {
                CommonPacket cpf = new CommonPacket();
                cpf.AddressItem = CommonPacketItem.GetConnectedAddressItem(_session.ConnectionParameters.O2T_CID);

                byte[] request = new byte[] { 0x07, 0x02, 0x20, 0x8E, 0x24, 0x01 };

                cpf.DataItem = CommonPacketItem.GetConnectedDataItem(request, SequenceNumberGenerator.SequenceNumber);

                _session.SendUnitData(cpf.AddressItem, cpf.DataItem);
            }
        }

        /// <summary>
        /// Gets the tag group with the specified name
        /// </summary>
        /// <param name="GroupName">Name of the group to get. This is case sensitive.</param>
        /// <returns>LogixTagGroup reference</returns>
        /// <exception cref="GroupNotFoundException">Thrown when the requested group is not found.</exception>
        public LogixTagGroup GetTagGroup(string GroupName)
        {
            LogixTagGroup retGroup = null;

            if (!_tagGroups.TryGetValue(GroupName, out retGroup))
                throw new GroupNotFoundException(Resources.ErrorStrings.GroupNotFound);

            return retGroup;
        }

        /// <summary>
        /// Gets a list of all the tags on the processor
        /// </summary>
        /// <returns></returns>
        public List<LogixTagInfo> EnumerateTags()
        {
            return GetTagInfo();
        }

        /// <summary>
        /// Get information about a tag
        /// </summary>
        /// <param name="Address">Address of the tag</param>
        /// <returns>LogixTagInfo object or null if the tag was not found</returns>
        public LogixTagInfo GetTagInformation(string Address)
        {
            List<LogixTagInfo> tagInfo = EnumerateTags();

            if (tagInfo != null && tagInfo.Count > 0)
            {
                LogixTagInfo retTag = tagInfo.Find(t => (string.Compare(t.TagName, Address, true) == 0));
                if (retTag == null)
                    return null;
                if (retTag.TagName != Address)
                    return null;
                return retTag;
            }

            return null;
        }

        /// <summary>
        /// Returns a string representation of the object
        /// </summary>
        /// <returns>String of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ControlLogix Processor");
            sb.AppendLine("\tIP Address     : " + _ipAddress.ToString());
            sb.AppendLine("\tConnected      : " + (_session.Connected ? "Yes" : "No"));
            sb.AppendLine("\tProcessor      : " + _productName);
            sb.AppendLine("\tFault State    : " + _faultState.ToString());
            sb.AppendLine("\tKey Position   : " + _keySw.ToString());
            sb.AppendLine("\tProcessor State: " + _state.ToString());

            return sb.ToString();
        }

        /// <summary>
        /// Enables or changes the AutoUpdate feature
        /// </summary>
        /// <remarks>
        /// <para>The AutoUpdate feature will automatically write/read all active tag
        /// groups on the processor. Calling this function with the AutoUpdate feature
        /// already active will change the time between updates. The actual time that
        /// it takes to update the groups is dependant upon how many tags are in the
        /// group and how many writes are active.</para>
        /// <para>To disable the AutoUpdate feature, call the <see cref="DisableAutoUpdate"/>
        /// method.</para>
        /// </remarks>
        /// <param name="UpdateRateMs">Milliseconds between updates</param>
        public void EnableAutoUpdate(int UpdateRateMs)
        {
            _autoUpdateEnabled = true;
            _autoUpdateTime = UpdateRateMs;

            if (_autoUpdateThread == null)
            {
                _autoUpdateThread = new Thread(new ThreadStart(AutoUpdateThread));
                _autoUpdateThread.Name = _ipAddress + " Group Update Thread";
                _autoUpdateThread.IsBackground = true;
                _autoUpdateThread.Start();
            }
        }

        /// <summary>
        /// Disables the AutoUpdate feature
        /// </summary>
        public void DisableAutoUpdate()
        {
            if (!_autoUpdateEnabled)
                return;

            try
            {
                _autoUpdateEnabled = false;
                _autoUpdateThread.Abort();
                _autoUpdateThread = null;
            }
            catch { }

        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Adds a tag to the processor
        /// </summary>
        /// <param name="tag">Tag to add (must be a LogixTag)</param>
        /// <returns>True if the tag was successfully added</returns>
        /// <remarks>This is used by the tag service to verify the
        /// tag actually exists.</remarks>
        internal bool AddTag(LogixTag tag)
        {
            //Here is the procedure for adding a tag:
            //1. Run some verification tests on the tag first
            //2. First create a read request for a single tag
            //3. Send the request to the PLC
            //4. Verify the response and data type
            //5. Store it in the tag

            LogixTag lgxTag = tag as LogixTag;

            if (lgxTag == null)
                throw new ArgumentException(Resources.ErrorStrings.IncorrectArgTagType, "tag");

            if (!ReadTag(lgxTag))
                return false;

            return true;
        }

        /// <summary>
        /// Returns the structure handle for a particular tag
        /// </summary>
        /// <param name="tagName">Name of the tag</param>
        /// <returns>Structure handle or 0 if the tag wasn't found...</returns>
        internal ushort GetStructureHandle(string tagName)
        {
            bool refreshed = false;

            //lock (_lockObject)
            {
                //First we find the tag information..
                if (_tagInfo == null)
                {
                    RefreshInternalInfo();
                    refreshed = true;
                }

                LogixTagInfo ti = GetTagInfo(tagName);

                if (ti == null && !refreshed)
                {
                    RefreshInternalInfo();
                    ti = GetTagInfo(tagName);
                }

                if (ti == null)
                    return 0;
                else
                    return (ushort)(ti.FullTypeInfo & 0x7FFF);
            }

        }

        /// <summary>
        /// Gets information for the specified tag
        /// </summary>
        /// <param name="tagName">Name of the tag</param>
        /// <returns>LogixTagInfo or null if not found</returns>
        internal LogixTagInfo GetInfoForTag(string tagName)
        {
            bool refreshed = false;

            //lock (_lockObject)
            {
                //First we find the tag information..
                if (_tagInfo == null)
                {
                    RefreshInternalInfo();
                    refreshed = true;
                }

                LogixTagInfo ti = GetTagInfo(tagName);

                if (ti == null && !refreshed)
                {
                    RefreshInternalInfo();
                    ti = GetTagInfo(tagName);
                }

                return ti;
            }
        }

        #endregion

        #region Private Methods

        private LogixTagInfo GetTagInfo(string tagName)
        {
            for (int i = 0; i < _tagInfo.Count; i++)
            {
                if (_tagInfo[i].TagName.ToLower() == tagName.ToLower())
                    return _tagInfo[i];
            }

            return null;
        }

        private void RefreshInternalInfo()
        {
            _tagInfo = GetTagInfo();
        }

        private void PreOperationCallback()
        {
            if (_initializing)
                return;

            //lock (_lockObject)
            {
                if (_lastOperationTime.AddMilliseconds(60000) < DateTime.Now)
                {
                    //We have to forward open again...
                    _initializing = true;
                    ConnectionManager.ConnectOverControlNet(_session, _path, 0);//0x43F4);
                    LogixTag fakeTag = new ControlLogixNET.LogixType.LogixDINT("Program:MainProgram", this);
                    _initializing = false;
                }
            }


            _lastOperationTime = DateTime.Now;
        }

        private void PostOperationCallback()
        {

        }

        private void SocketErrorCallback(SocketException se)
        {
            _errorCode = se.NativeErrorCode;
            _errorString = Resources.ErrorStrings.SocketError;
        }

        private void UpdateStateTimer(object state)
        {
            //lock (_lockObject)
            {
                UpdateState();
            }
        }

        private void UpdateState()
        {
            EncapsRRData rrData = new EncapsRRData();
            rrData.CPF = new CommonPacket();
            rrData.CPF.AddressItem = CommonPacketItem.GetNullAddressItem();

            UnconnectedSend ucmm = new UnconnectedSend();
            ucmm.RequestPath = CommonPaths.ConnectionManager;
            ucmm.RoutePath = _path;
            ucmm.Priority_TimeTick = _session.ConnectionParameters.PriorityAndTick;
            ucmm.Timeout_Ticks = _session.ConnectionParameters.ConnectionTimeoutTicks;
            ucmm.MessageRequest = new MR_Request();
            ucmm.MessageRequest.Service = 0x03;
            ucmm.MessageRequest.Request_Path = CommonPaths.IdentityObject;
            ucmm.MessageRequest.Request_Data = new byte[] { 0x07, 0x00, 0x01, 0x00, 0x02, 0x00, 0x03, 0x00, 0x04, 0x00, 0x05, 0x00, 0x06, 0x00, 0x07, 0x00 };
            rrData.CPF.DataItem = CommonPacketItem.GetUnconnectedDataItem(ucmm.Pack());

            EncapsReply reply = _session.SendRRData(rrData.CPF.AddressItem, rrData.CPF.DataItem);

            if (reply == null)
                return;

            if (reply.Status == 0)
            {
                UnconnectedSendReply ucReply = reply;

                if (ucReply.GeneralStatus == 0x00)
                {
                    UnconnectedSendReply_Success succReply = ucReply as UnconnectedSendReply_Success;
                    DecodeAttributeReply(succReply.ServiceResponse);
                }
                else
                {
                    //Failed, for some reason...
                    //TODO: Decipher the code and add it to the error property
                }
            }

        }

        private List<LogixTagInfo> GetTagInfo()
        {
            CommonPacketItem addressItem = CommonPacketItem.GetNullAddressItem();

            UnconnectedSend ucmm = new UnconnectedSend();
            ucmm.RequestPath = CommonPaths.ConnectionManager;
            ucmm.RoutePath = _path;
            ucmm.Priority_TimeTick = _session.ConnectionParameters.PriorityAndTick;
            ucmm.Timeout_Ticks = _session.ConnectionParameters.ConnectionTimeoutTicks;

            ucmm.MessageRequest = new MR_Request();
            ucmm.MessageRequest.Service = 0x55;
            ucmm.MessageRequest.Request_Path = new byte[] { 0x20, 0x6B, 0x25, 0x00, 0x00, 0x00 };       //Last 2 bytes are the offset...
            ucmm.MessageRequest.Request_Data = new byte[] { 0x04, 0x00, 0x02, 0x00, 0x07, 0x00, 0x08, 0x00, 0x01, 0x00 };

            CommonPacketItem dataItem = CommonPacketItem.GetUnconnectedDataItem(ucmm.Pack());

            EncapsReply reply = _session.SendRRData(addressItem, dataItem);

            List<LogixTagInfo> retVal = new List<LogixTagInfo>();

            if (reply.Status == 0)
            {
                UnconnectedSendReply ucReply = reply;

                if (ucReply.GeneralStatus == 0x00 || ucReply.GeneralStatus == 0x06)
                {
                    UnconnectedSendReply_Success succReply = ucReply as UnconnectedSendReply_Success;
                    List<byte> replyBytes = new List<byte>();
                    uint lastOffset = 0x0000;

                    retVal.AddRange(DecodeTagInfo(succReply.ServiceResponse, out lastOffset));

                    while (ucReply.GeneralStatus == 0x06)
                    {
                        lastOffset += 1;
                        if (lastOffset <= 0xFFFF)
                        {
                            Buffer.BlockCopy(BitConverter.GetBytes((ushort)lastOffset), 0, ucmm.MessageRequest.Request_Path, 4, 2);
                        }
                        else
                        {
                            byte[] tempPath = new byte[8];
                            Buffer.BlockCopy(ucmm.MessageRequest.Request_Path, 0, tempPath, 0, 6);
                            Buffer.BlockCopy(BitConverter.GetBytes(lastOffset), 0, tempPath, 4, 4);
                            ucmm.MessageRequest.Request_Path = tempPath;
                        }

                        dataItem = CommonPacketItem.GetUnconnectedDataItem(ucmm.Pack());

                        reply = _session.SendRRData(addressItem, dataItem);

                        if (reply.Status == 0x00)
                        {
                            ucReply = reply;

                            if (ucReply.GeneralStatus == 0x00 || ucReply.GeneralStatus == 0x06)
                            {
                                succReply = ucReply as UnconnectedSendReply_Success;
                                retVal.AddRange(DecodeTagInfo(succReply.ServiceResponse, out lastOffset));
                            }
                        }
                    }
                }
            }

            return retVal;
        }

        private List<LogixTagInfo> DecodeTagInfo(byte[] data, out uint lastOffset)
        {
            List<LogixTagInfo> retVal = new List<LogixTagInfo>();
            lastOffset = 0;
            for (int i = 0; i < data.Length; i++)
            {
                LogixTagInfo info = new LogixTagInfo();
                lastOffset = BitConverter.ToUInt32(data, i);
                i += 4;
                info.MemoryAddress = lastOffset;
                //Next two bytes are type code and dimension information
                ushort typeInfo = BitConverter.ToUInt16(data, i);
                i += 2;
                info.FullTypeInfo = typeInfo;
                //Need to decode the type info...
                info.DataType = (ushort)(typeInfo & 0x00FF);
                if ((ushort)(typeInfo & 0x8000) == 0x8000)
                    info.DataType = (ushort)(info.DataType | 0x8000);
                int dimInfo = (typeInfo & 0x6000) >> 13;
                info.Dimensions = (ushort)dimInfo;

                //Skip 2 bytes
                i += 2;
                if (i > data.Length)
                    continue;

                //Next 3 sets of 4 bytes are the dimension sizes
                info.Dimension1Size = BitConverter.ToUInt32(data, i);
                i += 4;
                if (i > data.Length)
                    continue;
                info.Dimension2Size = BitConverter.ToUInt32(data, i);
                i += 4;
                if (i > data.Length)
                    continue;
                info.Dimension3Size = BitConverter.ToUInt32(data, i);
                i += 4;
                if (i > data.Length)
                    continue;

                //Next 2 bytes are the length of the name...
                int strSize = BitConverter.ToUInt16(data, i);
                i += 2;
                if (i > data.Length)
                    continue;
                if (i + strSize > data.Length)
                    continue;
                info.TagName = System.Text.ASCIIEncoding.ASCII.GetString(data, i, strSize);
                i += strSize - 1;
                retVal.Add(info);
            }

            return retVal;
        }

        private void DecodeAttributeReply(byte[] data)
        {
            //Ok, first is the number of attributes...
            ushort numAttrs = BitConverter.ToUInt16(data, 0);

            if (numAttrs != 0x07)
            {
                return;     //Error, not enough attributes returned
            }
            int offset = 2;

            //The format of the reply seems to be:
            //[2:AttributeId][Variable Data]

            for (int i = 0; i < 7; i++)
            {
                //Read 2 bytes for the Attribute Id:
                ushort attribId = BitConverter.ToUInt16(data, offset);
                offset += 2;

                switch (attribId)
                {
                    case 0x01:      //VendorId
                        //Returns 2 words for some reason, data is in the lower
                        //word...
                        offset += 2;
                        _vendorId = BitConverter.ToUInt16(data, offset);
                        offset += 2;
                        break;
                    case 0x02:      //Device Type
                        //Also 2 word reply...
                        offset += 2;
                        _deviceType = BitConverter.ToUInt16(data, offset);
                        offset += 2;
                        break;
                    case 0x03:      //Product Code
                        //Again 2 words...
                        offset += 2;
                        _productCode = BitConverter.ToUInt16(data, offset);
                        offset += 2;
                        break;
                    case 0x04:      //Revision
                        //Another 2 words
                        offset += 2;
                        _majorRevision = data[offset];
                        _minorRevision = data[offset + 1];
                        offset += 2;
                        break;
                    case 0x05:      //Status
                        //2 words
                        offset += 2;
                        //This gets a little complicated here...
                        //State is stored in the upper nibble of the byte...
                        ProcessorState oldState = _state;
                        _state = (ProcessorState)(byte)((data[offset] & 0xF0) >> 4);
                        offset += 1;
                        //Fault state is in the lower nibble
                        ProcessorFaultState oldFault = _faultState;
                        _faultState = (ProcessorFaultState)(byte)((data[offset] & 0x0F));
                        //Keyswitch position is in the upper nibble
                        ProcessorKeySwitch oldKey = _keySw;
                        _keySw = (ProcessorKeySwitch)(byte)((data[offset] & 0xF0) >> 4);
                        offset += 1;

                        //Take care of events
                        if (_stateEventsEnabled)
                        {
                            if (oldState != _state)
                                RaiseProcessorStateChange(oldState, _state);

                            if (oldFault != _faultState)
                                RaiseFaultStateChange(oldFault, _faultState);

                            if (oldKey != _keySw)
                                RaiseKeySwitchChange(oldKey, _keySw);
                        }

                        break;
                    case 0x06:      //Serial Number
                        //This returns 3 words, data is in the lower 2...
                        offset += 2;
                        _serialNumber = BitConverter.ToUInt32(data, offset);
                        offset += 4;
                        break;
                    case 0x07:      //Product Name
                        //Skip one word
                        offset += 2;
                        //Get the size of the string...
                        byte sSize = data[offset];
                        offset += 1;
                        _productName = System.Text.ASCIIEncoding.ASCII.GetString(data, offset, sSize);
                        offset += sSize;
                        break;
                    default:
                        break;      //This is an error
                }
            }
        }

        private void RaiseProcessorStateChange(ProcessorState oldState, ProcessorState newState)
        {
            if (ProcessorStateChanged != null)
                ProcessorStateChanged(this, new LogixProcessorStateChangedEventArgs(oldState, newState));
        }

        private void RaiseFaultStateChange(ProcessorFaultState oldState, ProcessorFaultState newState)
        {
            if (FaultStateChanged != null)
                FaultStateChanged(this, new LogixFaultStateChangedEventArgs(oldState, newState));
        }

        private void RaiseKeySwitchChange(ProcessorKeySwitch oldState, ProcessorKeySwitch newState)
        {
            if (KeySwitchChanged != null)
                KeySwitchChanged(this, new LogixKeyChangedEventArgs(oldState, newState));
        }

        private void AutoUpdateTimer(object state)
        {
            _autoUpdateTimer.Change(0, _autoUpdateTime);        //Disable the timer so we can't pile up on long operations
            UpdateGroups();
            _autoUpdateTimer.Change(_autoUpdateTime, _autoUpdateTime);
        }

        private void AutoUpdateThread()
        {
            bool quitFlag = false;

            while (!quitFlag)
            {
                quitFlag = Thread.CurrentThread.ThreadState == System.Threading.ThreadState.AbortRequested ||
                           Thread.CurrentThread.ThreadState == System.Threading.ThreadState.StopRequested ||
                           !_autoUpdateEnabled;

                if (quitFlag)
                    continue;

                //lock (_lockObject)
                {
                    foreach (LogixTagGroup group in _tagGroups.Values)
                    {
                        if (group.Enabled)
                            group.UpdateGroup();
                    }
                }

                Thread.Sleep(_autoUpdateTime);
            }
        }

        private void StateUpdateThread()
        {
            bool quitFlag = false;

            while (!quitFlag)
            {
                quitFlag = Thread.CurrentThread.ThreadState == System.Threading.ThreadState.AbortRequested ||
                           Thread.CurrentThread.ThreadState == System.Threading.ThreadState.StopRequested ||
                           !_stateUpdateEnabled;

                if (quitFlag)
                    continue;

                //lock (_lockObject)
                {
                    //UpdateState();
                }

                Thread.Sleep(1000);
            }
        }
            
        #endregion

    }

    #region Events

    /// <summary>
    /// LogixKeyPositionChangedEvent
    /// </summary>
    /// <param name="sender">Processor raising the event</param>
    /// <param name="e">Event Arguments</param>
    public delegate void LogixKeyPositionChangedEvent(LogixProcessor sender, LogixKeyChangedEventArgs e);
    /// <summary>
    /// Event Arguments for the <see cref="LogixKeyPositionChangedEvent"/>
    /// </summary>
    public class LogixKeyChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the previous key position
        /// </summary>
        public ProcessorKeySwitch OldPosition { get; private set; }
        /// <summary>
        /// Gets the current key position
        /// </summary>
        public ProcessorKeySwitch NewPosition { get; private set; }

        /// <summary>
        /// Creates a new LogixKeyChangedEventArgs object
        /// </summary>
        /// <param name="oldPos">Old Key Position</param>
        /// <param name="newPos">New Key Position</param>
        internal LogixKeyChangedEventArgs(ProcessorKeySwitch oldPos, ProcessorKeySwitch newPos)
        {
            OldPosition = oldPos;
            NewPosition = newPos;
        }
    }

    /// <summary>
    /// LogixFaultStateChangedEvent
    /// </summary>
    /// <param name="sender">Processor raising the event</param>
    /// <param name="e">Event Arguments</param>
    public delegate void LogixFaultStateChangedEvent(LogixProcessor sender, LogixFaultStateChangedEventArgs e);
    /// <summary>
    /// Event Arguments for the <see cref="LogixFaultStateChangedEvent"/>
    /// </summary>
    public class LogixFaultStateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the old fault state
        /// </summary>
        public ProcessorFaultState OldState { get; private set; }
        /// <summary>
        /// Gets the new fault state
        /// </summary>
        public ProcessorFaultState NewState { get; private set; }

        /// <summary>
        /// Creates a new LogixFaultStateChangedEventArgs object
        /// </summary>
        /// <param name="oldFault">Old Fault State</param>
        /// <param name="newFault">New Fault State</param>
        internal LogixFaultStateChangedEventArgs(ProcessorFaultState oldFault, ProcessorFaultState newFault)
        {
            OldState = oldFault;
            NewState = newFault;
        }
    }

    /// <summary>
    /// LogixProcessorStateChangedEvent
    /// </summary>
    /// <param name="sender">Processor raising the event</param>
    /// <param name="e">Event Arguments</param>
    public delegate void LogixProcessorStateChangedEvent(LogixProcessor sender, LogixProcessorStateChangedEventArgs e);
    /// <summary>
    /// Event Arguments for the <see cref="LogixProcessorStateChangedEvent"/>
    /// </summary>
    public class LogixProcessorStateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the previous processor state
        /// </summary>
        public ProcessorState OldState { get; private set; }
        /// <summary>
        /// Gets the current processor state
        /// </summary>
        public ProcessorState NewState { get; private set; }

        internal LogixProcessorStateChangedEventArgs(ProcessorState oldState, ProcessorState newState)
        {
            OldState = oldState;
            NewState = newState;
        }
    }

    #endregion
}
