using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using EIPNET.EIP;

namespace ControlLogixNET
{
    /// <summary>
    /// Represents a group of tags that can be manipulated as a group
    /// </summary>
    public sealed class LogixTagGroup
    {

        #region Internal Types

        private class PacketMap
        {
            public int TagIndex { get; set; }
            public List<int> PacketIndex { get; set; }
            public List<int> ServiceIndex { get; set; }
            public List<uint> Offsets { get; set; }
            public int NumReplies { get; set; }
        }

        #endregion

        #region Fields

        private const int MAX_MSR_SIZE = 480;

        private LogixProcessor _parent;
        private string _groupName;
        private bool _enabled;

        private List<LogixTag> _tags;

        private List<PacketMap> _readPackets;
        private List<PacketMap> _writePackets;
        private List<MultiServiceRequest> _msrPackets;
        private List<MultiServiceRequest> _writeMsrPackets;
        private Dictionary<int, List<byte[]>> _byteReplies;

        private object _lockObject = new object();

        private Stopwatch _writeSw = new Stopwatch();
        private Stopwatch _readSw = new Stopwatch();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the processor that this LogixTagGroup belongs to
        /// </summary>
        public LogixProcessor Processor
        {
            get { return _parent; }
        }

        /// <summary>
        /// Gets the name of the LogixTagGroup
        /// </summary>
        public string GroupName
        {
            get { return _groupName; }
        }

        /// <summary>
        /// Gets or Sets the Enabled property
        /// </summary>
        /// <remarks>When this property is set to false, it will not update
        /// any of the tags that belong to the group. If the tag belongs to
        /// multiple groups, those groups may still update the tag unless the
        /// <see cref="LogixTag.Enabled"/> property is set to false.</remarks>
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        /// <summary>
        /// Gets the number of tags in this LogixTagGroup
        /// </summary>
        public int Count
        {
            get { return _tags.Count; }
        }

        /// <summary>
        /// Gets the tag at the specified index
        /// </summary>
        /// <param name="idx">Index of the tag</param>
        /// <returns>LogixTag at the specified index</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when the index >= Count or the index < 0</exception>
        public LogixTag this[int idx]
        {
            get
            {
                if (idx >= _tags.Count || idx < 0)
                    throw new IndexOutOfRangeException();

                return _tags[idx];
            }
        }

        /// <summary>
        /// Returns the amount of time it took to perform the last read, in Milliseconds
        /// </summary>
        public double ReadTime
        {
            get { return _readSw.Elapsed.TotalMilliseconds; }
        }

        /// <summary>
        /// Returns the amount of time it took to perform the last write, in Milliseconds
        /// </summary>
        public double WriteTime
        {
            get { return _writeSw.Elapsed.TotalMilliseconds; }
        }

        #endregion

        #region Events

        /// <summary>
        /// Event is raised just before the group is written/read
        /// </summary>
        public event EventHandler UpdateStart;
        /// <summary>
        /// Event is raised just after the group is written/read
        /// </summary>
        public event EventHandler<UpdateFinishedEventArgs> UpdateFinished;

        #endregion

        #region Construction / Deconstruction

        /// <summary>
        /// Creates a new LogixTagGroup on the specified processor. It is
        /// recommended that you use <see cref="LogixProcessor.CreateTagGroup"/>
        /// to create tag groups instead of this constructor so the processor
        /// can manage the group.
        /// </summary>
        /// <param name="Processor">Processor that the group belongs to</param>
        /// <param name="GroupName">Name of the group, must be unique on the processor</param>
        public LogixTagGroup(LogixProcessor Processor, string GroupName)
        {
            _parent = Processor;
            _groupName = GroupName;

            _enabled = true;
            _tags = new List<LogixTag>();
            _readPackets = new List<PacketMap>();
            _msrPackets = new List<MultiServiceRequest>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a tag to this tag group
        /// </summary>
        /// <param name="tag">Tag to be added</param>
        /// <remarks>
        /// <para>It is recommended that you create your groups once, then
        /// enable or disable them as you need them. Don't <see cref="RemoveTag"/> 
        /// frequently, as this may degrade the performance of your application.</para>
        /// </remarks>
        public void AddTag(LogixTag tag)
        {
            //lock (_lockObject)
            {
                int idx = _tags.Count;
                _tags.Add(tag);
                AddOrCreateReadPacket(_tags[idx], idx);
            }
        }

        /// <summary>
        /// Adds tags from an enumerable collection
        /// </summary>
        /// <param name="tags">Collection that contains the tags</param>
        public void AddTags(IEnumerable<LogixTag> tags)
        {
            foreach (LogixTag t in tags)
                AddTag(t);
        }

        /// <summary>
        /// Removes a tag from this tag group
        /// </summary>
        /// <param name="tag">Tag to be removed</param>
        /// <remarks>
        /// <para>Removing a tag triggers the tag group to re-optimize all
        /// of the tag read requests. This can be a long process for groups
        /// with large numbers of tags or tags that require a lot of data to
        /// be transferred.</para>
        /// <para>It is recommended that if you only need to remove a couple
        /// tags from a group that you disable them instead. Disabled tags
        /// will no longer update the value or send out update events.</para>
        /// </remarks>
        public void RemoveTag(LogixTag tag)
        {
            //lock (_lockObject)
            {
                _tags.Remove(tag);
                RebuildReadRequests();
            }
        }

        /// <summary>
        /// Removes a collection of tags from the group
        /// </summary>
        /// <param name="tags">Tags to be removed</param>
        /// <remarks>
        /// <para>Removing tags triggers the group to re-optimize all
        /// of the tag read requests. This can be a long process for groups
        /// with large numbers of tags or tags that require a lot of data to
        /// be transferred.</para>
        /// <para>It is recommended that if you want to remove a large number
        /// of tags, you use the <see cref="RemoveTags"/> method, which waits
        /// until after all the tags are removed to reoptimize. The
        /// <see cref="RemoveTag"/> function will reoptimize the read requests
        /// after each tag is removed, which will degrade performance with
        /// large tag groups.</para>
        /// </remarks>
        public void RemoveTags(IEnumerable<LogixTag> tags)
        {
            foreach (LogixTag t in tags)
                _tags.Remove(t);

            RebuildReadRequests();
        }

        /// <summary>
        /// Forces this tag group to write all the tags with pending update
        /// values then read all the tags.
        /// </summary>
        public void UpdateGroup()
        {
            lock (_lockObject)
            {
                if (UpdateStart != null)
                    UpdateStart(this, EventArgs.Empty);

                _writeSw.Reset();
                _writeSw.Start();
                WriteAll();
                _writeSw.Stop();
                _readSw.Reset();
                _readSw.Start();
                ReadAll();
                _readSw.Stop();
            }
        }

        #endregion

        #region Internal Methods



        #endregion

        #region Private Methods

        private void ReadAll()
        {
            //The lock object should already be held...

            List<MultiServiceReply> replies = new List<MultiServiceReply>();
            List<LogixTag> updatedTags = new List<LogixTag>();

            _byteReplies = new Dictionary<int, List<byte[]>>();
            
            CommonPacketItem addressItem = CommonPacketItem.GetConnectedAddressItem(_parent.SessionInfo.ConnectionParameters.O2T_CID);

            for (int i = 0; i < _msrPackets.Count; i++)
            {
                CommonPacketItem dataItem = CommonPacketItem.GetConnectedDataItem(_msrPackets[i].Pack(), SequenceNumberGenerator.SequenceNumber);

                EncapsReply reply = _parent.SessionInfo.SendUnitData(addressItem, dataItem);

                if (reply != null)
                {
                    //We need to suck all the replies out of the packet, and request more data if there is more
                    MultiServiceReply msReply = new MultiServiceReply(reply);

                    DecodeReadPacket(i, msReply);
                    
                    foreach (KeyValuePair<int, List<byte[]>> kvp in _byteReplies)
                    {
                        uint offset = 0;
                        for (int i2 = 0; i2 < kvp.Value.Count; i2++)
                        {
                            if (_tags[kvp.Key].UpdateValue(kvp.Value[i2], offset))
                                updatedTags.Add(_tags[kvp.Key]);
                            offset += (uint)(kvp.Value[i2].Length + 6);
                        }
                    }

                    if (UpdateFinished != null)
                        UpdateFinished(this, new UpdateFinishedEventArgs(updatedTags));

                    updatedTags.Clear();
                    _byteReplies.Clear();
                }

            }
        }

        private void DecodeReadPacket(int packetIdx, MultiServiceReply reply)
        {
            //Basically we need to find the tag(s) that this belongs to and add it to
            //a list of reply bytes at the specified offset.

            List<PacketMap> packetTags = GetTagsForPacket(packetIdx);

            for (int i = 0; i < packetTags.Count; i++)
            {
                PacketMap currentTag = packetTags[i];

                //We have to figure out what service this tag is on
                int idx = currentTag.PacketIndex.IndexOf(packetIdx);
                int serviceNum = currentTag.ServiceIndex[idx];

                //Now get the service data out and add it to the replies
                if (reply.ServiceReplies.Count > serviceNum)
                {
                    _tags[currentTag.TagIndex].SetTagError(reply.ServiceReplies[serviceNum].FullStatus);
                    if (!_byteReplies.ContainsKey(currentTag.TagIndex))
                        _byteReplies[currentTag.TagIndex] = new List<byte[]>();
                    _byteReplies[currentTag.TagIndex].Add(reply.ServiceReplies[serviceNum].ServiceData);
                }
            }
        }

        private List<PacketMap> GetTagsForPacket(int packetIdx)
        {
            var packets =
                from p in _readPackets
                where p.PacketIndex.Contains(packetIdx)
                select p;

            if (packets == null)
                return null;

            return new List<PacketMap>(packets);                
        }

        private void WriteAll()
        {
            //First we are going to have to build the write requests...
            _writeMsrPackets = new List<MultiServiceRequest>();
            BuildWriteRequests();

            //Now we have to send them out...
            for (int i = 0; i < _writeMsrPackets.Count; i++)
            {
                CommonPacketItem addressItem = CommonPacketItem.GetConnectedAddressItem(_parent.SessionInfo.ConnectionParameters.O2T_CID);
                CommonPacketItem dataItem = CommonPacketItem.GetConnectedDataItem(_writeMsrPackets[i].Pack(), SequenceNumberGenerator.SequenceNumber);

                EncapsReply reply = _parent.SessionInfo.SendUnitData(addressItem, dataItem);

                if (reply != null)
                {
                    MultiServiceReply msReply = new MultiServiceReply(reply);

                    DecodeWriteReply(i, msReply);
                }
            }
        }

        private void RebuildReadRequests()
        {
            _readPackets.Clear();
            _msrPackets.Clear();

            for (int i = 0; i < _tags.Count; i++)
            {
                AddOrCreateReadPacket(_tags[i], i);
            }
        }

        private void AddOrCreateReadPacket(LogixTag tag, int idx)
        {
            //First we create the request...
            int temp = 0;
            ReadDataServiceRequest request = LogixServices.BuildLogixReadDataRequest(
                tag.Address, tag.Elements, out temp);

            //Now we read it from the PLC to find out if it's fragmented...
            CommonPacketItem addressItem = CommonPacketItem.GetConnectedAddressItem(_parent.SessionInfo.ConnectionParameters.O2T_CID);
            CommonPacketItem dataItem = CommonPacketItem.GetConnectedDataItem(request.Pack(), SequenceNumberGenerator.SequenceNumber);

            EncapsReply reply = _parent.SessionInfo.SendUnitData(addressItem, dataItem);

            if (reply != null)
            {
                //It's a good tag, let's figure out if it's fragmented...
                ReadDataServiceReply rdReply = new ReadDataServiceReply(reply);

                PacketMap pm = new PacketMap() { TagIndex = idx };
                pm.PacketIndex = new List<int>();
                pm.ServiceIndex = new List<int>();
                pm.Offsets = new List<uint>();
                pm.NumReplies = 1;

                if (rdReply.Status == 0x06)
                {
                    //Partial read... We'll have to request more data, but first let's make this packet
                    request = LogixServices.BuildFragmentedReadDataRequest(tag.Address, tag.Elements,
                        0, out temp);
                    int[] status = FindPacketOrCreate(request.Pack(), (ushort)(rdReply.Data.Length + 2));
                    uint offset = (uint)rdReply.Data.Length;
                    pm.PacketIndex.Add(status[0]);
                    pm.ServiceIndex.Add(status[1]);
                    pm.Offsets.Add(0);

                    while (rdReply.Status == 0x06)
                    {
                        rdReply = LogixServices.ReadLogixDataFragmented(_parent.SessionInfo, tag.Address, tag.Elements,
                            offset);
                        request = LogixServices.BuildFragmentedReadDataRequest(tag.Address, tag.Elements,
                            offset, out temp);
                        status = FindPacketOrCreate(request.Pack(), (ushort)(rdReply.Data.Length + 2));
                        pm.PacketIndex.Add(status[0]);
                        pm.ServiceIndex.Add(status[1]);
                        offset += (uint)rdReply.Data.Length;
                        pm.Offsets.Add(offset);
                        pm.NumReplies++;
                    }
                }
                else if (rdReply.Status == 0x00 && rdReply.Data != null)
                {
                    //Full read, create the packet...
                    int[] status = FindPacketOrCreate(request.Pack(), (ushort)(rdReply.Data.Length + 2));
                    pm.PacketIndex.Add(status[0]);
                    pm.ServiceIndex.Add(status[1]);
                    pm.Offsets.Add(0);
                }

                _readPackets.Add(pm);
            }

        }

        private int[] FindPacketOrCreate(byte[] request, ushort replySize)
        {
            //We'll go through all the packets and find one that can fit both the
            //request size and the reply size...
            int[] retVal = new int[2];      //Packet Number, Service Number

            for (int i = 0; i < _msrPackets.Count; i++)
            {
                if (_msrPackets[i].ReplySize + replySize <= MAX_MSR_SIZE)
                {
                    //Possible candidate, let's see if we can put the request in it...
                    if (_msrPackets[i].Size + request.Length <= MAX_MSR_SIZE)
                    {
                        //Got one!
                        retVal[0] = i;
                        retVal[1] = _msrPackets[i].AddService(request);
                        _msrPackets[i].ReplySize += replySize;
                        return retVal;
                    }
                }
            }

            //If we got here, we need to create one...
            MultiServiceRequest newPacket = new MultiServiceRequest();
            retVal[0] = _msrPackets.Count;
            _msrPackets.Add(newPacket);
            retVal[1] = _msrPackets[retVal[0]].AddService(request);
            _msrPackets[retVal[0]].ReplySize += replySize;

            return retVal;
        }

        private void BuildWriteRequests()
        {
            List<byte[]> writeRequests = new List<byte[]>();
            List<int> writeTags = new List<int>();
            List<int> alignments = new List<int>();

            for (int i = 0; i < _tags.Count; i++)
            {
                if (_tags[i].HasPendingWrite())
                {
                    writeTags.Add(i);
                    int align = 0;
                    writeRequests.Add(_tags[i].GetWriteData(out align));
                    alignments.Add(align);
                }
            }

            _writePackets = new List<PacketMap>();

            //Ok, now we have all the data for the write, but we have to break it up into packets...
            for (int i = 0; i < writeTags.Count; i++)
            {
                PacketMap pm = new PacketMap() { TagIndex = writeTags[i] };
                pm.PacketIndex = new List<int>();
                pm.ServiceIndex = new List<int>();
                DistributeWriteRequest(pm, writeRequests[i], alignments[i]);
                _writePackets.Add(pm);
            }

            //And that's it...
        }

        private void DistributeWriteRequest(PacketMap pm, byte[] requestData, int alignment)
        {
            //Ok, the request may have to be broken up into multiple requests...
            if (requestData.Length > MAX_MSR_SIZE)
            {
                //This will have to be broken up... the overhead of an MSR packet is 12 bytes, so
                //that means we can only fit up to MAX_MSR_SIZE - 12 bytes into a single packet.
                //We need to figure out how much data we can stuff into one packet, then make
                //multiple ones based on that...
                //The first packet should always be the maximum size we can fit into one request...
                WriteDataServiceRequest fragReq = LogixServices.BuildFragmentedWriteRequest(
                    _tags[pm.TagIndex].Address, _tags[pm.TagIndex].DataType, _tags[pm.TagIndex].Elements,
                    0, null, _tags[pm.TagIndex].StructHandle);
                int maxSize = MAX_MSR_SIZE - 12 - fragReq.Size;
                int alignedSize = maxSize - (maxSize % alignment);
                int remainingSize = requestData.Length;
                uint offset = 0;

                while (remainingSize > 0)
                {
                    //We can fit up to alignedSize bytes in the array...
                    byte[] temp;
                    if (remainingSize < alignedSize)
                    {
                        temp = new byte[remainingSize];
                        remainingSize = 0;
                    }
                    else
                    {
                        temp = new byte[alignedSize];
                        remainingSize -= alignedSize;
                    }

                    Buffer.BlockCopy(requestData, (int)offset, temp, 0, temp.Length);
                    
                    fragReq = LogixServices.BuildFragmentedWriteRequest(_tags[pm.TagIndex].Address,
                        _tags[pm.TagIndex].DataType, _tags[pm.TagIndex].Elements, offset, temp, _tags[pm.TagIndex].StructHandle);

                    offset += (uint)temp.Length;

                    FindWritePacketOrCreate(pm, fragReq);
                }
            }
            else
            {
                //We can fit it into a single packet, we just need to find
                //one
                WriteDataServiceRequest request = LogixServices.BuildLogixWriteDataRequest(
                    _tags[pm.TagIndex].Address, _tags[pm.TagIndex].DataType, _tags[pm.TagIndex].Elements,
                    requestData, _tags[pm.TagIndex].StructHandle);

                FindWritePacketOrCreate(pm, request);
            }
        }

        private void FindWritePacketOrCreate(PacketMap pm, WriteDataServiceRequest request)
        {
            //The data is already broken up into appropriately sized chunks, we just have
            //to find a place to put it...

            int rSize = request.Size;
            for (int i = 0; i < _writeMsrPackets.Count; i++)
            {
                if (_writeMsrPackets[i].Size + rSize < MAX_MSR_SIZE)
                {
                    //This one will fit the packet
                    int svc = _writeMsrPackets[i].AddService(request.Pack());
                    pm.PacketIndex.Add(i);
                    pm.ServiceIndex.Add(i);
                    return;
                }
            }

            //If we got here, we have to create a new one...
            MultiServiceRequest msr = new MultiServiceRequest();
            pm.PacketIndex.Add(_writeMsrPackets.Count);
            _writeMsrPackets.Add(msr);
            pm.ServiceIndex.Add(msr.AddService(request.Pack()));
        }

        private void DecodeWriteReply(int packetIdx, MultiServiceReply reply)
        {
            List<PacketMap> writeTags = GetTagsForWritePacket(packetIdx);

            for (int i = 0; i < writeTags.Count; i++)
            {
                PacketMap currentTag = writeTags[i];

                int idx = currentTag.PacketIndex.IndexOf(packetIdx);
                int serviceNum = currentTag.ServiceIndex[idx];
                
                //TODO: This should look at all the replies for a particular tag before
                //telling the tag to clear the pending write...

                if (reply.ServiceReplies.Count > serviceNum)
                {
                    _tags[currentTag.TagIndex].SetTagError(reply.ServiceReplies[serviceNum].FullStatus);
                    if (reply.ServiceReplies[serviceNum].Status == 0x00)
                        _tags[currentTag.TagIndex].ClearPendingWrite();
                }
            }
        }

        private List<PacketMap> GetTagsForWritePacket(int packetIdx)
        {
            var packets =
                from p in _writePackets
                where p.PacketIndex.Contains(packetIdx)
                select p;

            if (packets == null)
                return null;

            return new List<PacketMap>(packets);
        }

        #endregion

    }

    public sealed class UpdateFinishedEventArgs : EventArgs
    {
        public List<LogixTag> UpdatedTags { get; private set; }

        internal UpdateFinishedEventArgs(List<LogixTag> UpdatedTags)
        {
            this.UpdatedTags = UpdatedTags;
        }
    }
}
