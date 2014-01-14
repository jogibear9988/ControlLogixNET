using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlLogixNET;
using ControlLogixNET.LogixType;
using System.Threading;

namespace ArrayTags
{
    class Program
    {
        /*
         * HOW TO USE THIS SAMPLE 
         * 
         * 1. First change the hostNameOrIp to the IP address or host name of your PLC
         * 2. Then change the path to be the path to your PLC, see comments below
         * 3. Create a 1 dimensional DINT array on the processor called dintArray1[10]
         * 4. Create a 2 dimensional DINT array on the processor called dintArray2[10,10]
         * 5. Create a 3 dimensional DINT array on the processor called dintArray3[10,10,10]
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

            //First create a group. Groups are much more efficient at reading and writing
            //large numbers of tags.
            LogixTagGroup myGroup = processor.CreateTagGroup("MyGroup");

            //Now let's create our first array. The number of elements is the TOTAL number
            //of elements to read, in all dimensions. 
            LogixDINT dintArray1 = new LogixDINT("dintArray1", processor, 10);

            //We don't need to set the number of dimensions on the tag here because it
            //assumes that it's a single dimension tag. All tags are set up to be arrays
            //by default, the .Value or similar member always returns the 0th element
            //of the array. With a tag that is not an array, that is where the value is.

            //Let's create the 2 dimensional array
            LogixDINT dintArray2 = new LogixDINT("dintArray2", processor, 100);

            //The number of elements are the subscripts multiplied by each other. In this
            //case, 10*10 = 100. If you put a lower value here you will only read that
            //much of the array. ControlLogix packs it's arrays in row major format, so
            //just keep that in mind if reading partial arrays.

            //If you want to set it up to read with a multidimensional accessor, we need
            //to tell the tag what the size of the dimensions are.
            dintArray2.SetMultipleDimensions(10, 10);

            //We can now access the tag by the tagName[row,column] format. If you didn't
            //set the size, you would get an exception when trying to access the tag
            //using that format.

            //Let's create the last tag
            LogixDINT dintArray3 = new LogixDINT("dintArray3", processor, 1000);

            //Set the dimensions
            dintArray3.SetMultipleDimensions(10, 10, 10);

            //Now let's add our tags to the tag group...
            myGroup.AddTag(dintArray1);
            myGroup.AddTag(dintArray2);
            myGroup.AddTag(dintArray3);

            Console.WriteLine("6D Systems LLC\n\n");
            Console.WriteLine("Tags created...");

            //Now let's pick out some random members and display them...
            Console.WriteLine("dintArray1[4]     = " + dintArray1[4].ToString());
            Console.WriteLine("dintArray2[5,2]   = " + dintArray2[5, 2].ToString());
            Console.WriteLine("dintArray3[4,7,3] = " + dintArray3[4, 7, 3].ToString());
            Console.WriteLine("\nPress any key to write a new value to each of the above tags");
            Console.ReadKey(false);

            //Now let's write some data to those tags...
            Random rnd = new Random();
            dintArray1[4] = rnd.Next(int.MinValue, int.MaxValue);
            dintArray2[5, 2] = rnd.Next(int.MinValue, int.MaxValue);
            dintArray3[4, 7, 3] = rnd.Next(int.MinValue, int.MaxValue);

            //Let's update the tag group
            processor.UpdateGroups();

            //Now print them back out for the user...
            Console.WriteLine("\nNew tag values...");
            Console.WriteLine("dintArray1[4]     = " + dintArray1[4].ToString());
            Console.WriteLine("dintArray2[5,2]   = " + dintArray2[5, 2].ToString());
            Console.WriteLine("dintArray3[4,7,3] = " + dintArray3[4, 7, 3].ToString());
            Console.WriteLine("\nPress any key to quit");
            Console.ReadKey(false);

            //Remember to disconnect from the processor
            processor.Disconnect();

        }
    }
}
