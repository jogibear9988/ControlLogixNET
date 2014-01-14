using System;
using System.Collections.Generic;
using System.Text;
using EIPNET.CIP;
using EIPNET.EIP;
using System.Runtime.CompilerServices;

namespace ControlLogixNET.LogixType
{

    /// <summary>
    /// This namespace contains information about specific tag types and utilities to manipulate them
    /// </summary>
    [CompilerGenerated]
    class NamespaceDoc { }

    /// <summary>
    /// Built-In Logix Data Types
    /// </summary>
    /// <remarks>
    /// This is as of Revision 16
    /// </remarks>
    public enum LogixTypes : ushort
    {
        /// <summary>
        /// Unknown type
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Logix String
        /// </summary>
        /// <remarks>[1:Len][(82):Char]</remarks>
        String,
        /*
        /// <summary>
        /// Alarm Information
        /// </summary>
        Alarm,
        /// <summary>
        /// Analog Alarm
        /// </summary>
        Alarm_Analog,
        /// <summary>
        /// Digital Alarm
        /// </summary>
        Alarm_Digital,
        /// <summary>
        /// Consumed Axis
        /// </summary>
        Axis_Consumed,
        /// <summary>
        /// Generic Axis
        /// </summary>
        Axis_Generic,
        /// <summary>
        /// Generic Drive Axis
        /// </summary>
        Axis_Generic_Drive,
        /// <summary>
        /// Generic Servo
        /// </summary>
        Axis_Servo,
        /// <summary>
        /// Generic Servo Drive
        /// </summary>
        Axis_Servo_Drive,
        /// <summary>
        /// Virtual Axis
        /// </summary>
        Axis_Virtual,
        */
        /// <summary>
        /// Boolean Value
        /// </summary>
        Bool,
        /*
        /// <summary>
        /// Cam
        /// </summary>
        Cam,
        /// <summary>
        /// Cam Profile
        /// </summary>
        Cam_Profile,
        /// <summary>
        /// Connection Status
        /// </summary>
        Connection_Status,
        */
        /// <summary>
        /// Control
        /// </summary>
        Control,
        /*
        /// <summary>
        /// Coordinate System
        /// </summary>
        Coordinate_System,
        */
        /// <summary>
        /// Counter
        /// </summary>
        Counter,
        /*
        /// <summary>
        /// DeadTime
        /// </summary>
        Deadtime,
        /// <summary>
        /// Derivative
        /// </summary>
        Derivative,
        */
        /// <summary>
        /// Double Integer
        /// </summary>
        DInt,
        /*
        /// <summary>
        /// Discrete 2 State
        /// </summary>
        Discrete_2State,
        /// <summary>
        /// Discrete 3 State
        /// </summary>
        Discrete_3State,
        /// <summary>
        /// Discrete Input
        /// </summary>
        Diverse_Input,
        /// <summary>
        /// Dominant Reset
        /// </summary>
        Dominant_Reset,
        /// <summary>
        /// Dominant Set
        /// </summary>
        Dominant_Set,
        /// <summary>
        /// Emergency Stop
        /// </summary>
        Emergency_Stop,
        /// <summary>
        /// Enable Pendant
        /// </summary>
        Enable_Pendant,
        /// <summary>
        /// External Routine Control
        /// </summary>
        Ext_Routine_Control,
        /// <summary>
        /// External Routine Parameters
        /// </summary>
        Ext_Routine_Parameters,
        /// <summary>
        /// (Function Block) Bit Field Distribute
        /// </summary>
        FBD_Bit_Field_Distribute,
        /// <summary>
        /// (Function Block) Boolean And
        /// </summary>
        FBD_Boolean_And,
        /// <summary>
        /// (Function Block) Boolean Not
        /// </summary>
        FBD_Boolean_Not,
        /// <summary>
        /// (Function Block) Boolean Or
        /// </summary>
        FBD_Boolean_Or,
        /// <summary>
        /// (Function Block) Boolean XOr
        /// </summary>
        FBD_Boolean_XOr,
        /// <summary>
        /// (Function Block) Compare
        /// </summary>
        FBD_Compare,
        /// <summary>
        /// (Function Block) Convert
        /// </summary>
        FBD_Convert,
        /// <summary>
        /// (Function Block) Counter
        /// </summary>
        FBD_Counter,
        /// <summary>
        /// (Function Block) Limit
        /// </summary>
        FBD_Limit,
        /// <summary>
        /// (Function Block) Logical
        /// </summary>
        FBD_Logical,
        /// <summary>
        /// (Function Block) Masked Move
        /// </summary>
        FBD_Masked_Move,
        /// <summary>
        /// (Function Block) Mask Equal
        /// </summary>
        FBD_Mask_Equal,
        /// <summary>
        /// (Function Block) Math
        /// </summary>
        FBD_Math,
        /// <summary>
        /// (Function Block) Math (Advanced)
        /// </summary>
        FBD_Math_Advanced,
        /// <summary>
        /// (Function Block) One Shot
        /// </summary>
        FBD_OneShot,
        /// <summary>
        /// (Function Block) Timer
        /// </summary>
        FBD_Timer,
        /// <summary>
        /// (Function Block) Truncate
        /// </summary>
        FBD_Truncate,
        /// <summary>
        /// High Pass Filter
        /// </summary>
        Filter_High_Pass,
        /// <summary>
        /// Low Pass Filter
        /// </summary>
        Filter_Low_Pass,
        /// <summary>
        /// Notch Filter
        /// </summary>
        Filter_Notch,
        /// <summary>
        /// Five Position Mode Selector
        /// </summary>
        Five_Pos_Mode_Selector,
        /// <summary>
        /// Flip Flop (D)
        /// </summary>
        Flip_Flop_D,
        /// <summary>
        /// Flip Flop (JK)
        /// </summary>
        Flip_Flop_JK,
        /// <summary>
        /// Function Generator
        /// </summary>
        Function_Generator,
        /// <summary>
        /// High-Low Limit
        /// </summary>
        HL_Limit,
        */
        /// <summary>
        /// Integer
        /// </summary>
        Int,
        /*
        /// <summary>
        /// Integrator
        /// </summary>
        Integrator,
        /// <summary>
        /// Lead/Lag
        /// </summary>
        Lead_Lag,
        /// <summary>
        /// Lead/Lag Second Order
        /// </summary>
        Lead_Lag_Sec_Order,
        /// <summary>
        /// Light Curtain
        /// </summary>
        Light_Curtain,
        */
        /// <summary>
        /// Long Integer
        /// </summary>
        LInt,
        /*
        /// <summary>
        /// Maximum Capture
        /// </summary>
        Maximum_Capture,
        /// <summary>
        /// Message
        /// </summary>
        Message,
        /// <summary>
        /// Minimum Capture
        /// </summary>
        Minimum_Capture,
        /// <summary>
        /// Motion Group
        /// </summary>
        Motion_Group,
        /// <summary>
        /// Motion Instruction
        /// </summary>
        Motion_Instruction,
        /// <summary>
        /// Moving Average
        /// </summary>
        Moving_Average,
        /// <summary>
        /// Moving Standard Deviation
        /// </summary>
        Moving_Std_Dev,
        /// <summary>
        /// Multiplexor
        /// </summary>
        Multiplexer,
        /// <summary>
        /// Output Cam
        /// </summary>
        Output_Cam,
        /// <summary>
        /// Output Compensation
        /// </summary>
        Output_Compensation,
        /// <summary>
        /// Phase
        /// </summary>
        Phase,
        /// <summary>
        /// Phase Compensation
        /// </summary>
        Phase_Compensation,
        /// <summary>
        /// Proportional-Integral-Derivative
        /// </summary>
        PID,
        /// <summary>
        /// PID Auto-Tune
        /// </summary>
        PIDE_AutoTune,
        /// <summary>
        /// Proportional-Integral-Deriviative Enhanced
        /// </summary>
        PID_Enhanced,
        /// <summary>
        /// Position Property
        /// </summary>
        Position_Prop,
        /// <summary>
        /// Prop Int
        /// </summary>
        Prop_Int,
        /// <summary>
        /// Pulse Multiplier
        /// </summary>
        Pulse_Multiplier,
        /// <summary>
        /// Ramp/Soak
        /// </summary>
        Ramp_Soak,
        /// <summary>
        /// Rate Limiter
        /// </summary>
        Rate_Limiter,
        */
        /// <summary>
        /// Real (Single Precision Float)
        /// </summary>
        Real,
        /*
        /// <summary>
        /// Redundant Input
        /// </summary>
        Redundant_Input,
        /// <summary>
        /// Redundant Output
        /// </summary>
        Redundant_Output,
        /// <summary>
        /// Scale
        /// </summary>
        Scale,
        /// <summary>
        /// Second Order Controller
        /// </summary>
        Sec_Order_Controller,
        /// <summary>
        /// Select
        /// </summary>
        Select,
        /// <summary>
        /// Selectable Negate
        /// </summary>
        Selectable_Negate,
        /// <summary>
        /// Selected Summer
        /// </summary>
        Selected_Summer,
        /// <summary>
        /// Select Enhanced
        /// </summary>
        Select_Enhanced,
        /// <summary>
        /// Serial Port Control
        /// </summary>
        Serial_Port_Control,
        /// <summary>
        /// (Sequential Function) Action
        /// </summary>
        SFC_Action,
        /// <summary>
        /// (Sequential Function) Step
        /// </summary>
        SFC_Step,
        /// <summary>
        /// Sequential Function) Stop
        /// </summary>
        SFC_Stop,
        */
        /// <summary>
        /// Single Integer
        /// </summary>
        SInt,
        /*
        /// <summary>
        /// Split Range
        /// </summary>
        Split_Range,
        /// <summary>
        /// S-Curve
        /// </summary>
        S_Curve,
        */
        /// <summary>
        /// Timer
        /// </summary>
        Timer,
        /*
        /// <summary>
        /// Totalizer
        /// </summary>
        Totalizer,
        /// <summary>
        /// Two Hand Run Station
        /// </summary>
        Two_Hand_Run_Station,
        /// <summary>
        /// Up-Down Accumulator
        /// </summary>
        Up_Down_Accum,
        */
        /// <summary>
        /// User Defined Type
        /// </summary>
        User_Defined
    }

    /// <summary>
    /// Helper class to create tags without knowing the underlying LOGIX type
    /// </summary>
    public static class LogixTagFactory
    {
#if MONO
		public static LogixTag CreateTag(string Address, LogixProcessor Processor)
		{
			return CreateTag(Address, Processor, 1);
		}
#endif
#if MONO
		public static LogixTag CreateTag(string Address, LogixProcessor Processor, ushort Elements)
#else
        /// <summary>
        /// Creates a LogixTag with the correct type
        /// </summary>
        /// <param name="Address">Address (Tag Name)</param>
        /// <param name="Processor">Processor the tag belongs to</param>
        /// <param name="Elements">Number of elements to read</param>
        /// <returns>LogixTag of the correct underlying type</returns>
        public static LogixTag CreateTag(string Address, LogixProcessor Processor, ushort Elements = 1)
#endif
		{
            //We'll do this by creating the tag first, then seeing what type it is
            //and returning it to the user...

            return CreateQuickTag(Address, Processor, Elements);
        }

        internal static LogixTag CreateLongTag(string Address, LogixProcessor Processor, ushort Elements)
        {
            //It doesn't exist in the list, maybe it was added later?
            //lock (Processor.SyncRoot)
            {
                ReadDataServiceReply lgxRead = LogixServices.ReadLogixData(
                    Processor.SessionInfo, Address, Elements);

                if (lgxRead == null)
                    return null;

                if (lgxRead.Data == null)
                    return null;

                if (lgxRead.Status != 0x00 && lgxRead.Status != 0x06)
                {
                    return null;
                }

                //The first two bytes are the type...
                CIPType tagType = (CIPType)lgxRead.DataType;

                if (tagType == CIPType.STRUCT)
                {
                    //We need to build an information object about this type
                    //The first two bytes are a handle to the structure...
                    ushort structureId = Processor.GetStructureHandle(Address);

                    //Now we can send a request to get the attributes...
                    TemplateInfo ti = ReadStructAttributes(structureId, Processor);

                    return CreateStructTag(Address, Processor, Elements, ti);
                }
                else
                {
                    switch (tagType)
                    {
                        case CIPType.BITS:
                        case CIPType.BOOL:
                            return new LogixBOOL(Address, Processor, Elements);
                        case CIPType.SINT:
                            return new LogixSINT(Address, Processor, Elements);
                        case CIPType.INT:
                            return new LogixINT(Address, Processor, Elements);
                        case CIPType.DINT:
                            return new LogixDINT(Address, Processor, Elements);
                        case CIPType.LINT:
                            return new LogixLINT(Address, Processor, Elements);
                        case CIPType.REAL:
                            return new LogixREAL(Address, Processor, Elements);
                        default:
                            break;      //Unknown type...
                    }
                }

                return null;
            }
        }

        internal static LogixTag CreateQuickTag(string Address, LogixProcessor Processor, ushort Elements)
        {
            if (Address.Contains(".") || Address.Contains("["))
            {
                return CreateLongTag(Address, Processor, Elements);
            }

            LogixTagInfo ti = Processor.GetInfoForTag(Address);

            if (ti == null)
                return null;

            if (ti.IsStructure)
            {
                //We need to build an information object about this type
                //The first two bytes are a handle to the structure...
                ushort structureId = Processor.GetStructureHandle(Address);

                //Now we can send a request to get the attributes...
                TemplateInfo tempInfo = ReadStructAttributes(structureId, Processor);

                LogixTag st = CreateStructTag(Address, Processor, Elements, tempInfo);
                st.TagInfo = ti;
            }
            else
            {
                LogixTag at = null;

                switch ((CIPType)ti.DataType)
                {
                    case CIPType.BITS:
                    case CIPType.BOOL:
                        at = new LogixBOOL(Address, Processor, Elements); break;
                    case CIPType.SINT:
                        at = new LogixSINT(Address, Processor, Elements); break;
                    case CIPType.INT:
                        at = new LogixINT(Address, Processor, Elements); break;
                    case CIPType.DINT:
                        at = new LogixDINT(Address, Processor, Elements); break;
                    case CIPType.LINT:
                        at = new LogixLINT(Address, Processor, Elements); break;
                    case CIPType.REAL:
                        at = new LogixREAL(Address, Processor, Elements); break;
                    default:
                        break;      //Unknown type...
                }

                if (at != null)
                {
                    at.TagInfo = ti;
                    return at;
                }
            }

            return null;
        }

        internal static TemplateInfo GetTemplateInfo(string Address, LogixProcessor Processor, ushort Elements)
        {
            //lock (Processor.SyncRoot)
            {
                ReadDataServiceReply lgxRead = LogixServices.ReadLogixData(
                    Processor.SessionInfo, Address, Elements);

                if (lgxRead == null)
                    return null;

                if (lgxRead.Data == null)
                    return null;

                if (lgxRead.Status != 0x00 && lgxRead.Status != 0x06)
                {
                    return null;
                }

                //The first two bytes are the type...
                CIPType tagType = (CIPType)lgxRead.DataType;

                if (tagType == CIPType.STRUCT)
                {
                    //We need to build an information object about this type
                    //The first two bytes are a handle to the structure...
                    ushort structureId = Processor.GetStructureHandle(Address);

                    //Now we can send a request to get the attributes...
                    TemplateInfo ti = ReadStructAttributes(structureId, Processor);

                    return ti;
                }
            }

            return null;
        }

        private static TemplateInfo ReadStructAttributes(ushort structureId, LogixProcessor processor)
        {
            //First we have to get the template info...
            GetStructAttribsRequest attribsReq = new GetStructAttribsRequest(structureId);
            CommonPacketItem addressItem = CommonPacketItem.GetNullAddressItem();

            UnconnectedSend ucmm = new UnconnectedSend();
            ucmm.Priority_TimeTick = 0x07;
            ucmm.Timeout_Ticks = 0x9B;
            ucmm.RoutePath = processor.Path;
            ucmm.MessageRequest = new MR_Request();
            ucmm.MessageRequest.Request_Path = new byte[] { 0x20, 0x6C, 0x25, 0x00, (byte)((structureId & 0x00FF)), (byte)((structureId & 0xFF00) >> 8) };
            ucmm.MessageRequest.Service = 0x03;
            ucmm.MessageRequest.Request_Data = new byte[] { 0x04, 0x00, 0x04, 0x00, 0x03, 0x00, 0x02, 0x00, 0x01, 0x00 };
            ucmm.RequestPath = CommonPaths.ConnectionManager;

            CommonPacketItem dataItem = CommonPacketItem.GetUnconnectedDataItem(ucmm.Pack());

            EncapsReply reply = processor.SessionInfo.SendRRData(addressItem, dataItem);

            if (reply.Status != 0x00)
                return null;

            //We have to get the data out...
            EncapsRRData rrData = new EncapsRRData();
            CommonPacket cpf = new CommonPacket();
            int temp = 0;
            rrData.Expand(reply.EncapsData, 0, out temp);
            cpf = rrData.CPF;

            byte[] replyData = new byte[28];
            Buffer.BlockCopy(cpf.DataItem.Data, 4, replyData, 0, 28);

            GetStructAttribsReply structAttribs = new GetStructAttribsReply(replyData);

            //Great... now we can request the structure template and be able to read it!
            ucmm.MessageRequest.Service = 0x4C;
            
            //We can only read about 480 bytes at a time, so we may have to break it up...
            uint bytesRemaining = (structAttribs.TemplateSize - 5) * 4;
            uint offset = 0;
            List<byte> structInfoBytes = new List<byte>();

            while (bytesRemaining > 0)
            {
                ushort bytesToRead;
                if (bytesRemaining < 480)
                {
                    bytesToRead = (ushort)bytesRemaining;
                    bytesRemaining = 0;
                }
                else
                {
                    bytesToRead = 480;
                    bytesRemaining -= 480;
                }

                byte[] tempB = new byte[6];
                Buffer.BlockCopy(BitConverter.GetBytes(offset), 0, tempB, 0, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(bytesToRead), 0, tempB, 4, 2);

                ucmm.MessageRequest.Request_Data = tempB;

                dataItem = CommonPacketItem.GetUnconnectedDataItem(ucmm.Pack());

                reply = processor.SessionInfo.SendRRData(addressItem, dataItem);

                if (reply.Status != 0x00)
                    continue;

                rrData.Expand(reply.EncapsData, 0, out temp);
                cpf = rrData.CPF;

                //get the data out...
                tempB = new byte[cpf.DataItem.Data.Length - 4];
                Buffer.BlockCopy(cpf.DataItem.Data, 4, tempB, 0, cpf.DataItem.Data.Length - 4);
                structInfoBytes.AddRange(tempB);
                offset += bytesToRead;
            }

            //Now we have all the data!!!!

            return new TemplateInfo(structInfoBytes.ToArray(), structAttribs);
        }

        private static LogixTag CreateStructTag(string Address, LogixProcessor Processor, ushort Elements, TemplateInfo Template)
        {
            if (Template.TemplateName == "COUNTER")
            {
                return new LogixCOUNTER(Address, Template, Processor, Elements);
            }
            else if (Template.TemplateName == "CONTROL")
            {
                return new LogixCONTROL(Address, Template, Processor, Elements);
            }
            else if (Template.TemplateName == "TIMER")
            {
                return new LogixTIMER(Address, Template, Processor, Elements);
            }
            else if (Template.TemplateName == "ASCIISTRING82")
            {
                return new LogixSTRING(Address, Template, Processor, Elements);
            }
            else
            {
                //Return as a UDT...
                return new LogixUDT(Address, Template, Processor, Elements);
            }
        }
    }

    #region Atomic Types

    /// <summary>
    /// Represents a type of BOOL in a Logix Processor
    /// </summary>
    public sealed class LogixBOOL : LogixTag
    {

        #region Fields

        private bool[] _state = new bool[] { false };
        private bool[] _pendingWriteValue = new bool[] { false };
        private bool[] _hasPendingWrite = new bool[] { false };
        private bool _lastWriteChanged = false;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or Sets the state of the tag
        /// </summary>
        public bool Value
        {
            get { return _state[0]; }
            set
            {
                _pendingWriteValue[0] = value;
                _hasPendingWrite[0] = true;
            }
        }

        /// <summary>
        /// Gets or Sets the element of the tag at the specified index
        /// </summary>
        /// <param name="index">Index of the element to get or set</param>
        /// <returns>True or False</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when index >= Elements * 32 or index < 0</exception>
        public bool this[int index]
        {
            get
            {
                if (index >= Elements * 32)
                    throw new IndexOutOfRangeException();

                if (index < 0)
                    throw new IndexOutOfRangeException();

                return _state[index];
            }
            set
            {
                if (index >= Elements * 32)
                    throw new IndexOutOfRangeException();

                if (index < 0)
                    throw new IndexOutOfRangeException();

                _pendingWriteValue[index] = value;
                _hasPendingWrite[index] = true;
            }
        }

        /// <summary>
        /// Gets or Sets the value at the specified row and column
        /// </summary>
        /// <param name="row">Row to set/get</param>
        /// <param name="col">Column to set/get</param>
        /// <returns>Value at the specified row and column</returns>
        public bool this[int row, int col]
        {
            get
            {
                if (Dim1 == 0 || Dim2 == 0)
                    throw new DimensionsNotSetException(Resources.ErrorStrings.DimensionsNotSet);

                if (row * col > Elements)
                    throw new IndexOutOfRangeException();

                if (row < 0 || col < 0)
                    throw new IndexOutOfRangeException();

                int idx = row * Dim2 + col;

                return this[idx];
            }
            set
            {
                if (Dim1 == 0 || Dim2 == 0)
                    throw new DimensionsNotSetException(Resources.ErrorStrings.DimensionsNotSet);

                if (row * col > Elements)
                    throw new IndexOutOfRangeException();

                if (row < 0 || col < 0)
                    throw new IndexOutOfRangeException();

                int idx = row * Dim2 + col;

                this[idx] = value;
            }
        }

        /// <summary>
        /// Gets or Sets the value at the specified row, column, and element
        /// </summary>
        /// <param name="row">Row to set/get</param>
        /// <param name="col">Column to set/get</param>
        /// <param name="element">Element to set/get</param>
        /// <returns>Value at the specified row, column, and element</returns>
        public bool this[int row, int col, int element]
        {
            get
            {
                if (Dim1 == 0 || Dim2 == 0 || Dim3 == 0)
                    throw new DimensionsNotSetException(Resources.ErrorStrings.DimensionsNotSet);

                if (row * col * element > Elements)
                    throw new IndexOutOfRangeException();

                if (row < 0 || col < 0 || element < 0)
                    throw new IndexOutOfRangeException();

                int idx = ((row * Dim2 + col) * Dim3 + element);

                return this[idx];
            }
            set
            {
                if (Dim1 == 0 || Dim2 == 0)
                    throw new DimensionsNotSetException(Resources.ErrorStrings.DimensionsNotSet);

                if (row * col * element > Elements)
                    throw new IndexOutOfRangeException();

                if (row < 0 || col < 0 || element < 0)
                    throw new IndexOutOfRangeException();

                int idx = row * col * element;

                this[idx] = value;
            }
        }

        /// <summary>
        /// Gets the Logix tag type
        /// </summary>
        public override LogixTypes LogixType
        {
            get { return LogixTypes.Bool; }
        }

        /// <summary>
        /// Gets or Sets the LogixBOOL as an object
        /// </summary>
        /// <remarks>
        /// <para>
        /// When using this to set the array value, the passed in array must be a single
        /// dimension array with the same number of elements as the LogixBOOL tag.
        /// </para>
        /// <para>
        /// It is also good to note that this function makes use of the <see cref="Convert"/> class to convert
        /// from the passed in object to the appropriate data type. If the object is an array, this function is
        /// called on every element of the array, not just the first element. This can be a performance burden if
        /// setting large numbers of elements in this fashion. In that case it is recommended to cast the LogixTag
        /// to the correct object and use the indexor to set the values.
        /// </para>
        /// </remarks>
        /// <exception cref="TypeConversionException">Thrown when the value type cannot be converted to a boolean</exception>
        /// <exception cref="ArrayLengthException">Thrown when the number of elements in the value array does not equal <see cref="Elements"/></exception>
        public override object ValueAsObject
        {
            get
            {
                if (Elements == 1)
                    return Value;
                else
                    return _state;
            }
            set
            {
                if (Elements == 1)
                {
                    try
                    {
                        Value = Convert.ToBoolean(value);
                    }
                    catch (Exception e)
                    {
                        throw new TypeConversionException(Resources.ErrorStrings.TypeConversionError);
                    }
                }
                else
                {
                    Array valueArray = value as Array;
                    if (valueArray != null)
                    {
                        if (valueArray.Length != Elements)
                            throw new ArrayLengthException(Resources.ErrorStrings.ArrayLengthException);
                        try
                        {
                            for (int i = 0; i < valueArray.Length; i++)
                            {
                                this[i] = Convert.ToBoolean(valueArray.GetValue(i));
                            }
                        }
                        catch (Exception e)
                        {
                            throw new TypeConversionException(Resources.ErrorStrings.TypeConversionError);
                        }

                    }
                    else
                        throw new TypeConversionException(Resources.ErrorStrings.TypeConversionError);
                }
            }
        }
        
        #endregion

        #region Construction / Deconstruction

        /// <summary>
        /// Creates a new BOOL Logix Tag
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor.</param>
        public LogixBOOL(string TagAddress)
            : base(TagAddress)
        {
            
        }

        /// <summary>
        /// Creates a new BOOL Logix Tag on the specified Processor
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor</param>
        /// <param name="Processor">Processor where the tag resides</param>
        public LogixBOOL(string TagAddress, LogixProcessor Processor)
            : base(TagAddress, Processor)
        {

        }

        /// <summary>
        /// Creates a new BOOL Logix Tag on the specified Processor with the number of Elements
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor</param>
        /// <param name="Processor">Processor where the tag resides</param>
        /// <param name="ElementCount">Number of elements to read</param>
        public LogixBOOL(string TagAddress, LogixProcessor Processor, ushort ElementCount)
            : base(TagAddress, Processor, ElementCount)
        {

        }

        #endregion

        #region Public Methods

   

        #endregion

        #region Internal Methods
		
#if MONO
		internal override void Initialize(object InitData)
#else
        internal override void Initialize(object InitData = null)
#endif
		{
            _state = new bool[Elements * 32];
            _pendingWriteValue = new bool[Elements * 32];
            _hasPendingWrite = new bool[Elements * 32];
        }
		
#if MONO
		internal override bool OnDataUpdated(byte[] data, uint byteOffset)
#else
        internal override bool OnDataUpdated(byte[] data, uint byteOffset = 0)
#endif
		{
            bool canRaiseEvent = false;
            //data should be at least as long as the state...
            if (DataType == 0xD3)
            {
                for (int iNum = 0; iNum < data.Length / 4; iNum++)
                {
                    int source = BitConverter.ToInt32(data, iNum * 4);
                    //The data returned is a 32 bit value representing up to
                    //32 boolean values starting at the index of 0...
                    int iStart = iNum * 32;
                    for (int i = iStart; i < (iStart + 32); i++)
                    {
                        bool newVal = ((source >> i) & 1) == 1;
                        _lastWriteChanged |= (newVal != _state[i]);
                        _state[i] = newVal;
                        if (i == _state.Length)
                            canRaiseEvent = true;
                    }
                }

            }
            else if (DataType == 0xC1)
            {
                bool newState = (data[0] == 0xFF ? true : false);
                _lastWriteChanged = _state[0] != newState;
                _state[0] = newState;
                canRaiseEvent = true;
            }
            else
            {
                LastErrorNumber = (int)LogixErrors.TypeMismatch;
                LastError = Resources.ErrorStrings.TypeMismatch;
            }

            if (canRaiseEvent && _lastWriteChanged)
            {
                _lastWriteChanged = false;
                RaiseTagDataChanged(this);
                return true;
            }

            return false;
        }

        internal override byte[] OnDataWrite()
        {
            //This has to be packed into 32 bit values...
            uint[] packed = new uint[Elements];

            for (int iNum = 0; iNum < Elements; iNum++)
            {
                uint currentNum = 0;
                int bitNum = 0;

                for (int i = iNum * 32; i < (iNum * 32) + 32; i++)
                {
                    if (_hasPendingWrite[i])
                    {
                        if (_pendingWriteValue[i])
                            currentNum |= ((uint)0x01 << bitNum);
                    }
                    else
                    {
                        if (_state[i])
                            currentNum |= ((uint)0x01 << bitNum);
                    }
                    bitNum++;
                }

                packed[iNum] = currentNum;
            }

            byte[] retVal = new byte[4 * Elements];

            for (int i = 0; i < Elements; i++)
                Buffer.BlockCopy(BitConverter.GetBytes(packed[i]), 0, retVal, i * 4, 4);

            return retVal;
        }
        
        internal override void ClearPendingWrite()
        {
            for (int i = 0; i < Elements; i++)
            {
                if (_hasPendingWrite[i])
                    _state[i] = _pendingWriteValue[i];
                _hasPendingWrite[i] = false;
            }
            this.RaiseTagDataChanged(this);
        }

        internal override bool HasPendingWrite()
        {
            for (int i = 0; i < _hasPendingWrite.Length; i++)
            {
                if (_hasPendingWrite[i])
                    return true;
            }

            return false;
        }

        internal override byte[] GetWriteData(out int byteAlignment)
        {
            if (Elements > 0)
                byteAlignment = 4;
            else
                byteAlignment = 1;

            return OnDataWrite();
        }

        #endregion

        #region Conversion Methods

        public static implicit operator bool(LogixBOOL tag)
        {
            return tag[0];
        }

        public override string ToString()
        {
            if (Elements == 1)
            {
                if (Value)
                    return "True";
                else
                    return "False";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                if (Dim1 == 0)
                {
                    //One dimensional, nothing set
                    sb.Append("[");
                    for (int i = 0; i < Elements; i++)
                    {
                        sb.Append(this[i] ? "True" : "False");
                        if (i < Elements - 1)
                            sb.Append(",");
                    }
                    sb.Append("]");
                }
                else if (Dim2 == 0)
                {
                    //One dimensional, set dimension
                    sb.Append("[");
                    for (int i = 0; i < Elements; i++)
                    {
                        sb.Append(this[i] ? "True" : "False");
                        if (i < Elements - 1)
                            sb.Append(",");
                    }
                    sb.Append("]");
                }
                else if (Dim3 == 0)
                {
                    //Two dimensional, set dimensions
                    sb.Append("[");
                    for (int row = 0; row < Dim1; row++)
                    {
                        sb.Append("[");
                        for (int col = 0; col < Dim2; col++)
                        {
                            sb.Append(this[row, col] ? "True" : "False");
                            if (col < Dim2 - 1)
                                sb.Append(",");
                        }
                        sb.AppendLine("]");
                    }
                    sb.Append("]");
                }
                else
                {
                    //Three dimensional, set dimensions
                    sb.Append("[");
                    for (int row = 0; row < Dim1; row++)
                    {
                        sb.Append("[");
                        for (int col = 0; col < Dim2; col++)
                        {
                            sb.Append("[");
                            for (int el = 0; el < Dim3; el++)
                            {
                                sb.Append(this[row, col, el] ? "True" : "False");
                                if (el < Dim3 - 1)
                                    sb.Append(",");
                            }
                            sb.AppendLine("]");
                        }
                        sb.AppendLine("]");
                    }
                    sb.Append("]");
                }

                return sb.ToString();
            }
        }

        #endregion

    }

    /// <summary>
    /// Represents a type of DINT in a Logix Processor
    /// </summary>
    public sealed class LogixDINT : LogixTag
    {

        #region Fields

        private int[] _value = new int[] { 0 };
        private int[] _pendingWriteValue = new int[] { 0 };
        private bool[] _hasPendingWrite = new bool[] { false };
        private bool _lastWriteChanged = false;
        
        #endregion

        #region Properties

        /// <summary>
        /// Gets or Sets the Value of the tag
        /// </summary>
        public int Value
        {
            get
            {
                return _value[0];
            }
            set
            {
                _pendingWriteValue[0] = value;
                _hasPendingWrite[0] = true;
            }
        }

        /// <summary>
        /// Gets or Sets the tag at the specified index
        /// </summary>
        /// <param name="index">Index to get or set</param>
        /// <returns>Value at the specified index</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when index >= Elements or index < 0</exception>
        public int this[int index]
        {
            get
            {
                if (index >= Elements)
                    throw new IndexOutOfRangeException();
                if (index < 0)
                    throw new IndexOutOfRangeException();

                return _value[index];
            }
            set
            {
                if (index >= Elements)
                    throw new IndexOutOfRangeException();
                if (index < 0)
                    throw new IndexOutOfRangeException();

                _pendingWriteValue[index] = value;
                _hasPendingWrite[index] = true;
            }
        }

        /// <summary>
        /// Gets the specified value at the row and column index
        /// </summary>
        /// <param name="row">Row index</param>
        /// <param name="col">Column index</param>
        /// <returns>Value at the specified row and column</returns>
        /// <exception cref="DimensionsNotSetException">Thrown when the dimensions haven't been set before accessing this property</exception>
        /// <exception cref="IndexOutOfRangeException">Thrown when row*col > Elements or either index < 0</exception>
        public int this[int row, int col]
        {
            get
            {
                if (Dim1 == 0 || Dim2 == 0)
                    throw new DimensionsNotSetException(Resources.ErrorStrings.DimensionsNotSet);

                if (row * col > Elements)
                    throw new IndexOutOfRangeException();

                if (row < 0 || col < 0)
                    throw new IndexOutOfRangeException();

                int idx = row * Dim2 + col;

                return this[idx];
            }
            set
            {
                if (Dim1 == 0 || Dim2 == 0)
                    throw new DimensionsNotSetException(Resources.ErrorStrings.DimensionsNotSet);

                if (row * col > Elements)
                    throw new IndexOutOfRangeException();

                if (row < 0 || col < 0)
                    throw new IndexOutOfRangeException();

                int idx = row * Dim2 + col;

                this[idx] = value;
            }
        }

        /// <summary>
        /// Gets the specified value at the row, column, and element index
        /// </summary>
        /// <param name="row">Row index</param>
        /// <param name="col">Column Index</param>
        /// <param name="element">Element Index</param>
        /// <returns>Value at the specified row, column and element index</returns>
        /// <exception cref="DimensionsNotSetException">Thrown when the dimensions haven't been set before accessing this property</exception>
        /// <exception cref="IndexOutOfRangeException">Thrown when row*col > Elements or either index < 0</exception>
        public int this[int row, int col, int element]
        {
            get
            {
                if (Dim1 == 0 || Dim2 == 0 || Dim3 == 0)
                    throw new DimensionsNotSetException(Resources.ErrorStrings.DimensionsNotSet);

                if (row * col * element > Elements)
                    throw new IndexOutOfRangeException();

                if (row < 0 || col < 0 || element < 0)
                    throw new IndexOutOfRangeException();

                int idx = ((row * Dim2 + col) * Dim3 + element);

                return this[idx];
            }
            set
            {
                if (Dim1 == 0 || Dim2 == 0)
                    throw new DimensionsNotSetException(Resources.ErrorStrings.DimensionsNotSet);

                if (row * col * element > Elements)
                    throw new IndexOutOfRangeException();

                if (row < 0 || col < 0 || element < 0)
                    throw new IndexOutOfRangeException();

                int idx = ((row * Dim2 + col) * Dim3 + element);

                this[idx] = value;
            }
        }

        /// <summary>
        /// Gets the Logix type of the tag
        /// </summary>
        public override LogixTypes LogixType
        {
            get { return LogixTypes.DInt; }
        }

        /// <summary>
        /// Gets or Sets the LogixDINT as an object
        /// </summary>
        /// <remarks>
        /// <para>
        /// When using this to set the array value, the passed in array must be a single
        /// dimension array with the same number of elements as the LogixDINT tag.
        /// </para>
        /// <para>
        /// It is also good to note that this function makes use of the <see cref="Convert"/> class to convert
        /// from the passed in object to the appropriate data type. If the object is an array, this function is
        /// called on every element of the array, not just the first element. This can be a performance burden if
        /// setting large numbers of elements in this fashion. In that case it is recommended to cast the LogixTag
        /// to the correct object and use the indexor to set the values.
        /// </para>
        /// </remarks>
        /// <exception cref="TypeConversionException">Thrown when the value type cannot be converted to an int</exception>
        /// <exception cref="ArrayLengthException">Thrown when the number of elements in the value array does not equal <see cref="Elements"/></exception>
        public override object ValueAsObject
        {
            get
            {
                if (Elements == 1)
                    return Value;
                else
                    return _value;
            }
            set
            {
                if (Elements == 1)
                {
                    try
                    {
                        Value = Convert.ToInt32(value);
                    }
                    catch (Exception e)
                    {
                        throw new TypeConversionException(Resources.ErrorStrings.TypeConversionError);
                    }
                }
                else
                {
                    //Ok, first we have to verify that it's an array
                    Array valueArray = value as Array;
                    if (valueArray != null)
                    {
                        if (valueArray.Length != Elements)
                            throw new ArrayLengthException(Resources.ErrorStrings.ArrayLengthException);
                        try
                        {
                            for (int i = 0; i < valueArray.Length; i++)
                            {
                                this[i] = Convert.ToInt32(valueArray.GetValue(i));
                            }
                        }
                        catch (Exception e)
                        {
                            throw new TypeConversionException(Resources.ErrorStrings.TypeConversionError);
                        }

                    }
                    else
                        throw new TypeConversionException(Resources.ErrorStrings.TypeConversionError);
                }
            }
        }

        #endregion

        #region Construction / Deconstruction

        /// <summary>
        /// Creates a new DINT Logix Tag
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor.</param>
        public LogixDINT(string TagAddress)
            : base(TagAddress)
        {
            
        }

        /// <summary>
        /// Creates a new DINT Logix Tag on the specified Processor
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor</param>
        /// <param name="Processor">Processor where the tag resides</param>
        public LogixDINT(string TagAddress, LogixProcessor Processor)
            : base(TagAddress, Processor)
        {

        }

        /// <summary>
        /// Creates a new DINT Logix Tag on the specified Processor with the number of Elements
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor</param>
        /// <param name="Processor">Processor where the tag resides</param>
        /// <param name="ElementCount">Number of elements to read</param>
        public LogixDINT(string TagAddress, LogixProcessor Processor, ushort ElementCount)
            : base(TagAddress, Processor, ElementCount)
        {

        }

        #endregion

        #region Public Methods



        #endregion

        #region Internal Methods
		
#if MONO
		internal override void Initialize(object InitData)
#else
        internal override void Initialize(object InitData = null)
#endif
		{
            _value = new int[Elements];
            _pendingWriteValue = new int[Elements];
            _hasPendingWrite = new bool[Elements];
        }
		
#if MONO
		internal override bool OnDataUpdated(byte[] data, uint byteOffset)
#else
        internal override bool OnDataUpdated(byte[] data, uint byteOffset = 0)
#endif
		{
            //Let's mask off the lower part of the data type...
            ushort tempType = (ushort)(DataType & 0xFFF);
            bool canRaiseEvent = false;

            if (tempType == 0xC4)
            {
                //32 Bit Signed Int

                uint startElement = byteOffset / 4;
                int dataPos = 0;
                int temp;

                //Might be an array though...
                for (uint i = startElement; i < Elements; i++)
                {
                    temp = BitConverter.ToInt32(data, dataPos);
                    _lastWriteChanged |= (temp != _value[i]);
                    _value[i] = temp;
                    dataPos += 4;
                    if (i == Elements - 1)
                        canRaiseEvent = true;
                    if (dataPos >= data.Length)
                        break;
                }
            }
            else
            {
                //Who knows???
                LastError = Resources.ErrorStrings.TypeMismatch + "0x" + tempType.ToString("X2");
                LastErrorNumber = (int)LogixErrors.TypeMismatch;
            }

            if (canRaiseEvent && _lastWriteChanged)
            {
                _lastWriteChanged = false;
                RaiseTagDataChanged(this);
                return true;
            }

            return false;
        }

        internal override byte[] OnDataWrite()
        {
            byte[] retVal = new byte[Elements * 4];

            for (int i = 0; i < Elements; i++)
            {
                Buffer.BlockCopy(BitConverter.GetBytes(_value[i]), 0, retVal, i * 4, 4);
            }

            return retVal;
        }
        
        internal override bool HasPendingWrite()
        {
            return PendingWriteCount() > 0;
        }

        internal override void ClearPendingWrite()
        {
            for (int i = 0; i < Elements; i++)
                _hasPendingWrite[i] = false;
        }

        internal override byte[] GetWriteData(out int byteAlignment)
        {
            List<byte> retVal = new List<byte>();
            byteAlignment = 4;

            if (Elements == 1)
            {
                retVal.AddRange(BitConverter.GetBytes(_pendingWriteValue[0]));
            }
            else
            {
                for (int i = 0; i < Elements; i++)
                {
                    if (_hasPendingWrite[i])
                        retVal.AddRange(BitConverter.GetBytes(_pendingWriteValue[i]));
                    else
                        retVal.AddRange(BitConverter.GetBytes(_value[i]));
                }
                    
            }

            return retVal.ToArray();
        }

        #endregion

        #region Private Methods

        private int PendingWriteCount()
        {
            int count = 0;
            for (int i = 0; i < Elements; i++)
            {
                if (_hasPendingWrite[i])
                    count++;
            }

            return count;
        }

        #endregion

        #region Conversion Methods

        public static implicit operator int(LogixDINT tag)
        {
            return tag[0];
        }

        #endregion

    }

    /// <summary>
    /// Represents a type of INT in a Logix Processor
    /// </summary>
    public sealed class LogixINT : LogixTag
    {

        #region Fields

        private short[] _value = new short[] { 0 };
        private short[] _pendingWriteValue = new short[] { 0 };
        private bool[] _hasPendingWrite = new bool[] { false };
        private bool _lastWriteChanged = false;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or Sets the Value of the tag
        /// </summary>
        public short Value
        {
            get
            {
                return _value[0];
            }
            set
            {
                _pendingWriteValue[0] = value;
                _hasPendingWrite[0] = true;
            }
        }

        /// <summary>
        /// Gets or Sets the tag at the specified index
        /// </summary>
        /// <param name="index">Index to get or set</param>
        /// <returns>Value at the specified index</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when index >= Elements or index < 0</exception>
        public short this[int index]
        {
            get
            {
                if (index >= Elements)
                    throw new IndexOutOfRangeException();
                if (index < 0)
                    throw new IndexOutOfRangeException();

                return _value[index];
            }
            set
            {
                if (index >= Elements)
                    throw new IndexOutOfRangeException();
                if (index < 0)
                    throw new IndexOutOfRangeException();

                _pendingWriteValue[index] = value;
                _hasPendingWrite[index] = true;
            }
        }

        /// <summary>
        /// Gets the specified value at the row and column index
        /// </summary>
        /// <param name="row">Row index</param>
        /// <param name="col">Column index</param>
        /// <returns>Value at the specified row and column</returns>
        /// <exception cref="DimensionsNotSetException">Thrown when the dimensions haven't been set before accessing this property</exception>
        /// <exception cref="IndexOutOfRangeException">Thrown when row*col > Elements or either index < 0</exception>
        public short this[int row, int col]
        {
            get
            {
                if (Dim1 == 0 || Dim2 == 0)
                    throw new DimensionsNotSetException(Resources.ErrorStrings.DimensionsNotSet);

                if (row * col > Elements)
                    throw new IndexOutOfRangeException();

                if (row < 0 || col < 0)
                    throw new IndexOutOfRangeException();

                int idx = row * Dim2 + col;

                return this[idx];
            }
            set
            {
                if (Dim1 == 0 || Dim2 == 0)
                    throw new DimensionsNotSetException(Resources.ErrorStrings.DimensionsNotSet);

                if (row * col > Elements)
                    throw new IndexOutOfRangeException();

                if (row < 0 || col < 0)
                    throw new IndexOutOfRangeException();

                int idx = row * Dim2 + col;

                this[idx] = value;
            }
        }

        /// <summary>
        /// Gets the specified value at the row, column, and element index
        /// </summary>
        /// <param name="row">Row index</param>
        /// <param name="col">Column Index</param>
        /// <param name="element">Element Index</param>
        /// <returns>Value at the specified row, column and element index</returns>
        /// <exception cref="DimensionsNotSetException">Thrown when the dimensions haven't been set before accessing this property</exception>
        /// <exception cref="IndexOutOfRangeException">Thrown when row*col > Elements or either index < 0</exception>
        public short this[int row, int col, int element]
        {
            get
            {
                if (Dim1 == 0 || Dim2 == 0 || Dim3 == 0)
                    throw new DimensionsNotSetException(Resources.ErrorStrings.DimensionsNotSet);

                if (row * col * element > Elements)
                    throw new IndexOutOfRangeException();

                if (row < 0 || col < 0 || element < 0)
                    throw new IndexOutOfRangeException();

                int idx = ((row * Dim2 + col) * Dim3 + element);

                return this[idx];
            }
            set
            {
                if (Dim1 == 0 || Dim2 == 0)
                    throw new DimensionsNotSetException(Resources.ErrorStrings.DimensionsNotSet);

                if (row * col * element > Elements)
                    throw new IndexOutOfRangeException();

                if (row < 0 || col < 0 || element < 0)
                    throw new IndexOutOfRangeException();

                int idx = ((row * Dim2 + col) * Dim3 + element);

                this[idx] = value;
            }
        }

        /// <summary>
        /// Gets the Logix type of the tag
        /// </summary>
        public override LogixTypes LogixType
        {
            get { return LogixTypes.Int; }
        }

        /// <summary>
        /// Gets or Sets the LogixINT as an object
        /// </summary>
        /// <remarks>
        /// <para>
        /// When using this to set the array value, the passed in array must be a single
        /// dimension array with the same number of elements as the LogixINT tag.
        /// </para>
        /// <para>
        /// It is also good to note that this function makes use of the <see cref="Convert"/> class to convert
        /// from the passed in object to the appropriate data type. If the object is an array, this function is
        /// called on every element of the array, not just the first element. This can be a performance burden if
        /// setting large numbers of elements in this fashion. In that case it is recommended to cast the LogixTag
        /// to the correct object and use the indexor to set the values.
        /// </para>
        /// </remarks>
        /// <exception cref="TypeConversionException">Thrown when the value type cannot be converted to a short</exception>
        /// <exception cref="ArrayLengthException">Thrown when the number of elements in the value array does not equal <see cref="Elements"/></exception>
        public override object ValueAsObject
        {
            get
            {
                if (Elements == 1)
                    return Value;
                else
                    return _value;
            }
            set
            {
                if (Elements == 1)
                {
                    try
                    {
                        Value = Convert.ToInt16(value);
                    }
                    catch (Exception e)
                    {
                        throw new TypeConversionException(Resources.ErrorStrings.TypeConversionError);
                    }
                }
                else
                {
                    //Ok, first we have to verify that it's an array
                    Array valueArray = value as Array;
                    if (valueArray != null)
                    {
                        if (valueArray.Length != Elements)
                            throw new ArrayLengthException(Resources.ErrorStrings.ArrayLengthException);
                        try
                        {
                            for (int i = 0; i < valueArray.Length; i++)
                            {
                                this[i] = Convert.ToInt16(valueArray.GetValue(i));
                            }
                        }
                        catch (Exception e)
                        {
                            throw new TypeConversionException(Resources.ErrorStrings.TypeConversionError);
                        }

                    }
                    else
                        throw new TypeConversionException(Resources.ErrorStrings.TypeConversionError);
                }
            }
        }

        #endregion

        #region Construction / Deconstruction

        /// <summary>
        /// Creates a new INT Logix Tag
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor.</param>
        public LogixINT(string TagAddress)
            : base(TagAddress)
        {
            
        }

        /// <summary>
        /// Creates a new INT Logix Tag on the specified Processor
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor</param>
        /// <param name="Processor">Processor where the tag resides</param>
        public LogixINT(string TagAddress, LogixProcessor Processor)
            : base(TagAddress, Processor)
        {

        }

        /// <summary>
        /// Creates a new INT Logix Tag on the specified Processor with the number of Elements
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor</param>
        /// <param name="Processor">Processor where the tag resides</param>
        /// <param name="ElementCount">Number of elements to read</param>
        public LogixINT(string TagAddress, LogixProcessor Processor, ushort ElementCount)
            : base(TagAddress, Processor, ElementCount)
        {

        }

        #endregion

        #region Public Methods



        #endregion

        #region Protected / Internal Methods
		
#if MONO
		internal override bool OnDataUpdated(byte[] data, uint byteOffset)
#else
        internal override bool OnDataUpdated(byte[] data, uint byteOffset = 0)
#endif
		{
            //Let's mask off the lower part of the data type...
            ushort tempType = (ushort)(DataType & 0xFFF);
            bool canRaiseEvent = false;

            if (tempType == 0xC3)
            {
                //16 Bit Signed Int

                uint startElement = byteOffset / 2;
                int dataPos = 0;
                short temp = 0;

                //Might be an array though...
                for (uint i = startElement; i < Elements; i++)
                {
                    temp = BitConverter.ToInt16(data, dataPos);
                    _lastWriteChanged |= (temp != _value[i]);
                    _value[i] = temp;
                    dataPos += 4;
                    if (i == Elements - 1)
                        canRaiseEvent = true;
                    if (dataPos >= data.Length)
                        break;
                }
            }
            else
            {
                //Who knows???
                LastError = Resources.ErrorStrings.TypeMismatch + "0x" + tempType.ToString("X2");
                LastErrorNumber = (int)LogixErrors.TypeMismatch;
            }

            if (canRaiseEvent && _lastWriteChanged)
            {
                _lastWriteChanged = false;
                RaiseTagDataChanged(this);
                return true;
            }
            return false;
        }

        internal override byte[] OnDataWrite()
        {
            byte[] retVal = new byte[Elements * 2];

            for (int i = 0; i < Elements; i++)
            {
                Buffer.BlockCopy(BitConverter.GetBytes(_value[i]), 0, retVal, i * 2, 2);
            }

            return retVal;
        }

        internal override bool HasPendingWrite()
        {
            return PendingWriteCount() > 0;
        }

        internal override void ClearPendingWrite()
        {
            for (int i = 0; i < Elements; i++)
                _hasPendingWrite[i] = false;
        }
		
#if MONO
		internal override void Initialize(object InitData)
#else
        internal override void Initialize(object InitData = null)
#endif
		{
            _value = new short[Elements];
            _pendingWriteValue = new short[Elements];
            _hasPendingWrite = new bool[Elements];
        }

        internal override byte[] GetWriteData(out int byteAlignment)
        {
            List<byte> retVal = new List<byte>();
            byteAlignment = 2;

            if (Elements == 1)
            {
                retVal.AddRange(BitConverter.GetBytes(_pendingWriteValue[0]));
            }
            else
            {
                for (int i = 0; i < Elements; i++)
                {
                    if (_hasPendingWrite[i])
                        retVal.AddRange(BitConverter.GetBytes(_pendingWriteValue[i]));
                    else
                        retVal.AddRange(BitConverter.GetBytes(_value[i]));
                }

            }

            return retVal.ToArray();
        }

        #endregion

        #region Private Methods

        private int PendingWriteCount()
        {
            int count = 0;
            for (int i = 0; i < Elements; i++)
            {
                if (_hasPendingWrite[i])
                    count++;
            }

            return count;
        }

        #endregion

        #region Conversion Methods

        public static implicit operator short(LogixINT tag)
        {
            return tag[0];
        }

        #endregion

    }
    
    /// <summary>
    /// Represents a type of SINT in a Logix Processor
    /// </summary>
    public sealed class LogixSINT : LogixTag
    {
        
        #region Fields

        private sbyte[] _value = new sbyte[] { 0 };
        private sbyte[] _pendingWriteValue = new sbyte[] { 0 };
        private bool[] _hasPendingWrite = new bool[] { false };
        private bool _lastWriteChanged = false;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or Sets the Value of the tag
        /// </summary>
        public sbyte Value
        {
            get
            {
                return _value[0];
            }
            set
            {
                _pendingWriteValue[0] = value;
                _hasPendingWrite[0] = true;
            }
        }

        /// <summary>
        /// Gets or Sets the tag at the specified index
        /// </summary>
        /// <param name="index">Index to get or set</param>
        /// <returns>Value at the specified index</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when index >= Elements or index < 0</exception>
        public sbyte this[int index]
        {
            get
            {
                if (index >= Elements)
                    throw new IndexOutOfRangeException();
                if (index < 0)
                    throw new IndexOutOfRangeException();

                return _value[index];
            }
            set
            {
                if (index >= Elements)
                    throw new IndexOutOfRangeException();
                if (index < 0)
                    throw new IndexOutOfRangeException();

                _pendingWriteValue[index] = value;
                _hasPendingWrite[index] = true;
            }
        }

        /// <summary>
        /// Gets the specified value at the row and column index
        /// </summary>
        /// <param name="row">Row index</param>
        /// <param name="col">Column index</param>
        /// <returns>Value at the specified row and column</returns>
        /// <exception cref="DimensionsNotSetException">Thrown when the dimensions haven't been set before accessing this property</exception>
        /// <exception cref="IndexOutOfRangeException">Thrown when row*col > Elements or either index < 0</exception>
        public sbyte this[int row, int col]
        {
            get
            {
                if (Dim1 == 0 || Dim2 == 0)
                    throw new DimensionsNotSetException(Resources.ErrorStrings.DimensionsNotSet);

                if (row * col > Elements)
                    throw new IndexOutOfRangeException();

                if (row < 0 || col < 0)
                    throw new IndexOutOfRangeException();

                int idx = row * Dim2 + col;

                return this[idx];
            }
            set
            {
                if (Dim1 == 0 || Dim2 == 0)
                    throw new DimensionsNotSetException(Resources.ErrorStrings.DimensionsNotSet);

                if (row * col > Elements)
                    throw new IndexOutOfRangeException();

                if (row < 0 || col < 0)
                    throw new IndexOutOfRangeException();

                int idx = row * Dim2 + col;

                this[idx] = value;
            }
        }

        /// <summary>
        /// Gets the specified value at the row, column, and element index
        /// </summary>
        /// <param name="row">Row index</param>
        /// <param name="col">Column Index</param>
        /// <param name="element">Element Index</param>
        /// <returns>Value at the specified row, column and element index</returns>
        /// <exception cref="DimensionsNotSetException">Thrown when the dimensions haven't been set before accessing this property</exception>
        /// <exception cref="IndexOutOfRangeException">Thrown when row*col > Elements or either index < 0</exception>
        public sbyte this[int row, int col, int element]
        {
            get
            {
                if (Dim1 == 0 || Dim2 == 0 || Dim3 == 0)
                    throw new DimensionsNotSetException(Resources.ErrorStrings.DimensionsNotSet);

                if (row * col * element > Elements)
                    throw new IndexOutOfRangeException();

                if (row < 0 || col < 0 || element < 0)
                    throw new IndexOutOfRangeException();

                int idx = ((row * Dim2 + col) * Dim3 + element);

                return this[idx];
            }
            set
            {
                if (Dim1 == 0 || Dim2 == 0)
                    throw new DimensionsNotSetException(Resources.ErrorStrings.DimensionsNotSet);

                if (row * col * element > Elements)
                    throw new IndexOutOfRangeException();

                if (row < 0 || col < 0 || element < 0)
                    throw new IndexOutOfRangeException();

                int idx = ((row * Dim2 + col) * Dim3 + element);

                this[idx] = value;
            }
        }

        /// <summary>
        /// Gets the Logix type of the tag
        /// </summary>
        public override LogixTypes LogixType
        {
            get { return LogixTypes.SInt; }
        }

        /// <summary>
        /// Gets or Sets the LogixSINT as an object
        /// </summary>
        /// <remarks>
        /// <para>
        /// When using this to set the array value, the passed in array must be a single
        /// dimension array with the same number of elements as the LogixSINT tag.
        /// </para>
        /// <para>
        /// It is also good to note that this function makes use of the <see cref="Convert"/> class to convert
        /// from the passed in object to the appropriate data type. If the object is an array, this function is
        /// called on every element of the array, not just the first element. This can be a performance burden if
        /// setting large numbers of elements in this fashion. In that case it is recommended to cast the LogixTag
        /// to the correct object and use the indexor to set the values.
        /// </para>
        /// </remarks>
        /// <exception cref="TypeConversionException">Thrown when the value type cannot be converted to a signed byte</exception>
        /// <exception cref="ArrayLengthException">Thrown when the number of elements in the value array does not equal <see cref="Elements"/></exception>
        public override object ValueAsObject
        {
            get
            {
                if (Elements == 1)
                    return Value;
                else
                    return _value;
            }
            set
            {
                if (Elements == 1)
                {
                    try
                    {
                        Value = Convert.ToSByte(value);
                    }
                    catch (Exception e)
                    {
                        throw new TypeConversionException(Resources.ErrorStrings.TypeConversionError);
                    }
                }
                else
                {
                    //Ok, first we have to verify that it's an array
                    Array valueArray = value as Array;
                    if (valueArray != null)
                    {
                        if (valueArray.Length != Elements)
                            throw new ArrayLengthException(Resources.ErrorStrings.ArrayLengthException);
                        try
                        {
                            for (int i = 0; i < valueArray.Length; i++)
                            {
                                this[i] = Convert.ToSByte(valueArray.GetValue(i));
                            }
                        }
                        catch (Exception e)
                        {
                            throw new TypeConversionException(Resources.ErrorStrings.TypeConversionError);
                        }

                    }
                    else
                        throw new TypeConversionException(Resources.ErrorStrings.TypeConversionError);
                }
            }
        }

        #endregion

        #region Construction / Deconstruction

        /// <summary>
        /// Creates a new SINT Logix Tag
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor.</param>
        public LogixSINT(string TagAddress)
            : base(TagAddress)
        {
            
        }

        /// <summary>
        /// Creates a new SINT Logix Tag on the specified Processor
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor</param>
        /// <param name="Processor">Processor where the tag resides</param>
        public LogixSINT(string TagAddress, LogixProcessor Processor)
            : base(TagAddress, Processor)
        {

        }

        /// <summary>
        /// Creates a new INT Logix Tag on the specified Processor with the number of Elements
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor</param>
        /// <param name="Processor">Processor where the tag resides</param>
        /// <param name="ElementCount">Number of elements to read</param>
        public LogixSINT(string TagAddress, LogixProcessor Processor, ushort ElementCount)
            : base(TagAddress, Processor, ElementCount)
        {

        }

        #endregion

        #region Public Methods



        #endregion

        #region Protected / Internal Methods
		
#if MONO
		internal override bool OnDataUpdated(byte[] data, uint byteOffset)
#else
        internal override bool OnDataUpdated(byte[] data, uint byteOffset = 0)
#endif
		{
            //Let's mask off the lower part of the data type...
            ushort tempType = (ushort)(DataType & 0xFFF);
            bool canRaiseEvent = false;

            if (tempType == 0xC2)
            {
                //16 Bit Signed Int

                uint startElement = byteOffset / 4;
                int dataPos = 0;
                sbyte temp = 0;
                //Might be an array though...
                for (uint i = startElement; i < Elements; i++)
                {
                    temp = (unchecked((sbyte)(data[dataPos])));
                    _lastWriteChanged |= (_value[i] != temp);
                    _value[i] = temp;
                    dataPos += 1;
                    if (i == Elements - 1)
                        canRaiseEvent = true;
                    if (dataPos >= data.Length)
                        break;
                }
            }
            else
            {
                //Who knows???
                LastError = Resources.ErrorStrings.TypeMismatch + "0x" + tempType.ToString("X2");
                LastErrorNumber = (int)LogixErrors.TypeMismatch;
            }

            if (canRaiseEvent && _lastWriteChanged)
            {
                _lastWriteChanged = false;
                RaiseTagDataChanged(this);
                return true;
            }

            return false;
        }

        internal override byte[] OnDataWrite()
        {
            byte[] retVal = new byte[Elements];

            for (int i = 0; i < Elements; i++)
            {
                Buffer.BlockCopy(BitConverter.GetBytes(_value[i]), 0, retVal, i, 1);
            }

            return retVal;
        }

        internal override bool HasPendingWrite()
        {
            return PendingWriteCount() > 0;
        }

        internal override void ClearPendingWrite()
        {
            for (int i = 0; i < Elements; i++)
                _hasPendingWrite[i] = false;
        }
		
#if MONO
		internal override void Initialize(object InitData)
#else
        internal override void Initialize(object InitData = null)
#endif
		{
            _value = new sbyte[Elements];
            _pendingWriteValue = new sbyte[Elements];
            _hasPendingWrite = new bool[Elements];
        }

        internal override byte[] GetWriteData(out int byteAlignment)
        {
            List<byte> retVal = new List<byte>();
            byteAlignment = 2;

            if (Elements == 1)
            {
                retVal.AddRange(BitConverter.GetBytes(_pendingWriteValue[0]));
            }
            else
            {
                for (int i = 0; i < Elements; i++)
                {
                    if (_hasPendingWrite[i])
                        retVal.AddRange(BitConverter.GetBytes(_pendingWriteValue[i]));
                    else
                        retVal.AddRange(BitConverter.GetBytes(_value[i]));
                }

            }

            return retVal.ToArray();
        }

        #endregion

        #region Private Methods

        private int PendingWriteCount()
        {
            int count = 0;
            for (int i = 0; i < Elements; i++)
            {
                if (_hasPendingWrite[i])
                    count++;
            }

            return count;
        }

        #endregion

        #region Conversion Methods

        public static implicit operator sbyte(LogixSINT tag)
        {
            return tag[0];
        }

        #endregion

    }

    /// <summary>
    /// Represents a type of LINT in a Logix Processor
    /// </summary>
    public sealed class LogixLINT : LogixTag
    {
        
        #region Fields

        private long[] _value = new long[] { 0 };
        private long[] _pendingWriteValue = new long[] { 0 };
        private bool[] _hasPendingWrite = new bool[] { false };
        private bool _lastWriteChanged = false;
        
        #endregion

        #region Properties

        /// <summary>
        /// Gets or Sets the Value of the tag
        /// </summary>
        public long Value
        {
            get
            {
                return _value[0];
            }
            set
            {
                _pendingWriteValue[0] = value;
                _hasPendingWrite[0] = true;
            }
        }

        /// <summary>
        /// Gets or Sets the tag at the specified index
        /// </summary>
        /// <param name="index">Index to get or set</param>
        /// <returns>Value at the specified index</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when index >= Elements or index < 0</exception>
        public long this[int index]
        {
            get
            {
                if (index >= Elements)
                    throw new IndexOutOfRangeException();
                if (index < 0)
                    throw new IndexOutOfRangeException();

                return _value[index];
            }
            set
            {
                if (index >= Elements)
                    throw new IndexOutOfRangeException();
                if (index < 0)
                    throw new IndexOutOfRangeException();

                _pendingWriteValue[index] = value;
                _hasPendingWrite[index] = true;
            }
        }

        /// <summary>
        /// Gets the specified value at the row and column index
        /// </summary>
        /// <param name="row">Row index</param>
        /// <param name="col">Column index</param>
        /// <returns>Value at the specified row and column</returns>
        /// <exception cref="DimensionsNotSetException">Thrown when the dimensions haven't been set before accessing this property</exception>
        /// <exception cref="IndexOutOfRangeException">Thrown when row*col > Elements or either index < 0</exception>
        public long this[int row, int col]
        {
            get
            {
                if (Dim1 == 0 || Dim2 == 0)
                    throw new DimensionsNotSetException(Resources.ErrorStrings.DimensionsNotSet);

                if (row * col > Elements)
                    throw new IndexOutOfRangeException();

                if (row < 0 || col < 0)
                    throw new IndexOutOfRangeException();

                int idx = row * Dim2 + col;

                return this[idx];
            }
            set
            {
                if (Dim1 == 0 || Dim2 == 0)
                    throw new DimensionsNotSetException(Resources.ErrorStrings.DimensionsNotSet);

                if (row * col > Elements)
                    throw new IndexOutOfRangeException();

                if (row < 0 || col < 0)
                    throw new IndexOutOfRangeException();

                int idx = row * Dim2 + col;

                this[idx] = value;
            }
        }

        /// <summary>
        /// Gets the specified value at the row, column, and element index
        /// </summary>
        /// <param name="row">Row index</param>
        /// <param name="col">Column Index</param>
        /// <param name="element">Element Index</param>
        /// <returns>Value at the specified row, column and element index</returns>
        /// <exception cref="DimensionsNotSetException">Thrown when the dimensions haven't been set before accessing this property</exception>
        /// <exception cref="IndexOutOfRangeException">Thrown when row*col > Elements or either index < 0</exception>
        public long this[int row, int col, int element]
        {
            get
            {
                if (Dim1 == 0 || Dim2 == 0 || Dim3 == 0)
                    throw new DimensionsNotSetException(Resources.ErrorStrings.DimensionsNotSet);

                if (row * col * element > Elements)
                    throw new IndexOutOfRangeException();

                if (row < 0 || col < 0 || element < 0)
                    throw new IndexOutOfRangeException();

                int idx = ((row * Dim2 + col) * Dim3 + element);

                return this[idx];
            }
            set
            {
                if (Dim1 == 0 || Dim2 == 0)
                    throw new DimensionsNotSetException(Resources.ErrorStrings.DimensionsNotSet);

                if (row * col * element > Elements)
                    throw new IndexOutOfRangeException();

                if (row < 0 || col < 0 || element < 0)
                    throw new IndexOutOfRangeException();

                int idx = ((row * Dim2 + col) * Dim3 + element);

                this[idx] = value;
            }
        }

        /// <summary>
        /// Gets the Logix type of the tag
        /// </summary>
        public override LogixTypes LogixType
        {
            get { return LogixTypes.LInt; }
        }

        /// <summary>
        /// Gets or Sets the LogixLINT as an object
        /// </summary>
        /// <remarks>
        /// <para>
        /// When using this to set the array value, the passed in array must be a single
        /// dimension array with the same number of elements as the LogixLINT tag.
        /// </para>
        /// <para>
        /// It is also good to note that this function makes use of the <see cref="Convert"/> class to convert
        /// from the passed in object to the appropriate data type. If the object is an array, this function is
        /// called on every element of the array, not just the first element. This can be a performance burden if
        /// setting large numbers of elements in this fashion. In that case it is recommended to cast the LogixTag
        /// to the correct object and use the indexor to set the values.
        /// </para>
        /// </remarks>
        /// <exception cref="TypeConversionException">Thrown when the value type cannot be converted to a long</exception>
        /// <exception cref="ArrayLengthException">Thrown when the number of elements in the value array does not equal <see cref="Elements"/></exception>
        public override object ValueAsObject
        {
            get
            {
                if (Elements == 1)
                    return Value;
                else
                    return _value;
            }
            set
            {
                if (Elements == 1)
                {
                    try
                    {
                        Value = Convert.ToInt64(value);
                    }
                    catch (Exception e)
                    {
                        throw new TypeConversionException(Resources.ErrorStrings.TypeConversionError);
                    }
                }
                else
                {
                    //Ok, first we have to verify that it's an array
                    Array valueArray = value as Array;
                    if (valueArray != null)
                    {
                        if (valueArray.Length != Elements)
                            throw new ArrayLengthException(Resources.ErrorStrings.ArrayLengthException);
                        try
                        {
                            for (int i = 0; i < valueArray.Length; i++)
                            {
                                this[i] = Convert.ToInt64(valueArray.GetValue(i));
                            }
                        }
                        catch (Exception e)
                        {
                            throw new TypeConversionException(Resources.ErrorStrings.TypeConversionError);
                        }

                    }
                    else
                        throw new TypeConversionException(Resources.ErrorStrings.TypeConversionError);
                }
            }
        }

        #endregion

        #region Construction / Deconstruction

        /// <summary>
        /// Creates a new LINT Logix Tag
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor.</param>
        public LogixLINT(string TagAddress)
            : base(TagAddress)
        {
            
        }

        /// <summary>
        /// Creates a new LINT Logix Tag on the specified Processor
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor</param>
        /// <param name="Processor">Processor where the tag resides</param>
        public LogixLINT(string TagAddress, LogixProcessor Processor)
            : base(TagAddress, Processor)
        {

        }

        /// <summary>
        /// Creates a new LINT Logix Tag on the specified Processor with the number of Elements
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor</param>
        /// <param name="Processor">Processor where the tag resides</param>
        /// <param name="ElementCount">Number of elements to read</param>
        public LogixLINT(string TagAddress, LogixProcessor Processor, ushort ElementCount)
            : base(TagAddress, Processor, ElementCount)
        {

        }

        #endregion

        #region Public Methods



        #endregion

        #region Protected / Internal Methods
		
#if MONO
		internal override bool OnDataUpdated(byte[] data, uint byteOffset)
#else
        internal override bool OnDataUpdated(byte[] data, uint byteOffset = 0)
#endif
		{
            //Let's mask off the lower part of the data type...
            ushort tempType = (ushort)(DataType & 0xFFF);
            bool canRaiseEvent = false;

            if (tempType == 0xC5)
            {
                //64 Bit Signed Int

                uint startElement = byteOffset / 8;
                int dataPos = 0;
                long temp = 0;

                //Might be an array though...
                for (uint i = startElement; i < Elements; i++)
                {
                    temp = BitConverter.ToInt64(data, dataPos);
                    _lastWriteChanged |= (_value[i] != temp);
                    _value[i] = temp;
                    dataPos += 8;
                    if (i == Elements - 1)
                        canRaiseEvent = true;
                    if (dataPos >= data.Length)
                        break;
                }
            }
            else
            {
                //Who knows???
                LastError = Resources.ErrorStrings.TypeMismatch + "0x" + tempType.ToString("X2");
                LastErrorNumber = (int)LogixErrors.TypeMismatch;
            }

            if (canRaiseEvent && _lastWriteChanged)
            {
                _lastWriteChanged = false;
                RaiseTagDataChanged(this);
                return true;
            }

            return false;
        }

        internal override byte[] OnDataWrite()
        {
            byte[] retVal = new byte[Elements * 8];

            for (int i = 0; i < Elements; i++)
            {
                Buffer.BlockCopy(BitConverter.GetBytes(_value[i]), 0, retVal, i * 8, 8);
            }

            return retVal;
        }

        internal override bool HasPendingWrite()
        {
            return PendingWriteCount() > 0;
        }

        internal override void ClearPendingWrite()
        {
            for (int i = 0; i < Elements; i++)
                _hasPendingWrite[i] = false;
        }
		
#if MONO
		internal override void Initialize(object InitData)
#else
        internal override void Initialize(object InitData = null)
#endif
		{
            _value = new long[Elements];
            _pendingWriteValue = new long[Elements];
            _hasPendingWrite = new bool[Elements];
        }

        internal override byte[] GetWriteData(out int byteAlignment)
        {
            List<byte> retVal = new List<byte>();
            byteAlignment = 8;

            if (Elements == 1)
            {
                retVal.AddRange(BitConverter.GetBytes(_pendingWriteValue[0]));
            }
            else
            {
                for (int i = 0; i < Elements; i++)
                {
                    if (_hasPendingWrite[i])
                        retVal.AddRange(BitConverter.GetBytes(_pendingWriteValue[i]));
                    else
                        retVal.AddRange(BitConverter.GetBytes(_value[i]));
                }

            }

            return retVal.ToArray();
        }

        #endregion

        #region Private Methods

        private int PendingWriteCount()
        {
            int count = 0;
            for (int i = 0; i < Elements; i++)
            {
                if (_hasPendingWrite[i])
                    count++;
            }

            return count;
        }

        #endregion

        #region Conversion Methods

        public static implicit operator long(LogixLINT tag)
        {
            return tag[0];
        }

        #endregion

    }

    /// <summary>
    /// Represents a type of REAL in a Logix Processor
    /// </summary>
    public sealed class LogixREAL : LogixTag
    {
        
        #region Fields

        private float[] _value = new float[] { 0 };
        private float[] _pendingWriteValue = new float[] { 0 };
        private bool[] _hasPendingWrite = new bool[] { false };
        private bool _lastWriteChanged = false;
        
        #endregion

        #region Properties

        /// <summary>
        /// Gets or Sets the Value of the tag
        /// </summary>
        public float Value
        {
            get
            {
                return _value[0];
            }
            set
            {
                _pendingWriteValue[0] = value;
                _hasPendingWrite[0] = true;
            }
        }

        /// <summary>
        /// Gets or Sets the tag at the specified index
        /// </summary>
        /// <param name="index">Index to get or set</param>
        /// <returns>Value at the specified index</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when index >= Elements or index < 0</exception>
        public float this[int index]
        {
            get
            {
                if (index >= Elements)
                    throw new IndexOutOfRangeException();
                if (index < 0)
                    throw new IndexOutOfRangeException();

                return _value[index];
            }
            set
            {
                if (index >= Elements)
                    throw new IndexOutOfRangeException();
                if (index < 0)
                    throw new IndexOutOfRangeException();

                _pendingWriteValue[index] = value;
                _hasPendingWrite[index] = true;
            }
        }

        /// <summary>
        /// Gets the specified value at the row and column index
        /// </summary>
        /// <param name="row">Row index</param>
        /// <param name="col">Column index</param>
        /// <returns>Value at the specified row and column</returns>
        /// <exception cref="DimensionsNotSetException">Thrown when the dimensions haven't been set before accessing this property</exception>
        /// <exception cref="IndexOutOfRangeException">Thrown when row*col > Elements or either index < 0</exception>
        public float this[int row, int col]
        {
            get
            {
                if (Dim1 == 0 || Dim2 == 0)
                    throw new DimensionsNotSetException(Resources.ErrorStrings.DimensionsNotSet);

                if (row * col > Elements)
                    throw new IndexOutOfRangeException();

                if (row < 0 || col < 0)
                    throw new IndexOutOfRangeException();

                int idx = row * Dim2 + col;

                return this[idx];
            }
            set
            {
                if (Dim1 == 0 || Dim2 == 0)
                    throw new DimensionsNotSetException(Resources.ErrorStrings.DimensionsNotSet);

                if (row * col > Elements)
                    throw new IndexOutOfRangeException();

                if (row < 0 || col < 0)
                    throw new IndexOutOfRangeException();

                int idx = row * Dim2 + col;

                this[idx] = value;
            }
        }

        /// <summary>
        /// Gets the specified value at the row, column, and element index
        /// </summary>
        /// <param name="row">Row index</param>
        /// <param name="col">Column Index</param>
        /// <param name="element">Element Index</param>
        /// <returns>Value at the specified row, column and element index</returns>
        /// <exception cref="DimensionsNotSetException">Thrown when the dimensions haven't been set before accessing this property</exception>
        /// <exception cref="IndexOutOfRangeException">Thrown when row*col > Elements or either index < 0</exception>
        public float this[int row, int col, int element]
        {
            get
            {
                if (Dim1 == 0 || Dim2 == 0 || Dim3 == 0)
                    throw new DimensionsNotSetException(Resources.ErrorStrings.DimensionsNotSet);

                if (row * col * element > Elements)
                    throw new IndexOutOfRangeException();

                if (row < 0 || col < 0 || element < 0)
                    throw new IndexOutOfRangeException();

                int idx = ((row * Dim2 + col) * Dim3 + element);

                return this[idx];
            }
            set
            {
                if (Dim1 == 0 || Dim2 == 0)
                    throw new DimensionsNotSetException(Resources.ErrorStrings.DimensionsNotSet);

                if (row * col * element > Elements)
                    throw new IndexOutOfRangeException();

                if (row < 0 || col < 0 || element < 0)
                    throw new IndexOutOfRangeException();

                int idx = ((row * Dim2 + col) * Dim3 + element);

                this[idx] = value;
            }
        }

        /// <summary>
        /// Gets the Logix type of the tag
        /// </summary>
        public override LogixTypes LogixType
        {
            get { return LogixTypes.Real; }
        }

        /// <summary>
        /// Gets or Sets the LogixREAL as an object
        /// </summary>
        /// <remarks>
        /// <para>
        /// When using this to set the array value, the passed in array must be a single
        /// dimension array with the same number of elements as the LogixREAL tag.
        /// </para>
        /// <para>
        /// It is also good to note that this function makes use of the <see cref="Convert"/> class to convert
        /// from the passed in object to the appropriate data type. If the object is an array, this function is
        /// called on every element of the array, not just the first element. This can be a performance burden if
        /// setting large numbers of elements in this fashion. In that case it is recommended to cast the LogixTag
        /// to the correct object and use the indexor to set the values.
        /// </para>
        /// </remarks>
        /// <exception cref="TypeConversionException">Thrown when the value type cannot be converted to a float</exception>
        /// <exception cref="ArrayLengthException">Thrown when the number of elements in the value array does not equal <see cref="Elements"/></exception>
        public override object ValueAsObject
        {
            get
            {
                if (Elements == 1)
                    return Value;
                else
                    return _value;
            }
            set
            {
                if (Elements == 1)
                {
                    try
                    {
                        Value = Convert.ToSingle(value);
                    }
                    catch (Exception e)
                    {
                        throw new TypeConversionException(Resources.ErrorStrings.TypeConversionError);
                    }
                }
                else
                {
                    //Ok, first we have to verify that it's an array
                    Array valueArray = value as Array;
                    if (valueArray != null)
                    {
                        if (valueArray.Length != Elements)
                            throw new ArrayLengthException(Resources.ErrorStrings.ArrayLengthException);
                        try
                        {
                            for (int i = 0; i < valueArray.Length; i++)
                            {
                                this[i] = Convert.ToSingle(valueArray.GetValue(i));
                            }
                        }
                        catch (Exception e)
                        {
                            throw new TypeConversionException(Resources.ErrorStrings.TypeConversionError);
                        }

                    }
                    else
                        throw new TypeConversionException(Resources.ErrorStrings.TypeConversionError);
                }
            }
        }

        #endregion

        #region Construction / Deconstruction

        /// <summary>
        /// Creates a new REAL Logix Tag
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor.</param>
        public LogixREAL(string TagAddress)
            : base(TagAddress)
        {
            
        }

        /// <summary>
        /// Creates a new REAL Logix Tag on the specified Processor
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor</param>
        /// <param name="Processor">Processor where the tag resides</param>
        public LogixREAL(string TagAddress, LogixProcessor Processor)
            : base(TagAddress, Processor)
        {

        }

        /// <summary>
        /// Creates a new REAL Logix Tag on the specified Processor with the number of Elements
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor</param>
        /// <param name="Processor">Processor where the tag resides</param>
        /// <param name="ElementCount">Number of elements to read</param>
        public LogixREAL(string TagAddress, LogixProcessor Processor, ushort ElementCount)
            : base(TagAddress, Processor, ElementCount)
        {

        }

        #endregion

        #region Public Methods



        #endregion

        #region Internal Methods
		
#if MONO
		internal override void Initialize(object InitData)
#else
        internal override void Initialize(object InitData = null)
#endif
		{
            _value = new float[Elements];
            _pendingWriteValue = new float[Elements];
            _hasPendingWrite = new bool[Elements];
        }
		
#if MONO
		internal override bool OnDataUpdated(byte[] data, uint byteOffset)
#else
        internal override bool OnDataUpdated(byte[] data, uint byteOffset = 0)
#endif
		{
            //Let's mask off the lower part of the data type...
            ushort tempType = (ushort)(DataType & 0xFFF);
            bool canRaiseEvent = false;

            if (tempType == 0xCA)
            {
                //32 Bit Signed Float

                uint startElement = byteOffset / 4;
                int dataPos = 0;
                float temp = 0;

                //Might be an array though...
                for (uint i = startElement; i < Elements; i++)
                {
                    temp = BitConverter.ToSingle(data, dataPos);
                    _lastWriteChanged |= (_value[i] != temp);
                    _value[i] = temp;
                    dataPos += 4;
                    if (i == Elements - 1)
                        canRaiseEvent = true;
                    if (dataPos >= data.Length)
                        break;
                }
            }
            else
            {
                //Who knows???
                LastError = Resources.ErrorStrings.TypeMismatch + "0x" + tempType.ToString("X2");
                LastErrorNumber = (int)LogixErrors.TypeMismatch;
            }

            if (canRaiseEvent && _lastWriteChanged)
            {
                _lastWriteChanged = false;
                RaiseTagDataChanged(this);
                return true;
            }

            return false;
        }

        internal override byte[] OnDataWrite()
        {
            byte[] retVal = new byte[Elements * 4];

            for (int i = 0; i < Elements; i++)
            {
                Buffer.BlockCopy(BitConverter.GetBytes(_value[i]), 0, retVal, i * 4, 4);
            }

            return retVal;
        }
        
        internal override bool HasPendingWrite()
        {
            return PendingWriteCount() > 0;
        }

        internal override void ClearPendingWrite()
        {
            for (int i = 0; i < Elements; i++)
                _hasPendingWrite[i] = false;
        }

        internal override byte[] GetWriteData(out int byteAlignment)
        {
            List<byte> retVal = new List<byte>();
            byteAlignment = 4;

            if (Elements == 1)
            {
                retVal.AddRange(BitConverter.GetBytes(_pendingWriteValue[0]));
            }
            else
            {
                for (int i = 0; i < Elements; i++)
                {
                    if (_hasPendingWrite[i])
                        retVal.AddRange(BitConverter.GetBytes(_pendingWriteValue[i]));
                    else
                        retVal.AddRange(BitConverter.GetBytes(_value[i]));
                }
                    
            }

            return retVal.ToArray();
        }

        #endregion

        #region Private Methods

        private int PendingWriteCount()
        {
            int count = 0;
            for (int i = 0; i < Elements; i++)
            {
                if (_hasPendingWrite[i])
                    count++;
            }

            return count;
        }

        #endregion

        #region Conversion Methods

        public static implicit operator float(LogixREAL tag)
        {
            return tag[0];
        }

        #endregion

    }

    #endregion

    #region UDT Support

    internal class UDTItem
    {
        #region Fields

        private TemplateInfo _templateInfo;
        private byte[] _data;
        private byte[] _pendingData;
        private bool _hasPendingWrite;
        private bool _writeDataInitd = false;

        #endregion

        #region Properties

        public object this[string name]
        {
            get { return GetMemberValue(name); }
            set { SetMemberValue(name, value); }
        }

        #endregion

        #region Construction / Deconstruction

        public UDTItem(TemplateInfo info)
        {
            _templateInfo = info;
            _data = new byte[_templateInfo.TagSize];
            _pendingData = new byte[_templateInfo.TagSize];
            _hasPendingWrite = false;
        }

        #endregion

        #region Public Methods

        public bool Update(byte[] newData, uint offset)
        {
            //This function returns true so that the tag knows if it needs
            //to raise the changed event or not...
            //The new data should be just as long as the old data...

            bool changed = !UtilityBelt.CompareArrays(_data, newData, offset);

            int bytesToCopy = (int)(_data.Length - offset);
            if (bytesToCopy > newData.Length)
                bytesToCopy = newData.Length;

            if (bytesToCopy > 0)
                Buffer.BlockCopy(newData, 0, _data, (int)offset, bytesToCopy);

            if (!_writeDataInitd)
            {
                //Need to find out if we got a full set of data first
                if (offset + bytesToCopy >= _data.Length)
                {
                    _writeDataInitd = true;
                    Buffer.BlockCopy(_data, 0, _pendingData, 0, _data.Length);
                }
            }

            return changed;
        }

        public void UpdatePendingData(byte[] newData)
        {
            //The data lengths must match...
            if (newData.Length != _pendingData.Length)
                throw new ArrayLengthException(Resources.ErrorStrings.ArrayLengthException);

            Buffer.BlockCopy(newData, 0, _pendingData, 0, _pendingData.Length);
        }

        public MemberInfo GetMemberInfo(string name)
        {
            for (int i = 0; i < _templateInfo.MemberInfo.Count; i++)
            {
                if (string.Compare(name, _templateInfo.MemberInfo[i].MemberName, true) == 0)
                {
                    //Got it...
                    return _templateInfo.MemberInfo[i];
                }
            }

            return null;        //Could not find the member
        }

        public object GetMemberValue(string name)
        {
            //We need to get some info about the offset of this tag...
            MemberInfo mi = GetMemberInfo(name);

            if (mi == null)
                throw new UDTMemberNotFoundException(Resources.ErrorStrings.UDTMemberNotFound);

            //Now depending on the type we have to convert it to
            //the original type. For now if it's an embedded struct
            //we'll just return the bytes instead of any other data
            bool isArray = (mi.MemberType & 0x6000) > 0;
            ushort realType = (ushort)((mi.MemberType & 0x00FF));
            switch ((CIPType)realType)
            {
                case CIPType.BITS:
                    return TypeConverter.GetBoolArray(TypeConverter.GetInt32Array(_data, mi.MemberOffset, mi.MemberSize));
                case CIPType.BOOL:
                    if ((_data[mi.MemberOffset] & (0x01 << mi.Info)) > 0)
                        return true;
                    return false;
                case CIPType.SINT:
                    if (isArray)
                    {
                        //Info has the number of bytes...
                        return TypeConverter.GetByteArray(_data, mi.MemberOffset, mi.Info);
                    }
                    else
                        return _data[mi.MemberOffset];
                case CIPType.INT:
                    if (isArray)
                    {
                        return TypeConverter.GetShortArray(_data, mi.MemberOffset, mi.Info * 2);
                    }
                    else
                        return BitConverter.ToInt16(_data, mi.MemberOffset);
                case CIPType.DINT:
                    if (isArray)
                    {
                        return TypeConverter.GetInt32Array(_data, mi.MemberOffset, mi.Info * 4);
                    }
                    else
                        return BitConverter.ToInt32(_data, mi.MemberOffset);
                case CIPType.LINT:
                    if (isArray)
                    {
                        return TypeConverter.GetInt64Array(_data, mi.MemberOffset, mi.Info * 8);
                    }
                    else
                        return BitConverter.ToInt64(_data, mi.MemberOffset);
                case CIPType.REAL:
                    if (isArray)
                    {
                        return TypeConverter.GetFloatArray(_data, mi.MemberOffset, mi.Info * 4);
                    }
                    else
                        return BitConverter.ToSingle(_data, mi.MemberOffset);
                case CIPType.STRUCT:
                    return TypeConverter.GetByteArray(_data, mi.MemberOffset, mi.MemberSize);
                default:
                    //Unknown type...
                    return null;
            }
        }

        public void SetMemberValue(string name, object value)
        {
            //We need to get some info about the offset of this tag...
            MemberInfo mi = GetMemberInfo(name);

            if (mi == null)
                throw new UDTMemberNotFoundException(Resources.ErrorStrings.UDTMemberNotFound);

            //Now depending on the type we have to convert it to
            //the original type. For now if it's an embedded struct
            //we'll just return the bytes instead of any other data
            bool isArray = (mi.MemberType & 0x6000) > 0;
            ushort realType = (ushort)((mi.MemberType & 0x00FF));
            byte[] temp;

            try
            {
                //First we have to convert it to the right type...
                switch ((CIPType)realType)
                {
                    case CIPType.BITS:
                        if (isArray)
                        {
                            if (!(value is bool[]))
                                break;
                            temp = TypeConverter.GetBytes(TypeConverter.GetBoolArray(value as bool[]), mi.MemberSize);
                            Buffer.BlockCopy(temp, 0, _pendingData, mi.MemberOffset, mi.MemberSize);
                            _hasPendingWrite = true;
                        }
                        else
                        {
                            //Should just be one bool val, we need to convert it to an int32 and store
                            //it at the data location...
                            if (!(value is bool))
                                break;
                            //We have to set/unset the right bit...
                            int bitVal = mi.Info & 0x00FF;
                            if ((bool)value)
                                _pendingData[mi.MemberOffset] |= (byte)(0x01 << bitVal);
                            else
                                _pendingData[mi.MemberOffset] &= (byte)~(0x01 << bitVal);
                            _hasPendingWrite = true;
                        }
                        return;
                    case CIPType.BOOL:
                        //This cannot be an array
                        if (!(value is bool))
                            break;
                        if ((bool)value)
                        {
                            //Need to set the bit...
                            _pendingData[mi.MemberOffset] |= (byte)(0x01 << mi.Info);
                        }
                        else
                        {
                            //Need to clear the bit
                            _pendingData[mi.MemberOffset] &= (byte)~(0x01 << mi.Info);
                        }
                        _hasPendingWrite = true;
                        return;
                    case CIPType.SINT:
                        if (isArray)
                        {
                            if (!(value is sbyte[] || !(value is byte[])))
                                break;
                            Array src = value as Array;
                            if (src == null)
                                return;
                            Buffer.BlockCopy(src, 0, _pendingData, mi.MemberOffset, src.Length);
                        }
                        else
                        {
                            if (!(value is sbyte) || !(value is byte))
                                break;
                            _pendingData[mi.MemberOffset] = (unchecked((byte)value));
                        }
                        _hasPendingWrite = true;
                        return;
                    case CIPType.INT:
                        if (isArray)
                        {
                            if (!(value is short[]))
                                break;
                            short[] valArray = value as short[];
                            if (valArray == null)
                                break;
                            temp = TypeConverter.GetBytes(valArray, mi.MemberSize);
                            Buffer.BlockCopy(temp, 0, _pendingData, mi.MemberOffset, mi.MemberSize);
                        }
                        else
                        {
                            if (!(value is short))
                                break;
                            Buffer.BlockCopy(BitConverter.GetBytes((short)value), 0, _pendingData, mi.MemberOffset, 2);
                        }
                        _hasPendingWrite = true;
                        return;
                    case CIPType.DINT:
                        if (isArray)
                        {
                            if (!(value is int[]))
                                break;
                            int[] valArray = value as int[];
                            if (valArray == null)
                                break;
                            temp = TypeConverter.GetBytes(valArray, mi.MemberSize);
                            Buffer.BlockCopy(temp, 0, _pendingData, mi.MemberOffset, mi.MemberSize);
                        }
                        else
                        {
                            if (!(value is int))
                                break;
                            Buffer.BlockCopy(BitConverter.GetBytes((int)value), 0, _pendingData, mi.MemberOffset, 4);
                        }

                        _hasPendingWrite = true;
                        return;
                    case CIPType.LINT:
                        if (isArray)
                        {
                            if (!(value is long[]))
                                break;
                            long[] valArray = value as long[];
                            if (valArray == null)
                                break;
                            temp = TypeConverter.GetBytes(valArray, mi.MemberSize);
                            Buffer.BlockCopy(temp, 0, _pendingData, mi.MemberOffset, mi.MemberSize);
                        }
                        else
                        {
                            if (!(value is long))
                                break;
                            Buffer.BlockCopy(BitConverter.GetBytes((long)value), 0, _pendingData, mi.MemberOffset, 8);
                        }

                        _hasPendingWrite = true;
                        return;
                    case CIPType.REAL:
                        if (isArray)
                        {
                            if (!(value is float[]))
                                break;
                            float[] valArray = value as float[];
                            if (valArray == null)
                                break;
                            temp = TypeConverter.GetBytes(valArray, mi.MemberSize);
                            Buffer.BlockCopy(temp, 0, _pendingData, mi.MemberOffset, mi.MemberSize);
                        }
                        else
                        {
                            if (!(value is float))
                                break;
                            Buffer.BlockCopy(BitConverter.GetBytes((float)value), 0, _pendingData, mi.MemberOffset, 4);
                        }

                        _hasPendingWrite = true;
                        return;
                    case CIPType.STRUCT:
                        if (!(value is byte[]))
                            break;
                        Buffer.BlockCopy((byte[])value, 0, _pendingData, mi.MemberOffset, mi.MemberSize);
                        _hasPendingWrite = true;
                        return;
                    default:
                        //Unknown type...
                        return;
                }

                throw new TypeConversionException(Resources.ErrorStrings.TypeConversionError);

            }
            catch
            {
                throw new TypeConversionException(Resources.ErrorStrings.TypeConversionError);
            }

        }

        public void ClearPendingWrite()
        {
            _hasPendingWrite = false;
        }

        public bool HasPendingWrite()
        {
            return _hasPendingWrite;
        }

        public byte[] GetWriteData()
        {
            return _pendingData;
        }

        public static implicit operator byte[](UDTItem item)
        {
            return item._data;
        }

        #endregion

    }

    /// <summary>
    /// Represents a User Defined Type in a Logix Processor
    /// </summary>
    /// <remarks>In order to create a LogixUDT tag, use the <see cref="LogixTagFactory.CreateTag"/> method. The
    /// tag factory will automatically read the template data from the processor and return one of the built
    /// in types or this custom type.</remarks>
    public class LogixUDT : LogixTag
    {

        #region Fields

        private TemplateInfo _templateInfo;
        private UDTItem[] _udtItem;
        private bool _lastWriteChanged = false;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the LogixType of this tag
        /// </summary>
        public override LogixTypes LogixType
        {
            get { return LogixTypes.User_Defined; }
        }

        /// <summary>
        /// Gets or Sets the value of the specified member.
        /// </summary>
        /// <note type="caution">
        /// Caution must be taken when setting the UDT value. If the UDT value is an array and you address
        /// the element as such: udtType["myArray"][2] = 4, the system can't figure out that you've changed
        /// the 2nd element of the array. This means that the tag won't be updated when the group is written
        /// to the processor. To perform a write like that, first copy the entire array out, then set it like:
        /// <c>Array myArray = udtType["myArray"];</c>
        /// <c>myArray[2] = 4;</c>
        /// <c>udtType["myArray"] = myArray;</c>
        /// </note>
        /// <param name="name">Name of the element to retrieve</param>
        /// <returns>Typed object of the specified element</returns>
        public object this[string name]
        {
            get { return _udtItem[0][name]; }
            set { _udtItem[0][name] = value; }
        }

        /// <summary>
        /// Gets or Sets the value of the specified member.
        /// </summary>
        /// <note type="caution">
        /// Caution must be taken when setting the UDT value. If the UDT value is an array and you address
        /// the element as such: udtType["myArray"][2] = 4, the system can't figure out that you've changed
        /// the 2nd element of the array. This means that the tag won't be updated when the group is written
        /// to the processor. To perform a write like that, first copy the entire array out, then set it like:
        /// <c>Array myArray = udtType["myArray"];</c>
        /// <c>myArray[2] = 4;</c>
        /// <c>udtType["myArray"] = myArray;</c>
        /// </note>
        /// <param name="name">Name of the element to retrieve</param>
        /// <returns>Typed object of the specified element</returns>
        public object this[int idx, string name]
        {
            get
            {
                if (idx > Elements)
                    throw new IndexOutOfRangeException();

                return _udtItem[idx][name];
            }
            set
            {
                if (idx > Elements)
                    throw new IndexOutOfRangeException();

                _udtItem[idx][name] = value;
            }
        }

        /// <summary>
        /// Gets the name of the type structure
        /// </summary>
        public string TypeName
        {
            get { return _templateInfo.TemplateName; }
        }

        /// <summary>
        /// Gets a list of the structure member names
        /// </summary>
        public List<string> MemberNames
        {
            get
            {
                List<string> retVal = new List<string>();
                foreach (MemberInfo mi in _templateInfo.MemberInfo)
                    retVal.Add(mi.MemberName);

                return retVal;
            }
        }

        /// <summary>
        /// Gets or Sets the LogixUDT as an object
        /// </summary>
        /// <remarks>
        /// <para>
        /// When using this to set the array value, the passed in array must be a single
        /// dimension array with the same number of elements as the LogixUDT tag.
        /// </para>
        /// <para>
        /// This property only accepts or returns an array of bytes. These bytes represent
        /// the UDT in memory, with the padding. It is important to keep in mind how the
        /// UDT is structured when setting values using this method. It is recommended that
        /// the tag be cast to a LogixUDT first, then use the appropriate method to set the
        /// correct field.
        /// </para>
        /// </remarks>
        /// <exception cref="TypeConversionException">Thrown when the value type cannot be converted to a byte array</exception>
        /// <exception cref="ArrayLengthException">Thrown when the number of elements in the value array does not equal <see cref="Elements"/></exception>
        public override object ValueAsObject
        {
            get
            {
                if (Elements == 1)
                    return (byte[])_udtItem[0];
                else
                {
                    List<byte> myBytes = new List<byte>();
                    for (int i = 0; i < _udtItem.Length; i++)
                        myBytes.AddRange((byte[])_udtItem[i]);
                    return myBytes.ToArray();
                }
            }
            set
            {
                //This is trickier...
                byte[] byteArray = value as byte[];

                if (byteArray == null)
                    throw new TypeConversionException(Resources.ErrorStrings.TypeConversionError);

                if (Elements == 1)
                {
                    _udtItem[0].UpdatePendingData(byteArray);
                }
                else
                {
                    SetPendingData(byteArray);
                }

            }
        }

        #endregion

        #region Events



        #endregion

        #region Construction / Deconstruction

        /// <summary>
        /// Creates a new LogixUDT
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) of the tag on the processor</param>
        /// <param name="Processor">Processor that the tag belongs to</param>
        public LogixUDT(string TagAddress, LogixProcessor Processor)
            : base (TagAddress, Processor, 1, null)
        {

        }

        /// <summary>
        /// Creates a new User-Defined Type Logix Tag
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor.</param>
        /// <param name="TemplateInfo">Template information about the type</param>
        internal LogixUDT(string TagAddress, TemplateInfo TemplateInfo)
            : base(TagAddress)
        {
            _templateInfo = TemplateInfo;
        }

        /// <summary>
        /// Creates a new User-Defined Type Logix Tag on the specified Processor
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor</param>
        /// <param name="TemplateInfo">Template information about the type</param>
        /// <param name="Processor">Processor where the tag resides</param>
        internal LogixUDT(string TagAddress, TemplateInfo TemplateInfo, LogixProcessor Processor)
            : base(TagAddress, Processor, 1, TemplateInfo)
        {
            _templateInfo = TemplateInfo;
        }

        /// <summary>
        /// Creates a new User-Defined Type Tag on the specified Processor with the number of Elements
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor</param>
        /// <param name="TemplateInfo">Template information about the type</param>
        /// <param name="Processor">Processor where the tag resides</param>
        /// <param name="ElementCount">Number of elements to read</param>
        internal LogixUDT(string TagAddress, TemplateInfo TemplateInfo, LogixProcessor Processor, ushort ElementCount)
            : base(TagAddress, Processor, ElementCount, TemplateInfo)
        {
            _templateInfo = TemplateInfo;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the type for a particular member in the type
        /// </summary>
        /// <param name="MemberName">Name of the member</param>
        /// <returns><see cref="ControlLogixNET.LogixType.LogixTypes"/> or 
        /// <see cref="ControlLogixNET.LogixType.LogixTypes.Unknown"/> if 
        /// the member was not found</returns>
        public LogixTypes GetTypeForMember(string MemberName)
        {
            foreach (MemberInfo mi in _templateInfo.MemberInfo)
            {
                if (string.Compare(mi.MemberName, MemberName) == 0)
                    return mi.LogixType;
            }

            return LogixTypes.Unknown;
        }

        #endregion

        #region Private Methods

        private void SetPendingData(byte[] data)
        {
            //Need to find out which element this is in, and if it spans elements...
            int structSize = _templateInfo.TagSize;
            int element = 0;
            int offset = 0;

            int bytesRemaining = data.Length;
            int arrayOffset = 0;
            byte[] temp;

            while (bytesRemaining > 0)
            {
                int bytesToRead = structSize - offset;
                if (bytesToRead < bytesRemaining)
                {
                    temp = new byte[bytesToRead];
                    Buffer.BlockCopy(data, arrayOffset, temp, 0, bytesToRead);
                    arrayOffset += bytesToRead;
                    bytesRemaining -= bytesToRead;
                    _udtItem[element].UpdatePendingData(temp);
                    offset = 0;
                    element++;
                    if (element >= Elements)
                        bytesRemaining = 0;
                }
                else
                {
                    //Not enough data to complete the request...
                    temp = new byte[bytesRemaining];
                    Buffer.BlockCopy(data, arrayOffset, temp, 0, bytesRemaining);
                    _udtItem[element].UpdatePendingData(temp);
                    bytesRemaining = 0;
                }

            }

        }

        #endregion

        #region Internal Methods
		
#if MONO
		internal override bool OnDataUpdated(byte[] data, uint byteOffset)
#else
        internal override bool OnDataUpdated(byte[] data, uint byteOffset = 0)
#endif
		{
            //Need to find out which element this is in, and if it spans elements...
            int structSize = _templateInfo.TagSize;
            int element = (int)(byteOffset / structSize);
            int offset = (int)(byteOffset - (element * structSize));

            int bytesRemaining = data.Length - 2;
            int arrayOffset = 2;
            byte[] temp;
            bool canRaiseEvent = false;

            while (bytesRemaining > 0)
            {
                int bytesToRead = structSize - offset;
                if (bytesToRead < bytesRemaining)
                {
                    temp = new byte[bytesToRead];
                    Buffer.BlockCopy(data, arrayOffset, temp, 0, bytesToRead);
                    arrayOffset += bytesToRead;
                    bytesRemaining -= bytesToRead;
                    _lastWriteChanged |= _udtItem[element].Update(temp, (uint)offset);
                    offset = 0;
                    element++;
                    if (element >= Elements)
                        bytesRemaining = 0;
                }
                else
                {
                    //Not enough data to complete the request...
                    temp = new byte[bytesRemaining];
                    Buffer.BlockCopy(data, arrayOffset, temp, 0, bytesRemaining);
                    _lastWriteChanged |= _udtItem[element].Update(temp, (uint)offset);
                    bytesRemaining = 0;
                }

                if (element >= Elements - 1)
                    canRaiseEvent = true;
            }

            if (canRaiseEvent && _lastWriteChanged)
            {
                _lastWriteChanged = false;
                RaiseTagDataChanged(this);
                return true;
            }

            return false;
        }

        internal override byte[] OnDataWrite()
        {
            List<byte> retVal = new List<byte>();
            for (int i = 0; i < _udtItem.Length; i++)
                retVal.AddRange(_udtItem[i].GetWriteData());
            return retVal.ToArray();
        }

        internal override bool HasPendingWrite()
        {
            for (int i = 0; i < _udtItem.Length; i++)
            {
                if (_udtItem[i].HasPendingWrite())
                    return true;
            }

            return false;
        }

        internal override void ClearPendingWrite()
        {
            for (int i = 0; i < _udtItem.Length; i++)
                _udtItem[i].ClearPendingWrite();
        }
		
#if MONO
		internal override void Initialize(object InitData)
#else
        internal override void Initialize(object InitData = null)
#endif
		{
            if (InitData == null)
            {
                //need to get the template info
                _templateInfo = LogixTagFactory.GetTemplateInfo(Address, Processor, Elements);
            }
            else
                _templateInfo = (TemplateInfo)InitData;
            StructHandle = _templateInfo.TemplateHandle;
            _udtItem = new UDTItem[Elements];
            for (int i = 0; i < Elements; i++)
                _udtItem[i] = new UDTItem(_templateInfo);
        }

        internal override byte[] GetWriteData(out int byteAlignment)
        {
            byteAlignment = 1;
            return OnDataWrite();
        }

        #endregion
        
    }

    #endregion

    #region Major Types

    /// <summary>
    /// Represents a type of COUNTER in a Logix Processor
    /// </summary>
    public sealed class LogixCONTROL : LogixUDT
    {

        #region Properties

        /// <summary>
        /// Gets the <see cref="ControlLogixNET.LogixType.LogixTypes"/> of this tag
        /// </summary>
        public override LogixTypes LogixType
        {
            get
            {
                return LogixTypes.Control;
            }
        }

        /// <summary>
        /// Gets or Sets the Length member
        /// </summary>
        public int LEN
        {
            get { return (int)base["LEN"]; }
            set { base["LEN"] = value; }
        }

        /// <summary>
        /// Gets or Sets the POS member
        /// </summary>
        public int POS
        {
            get { return (int)base["POS"]; }
            set { base["POS"] = value; }
        }

        /// <summary>
        /// Gets or Sets the Enabled member
        /// </summary>
        public bool EN
        {
            get { return (bool)base["EN"]; }
            set { base["EN"] = value; }
        }

        /// <summary>
        /// Gets or Sets the EU member
        /// </summary>
        public bool EU
        {
            get { return (bool)base["EU"]; }
            set { base["EU"] = value; }
        }

        /// <summary>
        /// Gets or Sets the Done member
        /// </summary>
        public bool DN
        {
            get { return (bool)base["DN"]; }
            set { base["DN"] = value; }
        }

        /// <summary>
        /// Gets or Sets the EM member
        /// </summary>
        public bool EM
        {
            get { return (bool)base["EM"]; }
            set { base["EM"] = value; }
        }

        /// <summary>
        /// Gets or Sets the Error member
        /// </summary>
        public bool ER
        {
            get { return (bool)base["ER"]; }
            set { base["ER"] = value; }
        }

        /// <summary>
        /// Gets or Sets the UL member
        /// </summary>
        public bool UL
        {
            get { return (bool)base["UL"]; }
            set { base["UL"] = value; }
        }

        /// <summary>
        /// Gets or Sets the IN member
        /// </summary>
        public bool IN
        {
            get { return (bool)base["IN"]; }
            set { base["IN"] = value; }
        }

        /// <summary>
        /// Gets or Sets the FD member
        /// </summary>
        public bool FD
        {
            get { return (bool)base["FD"]; }
            set { base["FD"] = value; }
        }

        #endregion

        #region Construction / Deconstruction

        /// <summary>
        /// Creates a new CONTROL Logix Tag on the specified Processor
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor</param>
        /// <param name="TemplateInfo">Template information about the type</param>
        /// <param name="Processor">Processor where the tag resides</param>
        internal LogixCONTROL(string TagAddress, TemplateInfo TemplateInfo, LogixProcessor Processor)
            : base(TagAddress, TemplateInfo, Processor)
        {
            
        }

        /// <summary>
        /// Creates a new CONTROL Tag on the specified Processor with the number of Elements
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor</param>
        /// <param name="TemplateInfo">Template information about the type</param>
        /// <param name="Processor">Processor where the tag resides</param>
        /// <param name="ElementCount">Number of elements to read</param>
        internal LogixCONTROL(string TagAddress, TemplateInfo TemplateInfo, LogixProcessor Processor, ushort ElementCount)
            : base(TagAddress, TemplateInfo, Processor, ElementCount)
        {
            
        }

        #endregion

    }

    /// <summary>
    /// Represents a type of TIMER in a Logix Processor
    /// </summary>
    public sealed class LogixTIMER : LogixUDT
    {

        #region Properties

        /// <summary>
        /// Gets the <see cref="ControlLogixNET.LogixType.LogixTypes"/> of this tag
        /// </summary>
        public override LogixTypes LogixType
        {
            get
            {
                return LogixTypes.Timer;
            }
        }

        /// <summary>
        /// Gets or Sets the counter preset member
        /// </summary>
        public int PRE
        {
            get { return (int)base["PRE"]; }
            set { base["PRE"] = value; }
        }

        /// <summary>
        /// Gets or Sets the accumulated value member
        /// </summary>
        public int ACC
        {
            get { return (int)base["ACC"]; }
            set { base["ACC"] = value; }
        }

        /// <summary>
        /// Gets or Sets the enabled member
        /// </summary>
        public bool EN
        {
            get { return (bool)base["EN"]; }
            set { base["EN"] = value; }
        }

        /// <summary>
        /// Gets or Sets the timing operation flag
        /// </summary>
        public bool TT
        {
            get { return (bool)base["TT"]; }
            set { base["TT"] = value; }
        }

        /// <summary>
        /// Gets or Sets the done member
        /// </summary>
        public bool DN
        {
            get { return (bool)base["DN"]; }
            set { base["DN"] = value; }
        }
        
        #endregion
        
        #region Construction / Deconstruction

        /// <summary>
        /// Creates a new TIMER Logix Tag on the specified Processor
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor</param>
        /// <param name="TemplateInfo">Template information about the type</param>
        /// <param name="Processor">Processor where the tag resides</param>
        internal LogixTIMER(string TagAddress, TemplateInfo TemplateInfo, LogixProcessor Processor)
            : base(TagAddress, TemplateInfo, Processor)
        {
            
        }

        /// <summary>
        /// Creates a new TIMER Tag on the specified Processor with the number of Elements
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor</param>
        /// <param name="TemplateInfo">Template information about the type</param>
        /// <param name="Processor">Processor where the tag resides</param>
        /// <param name="ElementCount">Number of elements to read</param>
        internal LogixTIMER(string TagAddress, TemplateInfo TemplateInfo, LogixProcessor Processor, ushort ElementCount)
            : base(TagAddress, TemplateInfo, Processor, ElementCount)
        {
            
        }

        #endregion

    }

    /// <summary>
    /// Represents a type of COUNTER in a Logix Processor
    /// </summary>
    public sealed class LogixCOUNTER : LogixUDT
    {
        
        #region Properties

        /// <summary>
        /// Gets the <see cref="ControlLogixNET.LogixType.LogixTypes"/> of this tag
        /// </summary>
        public override LogixTypes LogixType
        {
            get
            {
                return LogixTypes.Counter;
            }
        }

        /// <summary>
        /// Gets or Sets the counter preset member
        /// </summary>
        public int PRE
        {
            get { return (int)base["PRE"]; }
            set { base["PRE"] = value; }
        }

        /// <summary>
        /// Gets or Sets the accumulated value member
        /// </summary>
        public int ACC
        {
            get { return (int)base["ACC"]; }
            set { base["ACC"] = value; }
        }

        /// <summary>
        /// Gets or Sets the count up enabled member
        /// </summary>
        public bool CU
        {
            get { return (bool)base["CU"]; }
            set { base["CU"] = value; }
        }

        /// <summary>
        /// Gets or Sets the count down enabled member
        /// </summary>
        public bool CD
        {
            get { return (bool)base["CD"]; }
            set { base["CD"] = value; }
        }

        /// <summary>
        /// Gets or Sets the done member
        /// </summary>
        public bool DN
        {
            get { return (bool)base["DN"]; }
            set { base["DN"] = value; }
        }

        /// <summary>
        /// Gets or Sets the overflow member
        /// </summary>
        public bool OV
        {
            get { return (bool)base["OV"]; }
            set { base["OV"] = value; }
        }

        /// <summary>
        /// Gets or Sets the underflow member
        /// </summary>
        public bool UN
        {
            get { return (bool)base["UN"]; }
            set { base["UN"] = value; }
        }
        
        #endregion
        
        #region Construction / Deconstruction

        /// <summary>
        /// Creates a new COUNTER Logix Tag on the specified Processor
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor</param>
        /// <param name="TemplateInfo">Template information about the type</param>
        /// <param name="Processor">Processor where the tag resides</param>
        internal LogixCOUNTER(string TagAddress, TemplateInfo TemplateInfo, LogixProcessor Processor)
            : base(TagAddress, TemplateInfo, Processor)
        {
            
        }

        /// <summary>
        /// Creates a new COUNTER Tag on the specified Processor with the number of Elements
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor</param>
        /// <param name="TemplateInfo">Template information about the type</param>
        /// <param name="Processor">Processor where the tag resides</param>
        /// <param name="ElementCount">Number of elements to read</param>
        internal LogixCOUNTER(string TagAddress, TemplateInfo TemplateInfo, LogixProcessor Processor, ushort ElementCount)
            : base(TagAddress, TemplateInfo, Processor, ElementCount)
        {
            
        }

        #endregion

    }

    /// <summary>
    /// Represents a type of STRING in a logix procesor
    /// </summary>
    public sealed class LogixSTRING : LogixUDT
    {

        #region Properties

        /// <summary>
        /// Gets the Logix type of this tag
        /// </summary>
        public override LogixTypes LogixType
        {
            get
            {
                return LogixTypes.String;
            }
        }

        /// <summary>
        /// Gets or Sets the length of the string data in bytes
        /// </summary>
        public int Len
        {
            get { return (int)base["LEN"]; }
            set { base["LEN"] = value; }
        }

        /// <summary>
        /// Gets or Sets the string data in bytes
        /// 
        /// <para>The size of this array can't be any more than
        /// 82 elements.</para>
        /// </summary>
        public byte[] Data
        {
            get { return (byte[])base["DATA"]; }
            set { base["DATA"] = value; }
        }

        /// <summary>
        /// Gets or Sets the string representation of this tag
        /// 
        /// <para>This property automatically trims the string to fit
        /// in the 82 character limit and sets the LEN field.</para>
        /// </summary>
        public string StringValue
        {
            get
            {
                byte[] chars = Data;
                int len = Len;
                return System.Text.ASCIIEncoding.ASCII.GetString(chars, 0, len);
            }
            set
            {
                if (value.Length > 82)
                    value = value.Substring(0, 82);
                int originalLen = value.Length;
                if (value.Length < 82)
                    value = value + new String('\0', (82 - value.Length));

                byte[] chars = System.Text.ASCIIEncoding.ASCII.GetBytes(value);
                Len = originalLen;
                Data = chars;
            }
        }

        #endregion

        #region Construction / Deconstruction

        /// <summary>
        /// Creates a new STRING Logix Tag on the specified Processor
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor</param>
        /// <param name="TemplateInfo">Template information about the type</param>
        /// <param name="Processor">Processor where the tag resides</param>
        internal LogixSTRING(string TagAddress, TemplateInfo TemplateInfo, LogixProcessor Processor)
            : base(TagAddress, TemplateInfo, Processor)
        {
            
        }

        /// <summary>
        /// Creates a new STRING Tag on the specified Processor with the number of Elements
        /// </summary>
        /// <param name="TagAddress">Address (Tag Name) in the processor</param>
        /// <param name="TemplateInfo">Template information about the type</param>
        /// <param name="Processor">Processor where the tag resides</param>
        /// <param name="ElementCount">Number of elements to read</param>
        internal LogixSTRING(string TagAddress, TemplateInfo TemplateInfo, LogixProcessor Processor, ushort ElementCount)
            : base(TagAddress, TemplateInfo, Processor, ElementCount)
        {
            
        }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return StringValue;
        }

        #endregion

    }

    #endregion

}
