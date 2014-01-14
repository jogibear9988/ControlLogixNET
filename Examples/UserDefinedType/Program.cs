using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlLogixNET;
using ControlLogixNET.LogixType;

namespace UserDefinedType
{
    class Program
    {
        /*
         * HOW TO USE THIS SAMPLE 
         * 
         * 1. First change the hostNameOrIp to the IP address or host name of your PLC
         * 2. Then change the path to be the path to your PLC, see comments below
         * 3. Create a new User Defined Type on the processor called CustomUDT
         * 4. Add the following members to the type:
         *      1. Enabled : BOOL
         *      2. UpperLimit : DINT
         *      3. LowerLimit : DINT
         *      4. RunningValue : REAL
         *      5. Over : BOOL
         *      6. Under : BOOL
         * 5. Create a new tag of type CustomUDT called myCustomUDT
         * 6. Run
         * 
        */
        static void Main(string[] args)
        {
            //First we create the processor object. Typically the path is the slot
            //number of the processor module in the backplane, but if your communications
            //card is not in the same chassis as your processor, this is the path through
            //the chassis to get to your processor. You will have to add a 1 for every
            //chassis you go through, for example:
            //Chassis 1: ENBT card in Slot 1 (slot is irrelavent), ControlNet Card in Slot 2
            //Chassis 2: L61 in Slot 4
            //Path would be: { 2, 1, 4 }
            //Basically it's the target slot, 1 for backplane, target slot, 1 for backplane...
            //until you get to the processor.
            string hostNameOrIp = "192.168.1.10";
            byte[] path = new byte[] { 1 };
            LogixProcessor processor = new LogixProcessor(hostNameOrIp, path);

            //The processor has to be connected before you add any tags or tag groups.
            if (!processor.Connect())
            {
                Console.WriteLine("Could not connect to the processor");
                Console.ReadKey(false);
                return;
            }

            Console.WriteLine("6D Systems LLC\n\n");

            //First create a group. Groups are much more efficient at reading and writing
            //large numbers of tags or complex tags like UDTs.
            LogixTagGroup myGroup = processor.CreateTagGroup("MyGroup");

            CustomUDT myCustomUDT = new CustomUDT("myCustomUDT", processor);
            myCustomUDT.TagValueUpdated += new ICommon.TagValueUpdateEventHandler(TagValueUpdated);

            //Add the tag to the group...
            myGroup.AddTag(myCustomUDT);

            //Set the group to auto update
            processor.EnableAutoUpdate(500);

            //Print out some structure information:
            PrintStructure(myCustomUDT);

            //Now wait for updates...
            Console.WriteLine("Change some data in the custom type, then hit Enter to quit");

            Console.ReadLine();

            processor.Disconnect();
        }

        static void TagValueUpdated(ICommon.ITag sender, ICommon.TagValueUpdateEventArgs e)
        {
            LogixUDT udtTag = sender as LogixUDT;

            if (udtTag != null)
                PrintStructure(udtTag);
        }

        private static void PrintStructure(LogixUDT structureTag)
        {
            List<string> memberNames = structureTag.MemberNames;
            Console.WriteLine("myUDT1 is a " + structureTag.TypeName + " with members:");
            foreach (string mName in memberNames)
            {
                LogixTypes memberType = structureTag.GetTypeForMember(mName);
                Console.WriteLine("\t" + mName + " : " + memberType.ToString() + " - Value: " + structureTag[mName].ToString());
            }
        }
    }

    /// <summary>
    /// Custom User Defined Type
    /// </summary>
    /// <remarks>
    /// This shows how to create a custom user defined type on the processor.
    /// </remarks>
    public class CustomUDT : LogixUDT
    {
        private LogixUDT _myUDTBase;

        public bool Enabled
        {
            get { return (bool)this["Enabled"]; }
            set { this["Enabled"] = value; }
        }

        public int UpperLimit
        {
            get { return (int)this["UpperLimit"]; }
            set { this["UpperLimit"] = value; }
        }

        public int LowerLimit
        {
            get { return (int)this["LowerLimit"]; }
            set { this["LowerLimit"] = value; }
        }

        public float RunningValue
        {
            get { return (float)this["RunningValue"]; }
            set { this["RunningValue"] = value; }
        }

        public bool Over
        {
            get { return (bool)this["Over"]; }
            set { this["Over"] = value; }
        }

        public bool Under
        {
            get { return (bool)this["Under"]; }
            set { this["Under"] = value; }
        }

        public CustomUDT(string TagAddress, LogixProcessor Processor)
            : base(TagAddress, Processor)
        {

        }
    }
}
