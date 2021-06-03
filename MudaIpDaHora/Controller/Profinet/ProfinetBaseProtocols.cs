/**************************************************************************
*                           MIT License
* 
* Copyright (C) 2014 Morten Kvistgaard <mk@pch-engineering.dk>
*
* Permission is hereby granted, free of charge, to any person obtaining
* a copy of this software and associated documentation files (the
* "Software"), to deal in the Software without restriction, including
* without limitation the rights to use, copy, modify, merge, publish,
* distribute, sublicense, and/or sell copies of the Software, and to
* permit persons to whom the Software is furnished to do so, subject to
* the following conditions:
*
* The above copyright notice and this permission notice shall be included
* in all copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
* EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
* MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
* IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
* CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
* TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
* SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;

namespace MudaIpDahora.Controller.Profinet
{
    public class Ethernet
    {
        public enum Type : ushort
        {
            None = 0,
            Loop = 96,
            Echo = 512,
            IpV4 = 2048,
            Arp = 2054,
            WakeOnLan = 2114,
            ReverseArp = 32821,
            AppleTalk = 32923,
            AppleTalkArp = 33011,
            VLanTaggedFrame = 33024,
            NovellInternetworkPacketExchange = 33079,
            Novell = 33080,
            IpV6 = 34525,
            MacControl = 34824,
            CobraNet = 34841,
            MultiprotocolLabelSwitchingUnicast = 34887,
            MultiprotocolLabelSwitchingMulticast = 34888,
            PointToPointProtocolOverEthernetDiscoveryStage = 34915,
            PointToPointProtocolOverEthernetSessionStage = 34916,
            ExtensibleAuthenticationProtocolOverLan = 34958,
            HyperScsi = 34970,
            AtaOverEthernet = 34978,
            EtherCatProtocol = 34980,
            ProviderBridging = 34984,
            AvbTransportProtocol = 34997,
            LLDP = 35020,
            SerialRealTimeCommunicationSystemIii = 35021,
            CircuitEmulationServicesOverEthernet = 35032,
            HomePlug = 35041,
            MacSecurity = 35045,
            PrecisionTimeProtocol = 35063,
            ConnectivityFaultManagementOrOperationsAdministrationManagement = 35074,
            FibreChannelOverEthernet = 35078,
            FibreChannelOverEthernetInitializationProtocol = 35092,
            QInQ = 37120,
            VeritasLowLatencyTransport = 51966,
        }

        public static int Encode(System.IO.Stream buffer, System.Net.NetworkInformation.PhysicalAddress destination, System.Net.NetworkInformation.PhysicalAddress source, Type type)
        {
            //destination
            DCP.EncodeOctets(buffer, destination.GetAddressBytes());

            //source
            DCP.EncodeOctets(buffer, source.GetAddressBytes());

            //type
            DCP.EncodeU16(buffer, (ushort)type);

            return 14;
        }
    }

    public class VLAN
    {
        public enum Priorities
        {
            /// <summary>
            /// DCP, IP
            /// </summary>
            Priority0 = 0,
            /// <summary>
            /// Low prior RTA_CLASS_1 or RTA_CLASS_UDP
            /// </summary>
            Priority5 = 5,
            /// <summary>
            /// RT_CLASS_UDP, RT_CLASS_1, RT_CLASS_2, RT_CLASS_3, high prior RTA_CLASS_1 or RTA_CLASS_UDP
            /// </summary>
            Priority6 = 6,
            /// <summary>
            /// PTCP-AnnouncePDU
            /// </summary>
            Priority7 = 7,
        }

        public enum Type : ushort
        {
            /// <summary>
            /// UDP, RPC, SNMP, ICMP
            /// </summary>
            IP = 0x0800,
            ARP = 0x0806,
            TagControlInformation = 0x8100,
            /// <summary>
            /// RTC, RTA, DCP, PTCP, FRAG
            /// </summary>
            PN = 0x8892,
            IEEE_802_1AS = 0x88F7,
            LLDP = 0x88CC,
            MRP = 0x88E3,
        }

        public static int Encode(System.IO.Stream buffer, Priorities priority, Type type)
        {
            UInt16 tmp = 0;

            //Priority
            tmp |= (UInt16)((((UInt16)priority) & 0x7) << 13);

            //CanonicalFormatIdentificator
            tmp |= 0 << 12;

            //VLAN_Id
            tmp |= 0;

            DCP.EncodeU16(buffer, tmp);
            DCP.EncodeU16(buffer, (UInt16)type);

            return 4;
        }
    }

    public class RT
    {
        public enum FrameIds : ushort
        {
            PTCP_RTSyncPDU_With_Follow_Up = 0x20,
            PTCP_RTSyncPDU = 0x80,
            Alarm_High = 0xFC01,
            Alarm_Low = 0xFE01,
            DCP_Hello_ReqPDU = 0xFEFC,
            DCP_Get_Set_PDU = 0xFEFD,
            DCP_Identify_ReqPDU = 0xFEFE,
            DCP_Identify_ResPDU = 0xFEFF,
            PTCP_AnnouncePDU = 0xFF00,
            PTCP_FollowUpPDU = 0xFF20,
            PTCP_DelayReqPDU = 0xFF40,
            PTCP_DelayResPDU_With_Follow_Up = 0xFF41,
            PTCP_DelayFuResPDU_With_Follow_Up = 0xFF42,
            PTCP_DelayResPDU = 0xFF43,
            RTC_Start = 0xC000,
            RTC_End = 0xF7FF,
        }

        public const string MulticastMACAdd_Identify_Address = "01-0E-CF-00-00-00";
        public const string MulticastMACAdd_Hello_Address = "01-0E-CF-00-00-01";
        public const string MulticastMACAdd_Range1_Destination_Address = "01-0E-CF-00-01-01";
        public const string MulticastMACAdd_Range1_Invalid_Address = "01-0E-CF-00-01-02";
        public const string PTCP_MulticastMACAdd_Range2_Clock_Synchronization_Address = "01-0E-CF-00-04-00";
        public const string PTCP_MulticastMACAdd_Range3_Clock_Synchronization_Address = "01-0E-CF-00-04-20";
        public const string PTCP_MulticastMACAdd_Range4_Clock_Synchronization_Address = "01-0E-CF-00-04-40";
        public const string PTCP_MulticastMACAdd_Range6_Clock_Synchronization_Address = "01-0E-CF-00-04-80";
        public const string PTCP_MulticastMACAdd_Range8_Address = "01-80-C2-00-00-0E";
        public const string RTC_PDU_RT_CLASS_3_Destination_Address = "01-0E-CF-00-01-01";
        public const string RTC_PDU_RT_CLASS_3_Invalid_Address = "01-0E-CF-00-01-02";

        public static int EncodeFrameId(System.IO.Stream buffer, FrameIds value)
        {
            return DCP.EncodeU16(buffer, (ushort)value);
        }

        public static int DecodeFrameId(System.IO.Stream buffer, out FrameIds value)
        {
            ushort val;
            DCP.DecodeU16(buffer, out val);
            value = (FrameIds)val;
            return 2;
        }

        [Flags]
        public enum DataStatus : byte
        {
            State_Primary = 1,  /* 0 is Backup */
            Redundancy_Backup = 2,  /* 0 is Primary */
            DataItemValid = 4,  /* 0 is invalid */
            ProviderState_Run = 1 << 4, /* 0 is stop */
            StationProblemIndicator_Normal = 1 << 5,    /* 0 is Detected */
            Ignore = 1 << 7,    /* 0 is Evaluate */
        }

        [Flags]
        public enum TransferStatus : byte
        {
            OK = 0,
            AlignmentOrFrameChecksumError = 1,
            WrongLengthError = 2,
            MACReceiveBufferOverflow = 4,
            RT_CLASS_3_Error = 8,
        }

        [Flags]
        public enum IOxS : byte
        {
            Extension_MoreIOxSOctetFollows = 1,
            Instance_DetectedBySubslot = 0 << 5,
            Instance_DetectedBySlot = 1 << 5,
            Instance_DetectedByIODevice = 2 << 5,
            Instance_DetectedByIOController = 3 << 5,
            DataState_Good = 1 << 7,
        }

        [Flags]
        public enum PDUTypes : byte
        {
            //0x00 Reserved —
            Data = 1, //Shall only be used to encode the DATA-RTA-PDU
            Nack = 2, //Shall only be used to encode the NACK-RTA-PDU
            Ack = 3, //Shall only be used to encode the ACK-RTA-PDU
            Err = 4, //Shall only be used to encode the ERR-RTA-PDU
            //0x05 – 0x0F Reserved —
            Version1 = 1 << 4,
        }

        [Flags]
        public enum AddFlags : byte
        {
            WindowSizeOne = 1,
            TACK_ImmediateAcknowledge = 1 << 4,
        }

        public static int DecodeRTCStatus(System.IO.Stream buffer, out UInt16 CycleCounter, out DataStatus DataStatus, out TransferStatus TransferStatus)
        {
            int ret = 0;
            byte tmp;

            ret += DCP.DecodeU16(buffer, out CycleCounter);
            ret += DCP.DecodeU8(buffer, out tmp);
            DataStatus = (DataStatus)tmp;
            ret += DCP.DecodeU8(buffer, out tmp);
            TransferStatus = (TransferStatus)tmp;

            return ret;
        }

        public static int EncodeRTCStatus(System.IO.Stream buffer, UInt16 CycleCounter, DataStatus DataStatus, TransferStatus TransferStatus)
        {
            int ret = 0;

            ret += DCP.EncodeU16(buffer, CycleCounter);
            ret += DCP.EncodeU8(buffer, (byte)DataStatus);
            ret += DCP.EncodeU8(buffer, (byte)TransferStatus);

            return ret;
        }

        public static int DecodeRTAHeader(System.IO.Stream buffer, out UInt16 AlarmDestinationEndpoint, out UInt16 AlarmSourceEndpoint, out PDUTypes PDUType, out AddFlags AddFlags, out UInt16 SendSeqNum, out UInt16 AckSeqNum, out UInt16 VarPartLen)
        {
            int ret = 0;
            byte tmp;

            ret += DCP.DecodeU16(buffer, out AlarmDestinationEndpoint);
            ret += DCP.DecodeU16(buffer, out AlarmSourceEndpoint);
            ret += DCP.DecodeU8(buffer, out tmp);
            PDUType = (PDUTypes)tmp;
            ret += DCP.DecodeU8(buffer, out tmp);
            AddFlags = (AddFlags)tmp;
            ret += DCP.DecodeU16(buffer, out SendSeqNum);
            ret += DCP.DecodeU16(buffer, out AckSeqNum);
            ret += DCP.DecodeU16(buffer, out VarPartLen);

            return ret;
        }
    }

    public interface IProfinetSerialize
    {
        int Serialize(System.IO.Stream buffer);
    }

    public interface IProfinetDeserialize   //Should be merged with IProfinetSerialize, at some point
    {
        int Deserialize(System.IO.Stream buffer);
    }

    public class DCP
    {
        [Flags]
        public enum ServiceIds : ushort
        {
            Get_Request = 0x0300,
            Get_Response = 0x0301,
            Set_Request = 0x0400,
            Set_Response = 0x0401,
            Identify_Request = 0x0500,
            Identify_Response = 0x0501,
            Hello_Request = 0x0600,
            ServiceIDNotSupported = 0x0004,
        }

        public enum BlockOptions : ushort
        {
            //IP
            IP_MACAddress = 0x0101,
            IP_IPParameter = 0x0102,
            IP_FullIPSuite = 0x0103,

            //DeviceProperties
            DeviceProperties_DeviceVendor = 0x0201,
            DeviceProperties_NameOfStation = 0x0202,
            DeviceProperties_DeviceID = 0x0203,
            DeviceProperties_DeviceRole = 0x0204,
            DeviceProperties_DeviceOptions = 0x0205,
            DeviceProperties_AliasName = 0x0206,
            DeviceProperties_DeviceInstance = 0x0207,
            DeviceProperties_OEMDeviceID = 0x0208,

            //DHCP
            DHCP_HostName = 0x030C,
            DHCP_VendorSpecificInformation = 0x032B,
            DHCP_ServerIdentifier = 0x0336,
            DHCP_ParameterRequestList = 0x0337,
            DHCP_ClassIdentifier = 0x033C,
            DHCP_DHCPClientIdentifier = 0x033D,
            DHCP_FullyQualifiedDomainName = 0x0351,
            DHCP_UUIDClientIdentifier = 0x0361,
            DHCP_DHCP = 0x03FF,

            //Control
            Control_Start = 0x0501,
            Control_Stop = 0x0502,
            Control_Signal = 0x0503,
            Control_Response = 0x0504,
            Control_FactoryReset = 0x0505,
            Control_ResetToFactory = 0x0506,

            //DeviceInitiative
            DeviceInitiative_DeviceInitiative = 0x0601,

            //AllSelector
            AllSelector_AllSelector = 0xFFFF,
        }

        public enum BlockQualifiers : ushort
        {
            Temporary = 0,
            Permanent = 1,

            ResetApplicationData = 2,
            ResetCommunicationParameter = 4,
            ResetEngineeringParameter = 6,
            ResetAllStoredData = 8,
            ResetDevice = 16,
            ResetAndRestoreData = 18,
        }

        [Flags]
        public enum BlockInfo : ushort
        {
            IpSet = 1,
            IpSetViaDhcp = 2,
            IpConflict = 0x80,
        }

        [Flags]
        public enum DeviceRoles : byte
        {
            Device = 1,
            Controller = 2,
            Multidevice = 4,
            Supervisor = 8,
        }

        public enum BlockErrors : byte
        {
            NoError = 0,
            OptionNotSupported = 1,
            SuboptionNotSupported = 2,
            SuboptionNotSet = 3,
            ResourceError = 4,
            SetNotPossible = 5,
            Busy = 6,
        }

        public static int EncodeU32(System.IO.Stream buffer, UInt32 value)
        {
            buffer.WriteByte((byte)((value & 0xFF000000) >> 24));
            buffer.WriteByte((byte)((value & 0x00FF0000) >> 16));
            buffer.WriteByte((byte)((value & 0x0000FF00) >> 08));
            buffer.WriteByte((byte)((value & 0x000000FF) >> 00));
            return 4;
        }

        public static int EncodeU16(System.IO.Stream buffer, UInt16 value)
        {
            buffer.WriteByte((byte)((value & 0xFF00) >> 08));
            buffer.WriteByte((byte)((value & 0x00FF) >> 00));
            return 2;
        }

        public static int EncodeU8(System.IO.Stream buffer, byte value)
        {
            buffer.WriteByte((byte)value);
            return 1;
        }

        public static int DecodeU16(System.IO.Stream buffer, out UInt16 value)
        {
            if (buffer.Position >= buffer.Length)
            {
                value = 0;
                return 0;
            }
            value = (UInt16)((buffer.ReadByte() << 8) | buffer.ReadByte());
            return 2;
        }

        public static int DecodeU8(System.IO.Stream buffer, out byte value)
        {
            if (buffer.Position >= buffer.Length)
            {
                value = 0;
                return 0;
            }
            value = (byte)buffer.ReadByte();
            return 1;
        }

        public static int DecodeU32(System.IO.Stream buffer, out UInt32 value)
        {
            if (buffer.Position >= buffer.Length)
            {
                value = 0;
                return 0;
            }
            value = (UInt32)((buffer.ReadByte() << 24) | (buffer.ReadByte() << 16) | (buffer.ReadByte() << 8) | buffer.ReadByte());
            return 4;
        }

        public static int EncodeString(System.IO.Stream buffer, string value)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(value);
            buffer.Write(bytes, 0, bytes.Length);
            return bytes.Length;
        }

        public static int DecodeString(System.IO.Stream buffer, int length, out string value)
        {
            byte[] tmp = new byte[length];
            buffer.Read(tmp, 0, length);
            value = Encoding.ASCII.GetString(tmp);
            return tmp.Length;
        }

        public static int EncodeOctets(System.IO.Stream buffer, byte[] value)
        {
            if (value == null || value.Length == 0) return 0;
            buffer.Write(value, 0, value.Length);
            return value.Length;
        }

        public static int DecodeOctets(System.IO.Stream buffer, int length, out byte[] value)
        {
            if (length <= 0)
            {
                value = null;
                return 0;
            }
            value = new byte[length];
            buffer.Read(value, 0, length);
            return value.Length;
        }

        public static int EncodeHeader(System.IO.Stream buffer, ServiceIds ServiceID, UInt32 Xid, UInt16 ResponseDelayFactor, UInt16 DCPDataLength)
        {
            long dummy;
            return EncodeHeader(buffer, ServiceID, Xid, ResponseDelayFactor, DCPDataLength, out dummy);
        }

        public static int EncodeHeader(System.IO.Stream buffer, ServiceIds ServiceID, UInt32 Xid, UInt16 ResponseDelayFactor, UInt16 DCPDataLength, out long DCPDataLength_pos)
        {
            EncodeU16(buffer, (ushort)ServiceID);

            //big endian uint32
            EncodeU32(buffer, Xid);

            //ResponseDelayFactor, 1 = Allowed value without spread, 2 – 0x1900 = Allowed value with spread
            EncodeU16(buffer, ResponseDelayFactor);

            DCPDataLength_pos = buffer.Position;
            EncodeU16(buffer, DCPDataLength);

            return 10;
        }

        public static void ReEncodeDCPDataLength(System.IO.Stream buffer, long DCPDataLength_pos)
        {
            long current_pos = buffer.Position;
            buffer.Position = DCPDataLength_pos;
            EncodeU16(buffer, (ushort)(current_pos - buffer.Position - 2));
            buffer.Position = current_pos;
        }

        public static int DecodeHeader(System.IO.Stream buffer, out ServiceIds ServiceID, out UInt32 Xid, out UInt16 ResponseDelayFactor, out UInt16 DCPDataLength)
        {
            ushort val;
            DecodeU16(buffer, out val);
            ServiceID = (ServiceIds)val;

            //big endian uint32
            DecodeU32(buffer, out Xid);

            //ResponseDelayFactor, 1 = Allowed value without spread, 2 – 0x1900 = Allowed value with spread
            DecodeU16(buffer, out ResponseDelayFactor);

            DecodeU16(buffer, out DCPDataLength);

            return 10;
        }

        public static int EncodeBlock(System.IO.Stream buffer, BlockOptions options, UInt16 DCPBlockLength)
        {
            long dummy;
            return EncodeBlock(buffer, options, DCPBlockLength, out dummy);
        }

        public static int EncodeBlock(System.IO.Stream buffer, BlockOptions options, UInt16 DCPBlockLength, out long DCPBlockLength_pos)
        {
            EncodeU16(buffer, (ushort)options);
            DCPBlockLength_pos = buffer.Position;
            EncodeU16(buffer, DCPBlockLength);
            return 4;
        }

        public static int DecodeBlock(System.IO.Stream buffer, out BlockOptions options, out UInt16 DCPBlockLength)
        {
            ushort opt;
            DecodeU16(buffer, out opt);
            options = (BlockOptions)opt;
            DecodeU16(buffer, out DCPBlockLength);
            return 4;
        }

        public static int EncodeIdentifyResponse(System.IO.Stream buffer, UInt32 Xid, Dictionary<DCP.BlockOptions, object> blocks)
        {
            int ret = 0;
            long dcp_data_length_pos;

            //Header
            ret += EncodeHeader(buffer, ServiceIds.Identify_Response, Xid, 0, 0, out dcp_data_length_pos);

            //{ IdentifyResBlock, NameOfStationBlockRes,IPParameterBlockRes, DeviceIDBlockRes, DeviceVendorBlockRes,DeviceOptionsBlockRes, DeviceRoleBlockRes, [DeviceInitiativeBlockRes],[DeviceInstanceBlockRes], [OEMDeviceIDBlockRes] }

            foreach (KeyValuePair<DCP.BlockOptions, object> entry in blocks)
            {
                ret += EncodeNextBlock(buffer, entry);
            }

            //adjust dcp_length
            ReEncodeDCPDataLength(buffer, dcp_data_length_pos);

            return ret;
        }

        /// <summary>
        /// This is a helper class for the block options
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter)), Serializable]
        public class BlockOptionMeta
        {
            public string Name { get; set; }
            public bool IsReadable { get; set; }
            public bool IsWriteable { get; set; }

            [Browsable(false), System.Xml.Serialization.XmlIgnore]
            public BlockOptions BlockOption { get; set; }

            public byte Option
            {
                get
                {
                    return (byte)((((UInt16)BlockOption) & 0xFF00) >> 8);
                }
                set
                {
                    BlockOption = (BlockOptions)((((UInt16)BlockOption) & 0x00FF) | ((UInt16)value << 8));
                }
            }

            public byte SubOption
            {
                get
                {
                    return (byte)((((UInt16)BlockOption) & 0x00FF) >> 0);
                }
                set
                {
                    BlockOption = (BlockOptions)((((UInt16)BlockOption) & 0xFF00) | ((UInt16)value << 0));
                }
            }

            private BlockOptionMeta()   //For XmlSerializer
            {
            }

            public BlockOptionMeta(string name, byte option, byte sub_option, bool is_readable, bool is_writeable)
            {
                this.BlockOption = 0;
                this.Name = name;
                this.Option = option;
                this.SubOption = sub_option;
                this.IsReadable = is_readable;
                this.IsWriteable = is_writeable;
            }

            public BlockOptionMeta(BlockOptions opt)
            {
                BlockOption = opt;
                Name = opt.ToString();
                if (opt == BlockOptions.IP_MACAddress ||
                    opt == BlockOptions.DeviceProperties_DeviceID ||
                    opt == BlockOptions.DeviceProperties_DeviceVendor ||
                    opt == BlockOptions.DeviceProperties_DeviceRole ||
                    opt == BlockOptions.DeviceProperties_DeviceOptions ||
                    opt == BlockOptions.DeviceProperties_DeviceInstance ||
                    opt == BlockOptions.DeviceProperties_OEMDeviceID ||
                    opt == BlockOptions.DeviceInitiative_DeviceInitiative)
                {
                    IsReadable = true;
                }
                else if (opt == BlockOptions.DeviceProperties_AliasName ||
                         opt == BlockOptions.Control_Response ||
                         opt == BlockOptions.AllSelector_AllSelector)
                {
                    //none
                }
                else if (opt == BlockOptions.Control_Start ||
                         opt == BlockOptions.Control_Stop ||
                         opt == BlockOptions.Control_Signal ||
                         opt == BlockOptions.Control_FactoryReset ||
                         opt == BlockOptions.Control_ResetToFactory)
                {
                    IsWriteable = true;
                }
                else
                {
                    //default
                    IsReadable = true;
                    IsWriteable = true;
                }
            }
        }

        public class IpAddressConverter : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string)) return true;
                return base.CanConvertFrom(context, sourceType);
            }
            public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
            {
                if (value is string)
                    return System.Net.IPAddress.Parse((string)value);
                return base.ConvertFrom(context, culture, value);
            }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class IpInfo : IProfinetSerialize
        {
            public BlockInfo Info { get; set; }
            [TypeConverter(typeof(IpAddressConverter))]
            public System.Net.IPAddress Ip { get; set; }
            [TypeConverter(typeof(IpAddressConverter))]
            public System.Net.IPAddress SubnetMask { get; set; }
            [TypeConverter(typeof(IpAddressConverter))]
            public System.Net.IPAddress Gateway { get; set; }
            public IpInfo(BlockInfo info, byte[] ip, byte[] subnet, byte[] gateway)
            {
                Info = info;
                Ip = new System.Net.IPAddress(ip);
                SubnetMask = new System.Net.IPAddress(subnet);
                Gateway = new System.Net.IPAddress(gateway);
            }
            public override string ToString()
            {
                return "{" + Ip.ToString() + " - " + SubnetMask.ToString() + " - " + Gateway.ToString() + "}";
            }

            public int Serialize(System.IO.Stream buffer)
            {
                int ret = 0;
                byte[] tmp;
                tmp = Ip.GetAddressBytes();
                buffer.Write(tmp, 0, tmp.Length);
                ret += tmp.Length;
                tmp = SubnetMask.GetAddressBytes();
                buffer.Write(tmp, 0, tmp.Length);
                ret += tmp.Length;
                tmp = Gateway.GetAddressBytes();
                buffer.Write(tmp, 0, tmp.Length);
                ret += tmp.Length;
                return ret;
            }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class DeviceIdInfo : IProfinetSerialize
        {
            public UInt16 VendorId { get; set; }
            public UInt16 DeviceId { get; set; }
            public DeviceIdInfo(UInt16 vendor_id, UInt16 device_id)
            {
                VendorId = vendor_id;
                DeviceId = device_id;
            }
            public override string ToString()
            {
                return "Vendor 0x" + VendorId.ToString("X") + " - Device 0x" + DeviceId.ToString("X");
            }

            public int Serialize(System.IO.Stream buffer)
            {
                int ret = 0;
                ret += EncodeU16(buffer, VendorId);
                ret += EncodeU16(buffer, DeviceId);
                return ret;
            }
            public override bool Equals(object obj)
            {
                if (!(obj is DeviceIdInfo)) return false;
                DeviceIdInfo o = (DeviceIdInfo)obj;
                return o.DeviceId == DeviceId && o.VendorId == VendorId;
            }
            public override int GetHashCode()
            {
                UInt32 tmp = (uint)(VendorId << 16);
                tmp |= DeviceId;
                return tmp.GetHashCode();
            }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class DeviceRoleInfo : IProfinetSerialize
        {
            public DeviceRoles DeviceRole { get; set; }

            public DeviceRoleInfo(DeviceRoles device_role)
            {
                DeviceRole = device_role;
            }

            public int Serialize(System.IO.Stream buffer)
            {
                int ret = 0;
                ret += EncodeU8(buffer, (byte)DeviceRole);
                buffer.WriteByte(0);    //padding
                ret++;
                return ret;
            }

            public override string ToString()
            {
                return DeviceRole.ToString();
            }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class ResponseStatus
        {
            public BlockOptions Option { get; set; }
            public BlockErrors Error { get; set; }
            public ResponseStatus()
            {
            }
            public ResponseStatus(BlockOptions Option, BlockErrors Error)
            {
                this.Option = Option;
                this.Error = Error;
            }
            public override string ToString()
            {
                return Error.ToString();
            }
        }

        private static int EncodeNextBlock(System.IO.Stream buffer, KeyValuePair<BlockOptions, object> block)
        {
            int ret = 0;
            int tmp = 0;
            long DCPBlockLength_pos;

            ret += EncodeBlock(buffer, block.Key, 0, out DCPBlockLength_pos);

            //block info
            if (block.Value != null) ret += EncodeU16(buffer, 0);
            else return ret;

            if (block.Value is ResponseStatus)
            {
                buffer.Position -= 2;
                EncodeU16(buffer, (ushort)((ResponseStatus)block.Value).Option);
                tmp += EncodeU8(buffer, (byte)((ResponseStatus)block.Value).Error);
            }
            else if (block.Value is IProfinetSerialize)
            {
                tmp += ((IProfinetSerialize)block.Value).Serialize(buffer);
            }
            else if (block.Value is string)
            {
                tmp += EncodeString(buffer, (string)block.Value);
            }
            else if (block.Value is byte[])
            {
                tmp += EncodeOctets(buffer, (byte[])block.Value);
            }
            else if (block.Value is BlockOptions[])
            {
                foreach (BlockOptions b in (BlockOptions[])block.Value)
                {
                    tmp += EncodeU16(buffer, (ushort)b);
                }
            }
            else
            {
                throw new NotImplementedException();
            }

            //adjust length
            ReEncodeDCPDataLength(buffer, DCPBlockLength_pos);

            //padding
            ret += tmp;
            if ((tmp % 2) != 0)
            {
                buffer.WriteByte(0);
                ret++;
            }

            return ret;
        }

        private static int DecodeNextBlock(System.IO.Stream buffer, ushort dcp_length, out KeyValuePair<BlockOptions, object> block)
        {
            int ret = 0;
            BlockOptions options;
            UInt16 dcp_block_length;
            UInt16 block_info = 0;
            UInt16 tmp, tmp2;
            string str;
            object content;
            ResponseStatus set_response;

            if (buffer.Position >= buffer.Length || dcp_length <= 0)
            {
                block = new KeyValuePair<BlockOptions, object>(0, null);
                return ret;
            }

            ret += DecodeBlock(buffer, out options, out dcp_block_length);
            if (dcp_block_length >= 2) ret += DecodeU16(buffer, out block_info);
            dcp_block_length -= 2;

            switch (options)
            {
                case BlockOptions.DeviceProperties_NameOfStation:
                    ret += DecodeString(buffer, dcp_block_length, out str);
                    content = str;
                    break;
                case BlockOptions.IP_IPParameter:
                    byte[] ip, subnet, gateway;
                    ret += DecodeOctets(buffer, 4, out ip);
                    ret += DecodeOctets(buffer, 4, out subnet);
                    ret += DecodeOctets(buffer, 4, out gateway);
                    content = new IpInfo((BlockInfo)block_info, ip, subnet, gateway); ;
                    break;
                case BlockOptions.DeviceProperties_DeviceID:
                    ret += DecodeU16(buffer, out tmp);
                    ret += DecodeU16(buffer, out tmp2);
                    content = new DeviceIdInfo(tmp, tmp2);
                    break;
                case BlockOptions.DeviceProperties_DeviceOptions:
                    BlockOptions[] option_list = new BlockOptions[dcp_block_length / 2];
                    for (int i = 0; i < option_list.Length; i++)
                    {
                        ret += DecodeU16(buffer, out tmp);
                        option_list[i] = (BlockOptions)tmp;
                    }
                    content = option_list;
                    break;
                case BlockOptions.DeviceProperties_DeviceRole:
                    DeviceRoles roles = (DeviceRoles)buffer.ReadByte();
                    buffer.ReadByte(); //padding
                    ret += 2;
                    content = new DeviceRoleInfo(roles);
                    break;
                case BlockOptions.DeviceProperties_DeviceVendor:
                    ret += DecodeString(buffer, dcp_block_length, out str);
                    content = str;
                    break;
                case BlockOptions.Control_Response:
                    set_response = new ResponseStatus();
                    set_response.Option = (BlockOptions)block_info;
                    set_response.Error = (BlockErrors)buffer.ReadByte();
                    ret++;
                    content = set_response;
                    break;
                default:
                    byte[] arr;
                    ret += DecodeOctets(buffer, dcp_block_length, out arr);
                    content = arr;
                    break;
            }
            block = new KeyValuePair<BlockOptions, object>(options, content);

            //padding
            if ((dcp_block_length % 2) != 0)
            {
                buffer.ReadByte();
                ret++;
            }

            return ret;
        }

        public static int DecodeAllBlocks(System.IO.Stream buffer, ushort dcp_length, out Dictionary<BlockOptions, object> blocks)
        {
            int ret = 0, r;
            KeyValuePair<Profinet.DCP.BlockOptions, object> value;
            blocks = new Dictionary<BlockOptions, object>();
            while ((r = Profinet.DCP.DecodeNextBlock(buffer, (ushort)(dcp_length - ret), out value)) > 0)
            {
                ret += r;
                if (!blocks.ContainsKey(value.Key)) blocks.Add(value.Key, value.Value);
                else Trace.TraceError("Multiple blocks in reply: " + value.Key);
            }
            if (r < 0) return r;    //error
            else return ret;
        }

        public static int EncodeIdentifyRequest(System.IO.Stream buffer, UInt32 Xid)
        {
            int ret = 0;

            //Header
            ret += EncodeHeader(buffer, ServiceIds.Identify_Request, Xid, 1, 4);

            //optional filter (instead of the ALL block)
            //[NameOfStationBlock] ^ [AliasNameBlock], IdentifyReqBlock

            //IdentifyReqBlock
            /*  DeviceRoleBlock ^ DeviceVendorBlock ^ DeviceIDBlock ^
                DeviceOptionsBlock ^ OEMDeviceIDBlock ^ MACAddressBlock ^
                IPParameterBlock ^ DHCPParameterBlock ^
                ManufacturerSpecificParameterBlock */

            //AllSelectorType
            ret += EncodeBlock(buffer, BlockOptions.AllSelector_AllSelector, 0);

            return ret;
        }

        public static int EncodeSetRequest(System.IO.Stream buffer, UInt32 Xid, BlockOptions options, BlockQualifiers qualifiers, byte[] data)
        {
            int ret = 0;
            int data_length = 0;
            bool do_pad = false;

            if (data != null) data_length = data.Length;
            if ((data_length % 2) != 0) do_pad = true;

            //The following is modified by F.Chaxel. 
            //TODO: Test that decode still work

            ret += EncodeHeader(buffer, ServiceIds.Set_Request, Xid, 0, (ushort)(12 + data_length + (do_pad ? 1 : 0)));
            ret += EncodeBlock(buffer, options, (ushort)(2 + data_length));

            ret += EncodeU16(buffer, (ushort)0); // Don't care

            //data
            EncodeOctets(buffer, data);

            //pad (re-ordered by f.chaxel)
            if (do_pad) ret += EncodeU8(buffer, 0);

            //BlockQualifier
            ret += EncodeBlock(buffer, BlockOptions.Control_Stop, (ushort)(2 + data_length));
            ret += EncodeU16(buffer, (ushort)qualifiers);

            return ret;
        }

        public static int EncodeGetRequest(System.IO.Stream buffer, UInt32 Xid, BlockOptions options)
        {
            int ret = 0;

            ret += EncodeHeader(buffer, ServiceIds.Get_Request, Xid, 0, 2);
            ret += EncodeU16(buffer, (ushort)options);

            return ret;
        }

        public static int EncodeHelloRequest(System.IO.Stream buffer, UInt32 Xid)
        {
            throw new NotImplementedException();
        }

        public static int EncodeGetResponse(System.IO.Stream buffer, UInt32 Xid, BlockOptions options, object data)
        {
            int ret = 0;
            long dcp_length;

            ret += EncodeHeader(buffer, ServiceIds.Get_Response, Xid, 0, 0, out dcp_length);
            ret += EncodeNextBlock(buffer, new KeyValuePair<BlockOptions, object>(options, data));
            ReEncodeDCPDataLength(buffer, dcp_length);

            return ret;
        }

        public static int EncodeSetResponse(System.IO.Stream buffer, UInt32 Xid, BlockOptions options, BlockErrors status)
        {
            int ret = 0;
            long dcp_length;

            ret += EncodeHeader(buffer, ServiceIds.Set_Response, Xid, 0, 0, out dcp_length);
            ret += EncodeNextBlock(buffer, new KeyValuePair<BlockOptions, object>(BlockOptions.Control_Response, new ResponseStatus(options, status)));
            ReEncodeDCPDataLength(buffer, dcp_length);

            return ret;
        }
    }

    public class RPC
    {
        public static Guid UUID_IO_ObjectInstance_XYZ = Guid.Parse("DEA00000-6C97-11D1-8271-000000000000");
        public static Guid UUID_IO_DeviceInterface = Guid.Parse("DEA00001-6C97-11D1-8271-00A02442DF7D");
        public static Guid UUID_IO_ControllerInterface = Guid.Parse("DEA00002-6C97-11D1-8271-00A02442DF7D");
        public static Guid UUID_IO_SupervisorInterface = Guid.Parse("DEA00003-6C97-11D1-8271-00A02442DF7D");
        public static Guid UUID_IO_ParameterServerInterface = Guid.Parse("DEA00004-6C97-11D1-8271-00A02442DF7D");
        public static Guid UUID_EPMap_Interface = Guid.Parse("E1AF8308-5D1F-11C9-91A4-08002B14A0FA");
        public static Guid UUID_EPMap_Object = Guid.Parse("00000000-0000-0000-0000-000000000000");

        public enum PacketTypes : byte
        {
            Request = 0,
            Ping = 1,
            Response = 2,
            Fault = 3,
            Working = 4,
            NoCall = 5,
            Reject = 6,
            Acknowledge = 7,
            ConnectionlessCancel = 8,
            FragmentAcknowledge = 9,
            CancelAcknowledge = 10,
        }

        [Flags]
        public enum Flags1 : byte
        {
            LastFragment = 2,
            Fragment = 4,
            NoFragmentAckRequested = 8,
            Maybe = 16,
            Idempotent = 32,
            Broadcast = 64,
        }

        [Flags]
        public enum Flags2 : byte
        {
            CancelPendingAtCallEnd = 2,
        }

        [Flags]
        public enum Encodings : ushort
        {
            ASCII = 0x000,
            EBCDIC = 0x100,
            BigEndian = 0x000,
            LittleEndian = 0x1000,
            IEEE_float = 0,
            VAX_float = 1,
            CRAY_float = 2,
            IBM_float = 3,
        }

        public enum Operations : ushort
        {
            //IO device
            Connect = 0,
            Release = 1,
            Read = 2,
            Write = 3,
            Control = 4,
            ReadImplicit = 5,

            //Endpoint mapper
            Insert = 0,
            Delete = 1,
            Lookup = 2,
            Map = 3,
            LookupHandleFree = 4,
            InqObject = 5,
            MgmtDelete = 6,
        }

        public static Guid GenerateObjectInstanceUUID(UInt16 InstanceNo, byte InterfaceNo, UInt16 DeviceId, UInt16 VendorId)
        {
            byte[] bytes = UUID_IO_ObjectInstance_XYZ.ToByteArray();
            UInt32 Data1 = BitConverter.ToUInt32(bytes, 0);
            UInt16 Data2 = BitConverter.ToUInt16(bytes, 4);
            UInt16 Data3 = BitConverter.ToUInt16(bytes, 6);
            byte Data4 = bytes[8];
            byte Data5 = bytes[9];
            InstanceNo &= 0xFFF;
            InstanceNo |= (UInt16)(InterfaceNo << 12);
            Guid ret = new Guid(Data1, Data2, Data3, Data4, Data5, (byte)(InstanceNo >> 8), (byte)(InstanceNo & 0xFF), (byte)(DeviceId >> 8), (byte)(DeviceId & 0xFF), (byte)(VendorId >> 8), (byte)(VendorId & 0xFF));
            return ret;
        }

        public static int EncodeU32(System.IO.Stream buffer, Encodings encoding, UInt32 value)
        {
            if ((encoding & Encodings.LittleEndian) == Encodings.BigEndian)
            {
                buffer.WriteByte((byte)((value & 0xFF000000) >> 24));
                buffer.WriteByte((byte)((value & 0x00FF0000) >> 16));
                buffer.WriteByte((byte)((value & 0x0000FF00) >> 08));
                buffer.WriteByte((byte)((value & 0x000000FF) >> 00));
            }
            else
            {
                buffer.WriteByte((byte)((value & 0x000000FF) >> 00));
                buffer.WriteByte((byte)((value & 0x0000FF00) >> 08));
                buffer.WriteByte((byte)((value & 0x00FF0000) >> 16));
                buffer.WriteByte((byte)((value & 0xFF000000) >> 24));
            }
            return 4;
        }

        public static int DecodeU32(System.IO.Stream buffer, Encodings encoding, out UInt32 value)
        {
            if ((encoding & Encodings.LittleEndian) == Encodings.BigEndian)
            {
                value = (UInt32)((buffer.ReadByte() << 24) | (buffer.ReadByte() << 16) | (buffer.ReadByte() << 8) | (buffer.ReadByte() << 0));
            }
            else
            {
                value = (UInt32)((buffer.ReadByte() << 0) | (buffer.ReadByte() << 8) | (buffer.ReadByte() << 16) | (buffer.ReadByte() << 24));
            }
            return 4;
        }

        public static int EncodeU16(System.IO.Stream buffer, Encodings encoding, UInt16 value)
        {
            if ((encoding & Encodings.LittleEndian) == Encodings.BigEndian)
            {
                buffer.WriteByte((byte)((value & 0x0000FF00) >> 08));
                buffer.WriteByte((byte)((value & 0x000000FF) >> 00));
            }
            else
            {
                buffer.WriteByte((byte)((value & 0x000000FF) >> 00));
                buffer.WriteByte((byte)((value & 0x0000FF00) >> 08));
            }
            return 2;
        }

        public static int DecodeU16(System.IO.Stream buffer, Encodings encoding, out UInt16 value)
        {
            if ((encoding & Encodings.LittleEndian) == Encodings.BigEndian)
            {
                value = (UInt16)((buffer.ReadByte() << 8) | (buffer.ReadByte() << 0));
            }
            else
            {
                value = (UInt16)((buffer.ReadByte() << 0) | (buffer.ReadByte() << 8));
            }
            return 2;
        }

        public static int EncodeGuid(System.IO.Stream buffer, Encodings encoding, Guid value)
        {
            int ret = 0;
            byte[] bytes = value.ToByteArray();
            UInt32 Data1 = BitConverter.ToUInt32(bytes, 0);
            UInt16 Data2 = BitConverter.ToUInt16(bytes, 4);
            UInt16 Data3 = BitConverter.ToUInt16(bytes, 6);
            byte[] Data4 = new byte[8];
            Array.Copy(bytes, 8, Data4, 0, 8);
            ret += EncodeU32(buffer, encoding, Data1);
            ret += EncodeU16(buffer, encoding, Data2);
            ret += EncodeU16(buffer, encoding, Data3);
            ret += DCP.EncodeOctets(buffer, Data4);
            return ret;
        }

        public static int DecodeGuid(System.IO.Stream buffer, Encodings encoding, out Guid value)
        {
            int ret = 0;
            UInt32 Data1;
            UInt16 Data2;
            UInt16 Data3;
            byte[] Data4 = new byte[8];

            ret += DecodeU32(buffer, encoding, out Data1);
            ret += DecodeU16(buffer, encoding, out Data2);
            ret += DecodeU16(buffer, encoding, out Data3);
            buffer.Read(Data4, 0, Data4.Length);
            ret += Data4.Length;

            value = new Guid((int)Data1, (short)Data2, (short)Data3, Data4);

            return ret;
        }

        public static int EncodeHeader(System.IO.Stream buffer, PacketTypes type, Flags1 flags1, Flags2 flags2, Encodings encoding, UInt16 serial_high_low, Guid object_id, Guid interface_id, Guid activity_id, UInt32 server_boot_time, UInt32 sequence_no, Operations op, UInt16 body_length, UInt16 fragment_no, out long body_length_position)
        {
            int ret = 0;

            ret += DCP.EncodeU8(buffer, 4); //RPCVersion
            ret += DCP.EncodeU8(buffer, (byte)type);
            ret += DCP.EncodeU8(buffer, (byte)flags1);
            ret += DCP.EncodeU8(buffer, (byte)flags2);
            ret += DCP.EncodeU16(buffer, (ushort)encoding);
            ret += DCP.EncodeU8(buffer, 0); //pad
            ret += DCP.EncodeU8(buffer, (byte)(serial_high_low >> 8));
            ret += EncodeGuid(buffer, encoding, object_id);
            ret += EncodeGuid(buffer, encoding, interface_id);
            ret += EncodeGuid(buffer, encoding, activity_id);
            ret += EncodeU32(buffer, encoding, server_boot_time);
            ret += EncodeU32(buffer, encoding, 1);   //interface version
            ret += EncodeU32(buffer, encoding, sequence_no);
            ret += EncodeU16(buffer, encoding, (ushort)op);
            ret += EncodeU16(buffer, encoding, 0xFFFF);     //interface hint
            ret += EncodeU16(buffer, encoding, 0xFFFF);     //activity hint
            body_length_position = buffer.Position;
            ret += EncodeU16(buffer, encoding, body_length);
            ret += EncodeU16(buffer, encoding, fragment_no);
            ret += DCP.EncodeU8(buffer, 0); //authentication protocol
            ret += DCP.EncodeU8(buffer, (byte)(serial_high_low & 0xFF));

            return ret;
        }

        public static int DecodeHeader(System.IO.Stream buffer, out PacketTypes type, out Flags1 flags1, out Flags2 flags2, out Encodings encoding, out UInt16 serial_high_low, out Guid object_id, out Guid interface_id, out Guid activity_id, out UInt32 server_boot_time, out UInt32 sequence_no, out Operations op, out UInt16 body_length, out UInt16 fragment_no)
        {
            int ret = 0;
            byte tmp1;
            UInt16 tmp2;
            UInt32 tmp3;

            serial_high_low = 0;

            ret += DCP.DecodeU8(buffer, out tmp1); //RPCVersion
            if (tmp1 != 4) throw new Exception("Wrong protocol");
            ret += DCP.DecodeU8(buffer, out tmp1);
            type = (PacketTypes)tmp1;
            ret += DCP.DecodeU8(buffer, out tmp1);
            flags1 = (Flags1)tmp1;
            ret += DCP.DecodeU8(buffer, out tmp1);
            flags2 = (Flags2)tmp1;
            ret += DCP.DecodeU16(buffer, out tmp2);
            encoding = (Encodings)tmp2;
            ret += DCP.DecodeU8(buffer, out tmp1); //pad
            ret += DCP.DecodeU8(buffer, out tmp1);
            serial_high_low |= (UInt16)(tmp1 << 8);
            ret += DecodeGuid(buffer, encoding, out object_id);
            ret += DecodeGuid(buffer, encoding, out interface_id);
            ret += DecodeGuid(buffer, encoding, out activity_id);
            ret += DecodeU32(buffer, encoding, out server_boot_time);
            ret += DecodeU32(buffer, encoding, out tmp3);   //interface version
            if ((tmp3 & 0xFFFF) != 1) throw new Exception("Wrong protocol version");
            ret += DecodeU32(buffer, encoding, out sequence_no);
            ret += DecodeU16(buffer, encoding, out tmp2);
            op = (Operations)tmp2;
            ret += DecodeU16(buffer, encoding, out tmp2);     //interface hint
            ret += DecodeU16(buffer, encoding, out tmp2);     //activity hint
            ret += DecodeU16(buffer, encoding, out body_length);
            ret += DecodeU16(buffer, encoding, out fragment_no);
            ret += DCP.DecodeU8(buffer, out tmp1); //authentication protocol
            ret += DCP.DecodeU8(buffer, out tmp1);
            serial_high_low |= tmp1;

            return ret;
        }

        public static void ReEncodeHeaderLength(System.IO.Stream buffer, Encodings encoding, long body_length_position)
        {
            long current_pos = buffer.Position;
            UInt16 actual_length = (UInt16)(buffer.Position - body_length_position - 6);

            buffer.Position = body_length_position;
            EncodeU16(buffer, encoding, actual_length);

            buffer.Position = current_pos;
        }

        public static int EncodeNDRDataHeader(System.IO.Stream buffer, Encodings encoding, UInt32 ArgsMaximum_or_PNIOStatus, UInt32 ArgsLength, UInt32 MaximumCount, UInt32 Offset, UInt32 ActualCount, out long NDRDataHeader_position)
        {
            int ret = 0;

            NDRDataHeader_position = buffer.Position;
            ret += EncodeU32(buffer, encoding, ArgsMaximum_or_PNIOStatus);
            ret += EncodeU32(buffer, encoding, ArgsLength);
            ret += EncodeU32(buffer, encoding, MaximumCount);
            ret += EncodeU32(buffer, encoding, Offset);
            ret += EncodeU32(buffer, encoding, ActualCount);

            return ret;
        }

        public static int DecodeNDRDataHeader(System.IO.Stream buffer, Encodings encoding, out UInt32 ArgsMaximum_or_PNIOStatus, out UInt32 ArgsLength, out UInt32 MaximumCount, out UInt32 Offset, out UInt32 ActualCount)
        {
            int ret = 0;

            ret += DecodeU32(buffer, encoding, out ArgsMaximum_or_PNIOStatus);
            ret += DecodeU32(buffer, encoding, out ArgsLength);
            ret += DecodeU32(buffer, encoding, out MaximumCount);
            ret += DecodeU32(buffer, encoding, out Offset);
            ret += DecodeU32(buffer, encoding, out ActualCount);

            return ret;
        }

        public static void ReEncodeNDRDataHeaderLength(System.IO.Stream buffer, Encodings encoding, long NDRDataHeader_position, bool re_encode_pniostatus)
        {
            long current_pos = buffer.Position;
            UInt32 actual_length = (UInt32)(buffer.Position - NDRDataHeader_position - 20);

            buffer.Position = NDRDataHeader_position;
            if (re_encode_pniostatus) EncodeU32(buffer, encoding, actual_length);     //ArgsMaximum/PNIOStatus
            else buffer.Position += 4;
            EncodeU32(buffer, encoding, actual_length);     //ArgsLength
            EncodeU32(buffer, encoding, actual_length);     //MaximumCount
            buffer.Position += 4;
            EncodeU32(buffer, encoding, actual_length);     //ActualCount

            buffer.Position = current_pos;
        }
    }
}
