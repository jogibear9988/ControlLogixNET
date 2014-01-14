using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlLogixNET;
using ControlLogixNET.LogixType;

namespace SimpleOperations
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

            //The processor has to be connected before you add any tags or tag groups.
            if (!processor.Connect())
            {
                Console.WriteLine("Could not connect to the processor");
                Console.ReadKey(false);
                return;
            }

            string menu = "6D Systems LLC\n"
                        + "-----------------------------------------\n"
                        + "(1) - Get Information About A Tag\n"
                        + "(2) - Read a tag\n"
                        + "(3) - Write a tag\n"
                        + "(Q) - Quit\n"
                        + "-----------------------------------------\n"
                        + "Enter your choice:";

            bool quitFlag = false;

            while (!quitFlag)
            {
                Console.Clear();
                Console.Write(menu);

                string key = Console.ReadLine();

                switch (key)
                {
                    case "1":
                        TagInformation(processor);
                        break;
                    case "2":
                        ReadTag(processor);
                        break;
                    case "3":
                        WriteTag(processor);
                        break;
                    case "q":
                    case "Q":
                        quitFlag = true;
                        continue;
                    default:
                        Console.WriteLine("Invalid entry");
                        break;
                }

                Console.WriteLine("Hit any key to go back to the menu");
                Console.ReadKey(false);
            }

            //Remember to disconnect from the processor. If you forget, the processor won't allow you
            //to reconnect until the session times out, which is typically 60 seconds.

            processor.Disconnect();
        }

        static void TagInformation(LogixProcessor processor)
        {
            //Getting detailed tag information is actually an expensive process. Currently
            //there is no way to get detailed information about a tag except to request all
            //the tag information in the PLC. The GetTagInformation will read all the tags
            //in the PLC, then return the one you are looking for.
            string address = GetTagAddress();

            if (string.IsNullOrEmpty(address))
                return;

            LogixTagInfo tagInfo = processor.GetTagInformation(address);

            if (tagInfo == null)
            {
                Console.WriteLine("The tag '" + address + "' could not be found on the processor");
                Console.ReadKey(false);
                return;
            }

            Console.WriteLine(tagInfo.ToString());
        }

        static void ReadTag(LogixProcessor processor)
        {
            //Reading a tag is very easy. First, create a tag...
            string address = GetTagAddress();

            if (string.IsNullOrEmpty(address))
                return;

            //Now we have to create the tag on the processor. The easiest way to
            //do this without knowing the underlying type is to use the
            //LogixTagFactory class.
            LogixTag userTag = LogixTagFactory.CreateTag(address, processor);

            if (userTag == null)
            {
                Console.WriteLine("Could not create the tag " + address + " on the processor");
                return;
            }

            //The tag is automatically read when it is created. The LogixProcessor does this
            //to verify the tag exists and to get type information about the tag. From this
            //point on you can read/write the tag all you want, either by using tag groups
            //or by directly writing it with the LogixProcessor.WriteTag() function.

            //We'll demonstrate a read anyway...
            if (!processor.ReadTag(userTag))
                Console.WriteLine("Could not read the tag: " + userTag.LastError);

            //Print the value out with our handy helper function
            PrintTagValue(userTag);

            //And go back to the main menu
        }

        static void WriteTag(LogixProcessor processor)
        {
            //Writing a tag is also very easy. First, create a tag...
            string address = GetTagAddress();

            if (string.IsNullOrEmpty(address))
                return;

            //Now we have to create the tag on the processor. The easiest way to
            //do this without knowing the underlying type is to use the
            //LogixTagFactory class.
            LogixTag userTag = LogixTagFactory.CreateTag(address, processor);

            if (userTag == null)
            {
                Console.WriteLine("Could not create the tag " + address + " on the processor");
                return;
            }

            switch (userTag.LogixType)
            {
                case LogixTypes.Bool:
                    WriteBool(userTag, processor);
                    break;
                case LogixTypes.DInt:
                case LogixTypes.LInt:
                case LogixTypes.Real:
                case LogixTypes.SInt:
                    WriteOther(userTag, processor);
                    break;
                case LogixTypes.Control:
                case LogixTypes.Counter:
                case LogixTypes.Timer:
                case LogixTypes.User_Defined:
                    WriteStructure(userTag, processor);
                    break;
                default:
                    Console.WriteLine("The LogixType of " + userTag.LogixType.ToString() + " is not supported in this sample");
                    return;
            }

            PrintTagValue(userTag);

            //And go back to the menu
        }

        static void PrintTagValue(LogixTag tag)
        {
            //Now we'll determine the value of the tag...
            switch (tag.LogixType)
            {
                case LogixTypes.Bool:
                    Console.WriteLine("BOOL value is: " + ((LogixBOOL)tag).Value.ToString());
                    break;
                case LogixTypes.Control:
                    //The control tag is a lot more complicated, there is no way currently to know
                    //which member was updated, so all you can do is say it was updated, we'll print
                    //out one of the members though.
                    Console.WriteLine("Control.POS is: " + ((LogixCONTROL)tag).POS.ToString());
                    break;
                case LogixTypes.Counter:
                    //Same as the counter above, we'll just print out the ACC value
                    Console.WriteLine("Counter.ACC value is: " + ((LogixCOUNTER)tag).ACC.ToString());
                    break;
                case LogixTypes.DInt:
                    //Print out the value. DINT's are equivalent to int in .NET
                    Console.WriteLine("DINT value is: " + ((LogixDINT)tag).Value.ToString());
                    break;
                case LogixTypes.Int:
                    //An INT in a logix processor is more like a short in .NET
                    Console.WriteLine("INT value is: " + ((LogixINT)tag).Value.ToString());
                    break;
                case LogixTypes.LInt:
                    //LINT's are equivalent to long in .NET
                    Console.WriteLine("LINT value is: " + ((LogixLINT)tag).Value.ToString());
                    break;
                case LogixTypes.Real:
                    //REALs are single precision floats
                    Console.WriteLine("REAL value is: " + ((LogixREAL)tag).Value.ToString());
                    break;
                case LogixTypes.SInt:
                    //SINTs are signed bytes
                    Console.WriteLine("SINT value is: " + ((LogixSINT)tag).Value.ToString());
                    break;
                case LogixTypes.String:
                    //Strings are just like .NET strings, so notice how we can skip the .StringValue
                    //member, since the .ToString() will automatically be called, which returns the
                    //same value as .StringValue
                    Console.WriteLine("STRING value is: " + ((LogixSTRING)tag));
                    break;
                case LogixTypes.Timer:
                    //Timers again are like the CONTROL and COUNTER types
                    Console.WriteLine("Timer.ACC value is: " + ((LogixTIMER)tag).ACC.ToString());
                    break;
                case LogixTypes.User_Defined:
                    //The only way to get the value out of a UDT, PDT, or MDT is to define the
                    //structure or know the member name you wish to read. We'll just print that
                    //we know its a UDT, MDT, or PDT that changed.
                    Console.WriteLine("User defined type");
                    break;
                default:
                    break;
            }
        }

        static string GetTagAddress()
        {
            Console.Write("\nEnter a Tag Address: ");

            string address = Console.ReadLine();

            if (string.IsNullOrEmpty(address))
            {
                Console.WriteLine("Address can't be a null or empty string");
                return string.Empty;
            }

            return address;
        }

        static void WriteStructure(LogixTag tag, LogixProcessor processor)
        {
            Console.Write("The tag is a structure called " + ((LogixUDT)tag).TypeName + ", please enter a member name: ");
            string memberName = Console.ReadLine();

            //First we have to find out if the member exists, if it doesn't we can't write to it...
            List<string> memberNames = ((LogixUDT)tag).MemberNames;
            
            bool hasMember = false;
            for (int i = 0; i < memberNames.Count; i++)
            {
                if (string.Compare(memberNames[i], memberName) == 0)
                {
                    hasMember = true;
                    break;
                }
            }

            if (!hasMember)
            {
                Console.WriteLine("The specified member could not be found in the structure");
                return;
            }

            Console.Write("Enter a value: ");
            string sValue = Console.ReadLine();

            //Now we have to convert it to the right type...
            try
            {
                switch (tag.LogixType)
                {
                    case LogixTypes.Bool:
                        if (sValue == "1")
                            ((LogixUDT)tag)[memberName] = true;
                        else
                            ((LogixUDT)tag)[memberName] = false;
                        break;
                    case LogixTypes.DInt:
                        ((LogixUDT)tag)[memberName] = Convert.ToInt32(sValue);
                        break;
                    case LogixTypes.Int:
                        ((LogixUDT)tag)[memberName] = Convert.ToInt16(sValue);
                        break;
                    case LogixTypes.LInt:
                        ((LogixUDT)tag)[memberName] = Convert.ToInt64(sValue);
                        break;
                    case LogixTypes.Real:
                        ((LogixUDT)tag)[memberName] = Convert.ToSingle(sValue);
                        break;
                    case LogixTypes.SInt:
                        ((LogixUDT)tag)[memberName] = Convert.ToSByte(sValue);
                        break;
                    case LogixTypes.User_Defined:
                    default:
                        Console.WriteLine("This demo does not support writing to nested structure tags");
                        return;
                }

                //At this point the tag has not been committed to the processor. The
                //tag must be written, then read back for the value to change. The
                //easiest way to do this with a single tag is to use the processor
                //LogixProcessor.WriteRead() which performs the write, then the 
                //subsequent read on the tag.
                processor.WriteRead(tag);
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not convert " + sValue + " to the correct type for " + tag.Address);
            }
        }

        static void WriteBool(LogixTag tag, LogixProcessor processor)
        {
            Console.WriteLine("Enter 1 for True, 0 for False: ");
            char key = Console.ReadKey().KeyChar;

            if (key == '1')
                ((LogixBOOL)tag).Value = true;
            else
                ((LogixBOOL)tag).Value = false;

            //At this point the tag has not been committed to the processor. The
            //tag must be written, then read back for the value to change. The
            //easiest way to do this with a single tag is to use the processor
            //LogixProcessor.WriteRead() which performs the write, then the 
            //subsequent read on the tag.
            processor.WriteRead(tag);      
        }

        static void WriteOther(LogixTag tag, LogixProcessor processor)
        {
            Console.Write("Enter a value: ");
            string sValue = Console.ReadLine();

            //Now we have to convert it to the right type...
            try
            {
                switch (tag.LogixType)
                {
                    case LogixTypes.DInt:
                        ((LogixDINT)tag).Value = Convert.ToInt32(sValue);
                        break;
                    case LogixTypes.Int:
                        ((LogixINT)tag).Value = Convert.ToInt16(sValue);
                        break;
                    case LogixTypes.LInt:
                        ((LogixLINT)tag).Value = Convert.ToInt64(sValue);
                        break;
                    case LogixTypes.Real:
                        ((LogixREAL)tag).Value = Convert.ToSingle(sValue);
                        break;
                    case LogixTypes.SInt:
                        ((LogixSINT)tag).Value = Convert.ToSByte(sValue);
                        break;
                    default:
                        return;
                }

                //At this point the tag has not been committed to the processor. The
                //tag must be written, then read back for the value to change. The
                //easiest way to do this with a single tag is to use the processor
                //LogixProcessor.WriteRead() which performs the write, then the 
                //subsequent read on the tag.
                processor.WriteRead(tag);
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not convert " + sValue + " to the correct type for " + tag.Address);
            }

        }

    }
}
