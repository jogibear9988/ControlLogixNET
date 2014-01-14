using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlLogixNET;

namespace Processor
{
    class Program
    {
        /*
         * HOW TO USE THIS SAMPLE 
         * 
         * 1. First change the hostNameOrIp to the IP address or host name of your PLC
         * 2. Then change the path to be the path to your PLC, see comments below
         * 3. Run
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
            byte[] path = new byte[] { 0 };
            LogixProcessor processor = new LogixProcessor(hostNameOrIp, path);

            //Connect to the PLC, you can create the events before or after the connect function
            if (!processor.Connect())
            {
                Console.WriteLine("Could not connect to the processor");
                Console.ReadKey(false);
                return;
            }
            
            //Create the events, the processor state is updated every second, and if there is
            //a change in either the fault state, key switch position, or processor state (RUN, PROGRAM, TEST),
            //then one of these events will be fired.
            processor.FaultStateChanged += new LogixFaultStateChangedEvent(processor_FaultStateChanged);
            processor.KeySwitchChanged += new LogixKeyPositionChangedEvent(processor_KeySwitchChanged);
            processor.ProcessorStateChanged += new LogixProcessorStateChangedEvent(processor_ProcessorStateChanged);

            Console.WriteLine("6D Systems LLC");
            Console.WriteLine("Processor State Example: Change the key switch, fault state, or processor\nmode to see a message displayed");
            Console.WriteLine("\nProcessor Information:\n" + processor);
            Console.WriteLine("\n\n");
            
            //The processor can, through source code, be put in Program mode or Run mode. This is useful
            //if you are developing a critical process where you want to be able to shut all the outputs
            //off on the PLC at one time. Mode changes only work if the processor key is in Remote

            //The .UserData field can be used to store any data you desire, and it will be persisted
            //with the processor object. This is useful, for example, for storing information about
            //a processor when it's in a dictionary...

            processor.UserData = "MainPLC_1";

            bool quitFlag = false;

            while (!quitFlag)
            {
                Console.WriteLine("\n\n=============================MENU=============================");
                Console.WriteLine("Press the 'P' key to put the processor in Program mode");
                Console.WriteLine("Press the 'R' key to put the processor in Run mode");
                Console.WriteLine("Press the 'U' key to display the processor UserData");
                Console.WriteLine("Press the 'T' key to display all the tags on the processor");
                Console.WriteLine("Press the 'Q' key to quit");
                Console.WriteLine("==============================================================");

                char key = Console.ReadKey(true).KeyChar;

                switch (key)
                {
                    case 'p':
                    case 'P':
                        Console.WriteLine("Setting processor to Program mode...");
                        processor.SetProgramMode();
                        break;
                    case 'r':
                    case 'R':
                        Console.WriteLine("Setting processor to Run mode...");
                        processor.SetRunMode();
                        break;
                    case 'u':
                    case 'U':
                        Console.WriteLine("UserData: " + (string)processor.UserData);
                        break;
                    case 't':
                    case 'T':
                        List<LogixTagInfo> tagInfo = processor.EnumerateTags();
                        if (tagInfo == null)
                        {
                            Console.WriteLine("No tags found");
                            break;
                        }
                        Console.WriteLine("There are " + tagInfo.Count + " tags...");
                        foreach (LogixTagInfo info in tagInfo)
                        {
                            string name = info.TagName;
                            if (info.Dimensions > 0)
                                name += "[" + info.Dimension1Size.ToString();
                            if (info.Dimensions > 1)
                                name += ", " + info.Dimension2Size.ToString();
                            if (info.Dimensions > 2)
                                name += ", " + info.Dimension3Size.ToString();
                            if (info.Dimensions > 0)
                                name += "]";
                            Console.WriteLine("\t" + name);
                        }
                        break;
                    case 'q':
                    case 'Q':
                        quitFlag = true;
                        break;
                    default:
                        break;
                }
            }

            //Always remember to disconnect the PLC. If you forget, the PLC won't allow you to reconnect
            //until the session times out. This is typically about 45-60 seconds.
            processor.Disconnect();
        }

        static void processor_ProcessorStateChanged(LogixProcessor sender, LogixProcessorStateChangedEventArgs e)
        {
            //This function will be called whenever the processor changes state. The processor state is information
            //like what mode it's in, if it has a communications fault, if it's in firmware update mode, etc.
            Console.WriteLine("Processor State Changed from " + e.OldState.ToString() + " to " + e.NewState.ToString());
        }

        static void processor_KeySwitchChanged(LogixProcessor sender, LogixKeyChangedEventArgs e)
        {
            //This function will be called when the key switch changes position. The key switch is on the front of
            //the processor and can either be in Run, Program or Remote mode. There is an additional member of the
            //ProcessorKeySwitch enumeration called "Unknown" which is used when the value hasn't been read yet or
            //can't be obtained.
            Console.WriteLine("Processor Key Position Changed from " + e.OldPosition.ToString() + " to " + e.NewPosition.ToString());
        }

        static void processor_FaultStateChanged(LogixProcessor sender, LogixFaultStateChangedEventArgs e)
        {
            //This function is called when the processor fault state changes. The fault states are None, Minor
            //Recoverable, Minor Unrecoverable, Major Recoverable, and Major Unrecoverable.
            Console.WriteLine("Processor Fault Mode Changed from " + e.OldState.ToString() + " to " + e.NewState.ToString());
        }
    }
}
