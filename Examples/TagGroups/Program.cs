using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlLogixNET;
using ControlLogixNET.LogixType;

namespace TagGroups
{
    class Program
    {
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

            //Connect to the PLC, you can create the events before or after the connect function
            if (!processor.Connect())
            {
                Console.WriteLine("Could not connect to the processor");
                Console.ReadKey(false);
                return;
            }

            //Tag groups allow you to group tags in a useful manner. For example in an HMI you
            //could create tag groups for each page. Disabling a tag group that is not in use
            //frees up resources on the processor and the network.

            //You also have to be careful about the two different kinds of Enabled properties.
            //There is an enabled property for the tag group, and there is an Enabled property
            //for the tag itself. Disabling the tag group stops it from being updated, so no
            //tags belonging to that group will be updated (unless they also belong to another
            //active tag group). Disabling the tag by setting the LogixTag.Enabled property
            //to false means that the tag won't accept new data or pending values, and that
            //any tag group that it belongs to won't update it.

            //First, we need to create a LogixTagGroup on the processor. The easiest way to do
            //this is to use the LogixProcessor.CreateTagGroup() method. This allows the 
            //processor to create the tag group, verify it doesn't conflict with another tag
            //group, and manage the group.

            LogixTagGroup tg = processor.CreateTagGroup("MyGroup");

            //Now that we've created a tag group, we can add some tags to it. Adding and removing
            //tags from a tag group is an expensive process. The tag group will automatically
            //re-optimize all the tags it's responsible for when you add or remove a tag. It's
            //recommended that you don't add or remove tags very often, if you don't need a tag
            //to be updated anymore just set the LogixTag.Enabled property to false.

            //Here we are going to ask the user (probably you) for some tags. The easiest way to
            //create tags without knowing the underlying data type in the processor is to use
            //the LogixTagFactory.

            bool quitFlag = false;
            LogixDINT dTag = new LogixDINT("tst_Dint", processor);
            dTag.TagValueUpdated += new ICommon.TagValueUpdateEventHandler(TagValueUpdated);
            tg.AddTag(dTag);

            while (!quitFlag)
            {
                Console.Write("Please enter a tag name to monitor, enter 'done' when finished: ");

                string tagName = Console.ReadLine();

                if (tagName.ToLower() == "done")
                {
                    quitFlag = true;
                    continue;
                }

                LogixTag userTag = LogixTagFactory.CreateTag(tagName, processor);

                if (userTag == null)
                {
                    //When the tag factory returns null, the tag was not found or some other
                    //catastrophic error occurred trying to reference it on the processor.
                    Console.WriteLine("The tag " + tagName + " could not be created");
                    continue;
                }

                //If we got here, we were able to successfully create the tag. Let's print
                //some information about it...
                Console.WriteLine("Created " + tagName + " as a(n) " + userTag.LogixType.ToString());

                //Let's reference the update functions...
                userTag.TagValueUpdated += new ICommon.TagValueUpdateEventHandler(TagValueUpdated);

                //Now let's add it to the tag group...
                tg.AddTag(userTag);
            }

            //The processor has a feature that allows them to automatically update the tag group. This
            //helps to free up your logic and not worry about having to update tag groups that are
            //enabled or disabled. The argument for this function is the time between updates in
            //milliseconds. The actual time from the start of one update to the start of another is
            //dependant on how many tags there are and how much data needs to be transferred.
            processor.EnableAutoUpdate(500);

            Console.WriteLine("Press Enter to quit");
            Console.ReadLine();

            processor.Disconnect();

        }

        static void TagValueUpdated(ICommon.ITag sender, ICommon.TagValueUpdateEventArgs e)
        {
            //Here we'll just display the name of the tag and that it was updated. If you
            //want to extract the value you'll have to cast it to the correct type. You
            //can do this by using a switch as shown below.
            Console.WriteLine("Tag " + e.Tag.Address + " Updated");

            switch (((LogixTag)sender).LogixType)
            {
                case LogixTypes.Bool:
                    Console.WriteLine("New value is: " + ((LogixBOOL)sender).Value.ToString());
                    break;
                case LogixTypes.Control:
                    //The control tag is a lot more complicated, there is no way currently to know
                    //which member was updated, so all you can do is say it was updated, we'll print
                    //out one of the members though.
                    Console.WriteLine("New value is: " + ((LogixCONTROL)sender).POS.ToString());
                    break;
                case LogixTypes.Counter:
                    //Same as the counter above, we'll just print out the ACC value
                    Console.WriteLine("New ACC value is: " + ((LogixCOUNTER)sender).ACC.ToString());
                    break;
                case LogixTypes.DInt:
                    //Print out the value. DINT's are equivalent to int in .NET
                    Console.WriteLine("New DINT value is: " + ((LogixDINT)sender).Value.ToString());
                    break;
                case LogixTypes.Int:
                    //An INT in a logix processor is more like a short in .NET
                    Console.WriteLine("New INT value is: " + ((LogixINT)sender).Value.ToString());
                    break;
                case LogixTypes.LInt:
                    //LINT's are equivalent to long in .NET
                    Console.WriteLine("New LINT value is: " + ((LogixLINT)sender).Value.ToString());
                    break;
                case LogixTypes.Real:
                    //REALs are single precision floats
                    Console.WriteLine("New REAL value is: " + ((LogixREAL)sender).Value.ToString());
                    break;
                case LogixTypes.SInt:
                    //SINTs are signed bytes
                    Console.WriteLine("New SINT value is: " + ((LogixSINT)sender).Value.ToString());
                    break;
                case LogixTypes.String:
                    //Strings are just like .NET strings, so notice how we can skip the .StringValue
                    //member, since the .ToString() will automatically be called, which returns the
                    //same value as .StringValue
                    Console.WriteLine("New STRING value is: " + ((LogixSTRING)sender));
                    break;
                case LogixTypes.Timer:
                    //Timers again are like the CONTROL and COUNTER types
                    Console.WriteLine("New Timer.ACC value is: " + ((LogixTIMER)sender).ACC.ToString());
                    break;
                case LogixTypes.User_Defined:
                    //The only way to get the value out of a UDT, PDT, or MDT is to define the
                    //structure or know the member name you wish to read. We'll just print that
                    //we know its a UDT, MDT, or PDT that changed.
                    Console.WriteLine("The user defined type has changed");
                    break;
                default:
                    break;
            }
        }

    }
}
