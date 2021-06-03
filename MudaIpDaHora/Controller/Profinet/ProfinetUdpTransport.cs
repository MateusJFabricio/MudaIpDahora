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
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;

namespace MudaIpDahora.Controller.Profinet
{
    public class ConnectionInfoUdp
    {
        public ProfinetUdpTransport Adapter { get; set; }
        public IPEndPoint Source { get; set; }
        public ConnectionInfoUdp(ProfinetUdpTransport adapter, IPEndPoint source)
        {
            Adapter = adapter;
            Source = source;
        }
    }

    public class ProfinetUdpTransport
    {
        private UdpClient m_receiver_conn;
        private UdpClient m_sender_conn;
        private IPAddress m_interface_ip = null;
        private int m_port = 0x8894;
        private Guid m_object_instance;
        private System.Net.NetworkInformation.PhysicalAddress m_source_mac;
        private IPAddress m_destination_ip;
        private uint m_sequence_no = 0;
        private DCP.DeviceIdInfo m_device_id;
        private Guid m_activity;
        private Guid m_aruuid;
        private bool m_is_closed = true;
        private ushort m_input_cycle_time_ms = 16;
        private ushort m_output_cycle_time_ms = 16;

        public delegate void OnRPCMessageReceivedHandler(ConnectionInfoUdp sender, RPC.PacketTypes type, RPC.Flags1 flags1, RPC.Flags2 flags2, RPC.Encodings encoding, UInt16 serial_high_low, Guid object_id, Guid interface_id, Guid activity_id, UInt32 server_boot_time, UInt32 sequence_no, RPC.Operations op, UInt16 body_length, UInt16 fragment_no, Stream data);
        public event OnRPCMessageReceivedHandler OnRPCMessageReceived;

        public bool HasConnected { get; private set; }
        public ushort InputCycleTimeMs
        {
            get { return m_input_cycle_time_ms; }
            set
            {
                //find highest 2-base
                ushort n = 512;
                while (n > 0)
                {
                    if ((value / n) > 0)
                    {
                        m_input_cycle_time_ms = n;
                        return;
                    }
                    n >>= 1;
                }
                throw new ArgumentException("Number not supported");
            }
        }
        public ushort OutputCycleTimeMs
        {
            get { return m_output_cycle_time_ms; }
            set
            {
                //find highest 2-base
                ushort n = 512;
                while (n > 0)
                {
                    if ((value / n) > 0)
                    {
                        m_output_cycle_time_ms = n;
                        return;
                    }
                    n >>= 1;
                }
                throw new ArgumentException("Number not supported");
            }
        }

        public ProfinetUdpTransport(string interface_ip, System.Net.NetworkInformation.PhysicalAddress source_mac)
        {
            m_source_mac = source_mac;
            if (!string.IsNullOrEmpty(interface_ip)) m_interface_ip = IPAddress.Parse(interface_ip);
            m_object_instance = RPC.GenerateObjectInstanceUUID(1, 0, 0x25, 0x120);
        }

        private void OnDataReceived(IAsyncResult result)
        {
            if (m_is_closed) return;
            UdpClient s = (UdpClient)result.AsyncState;
            try
            {
                IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
                byte[] bytes = s.EndReceive(result, ref sender);
                if (bytes != null && bytes.Length != 0)
                {
                    if (bytes[0] == 4)   //RPC version
                    {
                        MemoryStream mem = new MemoryStream(bytes, false);

                        //RPC
                        RPC.PacketTypes type; RPC.Flags1 flags1; RPC.Flags2 flags2; RPC.Encodings encoding; UInt16 serial_high_low; Guid object_id; Guid interface_id; Guid activity_id; UInt32 server_boot_time; UInt32 sequence_no; RPC.Operations op; UInt16 body_length; UInt16 fragment_no;
                        RPC.DecodeHeader(mem, out type, out flags1, out flags2, out encoding, out serial_high_low, out object_id, out interface_id, out activity_id, out server_boot_time, out sequence_no, out op, out body_length, out fragment_no);
                        if (OnRPCMessageReceived != null) OnRPCMessageReceived(new ConnectionInfoUdp(this, sender), type, flags1, flags2, encoding, serial_high_low, object_id, interface_id, activity_id, server_boot_time, sequence_no, op, body_length, fragment_no, mem);
                    }
                    else
                        Trace.TraceWarning("Something else received on udp port");
                }
                else
                {
                    // Empty frame : port scanner maybe
                }
            }
            catch (Exception ex)
            {
                Trace.TraceWarning("Udp recive error: " + ex.Message);
            }
            try
            {
                s.BeginReceive(new AsyncCallback(OnDataReceived), s);
            }
            catch (ObjectDisposedException)
            {
                //close down
            }
            catch (Exception)
            {
                if (m_is_closed) return;
                else throw;
            }
        }

        private UdpClient CreateServerSocket(int port)
        {
            //create transport
            IPEndPoint local = new IPEndPoint(m_interface_ip, port);
            UdpClient conn = new UdpClient();
            conn.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            conn.Client.Bind(local);
            conn.EnableBroadcast = true;
            conn.MulticastLoopback = false;
            return conn;
        }

        public void Open(string destination_ip, DCP.DeviceIdInfo device_id)
        {
            //create transport, bind to first free port
            //this is the wrong way to do it. You're supposed to create an outgoing socket and receive 'responses' on that
            //However, the Renesas TPS-1 is sending the response from a diffierent src port, thereby breaking the conversation
            //to fix this we can setup a server socket instead
            m_sender_conn = CreateServerSocket(0);
            m_sender_conn.BeginReceive(new AsyncCallback(OnDataReceived), m_sender_conn);
            m_device_id = device_id;
            m_destination_ip = IPAddress.Parse(destination_ip);

            //we also need another server socket, to receive stuff
            m_receiver_conn = CreateServerSocket(m_port);
            m_is_closed = false;
            m_receiver_conn.BeginReceive(new AsyncCallback(OnDataReceived), m_receiver_conn);
        }

        public IAsyncResult BeginConnectRequest()
        {
            //create new activity id
            m_activity = Guid.NewGuid();
            m_aruuid = Guid.NewGuid();

            //Create RPC packet
            long body_length_position;
            long NDRDataHeader_position;
            MemoryStream mem = new MemoryStream();
            RPC.Encodings encoding = RPC.Encodings.ASCII | RPC.Encodings.LittleEndian | RPC.Encodings.IEEE_float;
            Guid destination_instance = RPC.GenerateObjectInstanceUUID(1, 0, m_device_id.DeviceId, m_device_id.VendorId);
            RPC.EncodeHeader(mem, RPC.PacketTypes.Request, RPC.Flags1.Idempotent, 0, encoding, 0, destination_instance, RPC.UUID_IO_DeviceInterface, m_activity, 0, m_sequence_no++, RPC.Operations.Connect, 0, 0, out body_length_position);
            RPC.EncodeNDRDataHeader(mem, encoding, 0, 0, 0, 0, 0, out NDRDataHeader_position);

            //Create IO Packet (I have no idea what most of this is)
            ProfinetIO.ARBlockRequest ar_block_req = new ProfinetIO.ARBlockRequest();
            ar_block_req.Type = ProfinetIO.ARTypes.IOCARSingle;
            ar_block_req.UUID = m_aruuid;
            ar_block_req.SessionKey = 1;
            ar_block_req.CMInitiatorMac = m_source_mac;
            ar_block_req.CMInitiatorObjectUUID = m_object_instance;
            ar_block_req.Properties = ProfinetIO.ARProperties.State_Active | ProfinetIO.ARProperties.ParameterizationServer_CMInitiator;
            ar_block_req.CMInitiatorActivityTimeoutFactor = 600;
            ar_block_req.CMInitiatorUDPRTPort = 0x8892;
            ar_block_req.CMInitiatorStationName = "ProfinetExplorer";
            ProfinetIO.IOCRBlockRequest[] iocr_block_req = new ProfinetIO.IOCRBlockRequest[2] { new ProfinetIO.IOCRBlockRequest(), new ProfinetIO.IOCRBlockRequest() };

            iocr_block_req[0].Type = ProfinetIO.IOCRTypes.Input;
            iocr_block_req[0].Reference = 1;
            iocr_block_req[0].Properties = ProfinetIO.IOCRProperties.RT_CLASS1;
            iocr_block_req[0].DataLength = 216;
            iocr_block_req[0].FrameID = 0xc000;
            iocr_block_req[0].SendClockFactor = 32;     //clock cycle is 1 ms
            iocr_block_req[0].ReductionRatio = m_input_cycle_time_ms;
            iocr_block_req[0].Phase = 1;
            iocr_block_req[0].Sequence = 0;
            iocr_block_req[0].FrameSendOffset = 0xFFFFFFFF;
            iocr_block_req[0].DataHoldFactorA = 3;
            iocr_block_req[0].DataHoldFactorB = 3;
            iocr_block_req[0].TagHeader = ProfinetIO.IOCRTagHeaders.IOUserPriority_CR;
            iocr_block_req[0].MulticastMAC = new System.Net.NetworkInformation.PhysicalAddress(new byte[] { 0, 0, 0, 0, 0, 0 });
            iocr_block_req[0].APIs = new ProfinetIO.IOC_API[1];
            iocr_block_req[0].APIs[0] = new ProfinetIO.IOC_API();
            iocr_block_req[0].APIs[0].No = 0;
            iocr_block_req[0].APIs[0].DataObjects = new ProfinetIO.IOC_DataObject[5];
            iocr_block_req[0].APIs[0].DataObjects[0] = new ProfinetIO.IOC_DataObject(0, 0x8000, 0);
            iocr_block_req[0].APIs[0].DataObjects[1] = new ProfinetIO.IOC_DataObject(0, 0x8001, 1);
            iocr_block_req[0].APIs[0].DataObjects[2] = new ProfinetIO.IOC_DataObject(0, 0x8002, 2);
            iocr_block_req[0].APIs[0].DataObjects[3] = new ProfinetIO.IOC_DataObject(0, 1, 3);
            iocr_block_req[0].APIs[0].DataObjects[4] = new ProfinetIO.IOC_DataObject(1, 1, 5);
            iocr_block_req[0].APIs[0].IOCS = new ProfinetIO.IOC_DataObject[1];
            iocr_block_req[0].APIs[0].IOCS[0] = new ProfinetIO.IOC_DataObject(1, 1, 4);

            iocr_block_req[1].Type = ProfinetIO.IOCRTypes.Output;
            iocr_block_req[1].Reference = 2;
            iocr_block_req[1].Properties = ProfinetIO.IOCRProperties.RT_CLASS1;
            iocr_block_req[1].DataLength = 40;
            iocr_block_req[1].FrameID = 0xc001;
            iocr_block_req[1].SendClockFactor = 32;     //clock cycle is 1 ms
            iocr_block_req[1].ReductionRatio = m_output_cycle_time_ms;
            iocr_block_req[1].Phase = 1;
            iocr_block_req[1].Sequence = 0;
            iocr_block_req[1].FrameSendOffset = 0xFFFFFFFF;
            iocr_block_req[1].DataHoldFactorA = 3;
            iocr_block_req[1].DataHoldFactorB = 3;
            iocr_block_req[1].TagHeader = ProfinetIO.IOCRTagHeaders.IOUserPriority_CR;
            iocr_block_req[1].MulticastMAC = new System.Net.NetworkInformation.PhysicalAddress(new byte[] { 0, 0, 0, 0, 0, 0 });
            iocr_block_req[1].APIs = new ProfinetIO.IOC_API[1];
            iocr_block_req[1].APIs[0] = new ProfinetIO.IOC_API();
            iocr_block_req[1].APIs[0].No = 0;
            iocr_block_req[1].APIs[0].DataObjects = new ProfinetIO.IOC_DataObject[1];
            iocr_block_req[1].APIs[0].DataObjects[0] = new ProfinetIO.IOC_DataObject(1, 1, 5);
            iocr_block_req[1].APIs[0].IOCS = new ProfinetIO.IOC_DataObject[5];
            iocr_block_req[1].APIs[0].IOCS[0] = new ProfinetIO.IOC_DataObject(0, 0x8000, 0);
            iocr_block_req[1].APIs[0].IOCS[1] = new ProfinetIO.IOC_DataObject(0, 0x8001, 1);
            iocr_block_req[1].APIs[0].IOCS[2] = new ProfinetIO.IOC_DataObject(0, 0x8002, 2);
            iocr_block_req[1].APIs[0].IOCS[3] = new ProfinetIO.IOC_DataObject(0, 0x1, 3);
            iocr_block_req[1].APIs[0].IOCS[4] = new ProfinetIO.IOC_DataObject(1, 0x1, 4);

            ProfinetIO.AlarmCRBlockRequest alarm_cr_block_req = new ProfinetIO.AlarmCRBlockRequest();
            alarm_cr_block_req.Properties = ProfinetIO.AlarmCRProperties.RTA_CLASS_1 | ProfinetIO.AlarmCRProperties.UserPriority;
            alarm_cr_block_req.RTATimeoutFactor = 1;
            alarm_cr_block_req.RTARetries = 3;
            alarm_cr_block_req.LocalAlarmReference = 1;
            alarm_cr_block_req.MaxAlarmDataLength = 256;
            alarm_cr_block_req.AlarmCRTagHeaderHigh = 0xC000;
            alarm_cr_block_req.AlarmCRTagHeaderLow = 0xA000;

            ProfinetIO.ExpectedSubmoduleBlockRequest[] expected_submodule_block_req = new ProfinetIO.ExpectedSubmoduleBlockRequest[2] { new ProfinetIO.ExpectedSubmoduleBlockRequest(), new ProfinetIO.ExpectedSubmoduleBlockRequest() };
            expected_submodule_block_req[0].APIs = new ProfinetIO.Submodule_API[1];
            expected_submodule_block_req[0].APIs[0] = new ProfinetIO.Submodule_API();
            expected_submodule_block_req[0].APIs[0].No = 0;
            expected_submodule_block_req[0].APIs[0].SlotNumber = 0;
            expected_submodule_block_req[0].APIs[0].ModuleIdentNumber = 1;
            expected_submodule_block_req[0].APIs[0].SubModules = new ProfinetIO.Submodule[4];
            expected_submodule_block_req[0].APIs[0].SubModules[0] = new ProfinetIO.Submodule();
            expected_submodule_block_req[0].APIs[0].SubModules[0].SubslotNumber = 0x8000;
            expected_submodule_block_req[0].APIs[0].SubModules[0].SubmoduleIdentNumber = 0xA;
            expected_submodule_block_req[0].APIs[0].SubModules[0].SubmoduleProperties = ProfinetIO.SubmoduleProperties.Type_NO_IO | ProfinetIO.SubmoduleProperties.SharedInput_IOControllerOnly | ProfinetIO.SubmoduleProperties.ReduceInputSubmoduleDataLength_Expected | ProfinetIO.SubmoduleProperties.ReduceOutputSubmoduleDataLength_Expected | ProfinetIO.SubmoduleProperties.DiscardIOXS_Expected;
            expected_submodule_block_req[0].APIs[0].SubModules[0].DataDescription = new ProfinetIO.DataDescription[1];
            expected_submodule_block_req[0].APIs[0].SubModules[0].DataDescription[0] = new ProfinetIO.DataDescription(ProfinetIO.DataDescription.Types.Input, 0, 1, 1);
            expected_submodule_block_req[0].APIs[0].SubModules[1] = new ProfinetIO.Submodule();
            expected_submodule_block_req[0].APIs[0].SubModules[1].SubslotNumber = 0x8001;
            expected_submodule_block_req[0].APIs[0].SubModules[1].SubmoduleIdentNumber = 0xB;
            expected_submodule_block_req[0].APIs[0].SubModules[1].SubmoduleProperties = ProfinetIO.SubmoduleProperties.Type_NO_IO | ProfinetIO.SubmoduleProperties.SharedInput_IOControllerOnly | ProfinetIO.SubmoduleProperties.ReduceInputSubmoduleDataLength_Expected | ProfinetIO.SubmoduleProperties.ReduceOutputSubmoduleDataLength_Expected | ProfinetIO.SubmoduleProperties.DiscardIOXS_Expected;
            expected_submodule_block_req[0].APIs[0].SubModules[1].DataDescription = new ProfinetIO.DataDescription[1];
            expected_submodule_block_req[0].APIs[0].SubModules[1].DataDescription[0] = new ProfinetIO.DataDescription(ProfinetIO.DataDescription.Types.Input, 0, 1, 1);
            expected_submodule_block_req[0].APIs[0].SubModules[2] = new ProfinetIO.Submodule();
            expected_submodule_block_req[0].APIs[0].SubModules[2].SubslotNumber = 0x8002;
            expected_submodule_block_req[0].APIs[0].SubModules[2].SubmoduleIdentNumber = 0xC;
            expected_submodule_block_req[0].APIs[0].SubModules[2].SubmoduleProperties = ProfinetIO.SubmoduleProperties.Type_NO_IO | ProfinetIO.SubmoduleProperties.SharedInput_IOControllerOnly | ProfinetIO.SubmoduleProperties.ReduceInputSubmoduleDataLength_Expected | ProfinetIO.SubmoduleProperties.ReduceOutputSubmoduleDataLength_Expected | ProfinetIO.SubmoduleProperties.DiscardIOXS_Expected;
            expected_submodule_block_req[0].APIs[0].SubModules[2].DataDescription = new ProfinetIO.DataDescription[1];
            expected_submodule_block_req[0].APIs[0].SubModules[2].DataDescription[0] = new ProfinetIO.DataDescription(ProfinetIO.DataDescription.Types.Input, 0, 1, 1);
            expected_submodule_block_req[0].APIs[0].SubModules[3] = new ProfinetIO.Submodule();
            expected_submodule_block_req[0].APIs[0].SubModules[3].SubslotNumber = 1;
            expected_submodule_block_req[0].APIs[0].SubModules[3].SubmoduleIdentNumber = 1;
            expected_submodule_block_req[0].APIs[0].SubModules[3].SubmoduleProperties = ProfinetIO.SubmoduleProperties.Type_NO_IO | ProfinetIO.SubmoduleProperties.SharedInput_IOControllerOnly | ProfinetIO.SubmoduleProperties.ReduceInputSubmoduleDataLength_Expected | ProfinetIO.SubmoduleProperties.ReduceOutputSubmoduleDataLength_Expected | ProfinetIO.SubmoduleProperties.DiscardIOXS_Expected;
            expected_submodule_block_req[0].APIs[0].SubModules[3].DataDescription = new ProfinetIO.DataDescription[1];
            expected_submodule_block_req[0].APIs[0].SubModules[3].DataDescription[0] = new ProfinetIO.DataDescription(ProfinetIO.DataDescription.Types.Input, 0, 1, 1);

            expected_submodule_block_req[1].APIs = new ProfinetIO.Submodule_API[1];
            expected_submodule_block_req[1].APIs[0] = new ProfinetIO.Submodule_API();
            expected_submodule_block_req[1].APIs[0].No = 0;
            expected_submodule_block_req[1].APIs[0].SlotNumber = 1;
            expected_submodule_block_req[1].APIs[0].ModuleIdentNumber = 2;
            expected_submodule_block_req[1].APIs[0].SubModules = new ProfinetIO.Submodule[1];
            expected_submodule_block_req[1].APIs[0].SubModules[0] = new ProfinetIO.Submodule();
            expected_submodule_block_req[1].APIs[0].SubModules[0].SubslotNumber = 1;
            expected_submodule_block_req[1].APIs[0].SubModules[0].SubmoduleIdentNumber = 2;
            expected_submodule_block_req[1].APIs[0].SubModules[0].SubmoduleProperties = ProfinetIO.SubmoduleProperties.Type_INPUT_OUTPUT | ProfinetIO.SubmoduleProperties.SharedInput_IOControllerOnly | ProfinetIO.SubmoduleProperties.ReduceInputSubmoduleDataLength_Expected | ProfinetIO.SubmoduleProperties.ReduceOutputSubmoduleDataLength_Expected | ProfinetIO.SubmoduleProperties.DiscardIOXS_Expected;
            expected_submodule_block_req[1].APIs[0].SubModules[0].DataDescription = new ProfinetIO.DataDescription[2];
            expected_submodule_block_req[1].APIs[0].SubModules[0].DataDescription[0] = new ProfinetIO.DataDescription(ProfinetIO.DataDescription.Types.Input, 210, 1, 1);
            expected_submodule_block_req[1].APIs[0].SubModules[0].DataDescription[1] = new ProfinetIO.DataDescription(ProfinetIO.DataDescription.Types.Output, 4, 1, 1);
            ProfinetIO.EncodeConnectRequest(mem, ar_block_req, iocr_block_req, alarm_cr_block_req, expected_submodule_block_req, null, null, null, null, null, null, null);

            //re-encode rpc lengths
            RPC.ReEncodeHeaderLength(mem, encoding, body_length_position);
            RPC.ReEncodeNDRDataHeaderLength(mem, encoding, NDRDataHeader_position, true);

            //send
            Trace.TraceInformation("Sending Connect message");
            HasConnected = true;
            return new ProfinetAsyncRPCResult(this, mem);
        }

        public IAsyncResult BeginWriteRequest(IEnumerable<KeyValuePair<ProfinetIO.IODWriteReqHeader, byte[]>> values)
        {
            //Create RPC packet
            long body_length_position;
            long NDRDataHeader_position;
            MemoryStream mem = new MemoryStream();
            RPC.Encodings encoding = RPC.Encodings.ASCII | RPC.Encodings.LittleEndian | RPC.Encodings.IEEE_float;
            Guid destination_instance = RPC.GenerateObjectInstanceUUID(1, 0, m_device_id.DeviceId, m_device_id.VendorId);
            RPC.EncodeHeader(mem, RPC.PacketTypes.Request, RPC.Flags1.Idempotent, 0, encoding, 0, destination_instance, RPC.UUID_IO_DeviceInterface, m_activity, 0, m_sequence_no++, RPC.Operations.Write, 0, 0, out body_length_position);
            RPC.EncodeNDRDataHeader(mem, encoding, 0, 0, 0, 0, 0, out NDRDataHeader_position);

            //Create IO Packet
            ProfinetIO.EncodeWriteMultipleRequest(mem, m_aruuid, values);

            //re-encode rpc lengths
            RPC.ReEncodeHeaderLength(mem, encoding, body_length_position);
            RPC.ReEncodeNDRDataHeaderLength(mem, encoding, NDRDataHeader_position, true);

            //send
            Trace.TraceInformation("Sending Write message");
            return new ProfinetAsyncRPCResult(this, mem);
        }

        public IAsyncResult BeginControlRequest(ProfinetIO.BlockTypes block_type, ProfinetIO.ControlCommands control)
        {
            //Create RPC packet
            long body_length_position;
            long NDRDataHeader_position;
            MemoryStream mem = new MemoryStream();
            RPC.Encodings encoding = RPC.Encodings.ASCII | RPC.Encodings.LittleEndian | RPC.Encodings.IEEE_float;
            Guid destination_instance = RPC.GenerateObjectInstanceUUID(1, 0, m_device_id.DeviceId, m_device_id.VendorId);
            RPC.EncodeHeader(mem, RPC.PacketTypes.Request, RPC.Flags1.Idempotent, 0, encoding, 0, destination_instance, RPC.UUID_IO_DeviceInterface, m_activity, 0, m_sequence_no++, RPC.Operations.Control, 0, 0, out body_length_position);
            RPC.EncodeNDRDataHeader(mem, encoding, 0, 0, 0, 0, 0, out NDRDataHeader_position);

            //Create IO Packet
            ProfinetIO.ControlBlockConnect ctrl = new ProfinetIO.ControlBlockConnect();
            ctrl.BlockType = block_type;
            ctrl.ARUUID = m_aruuid;
            ctrl.SessionKey = 1;
            ctrl.ControlCommand = control;
            ProfinetIO.EncodeControlRequest(mem, ctrl);

            //re-encode rpc lengths
            RPC.ReEncodeHeaderLength(mem, encoding, body_length_position);
            RPC.ReEncodeNDRDataHeaderLength(mem, encoding, NDRDataHeader_position, true);

            //send
            Trace.TraceInformation("Sending Control message");
            return new ProfinetAsyncRPCResult(this, mem);
        }

        public IAsyncResult BeginReleaseRequest()
        {
            //Create RPC packet
            long body_length_position;
            long NDRDataHeader_position;
            MemoryStream mem = new MemoryStream();
            RPC.Encodings encoding = RPC.Encodings.ASCII | RPC.Encodings.LittleEndian | RPC.Encodings.IEEE_float;
            Guid destination_instance = RPC.GenerateObjectInstanceUUID(1, 0, m_device_id.DeviceId, m_device_id.VendorId);
            RPC.EncodeHeader(mem, RPC.PacketTypes.Request, RPC.Flags1.Idempotent, 0, encoding, 0, destination_instance, RPC.UUID_IO_DeviceInterface, m_activity, 0, m_sequence_no++, RPC.Operations.Release, 0, 0, out body_length_position);
            RPC.EncodeNDRDataHeader(mem, encoding, 0, 0, 0, 0, 0, out NDRDataHeader_position);

            //Create IO Packet
            ProfinetIO.EncodeReleaseRequest(mem, m_aruuid, 1);

            //re-encode rpc lengths
            RPC.ReEncodeHeaderLength(mem, encoding, body_length_position);
            RPC.ReEncodeNDRDataHeaderLength(mem, encoding, NDRDataHeader_position, true);

            //send
            Trace.TraceInformation("Sending Release message");
            return new ProfinetAsyncRPCResult(this, mem);
        }

        public void ControlResponse(Guid activity, uint sequence_no, ProfinetIO.BlockTypes block_type, ProfinetIO.ControlCommands control)
        {
            //Create RPC packet
            long body_length_position;
            long NDRDataHeader_position;
            MemoryStream mem = new MemoryStream();
            RPC.Encodings encoding = RPC.Encodings.ASCII | RPC.Encodings.LittleEndian | RPC.Encodings.IEEE_float;
            Guid destination_instance = RPC.GenerateObjectInstanceUUID(1, 0, m_device_id.DeviceId, m_device_id.VendorId);
            RPC.EncodeHeader(mem, RPC.PacketTypes.Response, RPC.Flags1.LastFragment | RPC.Flags1.NoFragmentAckRequested, 0, encoding, 0, destination_instance, RPC.UUID_IO_DeviceInterface, activity, 0, sequence_no, RPC.Operations.Control, 0, 0, out body_length_position);
            RPC.EncodeNDRDataHeader(mem, encoding, 0, 0, 0, 0, 0, out NDRDataHeader_position);

            //Create IO Packet
            ProfinetIO.ControlBlockConnect ctrl = new ProfinetIO.ControlBlockConnect();
            ctrl.BlockType = block_type;
            ctrl.ARUUID = m_aruuid;
            ctrl.SessionKey = 1;
            ctrl.ControlCommand = control;
            ProfinetIO.EncodeControlResponse(mem, ctrl);

            //re-encode rpc lengths
            RPC.ReEncodeHeaderLength(mem, encoding, body_length_position);
            RPC.ReEncodeNDRDataHeaderLength(mem, encoding, NDRDataHeader_position, false);

            //send
            Trace.TraceInformation("Sending Control response");
            SendResponse(mem);
        }

        public void EndTransmit(IAsyncResult result, int timeout_ms)
        {
            ProfinetAsyncRPCResult r = (ProfinetAsyncRPCResult)result;

            if (result.AsyncWaitHandle.WaitOne(timeout_ms))
            {
                r.Dispose();
                return;
            }
            else
            {
                r.Dispose();
                throw new TimeoutException("No response received");
            }
        }

        public void ConnectRequest(int timeout_ms, int retries)
        {
            for (int r = 0; r < retries; r++)
            {
                IAsyncResult async_result = BeginConnectRequest();
                try
                {
                    EndTransmit(async_result, timeout_ms);
                    return;
                }
                catch (TimeoutException)
                {
                    //continue
                }
            }
            throw new TimeoutException("No response received");
        }

        public void ReleaseRequest(int timeout_ms, int retries)
        {
            for (int r = 0; r < retries; r++)
            {
                IAsyncResult async_result = BeginReleaseRequest();
                try
                {
                    EndTransmit(async_result, timeout_ms);
                    return;
                }
                catch (TimeoutException)
                {
                    //continue
                }
            }
            throw new TimeoutException("No response received");
        }

        public void WriteRequest(IEnumerable<KeyValuePair<ProfinetIO.IODWriteReqHeader, byte[]>> values, int timeout_ms, int retries)
        {
            for (int r = 0; r < retries; r++)
            {
                IAsyncResult async_result = BeginWriteRequest(values);
                try
                {
                    EndTransmit(async_result, timeout_ms);
                    return;
                }
                catch (TimeoutException)
                {
                    //continue
                }
            }
            throw new TimeoutException("No response received");
        }

        public enum Controls
        {
            ParameterEnd,
        }

        public void ControlRequest(Controls c, int timeout_ms, int retries)
        {
            ProfinetIO.BlockTypes block_type;
            ProfinetIO.ControlCommands control;

            switch (c)
            {
                case Controls.ParameterEnd:
                    block_type = ProfinetIO.BlockTypes.IODBlockReq_ControlBlockConnect_PrmEnd;
                    control = ProfinetIO.ControlCommands.PrmEnd;
                    break;
                default:
                    throw new NotImplementedException();
            }

            for (int r = 0; r < retries; r++)
            {
                IAsyncResult async_result = BeginControlRequest(block_type, control);
                try
                {
                    EndTransmit(async_result, timeout_ms);
                    return;
                }
                catch (TimeoutException)
                {
                    //continue
                }
            }
            throw new TimeoutException("No response received");
        }

        public class ProfinetAsyncRPCResult : IAsyncResult, IDisposable
        {
            private System.Threading.ManualResetEvent m_wait = new System.Threading.ManualResetEvent(false);
            private ProfinetUdpTransport m_conn = null;

            public object AsyncState { get; set; }
            public System.Threading.WaitHandle AsyncWaitHandle { get { return m_wait; } }
            public bool CompletedSynchronously { get; private set; }    //always false
            public bool IsCompleted { get; private set; }

            public Stream Result { get; private set; }
            public RPC.Encodings Encoding { get; private set; }

            public ProfinetAsyncRPCResult(ProfinetUdpTransport conn, MemoryStream message)
            {
                m_conn = conn;
                conn.OnRPCMessageReceived += new OnRPCMessageReceivedHandler(conn_OnRPCMessageReceived);
                conn.SendRequest(message);
            }

            private void conn_OnRPCMessageReceived(ConnectionInfoUdp sender, RPC.PacketTypes type, RPC.Flags1 flags1, RPC.Flags2 flags2, RPC.Encodings encoding, ushort serial_high_low, Guid object_id, Guid interface_id, Guid activity_id, uint server_boot_time, uint sequence_no, RPC.Operations op, ushort body_length, ushort fragment_no, Stream data)
            {
                //TODO: compare something to validate correct response

                //set result
                Result = data;
                Encoding = encoding;

                //mark as finished
                IsCompleted = true;
                m_wait.Set();
                Dispose();
            }

            public void Dispose()
            {
                if (m_conn != null)
                {
                    m_conn.OnRPCMessageReceived -= conn_OnRPCMessageReceived;
                    m_conn = null;
                }
            }
        }

        public void Close()
        {
            m_is_closed = true;
            if (m_sender_conn != null)
            {
                m_sender_conn.Close();
                m_sender_conn = null;
            }
            if (m_receiver_conn != null)
            {
                m_receiver_conn.Close();
                m_receiver_conn = null;
            }
        }

        private void SendRequest(MemoryStream stream)
        {
            IPEndPoint ep = new IPEndPoint(m_destination_ip, m_port);
            byte[] buffer = stream.GetBuffer();
            int tx = m_sender_conn.Send(buffer, (int)stream.Position, ep);
        }

        private void SendResponse(MemoryStream stream)
        {
            IPEndPoint ep = new IPEndPoint(m_destination_ip, m_port);
            byte[] buffer = stream.GetBuffer();
            int tx = m_receiver_conn.Send(buffer, (int)stream.Position, ep);
        }
    }
}
