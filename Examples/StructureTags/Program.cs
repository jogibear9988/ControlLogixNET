using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlLogixNET;
using ControlLogixNET.LogixType;

namespace StructureTags
{
    class Program
    {
        /*
         * HOW TO USE THIS SAMPLE 
         * 
         * 1. First change the hostNameOrIp to the IP address or host name of your PLC
         * 2. Then change the path to be the path to your PLC, see comments below
         * 3. Create a user defined type tag in your processor called myUDT1
         * 4. Create an ALARM tag in your processor called myAlarm1
         * 5. Run
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

            //Ok, let's create the first tag which is some random user defined type
            LogixTag genericTag = LogixTagFactory.CreateTag("myUDT1", processor);
            LogixUDT udtTag = genericTag as LogixUDT;

            if (udtTag == null)
            {
                Console.WriteLine("The tag 'myUDT1' on the processor is not a structure tag");
                Console.WriteLine("Press any key to quit");
                Console.ReadKey(false);
                processor.Disconnect();
                return;
            }

            //Let's print out some information about the UDT
            PrintStructure(udtTag);

            //The value of any member can also be set with the tagName[memberName] = value syntax

            //Now let's get information about the alarm tag that was created...
            LogixTag genericAlarm = LogixTagFactory.CreateTag("myAlarm1", processor);
            LogixUDT alarmTag = genericAlarm as LogixUDT;

            if (alarmTag == null)
            {
                Console.WriteLine("The tag 'myAlarm1' is not a structure tag");
                Console.WriteLine("Press any key to quit");
                Console.ReadKey(false);
                processor.Disconnect();
                return;
            }

            //Print out information about it...
            PrintStructure(alarmTag);

            //Now, let's set up the tags in the group, set the group to auto update, and watch
            //for tag update events...
            myGroup.AddTag(udtTag);
            myGroup.AddTag(alarmTag);

            udtTag.TagValueUpdated += new ICommon.TagValueUpdateEventHandler(TagValueUpdated);
            alarmTag.TagValueUpdated += new ICommon.TagValueUpdateEventHandler(TagValueUpdated);

            processor.EnableAutoUpdate(500);

            Console.WriteLine("Press Enter to quit");

            Console.ReadLine();

            processor.Disconnect();
        }

        static void TagValueUpdated(ICommon.ITag sender, ICommon.TagValueUpdateEventArgs e)
        {
            LogixUDT logixUDT = sender as LogixUDT;

            if (logixUDT != null)
                PrintStructure(logixUDT);
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
}
