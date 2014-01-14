using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIPNET.EIP;
using EIPNET.CIP;

namespace ControlLogixNET
{
    internal class MultiServiceRequest
    {
        private int _replySize = 0;

        byte[] _header = new byte[] { 0x0A, 0x02, 0x20, 0x02, 0x24, 0x01 };
        public List<byte[]> Services { get; set; }
        public int ReplySize
        {
            get { return _replySize; }
            set { _replySize = value; }
        }

        public MultiServiceRequest()
        {
            Services = new List<byte[]>();
        }

        public int Size {
            get
            {
                int retVal = _header.Length + (2 * Services.Count);

                for (int i = 0; i < Services.Count; i++)
                    retVal += Services[i].Length;

                return retVal + 2;
            }
        }

        public int AddService(byte[] Service)
        {
            Services.Add(Service);
            return Services.Count - 1;
        }

        public byte[] Pack()
        {
            byte[] retVal = new byte[Size];

            Buffer.BlockCopy(_header, 0, retVal, 0, _header.Length);
            int arrayOffset = _header.Length;

            //Now add 2 bytes for the count
            Buffer.BlockCopy(BitConverter.GetBytes((ushort)Services.Count), 0, retVal, arrayOffset, 2);
            arrayOffset += 2;

            byte[] offsetArray = new byte[2 * Services.Count];
            int pos = 2 + offsetArray.Length;
            for (int i = 0; i < Services.Count; i++)
            {
                Buffer.BlockCopy(BitConverter.GetBytes((ushort)pos), 0, offsetArray, 2 * i, 2);
                pos += Services[i].Length;
            }

            List<byte> serviceData = new List<byte>();
            for (int i = 0; i < Services.Count; i++)
                serviceData.AddRange(Services[i]);

            byte[] serviceBytes = serviceData.ToArray();
            Buffer.BlockCopy(offsetArray, 0, retVal, arrayOffset, offsetArray.Length);
            arrayOffset += offsetArray.Length;
            Buffer.BlockCopy(serviceBytes, 0, retVal, arrayOffset, serviceBytes.Length);

            return retVal;
        }
    }

    internal class MultiServiceReply
    {
        public byte ReplyService { get; internal set; }
        public byte Reserved { get; internal set; }
        public byte GenSTS { get; internal set; }
        public byte Reserved2 { get; internal set; }
        public ushort Count { get; internal set; }
        public List<ServiceReply> ServiceReplies { get; internal set; }
        public bool IsPartial { get; internal set; }
        public ushort PartialOffset { get; internal set; }

        public MultiServiceReply()
        {
            ServiceReplies = new List<ServiceReply>();
        }

        public MultiServiceReply(EncapsReply reply)
        {
            EncapsRRData rrData = new EncapsRRData();
            CommonPacket cpf = new CommonPacket();

            int temp = 0;
            rrData.Expand(reply.EncapsData, 0, out temp);
            cpf = rrData.CPF;

            MR_Response response = new MR_Response();
            response.Expand(cpf.DataItem.Data, 2, out temp);

            if (response.GeneralStatus == 0x1E)
                IsPartial = true;

            ReplyService = response.ReplyService;
            GenSTS = response.GeneralStatus;

            ServiceReplies = new List<ServiceReply>();

            if (response.ResponseData != null)
                Expand(response.ResponseData);
        }

        public void Expand(byte[] Data)
        {
            int offset = 0;
            Count = BitConverter.ToUInt16(Data, 0);
            offset += 2;

            try
            {
                for (int i = 0; i < Count; i++)
                {
                    ushort pos = BitConverter.ToUInt16(Data, offset);
                    offset += 2;
                    ushort nextPos = BitConverter.ToUInt16(Data, offset);
                    if (i == (Count - 1))
                        nextPos = (ushort)(Data.Length);
                    ServiceReply svcReply = GetServiceReply(pos, Data, (ushort)(nextPos - pos));
                    ServiceReplies.Add(svcReply);
                }
            }
            catch { }

        }

        private ServiceReply GetServiceReply(ushort offset, byte[] data, ushort len)
        {
            return new ServiceReply(data, offset, len);
        }

    }

    internal class ServiceReply
    {
        public byte Service { get; internal set; }
        public byte Status { get; internal set; }
        public byte AdditionalStatusSize { get; internal set; }
        public byte[] AdditionalStatus { get; internal set; }
        public byte[] FullStatus { get; internal set; }
        public bool IsPartial { get; internal set; }
        public byte[] ServiceData { get; internal set; }

        public ServiceReply(byte[] sourceArray, int start, int len)
        {
            //First byte is the Service
            Service = sourceArray[start];
            Status = sourceArray[start + 2];
            if (sourceArray[start + 2] == 0x06)
                IsPartial = true;

            AdditionalStatusSize = sourceArray[start + 3];
            int offset = start + 4;

            if (AdditionalStatusSize > 0)
            {
                byte[] temp = new byte[AdditionalStatusSize * 2];
                Buffer.BlockCopy(sourceArray, offset, temp, 0, temp.Length);
                AdditionalStatus = temp; 
            }

            FullStatus = new byte[2 + (AdditionalStatusSize * 2)];
            Buffer.BlockCopy(BitConverter.GetBytes((ushort)Status), 0, FullStatus, 0, 2);
            if (AdditionalStatusSize > 0)
                Buffer.BlockCopy(AdditionalStatus, 0, FullStatus, 2, AdditionalStatus.Length);

            if (len > 4)
            {
                byte[] temp = new byte[len - 4];
                Buffer.BlockCopy(sourceArray, start + 4, temp, 0, temp.Length);
                ServiceData = temp;
            }
        }

    }
}
