using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlLogixNET.LogixType;
using EIPNET.CIP;

namespace ControlLogixNET
{
    internal class MemberInfo
    {
        public ushort MemberType;
        public ushort Info;
        public int MemberOffset;
        public string MemberName;
        public int MemberSize;
        public LogixTypes LogixType;
    }

    internal class TemplateInfo
    {
        public ushort NumberOfMembers { get; private set; }
        public ushort TagSize { get; private set; }
        public ushort TemplateHandle { get; private set; }
        public string TemplateName { get; private set; }
        public List<MemberInfo> MemberInfo { get; private set; }

        public TemplateInfo(byte[] templateData, GetStructAttribsReply attribs)
        {
            MemberInfo = new List<MemberInfo>();

            //Ok, this is a little confusing, there's supposed to be a header, but
            //it seems that the data returned is not in the format specified in the
            //1756-RM005 publication. There is no header data (meaning we can't
            //know how many elements are in the template), so we kinda have to
            //pick through and cheat... Luckily this data is found in the
            //GetStructAttribs reply

            //The format of the data returned is:
            //[2:Info][2:Type][4:Offset]
            //When we reach a byte that is greater than 0x00
            int offset = 0;
            int lastMember = -1;
            int lastOffset = 0;

            for (int i = 0; i < attribs.MemberCount; i++)
            {
                MemberInfo mi = new MemberInfo();
                mi.Info = BitConverter.ToUInt16(templateData, offset);
                offset += 2;
                mi.MemberType = BitConverter.ToUInt16(templateData, offset);
                offset += 2;
                mi.MemberOffset = BitConverter.ToInt32(templateData, offset);
                if (lastMember >= 0)
                {
                    //Compute size for the last member...
                    int size = mi.MemberOffset - lastOffset;
                    MemberInfo[lastMember].MemberSize = size;
                }
                lastOffset = mi.MemberOffset;
                lastMember++;
                offset += 4;
                switch ((CIPType)(mi.MemberType & 0x00FF))
                {
                    case CIPType.BOOL:
                        mi.LogixType = LogixTypes.Bool;
                        break;
                    case CIPType.DINT:
                        mi.LogixType = LogixTypes.DInt;
                        break;
                    case CIPType.INT:
                        mi.LogixType = LogixTypes.Int;
                        break;
                    case CIPType.LINT:
                        mi.LogixType = LogixTypes.LInt;
                        break;
                    case CIPType.REAL:
                        mi.LogixType = LogixTypes.Real;
                        break;
                    case CIPType.SINT:
                        mi.LogixType = LogixTypes.SInt;
                        break;
                    case CIPType.STRUCT:
                        mi.LogixType = LogixTypes.User_Defined;
                        break;
                    default:
                        mi.LogixType = LogixTypes.Unknown;
                        break;
                }
                MemberInfo.Add(mi);

            }

            //Compute the size for the last member
            int lastSize = TagSize - lastOffset;
            MemberInfo[MemberInfo.Count - 1].MemberSize = lastSize;

            NumberOfMembers = (ushort)MemberInfo.Count;
            TemplateHandle = attribs.Handle;
            TagSize = attribs.MemorySize;

            //And now we have to go through the rest of the data and pick out
            //null terminated strings...
            int start = offset;
            string currentStr = "";
            int idx = -1;
            for (int i = start; i < templateData.Length; i++)
            {
                if (templateData[i] == 0x00)
                {
                    if (string.IsNullOrEmpty(currentStr))
                        continue;

                    if (idx == -1)
                    {
                        //This is the structure name...
                        TemplateName = currentStr;
                        if (TemplateName.Contains(';'))
                            TemplateName = TemplateName.Substring(0, TemplateName.IndexOf(';'));
                    }
                    else
                    {
                        MemberInfo[idx].MemberName = currentStr;
                    }

                    currentStr = string.Empty;
                    idx++;
                }
                else
                {
                    currentStr += (char)templateData[i];
                }
            }

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(TemplateName);

            for (int i = 0; i < MemberInfo.Count; i++)
            {
                sb.AppendLine("\t" + MemberInfo[i].MemberName);
            }

            return sb.ToString();
        }
    }
}
