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
using SharpPcap;
using System.Net.NetworkInformation;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace MudaIpDahora.Controller.Profinet
{
    public class ConnectionInfoEthernet
    {
        public ProfinetEthernetTransport Adapter;
        public PhysicalAddress Destination;
        public PhysicalAddress Source;
        public ConnectionInfoEthernet(ProfinetEthernetTransport adapter, PhysicalAddress destination, PhysicalAddress source)
        {
            Adapter = adapter;
            Destination = destination;
            Source = source;
        }
    }

    public class ProfinetEthernetTransport : IDisposable
    {
        private ICaptureDevice m_adapter;
        private UInt16 m_last_xid = 0;
        private bool m_is_open = false;

        public delegate void OnDcpMessageHandler(ConnectionInfoEthernet sender, DCP.ServiceIds service_id, uint xid, ushort response_delay_factor, Dictionary<DCP.BlockOptions, object> blocks);
        public event OnDcpMessageHandler OnDcpMessage;
        public delegate void OnAcyclicMessageHandler(ConnectionInfoEthernet sender, UInt16 AlarmDestinationEndpoint, UInt16 AlarmSourceEndpoint, RT.PDUTypes PDUType, RT.AddFlags AddFlags, UInt16 SendSeqNum, UInt16 AckSeqNum, UInt16 VarPartLen, Stream data);
        public event OnAcyclicMessageHandler OnAcyclicMessage;
        public delegate void OnCyclicMessageHandler(ConnectionInfoEthernet sender, UInt16 CycleCounter, RT.DataStatus DataStatus, RT.TransferStatus TransferStatus, Stream data, int data_length);
        public event OnCyclicMessageHandler OnCyclicMessage;

        public bool IsOpen { get { return m_is_open; } }
        public ICaptureDevice Adapter { get { return m_adapter; } }

        public ProfinetEthernetTransport(ICaptureDevice adapter)
        {
            m_adapter = adapter;
            m_adapter.OnPacketArrival += new PacketArrivalEventHandler(m_adapter_OnPacketArrival);
        }

        /// <summary>
        /// Will return pcap version. Use this to validate installed pcap library
        /// </summary>
        public static string PcapVersion
        {
            get
            {
                try
                {
                    return SharpPcap.Pcap.Version;
                }
                catch { }
                return "";
            }
        }

        public void Open()
        {
            if (m_is_open) return;
            if (m_adapter is SharpPcap.WinPcap.WinPcapDevice)
                ((SharpPcap.WinPcap.WinPcapDevice)m_adapter).Open(SharpPcap.WinPcap.OpenFlags.MaxResponsiveness | SharpPcap.WinPcap.OpenFlags.NoCaptureLocal, -1);
            else
                m_adapter.Open(DeviceMode.Normal);
            m_adapter.Filter = "ether proto 0x8892 or vlan 0";
            m_adapter.StartCapture();
            m_is_open = true;
            System.Threading.Thread.Sleep(50);  //let the pcap start up
        }

        public void Close()
        {
            if (!m_is_open) return;
            try
            {
                m_adapter.StopCapture();
            }
            catch
            {
            }
            m_is_open = false;
        }

        public void Dispose()
        {
            if (m_adapter != null)
            {
                Close();
                m_adapter.Close();
                m_adapter = null;
            }
        }

        public static DCP.IpInfo GetPcapIp(SharpPcap.ICaptureDevice pcap_device)
        {
            foreach (System.Net.NetworkInformation.NetworkInterface nic in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
            {
                PhysicalAddress mac = null;
                try
                {
                    mac = nic.GetPhysicalAddress();
                }
                catch (Exception)
                {
                    //interface have no mac address
                }

                if (mac != null && mac.Equals(pcap_device.MacAddress))
                {
                    IPInterfaceProperties ipp = nic.GetIPProperties();
                    foreach (var entry in ipp.UnicastAddresses)
                    {
                        if (entry.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            byte[] gw = new byte[] { 0, 0, 0, 0 };
                            if (ipp.GatewayAddresses.Count > 0) gw = ipp.GatewayAddresses[0].Address.GetAddressBytes();
                            return new DCP.IpInfo(DCP.BlockInfo.IpSet, entry.Address.GetAddressBytes(), entry.IPv4Mask.GetAddressBytes(), gw);
                        }
                    }
                }
            }
            return null;
        }

        public static PhysicalAddress GetDeviceMac(string interface_ip)
        {
            IPAddress search_ip = IPAddress.Parse(interface_ip);
            foreach (System.Net.NetworkInformation.NetworkInterface nic in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
            {
                PhysicalAddress mac = null;
                try
                {
                    mac = nic.GetPhysicalAddress();
                }
                catch (Exception)
                {
                    //interface have no mac address
                    continue;
                }
                foreach (var entry in nic.GetIPProperties().UnicastAddresses)
                {
                    if (search_ip.Equals(entry.Address))
                    {
                        return mac;
                    }
                }
            }
            return null;
        }

        public static SharpPcap.ICaptureDevice GetPcapDevice(string local_ip)
        {
            IPAddress search_ip = IPAddress.Parse(local_ip);
            PhysicalAddress search_mac = null;
            Dictionary<PhysicalAddress, SharpPcap.ICaptureDevice> networks = new Dictionary<PhysicalAddress, SharpPcap.ICaptureDevice>();

            //search all networks
            foreach (System.Net.NetworkInformation.NetworkInterface nic in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
            {
                PhysicalAddress mac = null;
                try
                {
                    mac = nic.GetPhysicalAddress();
                }
                catch (Exception)
                {
                    //interface have no mac address
                    continue;
                }
                foreach (var entry in nic.GetIPProperties().UnicastAddresses)
                {
                    if (search_ip.Equals(entry.Address))
                    {
                        search_mac = mac;
                        break;
                    }
                }
                if (search_mac != null) break;
            }

            //validate
            if (search_mac == null) return null;

            //search all pcap networks
            foreach (SharpPcap.ICaptureDevice dev in SharpPcap.CaptureDeviceList.Instance)
            {
                try
                {
                    dev.Open();
                    networks.Add(dev.MacAddress, dev);
                    dev.Close();
                }
                catch { }
            }

            //find link
            if (networks.ContainsKey(search_mac)) return networks[search_mac];
            else return null;
        }

        public static System.Net.IPAddress GetNetworkAddress(System.Net.IPAddress address, System.Net.IPAddress subnetMask)
        {
            byte[] ipAdressBytes = address.GetAddressBytes();
            byte[] subnetMaskBytes = subnetMask.GetAddressBytes();

            if (ipAdressBytes.Length != subnetMaskBytes.Length)
                throw new ArgumentException("Lengths of IP address and subnet mask do not match.");

            byte[] broadcastAddress = new byte[ipAdressBytes.Length];
            for (int i = 0; i < broadcastAddress.Length; i++)
                broadcastAddress[i] = (byte)(ipAdressBytes[i] & (subnetMaskBytes[i]));
            return new System.Net.IPAddress(broadcastAddress);
        }

        public static string GetLocalIpAddress(string ip_match)
        {
            System.Net.IPAddress target = System.Net.IPAddress.Parse(ip_match);
            foreach (System.Net.NetworkInformation.NetworkInterface nic in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (var entry in nic.GetIPProperties().UnicastAddresses)
                {
                    if (entry.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        if (GetNetworkAddress(entry.Address, entry.IPv4Mask).Equals(GetNetworkAddress(target, entry.IPv4Mask)))
                            return entry.Address.ToString();
                    }
                }
            }
            return "";
        }

        private void m_adapter_OnProfinetArrival(ConnectionInfoEthernet sender, Stream stream)
        {
            RT.FrameIds frame_id;

            //Real Time
            RT.DecodeFrameId(stream, out frame_id);
            if (frame_id == RT.FrameIds.DCP_Identify_ResPDU || frame_id == RT.FrameIds.DCP_Identify_ReqPDU || frame_id == RT.FrameIds.DCP_Get_Set_PDU || frame_id == RT.FrameIds.DCP_Hello_ReqPDU)
            {
                DCP.ServiceIds service_id;
                uint xid;
                ushort response_delay_factor;
                ushort dcp_data_length;
                DCP.DecodeHeader(stream, out service_id, out xid, out response_delay_factor, out dcp_data_length);
                Dictionary<DCP.BlockOptions, object> blocks;
                DCP.DecodeAllBlocks(stream, dcp_data_length, out blocks);
                if (OnDcpMessage != null) OnDcpMessage(sender, service_id, xid, response_delay_factor, blocks);
            }
            else if (frame_id == RT.FrameIds.PTCP_DelayReqPDU)
            {
                //ignore this for now
            }
            else if (frame_id >= RT.FrameIds.RTC_Start && frame_id <= RT.FrameIds.RTC_End)
            {
                long data_pos = stream.Position;
                stream.Position = stream.Length - 4;
                UInt16 CycleCounter;
                RT.DataStatus DataStatus;
                RT.TransferStatus TransferStatus;
                RT.DecodeRTCStatus(stream, out CycleCounter, out DataStatus, out TransferStatus);
                stream.Position = data_pos;
                if (OnCyclicMessage != null) OnCyclicMessage(sender, CycleCounter, DataStatus, TransferStatus, stream, (int)(stream.Length - data_pos - 4));
            }
            else if (frame_id == RT.FrameIds.Alarm_Low || frame_id == RT.FrameIds.Alarm_High)
            {
                UInt16 AlarmDestinationEndpoint;
                UInt16 AlarmSourceEndpoint;
                RT.PDUTypes PDUType;
                RT.AddFlags AddFlags;
                UInt16 SendSeqNum;
                UInt16 AckSeqNum;
                UInt16 VarPartLen;
                RT.DecodeRTAHeader(stream, out AlarmDestinationEndpoint, out AlarmSourceEndpoint, out PDUType, out AddFlags, out SendSeqNum, out AckSeqNum, out VarPartLen);
                if (OnAcyclicMessage != null) OnAcyclicMessage(sender, AlarmDestinationEndpoint, AlarmSourceEndpoint, PDUType, AddFlags, SendSeqNum, AckSeqNum, VarPartLen, stream);
            }
            else
            {
                Trace.TraceWarning("Unclassified RT message");
            }
        }

        private void m_adapter_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            if (e.Packet.LinkLayerType != PacketDotNet.LinkLayers.Ethernet) return;
            PacketDotNet.Utils.ByteArraySegment bas = new PacketDotNet.Utils.ByteArraySegment(e.Packet.Data);
            PacketDotNet.EthernetPacket eth_p = new PacketDotNet.EthernetPacket(bas);
            if (eth_p.Type != (PacketDotNet.EthernetPacketType)0x8892 && eth_p.Type != PacketDotNet.EthernetPacketType.VLanTaggedFrame) return;
            if (eth_p.PayloadPacket != null && eth_p.PayloadPacket is PacketDotNet.Ieee8021QPacket)
            {
                if (((PacketDotNet.Ieee8021QPacket)eth_p.PayloadPacket).Type != (PacketDotNet.EthernetPacketType)0x8892) return;
                if (((PacketDotNet.Ieee8021QPacket)eth_p.PayloadPacket).PayloadData == null)
                {
                    Trace.TraceWarning("Empty vlan package");
                    return;
                }
                m_adapter_OnProfinetArrival(new ConnectionInfoEthernet(this, eth_p.DestinationHwAddress, eth_p.SourceHwAddress), new MemoryStream(((PacketDotNet.Ieee8021QPacket)eth_p.PayloadPacket).PayloadData, false));
            }
            else
            {
                if (eth_p.PayloadData == null)
                {
                    Trace.TraceWarning("Empty ethernet package");
                    return;
                }
                m_adapter_OnProfinetArrival(new ConnectionInfoEthernet(this, eth_p.DestinationHwAddress, eth_p.SourceHwAddress), new MemoryStream(eth_p.PayloadData, false));
            }
        }

        private void Send(MemoryStream stream)
        {
            byte[] buffer = stream.GetBuffer();
            m_adapter.SendPacket(buffer, (int)stream.Position);
        }

        public void SendIdentifyBroadcast()
        {
            Trace.WriteLine("Sending identify broadcast", null);

            MemoryStream mem = new MemoryStream();

            //ethernet
            PhysicalAddress ethernetDestinationHwAddress = PhysicalAddress.Parse(RT.MulticastMACAdd_Identify_Address);
            Ethernet.Encode(mem, ethernetDestinationHwAddress, m_adapter.MacAddress, Ethernet.Type.VLanTaggedFrame);

            //VLAN
            VLAN.Encode(mem, VLAN.Priorities.Priority0, VLAN.Type.PN);

            //Profinet Real Time
            RT.EncodeFrameId(mem, RT.FrameIds.DCP_Identify_ReqPDU);

            //Profinet DCP
            DCP.EncodeIdentifyRequest(mem, ++m_last_xid);

            //Send
            Send(mem);
        }

        public void SendIdentifyResponse(PhysicalAddress destination, uint xid, Dictionary<DCP.BlockOptions, object> blocks)
        {
            Trace.WriteLine("Sending identify response", null);

            MemoryStream mem = new MemoryStream();

            //ethernet
            Ethernet.Encode(mem, destination, m_adapter.MacAddress, Ethernet.Type.VLanTaggedFrame);

            //VLAN
            VLAN.Encode(mem, VLAN.Priorities.Priority0, VLAN.Type.PN);

            //Profinet Real Time
            RT.EncodeFrameId(mem, RT.FrameIds.DCP_Identify_ResPDU);

            //Profinet DCP
            DCP.EncodeIdentifyResponse(mem, xid, blocks);

            //Send
            Send(mem);
        }

        public void SendCyclicData(PhysicalAddress destination, UInt16 frame_id, UInt16 cycle_counter, byte[] user_data)
        {
            Trace.WriteLine("Sending cyclic data", null);

            MemoryStream mem = new MemoryStream();

            //ethernet
            Ethernet.Encode(mem, destination, m_adapter.MacAddress, (Ethernet.Type)0x8892);

            //Profinet Real Time
            RT.EncodeFrameId(mem, (RT.FrameIds)frame_id);

            //user data
            if (user_data == null) user_data = new byte[40];
            if (user_data.Length < 40) Array.Resize<byte>(ref user_data, 40);
            mem.Write(user_data, 0, user_data.Length);

            //RT footer
            RT.EncodeRTCStatus(mem, cycle_counter, RT.DataStatus.DataItemValid |
                                                    RT.DataStatus.State_Primary |
                                                    RT.DataStatus.ProviderState_Run |
                                                    RT.DataStatus.StationProblemIndicator_Normal,
                                                    RT.TransferStatus.OK);

            //Send
            Send(mem);
        }

        public class ProfinetAsyncDCPResult : IAsyncResult, IDisposable
        {
            private System.Threading.ManualResetEvent m_wait = new System.Threading.ManualResetEvent(false);
            private ushort m_xid;
            private ProfinetEthernetTransport m_conn = null;

            public object AsyncState { get; set; }
            public System.Threading.WaitHandle AsyncWaitHandle { get { return m_wait; } }
            public bool CompletedSynchronously { get; private set; }    //always false
            public bool IsCompleted { get; private set; }

            public Dictionary<DCP.BlockOptions, object> Result { get; private set; }

            public ProfinetAsyncDCPResult(ProfinetEthernetTransport conn, MemoryStream message, UInt16 xid)
            {
                m_conn = conn;
                conn.OnDcpMessage += new OnDcpMessageHandler(conn_OnDcpMessage);
                m_xid = xid;
                conn.Send(message);
            }

            private void conn_OnDcpMessage(ConnectionInfoEthernet sender, DCP.ServiceIds service_id, uint xid, ushort response_delay_factor, Dictionary<DCP.BlockOptions, object> blocks)
            {
                if (xid == m_xid)
                {
                    Result = blocks;
                    IsCompleted = true;
                    m_wait.Set();
                }
            }

            public void Dispose()
            {
                if (m_conn != null)
                {
                    m_conn.OnDcpMessage -= conn_OnDcpMessage;
                    m_conn = null;
                }
            }
        }

        public IAsyncResult BeginGetRequest(PhysicalAddress destination, DCP.BlockOptions option)
        {
            Trace.WriteLine("Sending Get " + option.ToString() + " request", null);

            MemoryStream mem = new MemoryStream();

            //ethernet
            Ethernet.Encode(mem, destination, m_adapter.MacAddress, Ethernet.Type.VLanTaggedFrame);

            //VLAN
            VLAN.Encode(mem, VLAN.Priorities.Priority0, VLAN.Type.PN);

            //Profinet Real Time
            RT.EncodeFrameId(mem, RT.FrameIds.DCP_Get_Set_PDU);

            //Profinet DCP
            UInt16 xid = ++m_last_xid;
            DCP.EncodeGetRequest(mem, xid, option);
            //start Async
            return new ProfinetAsyncDCPResult(this, mem, xid);
        }

        public void SendGetResponse(PhysicalAddress destination, uint xid, DCP.BlockOptions option, object data)
        {
            Trace.WriteLine("Sending Get " + option.ToString() + " response", null);

            MemoryStream mem = new MemoryStream();

            //ethernet
            Ethernet.Encode(mem, destination, m_adapter.MacAddress, Ethernet.Type.VLanTaggedFrame);

            //VLAN
            VLAN.Encode(mem, VLAN.Priorities.Priority0, VLAN.Type.PN);

            //Profinet Real Time
            RT.EncodeFrameId(mem, RT.FrameIds.DCP_Get_Set_PDU);

            //Profinet DCP
            DCP.EncodeGetResponse(mem, xid, option, data);

            //send
            Send(mem);
        }

        public IAsyncResult BeginSetRequest(PhysicalAddress destination, DCP.BlockOptions option, DCP.BlockQualifiers qualifiers, byte[] data)
        {
            Trace.WriteLine("Sending Set " + option.ToString() + " request", null);

            MemoryStream mem = new MemoryStream();

            //ethernet
            Ethernet.Encode(mem, destination, m_adapter.MacAddress, Ethernet.Type.VLanTaggedFrame);

            //VLAN
            VLAN.Encode(mem, VLAN.Priorities.Priority0, VLAN.Type.PN);

            //Profinet Real Time
            RT.EncodeFrameId(mem, RT.FrameIds.DCP_Get_Set_PDU);

            //Profinet DCP
            UInt16 xid = ++m_last_xid;
            DCP.EncodeSetRequest(mem, xid, option, qualifiers, data);

            //start Async
            return new ProfinetAsyncDCPResult(this, mem, xid);
        }

        public void SendSetResponse(PhysicalAddress destination, uint xid, DCP.BlockOptions option, DCP.BlockErrors status)
        {
            Trace.WriteLine("Sending Set " + option.ToString() + " response", null);

            MemoryStream mem = new MemoryStream();

            //ethernet
            Ethernet.Encode(mem, destination, m_adapter.MacAddress, Ethernet.Type.VLanTaggedFrame);

            //VLAN
            VLAN.Encode(mem, VLAN.Priorities.Priority0, VLAN.Type.PN);

            //Profinet Real Time
            RT.EncodeFrameId(mem, RT.FrameIds.DCP_Get_Set_PDU);

            //Profinet DCP
            DCP.EncodeSetResponse(mem, xid, option, status);

            //Send
            Send(mem);
        }

        public DCP.BlockErrors EndSetRequest(IAsyncResult result, int timeout_ms)
        {
            ProfinetAsyncDCPResult r = (ProfinetAsyncDCPResult)result;

            if (result.AsyncWaitHandle.WaitOne(timeout_ms))
            {
                DCP.BlockErrors ret = ((DCP.ResponseStatus)r.Result[DCP.BlockOptions.Control_Response]).Error;
                r.Dispose();
                return ret;
            }
            else
            {
                r.Dispose();
                throw new TimeoutException("No response received");
            }
        }

        public Dictionary<DCP.BlockOptions, object> EndGetRequest(IAsyncResult result, int timeout_ms)
        {
            ProfinetAsyncDCPResult r = (ProfinetAsyncDCPResult)result;

            if (result.AsyncWaitHandle.WaitOne(timeout_ms))
            {
                Dictionary<DCP.BlockOptions, object> ret = r.Result;
                r.Dispose();
                return ret;
            }
            else
            {
                r.Dispose();
                throw new TimeoutException("No response received");
            }
        }

        public IAsyncResult BeginSetSignalRequest(PhysicalAddress destination)
        {
            return BeginSetRequest(destination, DCP.BlockOptions.Control_Signal, DCP.BlockQualifiers.Temporary, BitConverter.GetBytes((ushort)0x100));      //SignalValue - Flash once
        }

        public IAsyncResult BeginSetResetRequest(PhysicalAddress destination)
        {
            return BeginSetRequest(destination, DCP.BlockOptions.Control_FactoryReset, DCP.BlockQualifiers.Permanent, null);
        }

        public IAsyncResult BeginSetNameRequest(PhysicalAddress destination, string name)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(name);
            return BeginSetRequest(destination, DCP.BlockOptions.DeviceProperties_NameOfStation, DCP.BlockQualifiers.Permanent, bytes);
        }

        public IAsyncResult BeginSetIpRequest(PhysicalAddress destination, IPAddress ip, IPAddress subnet_mask, IPAddress gateway)
        {
            byte[] bytes = new byte[12];
            Array.Copy(ip.GetAddressBytes(), 0, bytes, 0, 4);
            Array.Copy(subnet_mask.GetAddressBytes(), 0, bytes, 4, 4);
            Array.Copy(gateway.GetAddressBytes(), 0, bytes, 8, 4);
            return BeginSetRequest(destination, DCP.BlockOptions.IP_IPParameter, DCP.BlockQualifiers.Permanent, bytes);
        }

        public IAsyncResult BeginSetIpFullRequest(PhysicalAddress destination, IPAddress ip, IPAddress subnet_mask, IPAddress gateway, IPAddress[] dns)
        {
            byte[] bytes = new byte[28];
            Array.Copy(ip.GetAddressBytes(), 0, bytes, 0, 4);
            Array.Copy(subnet_mask.GetAddressBytes(), 0, bytes, 4, 4);
            Array.Copy(gateway.GetAddressBytes(), 0, bytes, 8, 4);
            if (dns == null || dns.Length != 4) throw new ArgumentException("dns array length must be 4");
            for (int i = 0; i < 4; i++)
                Array.Copy(dns[i].GetAddressBytes(), 0, bytes, 12 + i * 4, 4);
            return BeginSetRequest(destination, DCP.BlockOptions.IP_FullIPSuite, DCP.BlockQualifiers.Permanent, bytes);
        }

        public DCP.BlockErrors SendSetRequest(PhysicalAddress destination, int timeout_ms, int retries, DCP.BlockOptions option, DCP.BlockQualifiers qualifiers, byte[] data)
        {
            for (int r = 0; r < retries; r++)
            {
                IAsyncResult async_result = BeginSetRequest(destination, option, qualifiers, data);
                try
                {
                    return EndSetRequest(async_result, timeout_ms);
                }
                catch (TimeoutException)
                {
                    //continue
                }
            }
            throw new TimeoutException("No response received");
        }

        public DCP.BlockErrors SendSetRequest(PhysicalAddress destination, int timeout_ms, int retries, DCP.BlockOptions option, byte[] data)
        {
            return SendSetRequest(destination, timeout_ms, retries, option, DCP.BlockQualifiers.Permanent, data);
        }

        public Dictionary<DCP.BlockOptions, object> SendGetRequest(PhysicalAddress destination, int timeout_ms, int retries, DCP.BlockOptions option)
        {
            for (int r = 0; r < retries; r++)
            {
                IAsyncResult async_result = BeginGetRequest(destination, option);
                try
                {
                    return EndGetRequest(async_result, timeout_ms);
                }
                catch (TimeoutException)
                {
                    //continue
                }
            }
            throw new TimeoutException("No response received");
        }

        public DCP.BlockErrors SendSetSignalRequest(PhysicalAddress destination, int timeout_ms, int retries)
        {
            for (int r = 0; r < retries; r++)
            {
                IAsyncResult async_result = BeginSetSignalRequest(destination);
                try
                {
                    return EndSetRequest(async_result, timeout_ms);
                }
                catch (TimeoutException)
                {
                    //continue
                }
            }
            throw new TimeoutException("No response received");
        }

        public DCP.BlockErrors SendSetResetRequest(PhysicalAddress destination, int timeout_ms, int retries)
        {
            for (int r = 0; r < retries; r++)
            {
                IAsyncResult async_result = BeginSetResetRequest(destination);
                try
                {
                    return EndSetRequest(async_result, timeout_ms);
                }
                catch (TimeoutException)
                {
                    //continue
                }
            }
            throw new TimeoutException("No response received");
        }

        public DCP.BlockErrors SendSetNameRequest(PhysicalAddress destination, int timeout_ms, int retries, string name)
        {
            for (int r = 0; r < retries; r++)
            {
                IAsyncResult async_result = BeginSetNameRequest(destination, name);
                try
                {
                    return EndSetRequest(async_result, timeout_ms);
                }
                catch (TimeoutException)
                {
                    //continue
                }
            }
            throw new TimeoutException("No response received");
        }

        public DCP.BlockErrors SendSetIpFullRequest(PhysicalAddress destination, int timeout_ms, int retries, IPAddress ip, IPAddress subnet_mask, IPAddress gateway, IPAddress[] dns)
        {
            for (int r = 0; r < retries; r++)
            {
                IAsyncResult async_result = BeginSetIpFullRequest(destination, ip, subnet_mask, gateway, dns);
                try
                {
                    return EndSetRequest(async_result, timeout_ms);
                }
                catch (TimeoutException)
                {
                    //continue
                }
            }
            throw new TimeoutException("No response received");
        }

        public DCP.BlockErrors SendSetIpRequest(PhysicalAddress destination, int timeout_ms, int retries, IPAddress ip, IPAddress subnet_mask, IPAddress gateway)
        {
            for (int r = 0; r < retries; r++)
            {
                IAsyncResult async_result = BeginSetIpRequest(destination, ip, subnet_mask, gateway);
                try
                {
                    return EndSetRequest(async_result, timeout_ms);
                }
                catch (TimeoutException)
                {
                    //continue
                }
            }
            throw new TimeoutException("No response received");
        }
    }
}
