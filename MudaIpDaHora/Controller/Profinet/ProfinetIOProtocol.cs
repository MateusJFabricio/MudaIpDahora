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

namespace MudaIpDahora.Controller.Profinet
{
    public class ProfinetIO
    {
        public enum BlockTypes : ushort
        {
            //0x0000 Reserved —
            AlarmNotificationHigh = 0x0001,         // RTA-SDU.AlarmNotification-PDU
            AlarmNotificationLow = 0x0002,          // RTA-SDU.AlarmNotification-PDU
            //0x0003 – 0x0007 Reserved —
            IODWriteReqHeader = 0x0008,             // PROFINETIO-ServiceReqPDU.IODWriteReq
            IODReadReqHeader = 0x0009,              // PROFINETIO-ServiceReqPDU.IODReadReq
            //0x000A – 0x000F Reserved —
            DiagnosisData = 0x0010,                 // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.DiagnosisData RTA-SDU.AlarmNotification-PDU.AlarmPayload.DiagnosisItem.DiagnosisData
            //0x0011 Reserved —
            ExpectedIdentificationData = 0x0012,    // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.ExpectedIdentificationData
            RealIdentificationData = 0x0013,        // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.RealIdentificationData
            SubstituteValue = 0x0014,               // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.SubstituteValue
            // PROFINETIO-ServiceReqPDU.IODWriteReq.RecordDataWrite.SubstituteValue
            RecordInputDataObjectElement = 0x0015,  // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.RecordInputDataObjectElement
            RecordOutputDataObjectElement = 0x0016, // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.RecordOutputDataObjectElement
            //0x0017 Reserved —
            ARData = 0x0018,                        // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.ARData
            LogBookData = 0x0019,                   // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.LogBookData
            APIData = 0x001A,                       // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.APIData
            SRLData = 0x001B,                       // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.ARDataInfo.SRLData
            //0x001C – 0x001F Reserved —
            I_M0 = 0x0020,                          // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.IMData.I&M0– 350 – 61158–6–10/CD ED 4 © IEC (E):2015 Value (hexadecimal) Used for Used by
            I_M1 = 0x0021,                          // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.IMData.I&M1 PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.IMDataWrite.I&M1
            I_M2 = 0x0022,                          // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.IMData.I&M2 PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.IMDataWrite.I&M2
            I_M3 = 0x0023,                          // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.IMData.I&M3 PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.IMDataWrite.I&M3
            I_M4 = 0x0024,                          // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.IMData.I&M4 PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.IMDataWrite.I&M4
            I_M5 = 0x0025,                          // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.IMData.I&M5 
            I_M6_Start = 0x0026,
            I_M6_End = 0x002F,                      // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite
            I_M0FilterDataSubmodule = 0x0030,       // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.I&M0FilterData.I&M0FilterDataSubmodule
            I_M0FilterDataModule = 0x0031,          // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.I&M0FilterData.I&M0FilterDataModule
            I_M0FilterDataDevice = 0x0032,          // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.I&M0FilterData.I&M0FilterDataDevice
            //0x0033 Reserved —
            I_M5Data = 0x0034,                      // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.IMData.I&M5.I&M5Data
            //0x0035 – 0x0100 Reserved —
            ARBlockReq = 0x0101,                    // PROFINETIOServiceReqPDU.IODConnectReq.ARBlockReq
            IOCRBlockReq = 0x0102,                  // PROFINETIOServiceReqPDU.IODConnectReq.IOCRBlockReq
            AlarmCRBlockReq = 0x0103,               // PROFINETIOServiceReqPDU.IODConnectReq.AlarmCRBlockReq
            ExpectedSubmoduleBlockReq = 0x0104,     // PROFINETIO-ServiceReqPDU.IODConnectReq.ExpectedSubmoduleBlockReq
            PrmServerBlockReq = 0x0105,             // PROFINETIOServiceReqPDU.IODConnectReq.PrmServerBlockReq 
            MCRBlockReq = 0x0106,                   // PROFINETIOServiceReqPDU.IODConnectReq.MCRBlockReq
            ARRPCBlockReq = 0x0107,                 // PROFINETIOServiceReqPDU.IODConnectReq.ARRPCBlockReq
            ARVendorBlockReq = 0x0108,              // PROFINETIOServiceReqPDU.IODConnectReq.ARVendorBlockReq
            IRInfoBlock = 0x0109,                   // PROFINETIOServiceReqPDU.IODConnectReq.IRInfoBlock
            SRInfoBlock = 0x010A,                   // PROFINETIOServiceReqPDU.IODConnectReq.SRInfoBlock
            ARFSUBlock = 0x010B,                    // PROFINETIOServiceReqPDU.IODConnectReq.ARFSUBlock
            //0x010C – 0x010F Reserved —
            IODBlockReq_ControlBlockConnect_PrmEnd = 0x0110,// shall only be used in conjunction with connection establishment phase PROFINETIOServiceReqPDU.IODControlReq.ControlBlockConnect (PrmEnd.req)
            IODBlockReq_ControlBlockPlug_PrmEnd = 0x0111,   // shall only be used in conjunction with a plug alarm event PROFINETIOServiceReqPDU.IODControlReq.ControlBlockPlug (PrmEnd.req)
            IOXBlockReq_ControlBlockConnect_ApplicationReady = 0x0112,// shall be used in conjunction within the connection establishment phase or a PrmBegin/PrmEnd sequence PROFINETIOServiceReqPDU.IOXControlReq.ControlBlockConnect (ApplicationReady.req)
            IOXBlockReq_ControlBlockPlug_ApplicationReady = 0x0113,  // shall only be used in conjunction with a plug alarm event PROFINETIOServiceReqPDU.IOXControlReq.ControlBlockPlug (ApplicationReady.req)
            ReleaseBlockReq = 0x0114,               // PROFINETIOServiceReqPDU.IODReleaseReq.ReleaseBlock
            //0x0115 Reserved —
            IOXBlockReq_ControlBlockConnect_ReadyForCompanion = 0x0116,// shall only be used in conjunction with connection establishment phase PROFINETIOServiceReqPDU.IOXControlReq.ControlBlockConnect (ReadyForCompanion.req)
            IOXBlockReq_ControlBlockConnect_ReadyForRT_CLASS_3 = 0x0117,// shall only be used in conjunction with connection establishment phase PROFINETIOServiceReqPDU.IOXControlReq.ControlBlockConnect (ReadyForRT_CLASS_3.req)
            IODBlockReq_ControlBlockConnect_PrmBegin = 0x0118,// PROFINETIOServiceReqPDU.IODControlReq.ControlBlockConnect (PrmBegin.req)
            SubmoduleListBlock = 0x0119,            // PROFINETIOServiceReqPDU.IODControlReq.SubmoduleListBlock (PrmBegin.req)
            //0x0118 – 0x01FF Reserved —
            PDPortDataCheck = 0x0200,               // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDPortDataCheck PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDPortDataCheck PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDExpectedData.PDPortDataCheck
            PdevData = 0x0201,                      // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PdevData
            PDPortDataAdjust = 0x0202,              // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDPortDataAdjust PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDPortDataAdjust PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDExpectedData.PDPortDataAdjust
            PDSyncData = 0x0203,                    // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDSyncData PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDSyncData
            IsochronousModeData = 0x0204,           // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.IsochronousModeData PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.IsochronousModeData
            PDIRData = 0x0205,                      // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDIRData PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDIRData
            PDIRGlobalData = 0x0206,                // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDIRGlobalData PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDIRGlobalData
            PDIRFrameData = 0x0207,                 // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDIRFrameData PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDIRFrameData
            PDIRBeginEndData = 0x0208,              // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDIRBeginEndData PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDIRBeginEndData
            AdjustDomainBoundary = 0x0209,          // Sub block for adjusting DomainBoundary PROFINETIO-ServiceReqPDU.IODReadReq.RecordDataRead. PDPortDataAdjust.AdjustDomainBoundary PROFINETIO-ServiceReqPDU.IODWriteReq.RecordDataWrite.PDPortDataAdjust.AdjustDomainBoundary
            //0x020A Sub block for checking Peers PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDPortDataCheck.CheckPeers PROFINETIO-ServiceReqPDU.IODWriteReq.RecordDataWrite.PDPortDataCheck.CheckPeers
            //0x020B Sub block for checking LineDelay PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDPortDataCheck.CheckLineDelay PROFINETIO-ServiceReqPDU.IODWriteReq.RecordDataWrite.PDPortDataCheck.CheckLineDelay
            //0x020C Sub block for checking MAUType PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDPortDataCheck.CheckMAUType PROFINETIO-ServiceReqPDU.IODWriteReq.RecordDataWrite.PDPortDataCheck.CheckMAUType
            AdjustMAUType = 0x020E,                 // Sub block for adjusting MAUType PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDPortDataAdjust.AdjustMAUType PROFINETIO-ServiceReqPDU.IODWriteReq.RecordDataWrite.PDPortDataAdjust.AdjustMAUType
            PDPortDataReal = 0x020F,                // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDPortDataReal PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDRealData.PDPortDataReal
            AdjustMulticastBoundary = 0x0210,       // Sub block for adjusting MulticastBoundary PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDPortDataAdjust.AdjustMulticastBoundary PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDPortDataAdjust.AdjustMulticastBoundary 
            PDInterfaceMrpDataAdjust = 0x0211,      // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDInterfaceMrpDataAdjust PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDInterfaceMrpDataAdjust PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDExpectedData.PDInterfaceMrpDataAdjust
            PDInterfaceMrpDataReal = 0x0212,        // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDInterfaceMrpDataReal PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDRealData.PDInterfaceMrpDataReal 
            PDInterfaceMrpDataCheck = 0x0213,       // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDInterfaceMrpDataCheck PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDInterfaceMrpDataCheck PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDExpectedData.PDInterfaceMrpDataCheck
            PDPortMrpDataAdjust = 0x0214,           // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDPortMrpDataAdjust PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDPortMrpDataAdjust PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDExpectedData.PDPortMrpDataAdjust
            PDPortMrpDataReal = 0x0215,             // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDPortMrpDataReal PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDRealData.PDPortMrpDataReal 
            MrpManagerParams = 0x0216,              // Sub block for media redundancy manager parameters PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDInterfaceMrpDataAdjust.MrpManagerParams PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDInterfaceMrpDataAdjust.MrpManagerParams PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDInterfaceMrpDataReal.MrpManagerParams
            MrpClientParams = 0x0217,               // Sub block for media redundancy client parameters PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDInterfaceMrpDataAdjust.MrpClientParams PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDInterfaceMrpDataAdjust.MrpClientParams PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDInterfaceMrpDataReal.MrpClientParams
            MrpRingStateData = 0x0219,              // Sub block for media redundancy ring state data PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDInterfaceMrpDataReal.MrpRingStateData 
            AdjustLinkState = 0x021B,               // Sub block for adjusting LinkState PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDPortDataAdjust.AdjustLinkState PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDPortDataAdjust.AdjustLinkState
            CheckLinkState = 0x021C,                // Sub block for checking LinkState PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDPortDataCheck.CheckLinkState PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDPortDataAdjust.CheckLinkState
            CheckSyncDifference = 0x021E,           // Sub block for checking local and remote CableDelay detected by LLDP to discover a sync difference PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDPortDataCheck.CheckSyncDifference PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDPortDataCheck.CheckSyncDifference
            CheckMAUTypeDifference = 0x021F,        // Sub block for checking local and remote MAUTypes detected by LLDP PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDPortDataCheck.CheckMAUTypeDifference PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDPortDataCheck.CheckMAUTypeDifference
            PDPortFODataReal = 0x0220,              // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDPortFODataReal PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDRealData.PDPortFODataReal 
            FiberOpticManufacturerSpecific = 0x0221,// Sub block for reading real fiber optic manufacturerspecific data PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDPortFODataReal.FiberOpticManufacturerSpecific PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDRealData.PDPortFODataReal.FiberOpticManufacturerSpecific
            PDPortFODataAdjust = 0x0222,            // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDPortFODataAdjust PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDPortFODataAdjust PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDExpectedData.PDPortFODataAdjust
            PDPortFODataCheck = 0x0223,             // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDPortFODataCheck PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDPortFODataCheck PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDExpectedData.PDPortFODataCheck
            AdjustPeerToPeerBoundary = 0x0224,      // Sub block for adjusting the peer to peer boundary PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDPortDataAdjust.AdjustPeerToPeerBoundary PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDPortDataAdjust.AdjustPeerToPeerBoundary
            AdjustDCPBoundary = 0x0225,             // Sub block for adjusting the DCP boundary PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDPortDataAdjust.AdjustDCPBoundary PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDPortDataAdjust.AdjustDCPBoundary
            AdjustPreambleLength = 0x0226,          // Sub block for adjusting the used length of preamble PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDPortDataAdjust.AdjustPreambleLength PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDPortDataAdjust.AdjustPreambleLength
            CheckMAUTypeExtension = 0x0227,         // Sub block for checking MAUTypeExtension PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDPortDataCheck. CheckMAUTypeExtension PROFINETIO-ServiceReqPDU.IODWriteReq.RecordDataWrite.PDPortDataCheck.CheckMAUTypeExtension
            FiberOpticDiagnosisInfo = 0x0228,       // Sub block for reading real fiber optic diagnosis data PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDPortFODataReal.FiberOpticDiagnosisInfo PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDRealData.FiberOpticDiagnosisInfo
            AdjustMAUTypeExtension = 0x0229,        // Sub block for adjusting MAUTypeExtension PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDPortDataAdjust.AdjustMAUTypeExtension PROFINETIO-ServiceReqPDU.IODWriteReq.RecordDataWrite.PDPortDataAdjust.AdjustMAUTypeExtension
            PDIRSubframeData = 0x022A,              // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDIRSubframeData PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDIRSubframeData PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PdevData.PDIRSubframeData 
            SubframeBlock = 0x022B,                 // Sub block for the persistent portion of DFP PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDIRSubframeData.SubframeBlock PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDIRSubframeData.SubframeBlock PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PdevData.PDIRSubframeData.SubframeBlock
            PDPortDataRealExtended = 0x022C,        // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDPortDataRealExtended PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDRealData.PDPortDataRealExtended 
            PDTimeData = 0x022D,                    // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDTimeData PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDTimeData
            //0x022E – 0x22F Reserved
            PDNCDataCheck = 0x0230,                 // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDNCDataCheck PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDNCDataCheck PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDExpectedData.PDNCDataCheck
            MrpInstanceDataAdjustBlock = 0x0231,    // Sub block of PDInterfaceMrpDataAdjust PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDInterfaceMrpDataAdjust PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDInterfaceMrpDataAdjust
            MrpInstanceDataRealBlock = 0x0232,      // Sub block of PDInterfaceMrpDataReal PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDInterfaceMrpDataReal PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDInterfaceMrpDataReal
            MrpInstanceDataCheckBlock = 0x0233,     // Sub block of PDInterfaceMrpDataCheck PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDInterfaceMrpDataCheck PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDInterfaceMrpDataCheck
            //0x0234 – 0x23F Reserved —
            PDInterfaceDataReal = 0x0240,           // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDInterfaceDataReal PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDRealData.PDInterfaceDataReal
            //0x0241 – 0x024F Reserved —
            PDInterfaceAdjust = 0x0250,             // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDInterfaceAdjust PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDInterfaceAdjust PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDRealData.PDInterfaceAdjust PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDExpectedData.PDInterfaceAdjust
            PDPortStatistic = 0x0251,               // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDPortStatistic PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDRealData.PDPortStatistic
            //0x0252 – 0x025F Reserved
            OwnPort = 0x0260,                       // Sub block of PDPortDataRealExtended PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDPortDataReal.OwnPort PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDRealData.PDPortDataRealExtended.OwnPort
            Neighbors = 0x0261,                     // Sub block of PDPortDataRealExtended PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDPortDataRealExtended.Neighbors PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDRealData.PDPortDataRealExtended.Neighbors
            //0x0262 – 0x03FF Reserved —
            MultipleBlockHeader = 0x0400,           // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDRealData.MultipleBlockHeader PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDExpectedData.MultipleBlockHeader
            COContainerContent = 0x0401,            // PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.CombinedObjectContainer.COContainerContent
            //0x0402 – 0x04FF Reserved —
            RecordDataReadQuery = 0x0500,           // PROFINETIOServiceResPDU.IODReadReq.RecordDataReadQuery
            //0x0501 – 0x05FF Reserved —
            FSHelloBlock = 0x0600,                  // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDInterfaceFSUDataAdjust.FSHelloBlock PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDInterfaceFSUDataAdjust.FSHelloBlock PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDExpectedData.PDInterfaceFSUDataAdjust.FSHelloBlock
            FSParameterBlock = 0x0601,              // PROFINETIOServiceReqPDU.IODConnectReq.IOCRBlockReq.ARFSUBlock.ARFSUDataAdjust.FSParameterBlock
            //0x0602 – 0x607 Reserved for FastStartUp —
            PDInterfaceFSUDataAdjust = 0x0608,      // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDInterfaceFSUDataAdjust PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.PDInterfaceFSUDataAdjust PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PDExpectedData.PDInterfaceFSUDataAdjust
            ARFSUDataAdjust = 0x0609,               // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.ARFSUDataAdjust PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite.ARFSUDataAdjust PROFINETIOServiceReqPDU.IODConnectReq.ARFSUBlock.ARFSUDataAdjust
            //0x060A – 0x60F Reserved for FastStartUp —
            //0x0610 – 0x06FF Reserved —
            AutoConfiguration = 0x0700,             // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.AutoConfiguration
            AutoConfigurationCommunication = 0x0701,// PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.AutoConfiguration.ACCommunication
            AutoConfigurationConfiguration = 0x0702,// PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.AutoConfiguration.ACConfiguration
            AutoConfigurationIsochronous = 0x0703,  // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.AutoConfiguration.ACIsochronous
            //0x0704 – 0x07FF Reserved —
            //0x0800 Reserved for profiles covering energy saving BlockType for request service PROFINETIOServiceReqPDU.IODWriteReq.RecordDataWrite
            //0x0801 Reserved for profiles covering energy saving BlockType for response service PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead
            //0x0802 – 0x080F Reserved for profiles covering energy saving —
            PE_EntityFilterData = 0x0810,           // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PE_EntityFilterData
            PE_EntityStatusData = 0x0811,           // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.PE_EntityStatusData
            //0x0812 – 0x08FF Reserved for profiles covering energy saving —
            //0x0900 – 0x09FF Reserved for profiles covering sequence of events —
            UploadBLOBQuery = 0x0A00,               // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.UploadBLOBQuery
            UploadBLOB = 0x0A01,                    // PROFINETIOServiceResPDU.IODReadRes.RecordDataRead.UploadBLOB
            NestedDiagnosisInfo = 0x0A02,           // PROFINETIOServiceReqPDU.IODReadReq.RecordDataRead.NestedDiagnosisInfo
            //0x0A03 – 0x0AFF Reserved for profiles covering controller to controller communication —
            //0x0B00 – 0x0EFF Reserved 
            MaintenanceItem = 0x0F00,               // RTA-SDU.AlarmnotificationPDU.AlarmPayload.MaintenanceItem
            UploadSelectedRecordsWithinUpload_RetrievalItem = 0x0F01,   // RTA-SDU.AlarmnotificationPDU.AlarmPayload.Upload&RetrievalItem
            iParameterItem = 0x0F02,                // RTA-SDU.AlarmnotificationPDU.AlarmPayload.iParameterItem
            RetrieveSelectedRecordsWithinUpload_RetrievalItem = 0x0F03, // RTA-SDU.AlarmnotificationPDU.AlarmPayload.Upload&RetrievalItem
            RetrieveAllStoredRecordsWithinUpload_RetrievalItem = 0x0F04,    // RTA-SDU.AlarmnotificationPDU.AlarmPayload.Upload&RetrievalItem
            SignalAPE_OperationalModeChangeWithinPE_EnergySavingStatus = 0x0F05,    // RTA-SDU.AlarmnotificationPDU.AlarmPayload.PE_AlarmItem
            //0x0F06 – 0x6FFF Reserved —
            //0x7000 – 0x7FFF Reserved for vendor specific data objects —
            //0x8000 Reserved —
            AlarmAckHigh = 0x8001,                  // RTA-SDU.AlarmAck-PDU
            AlarmAckLow = 0x8002,                   // RTA-SDU.AlarmAck-PDU
            IODWriteResHeader = 0x8008,             // PROFINETIO-ServiceResPDU.IODWriteRes
            IODReadResHeader = 0x8009,              // PROFINETIO-ServiceReqPDU.IODReadRes
            //0x800A – 0x8100 Reserved —
            ARBlockRes = 0x8101,                    // PROFINETIOServiceResPDU.IODConnectRes.ARBlockRes
            IOCRBlockRes = 0x8102,                  // PROFINETIOServiceResPDU.IODConnectRes.IOCRBlockRes
            AlarmCRBlockRes = 0x8103,               // PROFINETIOServiceResPDU.IODConnectRes.AlarmCRBlockRes
            ModuleDiffBlock = 0x8104,               // PROFINETIOServiceResPDU.IODConnectRes.ModuleDiffBlock PROFINETIOServiceResPDU.IODWriteRes.RecordDataRead.ModuleDiffBlock PROFINETIOServiceReqPDU.IOXControlReq.ModuleDiffBlock
            PrmServerBlockRes = 0x8105,             // PROFINETIOServiceResPDU.IODConnectRes.PrmServerBlockRes
            ARServerBlockRes = 0x8106,              // PROFINETIOServiceResPDU.IODConnectRes.ARServerBlockRes
            ARRPCBlockRes = 0x8107,                 // PROFINETIOServiceResPDU.IODConnectRes.ARRPCBlockRes
            ARVendorBlockRes = 0x8108,              // PROFINETIOServiceResPDU.IODConnectRes.ARVendorBlockRes
            //0x8109 – 0x810F Reserved —
            IODBlockRes_ControlBlockConnect_PrmEnd = 0x8110, // shall only be used in conjunction with connection establishment phase PROFINETIOServiceResPDU.IODControlRes.ControlBlockConnect (PrmEnd.rsp)
            IODBlockRes_ControlBlockPlug_PrmEnd = 0x8111, // shall only be used in conjunction with a plug alarm event PROFINETIOServiceResPDU.IODControlRes.ControlBlockPlug (PrmEnd.rsp)
            IOXBlockRes_ControlBlockConnect_ApplicationReady = 0x8112, // shall only be used in conjunction with connection establishment phase PROFINETIOServiceResPDU.IOXControlRes.ControlBlockConnect (ApplRdy.rsp)
            IOXBlockRes_ControlBlockPlug_ApplicationReady = 0x8113, // shall only be used in conjunction with a plug alarm event PROFINETIOServiceResPDU.IOXControlRes.ControlBlockPlug (ApplRdy.rsp)
            ReleaseBlockRes = 0x8114,               // PROFINETIOServiceResPDU.IODReleaseRes.ReleaseBlock
            //0x8115 Reserved —
            IOXBlockRes_ControlBlockConnect_ReadyForCompanion = 0x8116,  // shall only be used in conjunction with connection establishment phase PROFINETIOServiceResPDU.IOXControlRes.ControlBlockConnect (ReadyForCompanion.rsp)
            IOXBlockRes_ControlBlockConnect_ReadyForRT_CLASS_3 = 0x8117, // shall only be used in conjunction with connection establishment phase PROFINETIOServiceResPDU.IOXControlRes.ControlBlockConnect (ReadyForRT_CLASS_3.rsp)
            IODBlockRes_ControlBlockConnect_PrmBegin = 0x8118,      // PROFINETIOServiceResPDU.IODControlRes.ControlBlockConnect (PrmBegin.rsp)
            //Other Reserved —
        }

        public enum ARTypes : ushort
        {
            //0x0000 Reserved —
            IOCARSingle = 0x0001,
            //0x0002 – 0x0005 Reserved —
            IOSAR = 0x0006, //The supervisor AR is a special form of the IOCARSingle allowing takeover of the ownership of a submodule
            //0x0007 – 0x000F Reserved —
            IOCARSingle_RT_CLASS_3 = 0x0010, // using RT_CLASS_3 This is a special form of the IOCARSingle indicating RT_CLASS_3 communication
            //0x0011 – 0x001F Reserved —
            IOCARSR = 0x0020,   //The SR AR is a special form of the IOCARSingle indicating system redundancy or dynamic reconfiguration usage
            //0x0021 – 0xFFFF Reserved
        }

        [Flags]
        public enum ARProperties : uint
        {
            State_Active = 1,
            SupervisorTakeoverNotAllowed = 0 << 3,
            SupervisorTakeoverAllowed = 1 << 3,
            ParameterizationServer_ExternalPrmServer = 0 << 4,
            ParameterizationServer_CMInitiator = 1 << 4,
            DeviceAccess_OnlySubmodulesFromExpectedSubmoduleBlockAccessible = 0 << 8,
            DeviceAccess_SubmoduleAccessControlledIODevice = 1 << 8,
            CompanionAR_Single = 0 << 9,
            CompanionAR_First = 1 << 9,
            CompanionAR_Companion = 2 << 9,
            AcknowledgeCompanionAR_NoRequired = 0 << 11,
            AcknowledgeCompanionAR_WithAcknowledge = 1 << 11,
            StartupMode_Legacy = 0 << 30,
            StartupMode_Advanced = 1 << 30,
            PullModuleAlarmAllowed_Mode0 = 0 << 31,
            PullModuleAlarmAllowed_Mode1 = 1u << 31,
        }

        public enum IOCRTypes : ushort
        {
            //0x0000 Reserved
            Input = 0x0001,
            Output = 0x0002,
            MulticastProvider = 0x0003,
            MulticastConsumer = 0x0004,
            //0x0005 – 0xFFFF Reserved
        }

        public enum IOCRProperties : uint
        {
            //0x00 Reserved —
            RT_CLASS1 = 0x01,   // for the IOCRs Data-RTC-PDU IOCRProperties.RTClass == 0x02 shall be used as substitution by the engineering tool. Shall be supported for legacy IO devices by IO controller and IO supervisor. It should not be generated by an IO device.
            RT_CLASS2 = 0x02,   // for the IOCRs Data-RTC-PDU
            RT_CLASS3 = 0x03,   // for the IOCRs Data-RTC-PDU
            RT_CLASS4 = 0x04, // for the IOCRs UDP-RTC-PDU
            //0x05 – 0x07 Reserved
        }

        [Flags]
        public enum IOCRTagHeaders : ushort
        {
            IOCRVLANID_NoVLAN = 0,
            IOCRVLANID_Start = 1,   //According IEEE 802.1Q
            IOCRVLANID_End = 0xFFF,
            IOUserPriority_CR = 0xC000,
        }

        [Flags]
        public enum AlarmCRProperties : uint
        {
            UserPriority = 0,
            FixedPriority = 1,
            RTA_CLASS_1 = 0,
            RTA_CLASS_UDP = 2,
        }

        public enum SubmoduleProperties : ushort
        {
            Type_NO_IO = 0,
            Type_INPUT = 1,
            Type_OUTPUT = 2,
            Type_INPUT_OUTPUT = 3,
            SharedInput_IOControllerOnly = 0 << 2,
            SharedInput_IOControllerShared = 1 << 2,
            ReduceInputSubmoduleDataLength_Expected = 0 << 3,
            ReduceInputSubmoduleDataLength_Zero = 1 << 3,
            ReduceOutputSubmoduleDataLength_Expected = 0 << 4,
            ReduceOutputSubmoduleDataLength_Zero = 1 << 4,
            DiscardIOXS_Expected = 0 << 5,
            DiscardIOXS_Zero = 1 << 5,
        }

        public enum Index : ushort
        {
            /* user specific */
            //0 – 0x7FFF User specific RecordData

            /* subslot specific */
            ExpectedIdentificationData_subslot = 0x8000, //for one subslot //All Needed
            RealIdentificationData_subslot = 0x8001, //for one subslot //All —
            //0x8002 – 0x8009 Reserved — —
            DiagnosisChannel_subslot = 0x800A, //for one subslot //All Filter
            DiagnosisAll_subslot = 0x800B, //for one subslot //All Filter
            DiagnosisMaintenanceQualifiedStatus_subslot = 0x800C, //for one subslot //All Filter
            //0x800D – 0x800F Reserved — —
            MaintenanceRequiredChannel_subslot = 0x8010, //for one subslot //All Filter
            MaintenanceDemandedChannel_subslot = 0x8011, //for one subslot //All Filter
            MaintenanceRequiredAll_subslot = 0x8012, //for one subslot //All Filter
            MaintenanceDemandedAll_subslot = 0x8013, //for one subslot //All Filter
            //0x8014 – 0x801D Reserved — —
            SubstituteValue = 0x801E, //for one subslot //All —
            //0x801F Reserved — —
            PDIRSubframeData = 0x8020, //for one subslot //Interface —
            //0x8021 – 0x8026 Reserved — —
            PDPortDataRealExtended = 0x8027, //for one subslot //Port —
            RecordInputDataObjectElement = 0x8028, //for one subslot //All —
            RecordOutputDataObjectElement = 0x8029, //for one subslot //All —
            PDPortDataReal = 0x802A, //for one subslot //Port —
            PDPortDataCheck = 0x802B, //for one subslot //Port —
            PDIRData = 0x802C, //for one subslot //Interface —
            PDSyncData = 0x802D, //for one subslot with SyncID value 0 //Interface —
            //0x802E Reserved (legacy) — —
            PDPortDataAdjust = 0x802F, //for one subslot //Port —
            IsochronousModeData = 0x8030, //for one subslot //All —
            PDTimeData = 0x8031, //for one subslot //Interface —
            //0x8032 – 0x804F Reserved — —
            PDInterfaceMrpDataReal = 0x8050, //for one subslot //Interface —
            PDInterfaceMrpDataCheck = 0x8051, //for one subslot //Interface —
            PDInterfaceMrpDataAdjust = 0x8052, //for one subslot //Interface —
            PDPortMrpDataAdjust = 0x8053, //for one subslot //Port —
            PDPortMrpDataReal = 0x8054, //for one subslot //Port —
            //0x8055 – 0x805F Reserved — —
            PDPortFODataReal = 0x8060, //for one subslot //Port —
            PDPortFODataCheck = 0x8061, //for one subslot //Port —
            PDPortFODataAdjust = 0x8062, //for one subslot //Port —
            //0x8063 – 0x806F Reserved — —
            PDNCDataCheck = 0x8070, //for one subslot //Interface —
            PDInterfaceAdjust = 0x8071, //for one subslot //Interface —
            PDPortStatistic = 0x8072, //for one subslot //Interface, Port —
            //0x8073 – 0x807F Reserved — —
            PDInterfaceDataReal = 0x8080, //for one subslot //Interface —
            //0x8081 – 0x808F Reserved — —
            PDInterfaceFSUDataAdjust = 0x8090, //Interface —
            //0x8091 – 0x809F Reserved — —
            ProfilesCoveringEnergySavingRecord_0 = 0x80A0, //All —
            //0x80A1 – 0x80AE Reserved //for profiles covering energy saving — —
            PE_StatusData = 0x80AF, //for one subslot //All —
            CombinedObjectContainer = 0x80B0, //All —
            //0x80B1 – 0x80BF Reserved — —
            ProfilesCoveringSequenceOfEventsRecord_0 = 0x80C0, //All —
            //0x80C1 – 0x80CF Reserved for profiles covering sequence of events — —
            //0x80D0 – 0xAFEF Reserved — —
            I_M0 = 0xAFF0, //All —
            I_M1 = 0xAFF1, //All —
            I_M2 = 0xAFF2, //All —
            I_M3 = 0xAFF3, //All —
            I_M4 = 0xAFF4, //All —
            I_M5 = 0xAFF5, //All —
            //0xAFF6 – 0xAFFF I&M6 – I&M15 Reserved for additional identification and maintenance data 
            //0xB000 – 0xBFFF Reserved for profiles

            /* slot specific */
            ExpectedIdentificationData_slot = 0xC000, //for one slot //Needed
            RealIdentificationData_slot = 0xC001, //for one slot —
            //0xC002 – 0xC009 Reserved —
            DiagnosisChannel_slot = 0xC00A, //for one slot //Filter
            DiagnosisAll_slot = 0xC00B, //for one slot //Filter
            DiagnosisMaintenanceQualifiedStatus_slot = 0xC00C, //for one slot //Filter
            //0xC00D – 0xC00F Reserved —
            MaintenanceRequiredChannel_slot = 0xC010, //for one slot //Filter
            MaintenanceDemandedChannel_slot = 0xC011, //for one slot //Filter
            MaintenanceRequiredAll_slot = 0xC012, //for one slot //Filter
            MaintenanceDemandedAll_slot = 0xC013, //for one slot //Filter
            //0xC014 – 0xCFFF Reserved —
            //0xD000 – 0xDFFF Reserved for profiles

            /* AR specific */
            ExpectedIdentificationData_AR = 0xE000, //for one AR Needed
            RealIdentificationData_AR = 0xE001, //for one AR Needed
            ModuleDiffBlock = 0xE002, //for one AR Needed
            //0xE003 – 0xE009 Reserved —
            DiagnosisChannel_AR = 0xE00A, //for one AR Needed
            DiagnosisAll_AR = 0xE00B, //for one AR Needed
            DiagnosisMaintenanceQualifiedStatus_AR = 0xE00C, //for one AR Needed
            //0xE00D – 0xE00F Reserved —
            MaintenanceRequiredChannel_AR = 0xE010, //for one AR Needed
            MaintenanceDemandedChannel_AR = 0xE011, //for one AR Needed
            MaintenanceRequiredAll_AR = 0xE012, //for one AR Needed
            MaintenanceDemandedAll_AR = 0xE013, //for one AR Needed
            //0xE014 – 0xE02F Reserved —
            PE_EntityFilterData_AR = 0xE030, //for one AR Needed
            PE_EntityStatusData_AR = 0xE031, //for one AR Needed
            //0xE032 – 0xE03F Reserved —
            WriteMultiple = 0xE040,
            //0xE041 – 0xE04F Reserved —
            ARFSUDataAdjust = 0xE050, //for one AR Legacy, used only for ARProperties.StartupMode == Legacy Needed
            //0xE051 – 0xE05F Reserved for FastStartUp —
            //0xE060 – 0xEBFF Reserved —
            //0xEC00 – 0xEFFF Reserved for profiles Needed

            /* API specific */
            RealIdentificationData_API = 0xF000, //for one API —
            //0xF001 – 0xF009 Reserved —
            DiagnosisChannel_API = 0xF00A, //for one API —
            DiagnosisAll_API = 0xF00B, //for one API —
            DiagnosisMaintenanceQualifiedStatus_API = 0xF00C, //for one API —
            //0xF00D – 0xF00F Reserved —
            MaintenanceRequiredChannel_API = 0xF010, //for one API —
            MaintenanceDemandedChannel_API = 0xF011, //for one API —
            MaintenanceRequiredAll_API = 0xF012, //for one API —
            MaintenanceDemandedAll_API = 0xF013, //for one API —
            //0xF014 – 0xF01F Reserved —
            ARData_API = 0xF020, //for one API —
            //0xF021 – 0xF3FF Reserved —
            //0xF400 – 0xF7FF Reserved for profiles

            /* device specific */
            //0xF800 – 0xF80B Reserved —
            DiagnosisMaintenanceQualifiedStatus_Device = 0xF80C,//for one device —
            //0xF80D – 0xF81F Reserved —
            ARData_Device = 0xF820,
            APIData = 0xF821,
            //0xF822 – 0xF82F Reserved —
            LogBookData = 0xF830,
            PdevData = 0xF831,
            //0xF832 – 0xF83F Reserved —
            I_M0FilterData = 0xF840,
            PDRealData = 0xF841,
            PDExpectedData = 0xF842,
            //0xF843 – 0xF84F Reserved —
            AutoConfiguration = 0xF850,
            //0xF851 – F85F Reserved —
            GSDUpload = 0xF860,
            NestedDiagnosisInfo = 0xF861,
            //0xF862 – 0xF86F Reserved for controller to controller communication —
            PE_EntityFilterData_Device = 0xF870,
            PE_EntityStatusData_Device = 0xF871,
            //0xF872 – 0xFBFE Reserved —
            TriggerIndex = 0xFBFF,
            //0xFC00 – 0xFFFF Reserved for profiles a
        }

        public enum ControlCommands : ushort
        {
            PrmEnd = 1,
            ApplicationReady = 2,
            Release = 4,
            Done = 8,
            ReadyForCompanion = 16,
            ReadyForRT_CLASS_3 = 32,
            PrmBegin = 64,
        }

        public enum ControlBlockProperties : ushort
        {
            ImplicitControlCommandReadyForCompanion = 1,
            ImplicitControlCommandReadyForRT_CLASS_3 = 2,
        }

        private static int EncodeBlockHeader(System.IO.Stream buffer, BlockTypes type, UInt16 BlockLength, out long BlockLength_position)
        {
            int ret = 0;

            ret += DCP.EncodeU16(buffer, (ushort)type);
            BlockLength_position = buffer.Position;  //save it for later re-encode
            ret += DCP.EncodeU16(buffer, BlockLength);
            ret += DCP.EncodeU16(buffer, 0x100);    //version

            return ret;
        }

        private static int DecodeBlockHeader(System.IO.Stream buffer, out BlockTypes type, out UInt16 BlockLength)
        {
            int ret = 0;
            ushort tmp;

            ret += DCP.DecodeU16(buffer, out tmp);
            type = (BlockTypes)tmp;
            ret += DCP.DecodeU16(buffer, out BlockLength);
            ret += DCP.DecodeU16(buffer, out tmp);    //version

            return ret;
        }

        private static void ReEncodeBlockLength(System.IO.Stream buffer, long BlockLength_position)
        {
            long current_pos = buffer.Position;
            buffer.Position = BlockLength_position;
            DCP.EncodeU16(buffer, (ushort)((current_pos - buffer.Position) - 2));
            buffer.Position = current_pos;
        }

        public class ARBlockRequest : IProfinetSerialize
        {
            public ARTypes Type;
            public Guid UUID;
            public UInt16 SessionKey;
            public System.Net.NetworkInformation.PhysicalAddress CMInitiatorMac;
            public Guid CMInitiatorObjectUUID;
            public ARProperties Properties;
            public UInt16 CMInitiatorActivityTimeoutFactor;
            public UInt16 CMInitiatorUDPRTPort;
            public string CMInitiatorStationName;

            public int Serialize(System.IO.Stream buffer)
            {
                int ret = 0;
                long BlockLength_position;

                ret += EncodeBlockHeader(buffer, BlockTypes.ARBlockReq, 0, out BlockLength_position);
                ret += DCP.EncodeU16(buffer, (ushort)Type);
                ret += RPC.EncodeGuid(buffer, RPC.Encodings.BigEndian, UUID);
                ret += DCP.EncodeU16(buffer, SessionKey);
                ret += DCP.EncodeOctets(buffer, CMInitiatorMac.GetAddressBytes());
                ret += RPC.EncodeGuid(buffer, RPC.Encodings.BigEndian, CMInitiatorObjectUUID);
                ret += DCP.EncodeU32(buffer, (uint)Properties);
                ret += DCP.EncodeU16(buffer, CMInitiatorActivityTimeoutFactor);
                ret += DCP.EncodeU16(buffer, CMInitiatorUDPRTPort);
                ret += DCP.EncodeU16(buffer, (ushort)CMInitiatorStationName.Length);
                ret += DCP.EncodeString(buffer, CMInitiatorStationName);
                ReEncodeBlockLength(buffer, BlockLength_position);

                return ret;
            }
        }

        public class IOC_DataObject : IProfinetSerialize
        {
            public UInt16 SlotNumber { get; set; }
            public UInt16 SubslotNumber { get; set; }
            public UInt16 FrameOffset { get; set; }

            public IOC_DataObject(UInt16 slot_number, UInt16 subslot_number, UInt16 frame_offset)
            {
                SlotNumber = slot_number;
                SubslotNumber = subslot_number;
                FrameOffset = frame_offset;
            }

            public IOC_DataObject()
            {
            }

            public int Serialize(System.IO.Stream buffer)
            {
                int ret = 0;
                ret += DCP.EncodeU16(buffer, SlotNumber);
                ret += DCP.EncodeU16(buffer, SubslotNumber);
                ret += DCP.EncodeU16(buffer, FrameOffset);
                return ret;
            }
        }

        public class IOC_API : IProfinetSerialize
        {
            public UInt32 No { get; set; }
            public IOC_DataObject[] DataObjects { get; set; }
            public IOC_DataObject[] IOCS { get; set; }

            public int Serialize(System.IO.Stream buffer)
            {
                int ret = 0;
                ret += DCP.EncodeU32(buffer, No);
                ret += DCP.EncodeU16(buffer, (ushort)DataObjects.Length);
                for (int i = 0; i < DataObjects.Length; i++)
                    ret += DataObjects[i].Serialize(buffer);
                ret += DCP.EncodeU16(buffer, (ushort)IOCS.Length);
                for (int i = 0; i < IOCS.Length; i++)
                    ret += IOCS[i].Serialize(buffer);
                return ret;
            }
        }

        public class IOCRBlockRequest : IProfinetSerialize
        {
            public IOCRTypes Type;
            public UInt16 Reference;
            public IOCRProperties Properties;
            public UInt16 DataLength;
            public UInt16 FrameID;
            public UInt16 SendClockFactor;
            public UInt16 ReductionRatio;
            public UInt16 Phase;
            public UInt16 Sequence;
            public UInt32 FrameSendOffset;
            public UInt16 DataHoldFactorA;
            public UInt16 DataHoldFactorB;
            public IOCRTagHeaders TagHeader;
            public System.Net.NetworkInformation.PhysicalAddress MulticastMAC;
            public IOC_API[] APIs;

            public int Serialize(System.IO.Stream buffer)
            {
                int ret = 0;
                long BlockLength_position;

                /* BlockHeader, IOCRType, IOCRReference, LT, IOCRProperties, DataLength, FrameID,
                    SendClockFactor, ReductionRatio, Phase, Sequence, FrameSendOffset, DataHoldFactor a,
                    DataHoldFactor, IOCRTagHeader, IOCRMulticastMACAdd, NumberOfAPIs, (API,
                    NumberOfIODataObjects, (SlotNumber, SubslotNumber, IODataObjectFrameOffset)*,
                    NumberOfIOCS, (SlotNumber, SubslotNumber, IOCSFrameOffset)*)* */

                ret += EncodeBlockHeader(buffer, BlockTypes.IOCRBlockReq, 0, out BlockLength_position);
                ret += DCP.EncodeU16(buffer, (ushort)Type);
                ret += DCP.EncodeU16(buffer, Reference);
                ret += DCP.EncodeU16(buffer, 0x8892);   //LT
                ret += DCP.EncodeU32(buffer, (uint)Properties);
                ret += DCP.EncodeU16(buffer, DataLength);
                ret += DCP.EncodeU16(buffer, FrameID);
                ret += DCP.EncodeU16(buffer, SendClockFactor);
                ret += DCP.EncodeU16(buffer, ReductionRatio);
                ret += DCP.EncodeU16(buffer, Phase);
                ret += DCP.EncodeU16(buffer, Sequence);
                ret += DCP.EncodeU32(buffer, FrameSendOffset);
                ret += DCP.EncodeU16(buffer, DataHoldFactorA);
                ret += DCP.EncodeU16(buffer, DataHoldFactorB);
                ret += DCP.EncodeU16(buffer, (ushort)TagHeader);
                ret += DCP.EncodeOctets(buffer, MulticastMAC.GetAddressBytes());
                ret += DCP.EncodeU16(buffer, (ushort)APIs.Length);
                for (int i = 0; i < APIs.Length; i++)
                    ret += APIs[i].Serialize(buffer);
                ReEncodeBlockLength(buffer, BlockLength_position);

                return ret;
            }
        }

        public class AlarmCRBlockRequest : IProfinetSerialize
        {
            public AlarmCRProperties Properties;
            public UInt16 RTATimeoutFactor;
            public UInt16 RTARetries;
            public UInt16 LocalAlarmReference;
            public UInt16 MaxAlarmDataLength;
            public UInt16 AlarmCRTagHeaderHigh;
            public UInt16 AlarmCRTagHeaderLow;

            public int Serialize(System.IO.Stream buffer)
            {
                int ret = 0;
                long BlockLength_position;

                /* BlockHeader, AlarmCRType, LT, AlarmCRProperties, RTATimeoutFactor, RTARetries,
                    LocalAlarmReference, MaxAlarmDataLength, AlarmCRTagHeaderHigh,
                    AlarmCRTagHeaderLow */

                ret += EncodeBlockHeader(buffer, BlockTypes.AlarmCRBlockReq, 0, out BlockLength_position);
                ret += DCP.EncodeU16(buffer, 1);         //AlarmCRType
                ret += DCP.EncodeU16(buffer, 0x8892);    //LT
                ret += DCP.EncodeU32(buffer, (UInt32)Properties);
                ret += DCP.EncodeU16(buffer, RTATimeoutFactor);
                ret += DCP.EncodeU16(buffer, RTARetries);
                ret += DCP.EncodeU16(buffer, LocalAlarmReference);
                ret += DCP.EncodeU16(buffer, MaxAlarmDataLength);
                ret += DCP.EncodeU16(buffer, AlarmCRTagHeaderHigh);
                ret += DCP.EncodeU16(buffer, AlarmCRTagHeaderLow);
                ReEncodeBlockLength(buffer, BlockLength_position);

                return ret;
            }
        }

        public class DataDescription : IProfinetSerialize
        {
            public enum Types : ushort
            {
                Input = 1,
                Output = 2,
            }

            public Types Type { get; set; }
            public UInt16 SubmoduleDataLength { get; set; }
            public byte LengthIOCS { get; set; }
            public byte LengthIOPS { get; set; }

            public DataDescription()
            {
            }

            public DataDescription(Types type, UInt16 submodule_data_length, byte length_iocs, byte length_iops)
            {
                Type = type;
                SubmoduleDataLength = submodule_data_length;
                LengthIOCS = length_iocs;
                LengthIOPS = length_iops;
            }

            public int Serialize(System.IO.Stream buffer)
            {
                int ret = 0;
                ret += DCP.EncodeU16(buffer, (ushort)Type);
                ret += DCP.EncodeU16(buffer, SubmoduleDataLength);
                ret += DCP.EncodeU8(buffer, LengthIOCS);
                ret += DCP.EncodeU8(buffer, LengthIOPS);
                return ret;
            }
        }

        public class Submodule : IProfinetSerialize
        {
            public UInt16 SubslotNumber { get; set; }
            public UInt32 SubmoduleIdentNumber { get; set; }
            public SubmoduleProperties SubmoduleProperties { get; set; }
            public DataDescription[] DataDescription { get; set; }

            public int Serialize(System.IO.Stream buffer)
            {
                int ret = 0;
                ret += DCP.EncodeU16(buffer, SubslotNumber);
                ret += DCP.EncodeU32(buffer, SubmoduleIdentNumber);
                ret += DCP.EncodeU16(buffer, (ushort)SubmoduleProperties);
                for (int i = 0; i < DataDescription.Length; i++)
                    ret += DataDescription[i].Serialize(buffer);
                return ret;
            }
        }

        public class Submodule_API : IProfinetSerialize
        {
            public UInt32 No { get; set; }
            public UInt16 SlotNumber { get; set; }
            public UInt32 ModuleIdentNumber { get; set; }
            public Submodule[] SubModules { get; set; }

            public int Serialize(System.IO.Stream buffer)
            {
                int ret = 0;
                ret += DCP.EncodeU32(buffer, No);
                ret += DCP.EncodeU16(buffer, SlotNumber);
                ret += DCP.EncodeU32(buffer, ModuleIdentNumber);
                ret += DCP.EncodeU16(buffer, 0);    //ModuleProperties
                ret += DCP.EncodeU16(buffer, (ushort)SubModules.Length);
                for (int i = 0; i < SubModules.Length; i++)
                    ret += SubModules[i].Serialize(buffer);
                return ret;
            }
        }

        public class ExpectedSubmoduleBlockRequest : IProfinetSerialize
        {
            public Submodule_API[] APIs { get; set; }

            public int Serialize(System.IO.Stream buffer)
            {
                int ret = 0;
                long BlockLength_position;

                /* BlockHeader, NumberOfAPIs, (API, SlotNumber a, ModuleIdentNumber, ModuleProperties,
                    NumberOfSubmodules, (SubslotNumber, SubmoduleIdentNumber, SubmoduleProperties b,
                    (DataDescription, SubmoduleDataLength, LengthIOPS, LengthIOCS)*)*)* */

                ret += EncodeBlockHeader(buffer, BlockTypes.ExpectedSubmoduleBlockReq, 0, out BlockLength_position);
                ret += DCP.EncodeU16(buffer, (ushort)APIs.Length);
                for (int i = 0; i < APIs.Length; i++)
                    ret += APIs[i].Serialize(buffer);
                ReEncodeBlockLength(buffer, BlockLength_position);

                return ret;
            }
        }

        public class PrmServerBlock : IProfinetSerialize
        {
            public int Serialize(System.IO.Stream buffer)
            {
                throw new NotImplementedException();
            }
        }

        public class MCRBlockRequest : IProfinetSerialize
        {
            public int Serialize(System.IO.Stream buffer)
            {
                throw new NotImplementedException();
            }
        }

        public class ARRPCBlockRequest : IProfinetSerialize
        {
            public int Serialize(System.IO.Stream buffer)
            {
                throw new NotImplementedException();
            }
        }

        public class IRInfoBlock : IProfinetSerialize
        {
            public int Serialize(System.IO.Stream buffer)
            {
                throw new NotImplementedException();
            }
        }

        public class SRInfoBlock : IProfinetSerialize
        {
            public int Serialize(System.IO.Stream buffer)
            {
                throw new NotImplementedException();
            }
        }

        public class ARVendorBlockRequest : IProfinetSerialize
        {
            public int Serialize(System.IO.Stream buffer)
            {
                throw new NotImplementedException();
            }
        }

        public class ARFSUBlock : IProfinetSerialize
        {
            public int Serialize(System.IO.Stream buffer)
            {
                throw new NotImplementedException();
            }
        }

        public static int EncodeConnectRequest(System.IO.Stream buffer, ARBlockRequest ar_block_req, IOCRBlockRequest[] iocr_block_req, AlarmCRBlockRequest alarm_cr_block_req, ExpectedSubmoduleBlockRequest[] expected_submodule_block_req, PrmServerBlock prm_server_block, MCRBlockRequest[] mcr_block_req, ARRPCBlockRequest arrpc_block_req, IRInfoBlock ir_info_block, SRInfoBlock sr_info_block, ARVendorBlockRequest[] ar_vendor_block_request, ARFSUBlock arfsu_block)
        {
            int ret = 0;

            /* ARBlockReq, {[IOCRBlockReq*], [AlarmCRBlockReq], [ExpectedSubmoduleBlockReq*] b,
                [PrmServerBlock], [MCRBlockReq*] a, [ARRPCBlockReq], [IRInfoBlock], [SRInfoBlock],
                [ARVendorBlockReq*], [ARFSUBlock] } */

            if (ar_block_req == null) throw new ArgumentException("ar_block_req is mandatory");
            ret += ar_block_req.Serialize(buffer);
            if (iocr_block_req != null)
                foreach (IOCRBlockRequest v in iocr_block_req)
                    ret += v.Serialize(buffer);
            if (alarm_cr_block_req != null)
                ret += alarm_cr_block_req.Serialize(buffer);
            if (expected_submodule_block_req != null)
                foreach (ExpectedSubmoduleBlockRequest v in expected_submodule_block_req)
                    ret += v.Serialize(buffer);
            if (prm_server_block != null)
                ret += prm_server_block.Serialize(buffer);
            if (mcr_block_req != null)
                foreach (MCRBlockRequest v in mcr_block_req)
                    ret += v.Serialize(buffer);
            if (arrpc_block_req != null)
                ret += arrpc_block_req.Serialize(buffer);
            if (ir_info_block != null)
                ret += ir_info_block.Serialize(buffer);
            if (sr_info_block != null)
                ret += sr_info_block.Serialize(buffer);
            if (ar_vendor_block_request != null)
                foreach (ARVendorBlockRequest v in ar_vendor_block_request)
                    ret += v.Serialize(buffer);
            if (arfsu_block != null)
                ret += arfsu_block.Serialize(buffer);

            return ret;
        }

        public class IODWriteReqHeader : IProfinetSerialize
        {
            public UInt16 SeqNumber;
            public Guid ARUUID = Guid.Empty;
            public UInt32 API;
            public UInt16 SlotNumber;
            public UInt16 SubslotNumber;
            public Index Index;
            public UInt32 RecordDataLength;

            public int Serialize(System.IO.Stream buffer)
            {
                int ret = 0;
                long BlockLength_position;

                ret += EncodeBlockHeader(buffer, BlockTypes.IODWriteReqHeader, 0, out BlockLength_position);
                ret += DCP.EncodeU16(buffer, SeqNumber);
                ret += RPC.EncodeGuid(buffer, RPC.Encodings.BigEndian, ARUUID);
                ret += DCP.EncodeU32(buffer, API);
                ret += DCP.EncodeU16(buffer, SlotNumber);
                ret += DCP.EncodeU16(buffer, SubslotNumber);
                ret += DCP.EncodeU16(buffer, 0); //padding
                ret += DCP.EncodeU16(buffer, (ushort)Index);
                ret += DCP.EncodeU32(buffer, RecordDataLength);
                ret += DCP.EncodeOctets(buffer, new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });   //padding
                ReEncodeBlockLength(buffer, BlockLength_position);

                return ret;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is IODWriteReqHeader)) return false;
                IODWriteReqHeader o = (IODWriteReqHeader)obj;
                return o.Index == Index;
            }

            public override int GetHashCode()
            {
                return Index.GetHashCode();
            }
        }

        public static int EncodeWriteRequest(System.IO.Stream buffer, KeyValuePair<IODWriteReqHeader, byte[]> value)
        {
            int ret = 0;

            ret += value.Key.Serialize(buffer);
            ret += DCP.EncodeOctets(buffer, value.Value);

            return ret;
        }

        public static int EncodeWriteMultipleRequest(System.IO.Stream buffer, Guid ARUUID, IEnumerable<KeyValuePair<IODWriteReqHeader, byte[]>> values)
        {
            int ret = 0;
            ushort no = 1;
            int tmp = 0;

            //encode header
            long header_position = buffer.Position;
            IODWriteReqHeader header = new IODWriteReqHeader();
            header.ARUUID = ARUUID;
            header.Index = Index.WriteMultiple;
            ret += header.Serialize(buffer);

            foreach (KeyValuePair<IODWriteReqHeader, byte[]> v in values)
            {
                //padding
                while ((tmp % 4) != 0) tmp += DCP.EncodeU8(buffer, 0);

                if (v.Key.ARUUID == Guid.Empty) v.Key.ARUUID = ARUUID;
                v.Key.SeqNumber = no++;
                tmp += v.Key.Serialize(buffer);
                tmp += DCP.EncodeOctets(buffer, v.Value);
            }

            //re-encode header
            header.RecordDataLength = (uint)tmp;
            long current_pos = buffer.Position;
            buffer.Position = header_position;
            header.Serialize(buffer);
            buffer.Position = current_pos;

            return ret + tmp;
        }

        public static int EncodeReadRequest(System.IO.Stream buffer)
        {
            throw new NotImplementedException();
        }

        public class ControlBlockConnect : IProfinetSerialize, IProfinetDeserialize
        {
            public BlockTypes BlockType { get; set; }
            public Guid ARUUID { get; set; }
            public UInt16 SessionKey { get; set; }
            public ControlCommands ControlCommand { get; set; }
            public ControlBlockProperties ControlBlockProperties { get; set; }

            public int Serialize(System.IO.Stream buffer)
            {
                int ret = 0;
                long BlockLength_position;

                ret += EncodeBlockHeader(buffer, BlockType, 0, out BlockLength_position);
                ret += DCP.EncodeU16(buffer, 0);    //padding
                ret += RPC.EncodeGuid(buffer, RPC.Encodings.BigEndian, ARUUID);
                ret += DCP.EncodeU16(buffer, SessionKey);
                ret += DCP.EncodeU16(buffer, 0);    //padding
                ret += DCP.EncodeU16(buffer, (ushort)ControlCommand);
                ret += DCP.EncodeU16(buffer, (ushort)ControlBlockProperties);
                ReEncodeBlockLength(buffer, BlockLength_position);

                return ret;
            }

            public int Deserialize(System.IO.Stream buffer)
            {
                int ret = 0;
                ushort tmp;
                BlockTypes bt;
                Guid g;

                DecodeBlockHeader(buffer, out bt, out tmp);
                BlockType = bt;
                ret += DCP.DecodeU16(buffer, out tmp);    //padding
                ret += RPC.DecodeGuid(buffer, RPC.Encodings.BigEndian, out g);
                ARUUID = g;
                ret += DCP.DecodeU16(buffer, out tmp);
                SessionKey = tmp;
                ret += DCP.DecodeU16(buffer, out tmp);    //padding
                ret += DCP.DecodeU16(buffer, out tmp);
                ControlCommand = (ControlCommands)tmp;
                ret += DCP.DecodeU16(buffer, out tmp);
                ControlBlockProperties = (ControlBlockProperties)tmp;

                return ret;
            }
        }

        public static int EncodeControlRequest(System.IO.Stream buffer, ControlBlockConnect control_block_connect)
        {
            int ret = 0;

            ret += control_block_connect.Serialize(buffer);

            return ret;
        }

        public static int EncodeControlResponse(System.IO.Stream buffer, ControlBlockConnect control_block_connect)
        {
            int ret = 0;

            ret += control_block_connect.Serialize(buffer);

            return ret;
        }

        public static int EncodeReleaseRequest(System.IO.Stream buffer, Guid aruuid, ushort session_key)
        {
            ControlBlockConnect control_block_connect = new ControlBlockConnect();
            control_block_connect.BlockType = BlockTypes.ReleaseBlockReq;
            control_block_connect.ControlCommand = ControlCommands.Release;
            control_block_connect.ARUUID = aruuid;
            control_block_connect.SessionKey = session_key;

            int ret = 0;
            ret += control_block_connect.Serialize(buffer);
            return ret;
        }

        public class PNIOStatus : IProfinetDeserialize
        {
            public ErrorCodes ErrorCode { get; set; }
            public ErrorDecodes ErrorDecode { get; set; }
            public byte ErrorCode1 { get; set; }
            public byte ErrorCode2 { get; set; }

            public enum ErrorCodes : byte
            {
                OK = 0,
                //0x01 – 0x1F Reserved —
                //0x20 – 0x3F Manufacturer specific Within the LogBookData
                //0x40 – 0x80 Reserved —
                PNIO = 0x81, //Used for all errors not covered elsewhere.
                //0x82 – 0xCE Reserved —
                RTAError = 0xCF, //Within the ERR-RTA-PDU and UDP-RTA-PDU
                //0xD0 – 0xD9 Reserved —
                AlarmAck = 0xDA, //Within the DATA-RTA-PDU and UDP-RTA-PDU
                IODConnectRes = 0xDB, //Within the CL-RPC-PDU
                IODReleaseRes = 0xDC, //Within the CL-RPC-PDU
                IOxControlRes = 0xDD, //Within the CL-RPC-PDU
                IODReadRes = 0xDE, //Within the CL-RPC-PDU only used with ErrorDecode=PNIORW
                IODWriteRes = 0xDF //Within the CL-RPC-PDU only used with ErrorDecode=PNIORW
                //0xE0 – 0xEF Reserved —
                //0xF0 – 0xFF Reserved
            }

            public enum ErrorDecodes : byte
            {
                OK = 0,
                //0x01 – 0x7F Reserved —
                PNIORW = 0x80, //Used in context with user error codes of the services Read and Write
                PNIO = 0x81, //Used in context with other services or internal e.g. RPC errors
                ManufacturerSpecific = 0x82, //Used only in context of LogBookData
                //0x83 – 0xFF Reserved
            }

            public enum ErrorCode1_PNIORW : byte
            {
                OK = 0,

                Application_read_error = 0xA0,
                Application_write_error = 0xA1,
                Application_module_failure = 0xA2,
                Application_busy = 0xA7,
                Application_version_conflict = 0xA8,
                Application_feature_not_supported = 0xA9,
                Application_User_specific_1 = 0xAA,
                Application_User_specific_2 = 0xAB,
                Application_User_specific_3 = 0xAC,
                Application_User_specific_4 = 0xAD,
                Application_User_specific_5 = 0xAE,
                Application_User_specific_6 = 0xAF,

                Access_invalid_index = 0xB0,
                Access_write_length_error = 0xB1,
                Access_invalid_slot = 0xB2,
                Access_type_conflict = 0xB3,
                Access_invalid_area = 0xB4,
                Access_state_conflict = 0xB5,
                Access_access_denied = 0xB6,
                Access_invalid_range = 0xB7,
                Access_invalid_parameter = 0xB8,
                Access_invalid_type = 0xB9,
                Access_backup = 0xBA,
                Access_User_specific_7 = 0xBB,
                Access_User_specific_8 = 0xBC,
                Access_User_specific_9 = 0xBD,
                Access_User_specific_10 = 0xBE,
                Access_User_specific_11 = 0xBF,

                Resource_read_constrain_conflict = 0xC0,
                Resource_write_constrain_conflict = 0xC1,
                Resource_busy = 0xC2,
                Resource_unavailable = 0xC3,
                Resource_User_specific_12 = 0xC8,
                Resource_User_specific_13 = 0xC9,
                Resource_User_specific_14 = 0xCA,
                Resource_User_specific_15 = 0xCB,
                Resource_User_specific_16 = 0xCC,
                Resource_User_specific_17 = 0xCD,
                Resource_User_specific_18 = 0xCE,
                Resource_User_specific_19 = 0xCF,
            }

            public enum ErrorCode1_PNIO
            {
                OK = 0,

                FaultyARBlockReq = 1,
                FaultyIOCRBlockReq = 2,
                FaultyExpectedSubmoduleBlockReq = 3,
                FaultyAlarmCRBlockReq = 4,
                FaultyPrmServerBlockReq = 5,
                FaultyMCRBlockReq = 6,
                FaultyARRPCBlockReq = 7,
                FaultyRecord = 8,
                FaultyIRInfoBlock = 9,
                FaultySRInfoBlock = 10,
                FaultyARFSUBlock = 11,
                FaultyARVendorBlockReq = 12,
                FaultyControlBlockConnect = 20,
                FaultyControlBlockPlug = 21,
                FaultyControlBlockConnectAfterConnectionEstablishment = 22,
                FaultyControlBlockPlugAfterPlugAlarm = 23,
                FaultyControlBlockPrmBegin = 24,
                FaultySubmoduleListBlock = 25,
                FaultyReleaseBlock = 40,
                FaultyARBlockRes = 50,
                FaultyIOCRBlockRes = 51,
                FaultyAlarmCRBlockRes = 52,
                FaultyModuleDiffBlock = 53,
                FaultyARRPCBlockRes = 54,
                FaultyARServerBlockRes = 55,
                FaultyARVendorBlockRes = 56,
                AlarmAckError = 60,
                CMDEV = 61,
                CMCTL = 62,
                CTLDINA = 63,
                CMRPC = 64,
                ALPMI = 65,
                ALPMR = 66,
                LMPM = 67,
                MAC = 68,
                RPC = 69,
                APMR = 70,
                APMS = 71,
                CPM = 72,
                PPM = 73,
                DCPUCS = 74,
                DCPUCR = 75,
                DCPMCS = 76,
                DCPMCR = 77,
                FSPM = 78,
                CTLSM = 100,
                CTLRDI = 101,
                CTLRDR = 102,
                CTLWRI = 103,
                CTLWRR = 104,
                CTLIO = 105,
                CTLSU = 106,
                CTLRPC = 107,
                CTLPBE = 108,
                CTLSRL = 109,
                CMSM = 200,
                CMRDR = 202,
                CMWRR = 204,
                CMIO = 205,
                CMSU = 206,
                CMINA = 208,
                CMPBE = 209,
                CMDMC = 210,
                CMSRL = 211,
                RTA_ERR_CLS_PROTOCOL = 253,
            }

            public int Deserialize(System.IO.Stream buffer)
            {
                int ret = 0;
                byte tmp;

                ret += DCP.DecodeU8(buffer, out tmp);
                ErrorCode = (ErrorCodes)tmp;
                ret += DCP.DecodeU8(buffer, out tmp);
                ErrorDecode = (ErrorDecodes)tmp;
                ret += DCP.DecodeU8(buffer, out tmp);
                ErrorCode1 = tmp;
                ret += DCP.DecodeU8(buffer, out tmp);
                ErrorCode2 = tmp;

                return ret;
            }

            public override string ToString()
            {
                string str = ErrorCode.ToString() + " - " + ErrorDecode.ToString() + " - ";

                if (ErrorDecode == ErrorDecodes.PNIORW)
                {
                    ErrorCode1_PNIORW tmp = (ErrorCode1_PNIORW)ErrorCode1;
                    str += tmp.ToString() + " - " + ErrorCode2.ToString();
                }
                else if (ErrorDecode == ErrorDecodes.PNIO)
                {
                    ErrorCode1_PNIO tmp = (ErrorCode1_PNIO)ErrorCode1;
                    str += tmp.ToString() + " - " + ErrorCode2.ToString();
                }
                else
                {
                    str += ErrorCode1.ToString() + " - " + ErrorCode2.ToString();
                }
                return str;
            }
        }
    }
}
