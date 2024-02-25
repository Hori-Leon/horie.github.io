using System.Runtime.InteropServices;
using System.Text;

namespace Calibrator_Core
{
    class InterfaceUCS30
    {
        #region DLL Functions

        [DllImport(@".\USBControlLibrary.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void usxCloseUSXUCS30(int handle);

        [DllImport(@".\USBControlLibrary.dll", CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern void usxFindUSXUSBDevicesString([Out] byte[] str);

        [DllImport(@".\USBControlLibrary.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern void usxFindUSXUCS30sString([Out] byte[] str);

        [DllImport(@".\USBControlLibrary.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int usxGetNewUCS30SN(int device_path);

        [DllImport(@".\USBControlLibrary.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int usxGetPID();

        [DllImport(@".\USBControlLibrary.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int usxIsMCAConnected(int handle, int sn, int product_id);

        [DllImport(@".\USBControlLibrary.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int usxIsPIDActive(int process_id);

        [DllImport(@".\USBControlLibrary.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int usxIsUSXConnected(int handle, int sn, int product_id);

        [DllImport(@".\USBControlLibrary.dll", CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int usxOpenUSXUSBString([In] byte[] str);

        [DllImport(@".\USBControlLibrary.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int usxOpenUSXUCS30String([In] byte[] str);

        [DllImport(@".\USBControlLibrary.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int usxReadMCA(int handle, int size);

        [DllImport(@".\USBControlLibrary.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern void usxReadMCAString(int handle, [Out] byte[] str, int size);

        [DllImport(@".\USBControlLibrary.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int usxReadMCA_1All_Channels_Bytes(int handle, [Out] byte[] str, int size);

        [DllImport(@".\USBControlLibrary.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int usxReadMCA_1Channels(int handle, int size);

        [DllImport(@".\USBControlLibrary.dll", CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern void usxReadUSXUSBString(int handle, [Out] byte[] str);

        [DllImport(@".\USBControlLibrary.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int usxUpdateUCS30List();

        [DllImport(@".\USBControlLibrary.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int usxWriteMCAString(int handle, [In] byte[] str);

        [DllImport(@".\USBControlLibrary.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int usxWriteUSXUSB(int handle, int command);

        [DllImport(@".\HIDLibrary.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ClearHIDLogFile();
        [DllImport(@".\HIDLibrary.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetHIDDevices([Out] byte[] numDevs, [Out] byte[] Dev0, [Out] byte[] Dev1, [Out] byte[] Dev2, [Out] byte[] Dev3, [Out] byte[] Dev4, [Out] byte[] Dev5);

        [DllImport(@".\HIDLibrary.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool InitInstanceHID([In] byte[] inSN, [Out] byte[] outSN);

        [DllImport(@".\HIDLibrary.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte UnInitInstanceHID();

        [DllImport(@".\HIDLibrary.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SendQueryToUCS([In] byte[] Cmd, [Out] byte[] Resp, [Out] long[] Resp2);

        [DllImport(@".\HIDLibrary.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SendCommandToUCS([In] byte[] Cmd);

        #endregion

        #region Instruction Set

        public bool SetAcquire(int TargetDeviceNum, bool Enable)
        {
            if (Enable)
            {
                Command(TargetDeviceNum, UCS30Commands.ACQUIRE + " 1\n");
                IsAcq[TargetDeviceNum] = true;
            }
            else
            {
                Command(TargetDeviceNum, UCS30Commands.ACQUIRE + " 0\n");
                IsAcq[TargetDeviceNum] = false;
            }

            return true;
        }

        public bool SetVolt(int TargetDeviceNum, bool Enable)
            => Enable ? Command(TargetDeviceNum, UCS30Commands.VOLTAGE + " 1\n") : Command(TargetDeviceNum, UCS30Commands.VOLTAGE + " 0\n");
        public bool SetVoltValue(int TargetDeviceNum, int Volt)
            => Command(TargetDeviceNum, UCS30Commands.HIGH_VOLTAGE + " " + (int)(Volt / 2));
        public bool SetVoltFineValue(int TargetDeviceNum, int Volt)
    => Command(TargetDeviceNum, UCS30Commands.HIGH_VOLTAGE_FINE + " " + (Volt));
        public bool SetConversionGain(int TargetDeviceNum, int Value)
            => Command(TargetDeviceNum, UCS30Commands.CONVERSION_GAIN + " " + Value);
        public bool SetCoarseGain(int TargetDeviceNum, int Value)
            => Command(TargetDeviceNum, UCS30Commands.COARSE_GAIN + " " + Value);
        public bool SetFineGain(int TargetDeviceNum, double Value)
            => Command(TargetDeviceNum, UCS30Commands.FINE_GAIN + " " + Value);
        public bool SetULDR(int TargetDeviceNum, double Value)
            => Command(TargetDeviceNum, UCS30Commands.UPPER_LEVEL_DISCRIMINATOR + " " + Value);
        public bool SetLLDR(int TargetDeviceNum, double Value)
            => Command(TargetDeviceNum, UCS30Commands.LOWER_LEVEL_DISCRIMINATOR + " " + Value);
        public bool SetPresetRealtime(int TargetDeviceNum, int Value)
            => Command(TargetDeviceNum, UCS30Commands.PRESET_REAL_TIME + " " + Value);
        public bool SetPresetLivetime(int TargetDeviceNum, int Value)
          => Command(TargetDeviceNum, UCS30Commands.PRESET_LIVE_TIME + " " + Value);
        public bool SetPresetIntegral(int TargetDeviceNum, int ROI, long Value)
            => Command(TargetDeviceNum, UCS30Commands.PRESET_INTEGRAL + " " + ROI + "," + Value);
        public bool SetPresetIntegral(int TargetDeviceNum, int Start, int End, int Value)
            => Command(TargetDeviceNum, UCS30Commands.SET_REGION_OF_INTEREST + " " + Start + "," + End + "," + Value);
        public bool SetAcqMode(int TargetDeviceNum, int Value)
           => Command(TargetDeviceNum, UCS30Commands.ACQUISITION_MODE + " " + Value);
        public bool SetDwelltime(int TargetDeviceNum, int Value)
         => Command(TargetDeviceNum, UCS30Commands.SET_DWELL_TIME + " " + Value);
        public bool SetPresetPasses(int TargetDeviceNum, int Value)
            => Command(TargetDeviceNum, UCS30Commands.PRESET_PASSES + " " + Value);
        public bool SetElapsedPasses(int TargetDeviceNum, int Value)
          => Command(TargetDeviceNum, UCS30Commands.ELAPSED_PASSES + " " + Value);
        public bool SetOutVoltPolarity(int TargetDeviceNum, int Value)
        => Command(TargetDeviceNum, UCS30Commands.VOLTAGE_POLARITY + " " + Value);
        public bool SetInVoltPolarity(int TargetDeviceNum, int Value)
            => Command(TargetDeviceNum, UCS30Commands.INPUT_VOLTAGE_POLARITY + " " + Value);
        public bool SetPeakTimeShaping(int TargetDeviceNum, int Value)
          => Command(TargetDeviceNum, UCS30Commands.PEAK_TIME_SHAPING + " " + Value);
        public bool SetROIWidth(int TargetDeviceNum, int Value)
             => Command(TargetDeviceNum, UCS30Commands.ROI_WIDTH + " " + Value);
        public bool SetROIAmp(int TargetDeviceNum, int Value)
          => Command(TargetDeviceNum, UCS30Commands.ROI_AMPLITUDE + " " + Value);
        public bool SetTempStabilizeMode(int TargetDeviceNum, bool Enable)
           => Enable ? Command(TargetDeviceNum, UCS30Commands.STABILIZE_MODE + " 1\n") : Command(TargetDeviceNum, UCS30Commands.STABILIZE_MODE + " 0\n");
        public bool SetStabilizeCoefficient(int TargetDeviceNum, double Value)
        {
            if (Value < 0 || Value > 255) return false;

            int Coef = (int)(Value * 10);
            if (Coef < 0) Coef += 255;

            return Command(TargetDeviceNum, UCS30Commands.STABILIZE_COEFFICIENT + " " + Coef);
        }

        public int GetVoltValue(int TargetDeviceNum)
        {
            int Modifier = 2;
            string Result = Query(TargetDeviceNum, UCS30Commands.HIGH_VOLTAGE + "?");

            return int.Parse(Result) * Modifier;
        }
        public int GetVoltFineValue(int TargetDeviceNum)
        {
            string Result = Query(TargetDeviceNum, UCS30Commands.HIGH_VOLTAGE_FINE + "?");

            return int.Parse(Result);
        }
        public int GetConversionGain(int TargetDeviceNum)
        {
            string Result = Query(TargetDeviceNum, UCS30Commands.CONVERSION_GAIN + "?");

            return int.Parse(ParseValue(Result));
        }
        public string GetParameter(int TargetDeviceNum)
    => Query(TargetDeviceNum, UCS30Commands.GET_CARD_PARAMETERS + "?");
        public double GetLivetime(int TargetDeviceNum)
        {
            string Result = Query(TargetDeviceNum, UCS30Commands.ELAPSED_LIVE_TIME + "?");

            return double.Parse(ParseValue(Result.Replace(",", ".")));
        }
        public double GetRealtime(int TargetDeviceNum)
        {
            string Result = Query(TargetDeviceNum, UCS30Commands.ELAPSED_REAL_TIME + "?");

            return double.Parse(ParseValue(Result.Replace(",", ".")));
        }
        public double GetPresetRealtime(int TargetDeviceNum)
        {
            string Result = Query(TargetDeviceNum, UCS30Commands.PRESET_REAL_TIME + "?");

            return double.Parse(ParseValue(Result.Replace(",", ".")));
        }
        public double GetPresetLivetime(int TargetDeviceNum)
        {
            string Result = Query(TargetDeviceNum, UCS30Commands.PRESET_LIVE_TIME + "?");

            return double.Parse(ParseValue(Result.Replace(",", ".")));
        }
        public int GetDwelltime(int TargetDeviceNum)
        {
            string Result = Query(TargetDeviceNum, UCS30Commands.SET_DWELL_TIME + "?");
            return int.Parse(ParseValue(Result.Replace(",", ".")));
        }
        public int GetCoarseGain(int TargetDeviceNum)
        {
            string Result = Query(TargetDeviceNum, UCS30Commands.COARSE_GAIN + "?");

            return int.Parse(ParseValue(Result));
        }
        public int GetFineGain(int TargetDeviceNum)
        {
            string Result = Query(TargetDeviceNum, UCS30Commands.FINE_GAIN + "?");
            return int.Parse(ParseValue(Result.Replace(",", ".")));
        }
        public int GetULDR(int TargetDeviceNum)
        {
            string Result = Query(TargetDeviceNum, UCS30Commands.UPPER_LEVEL_DISCRIMINATOR + "?");
            return int.Parse(ParseValue(Result.Replace(",", ".")));
        }
        public int GetLLDR(int TargetDeviceNum)
        {
            string Result = Query(TargetDeviceNum, UCS30Commands.LOWER_LEVEL_DISCRIMINATOR + "?");
            return int.Parse(ParseValue(Result.Replace(",", ".")));
        }
        public int GetPresetIntegral(int TargetDeviceNum)
        {
            string Result = Query(TargetDeviceNum, UCS30Commands.PRESET_INTEGRAL + "?");
            return int.Parse(ParseValue(Result.Replace(",", ".")));
        }
        public int GetAcqMode(int TargetDeviceNum)
        {
            string Result = Query(TargetDeviceNum, UCS30Commands.ACQUISITION_MODE + "?");
            return int.Parse(ParseValue(Result.Replace(",", ".")));
        }
        public int GetTemperature(int TargetDeviceNum)
        {
            string Result = Query(TargetDeviceNum, UCS30Commands.GET_TEMPERATURE + "?");
            return int.Parse(ParseValue(Result.Replace(",", ".")));
        }
        public int GetPresetPasses(int TargetDeviceNum)
        {
            string Result = Query(TargetDeviceNum, UCS30Commands.PRESET_PASSES + "?");
            return int.Parse(ParseValue(Result.Replace(",", ".")));
        }
        public int GetElapsedPasses(int TargetDeviceNum)
        {
            string Result = Query(TargetDeviceNum, UCS30Commands.ELAPSED_PASSES + "?");
            return int.Parse(ParseValue(Result.Replace(",", ".")));
        }
        public int GetOutVoltPolarity(int TargetDeviceNum)
        {
            string Result = Query(TargetDeviceNum, UCS30Commands.VOLTAGE_POLARITY + "?");
            return int.Parse(ParseValue(Result.Replace(",", ".")));
        }
        public int GetInVoltPolarity(int TargetDeviceNum)
        {
            string Result = Query(TargetDeviceNum, UCS30Commands.INPUT_VOLTAGE_POLARITY + "?");
            return int.Parse(ParseValue(Result.Replace(",", ".")));
        }
        public int GetPeakTimeShaping(int TargetDeviceNum)
        {
            string Result = Query(TargetDeviceNum, UCS30Commands.PEAK_TIME_SHAPING + "?");
            return int.Parse(ParseValue(Result.Replace(",", ".")));
        }
        public int GetROIWidth(int TargetDeviceNum)
        {
            string Result = Query(TargetDeviceNum, UCS30Commands.ROI_WIDTH + "?");
            return int.Parse(ParseValue(Result.Replace(",", ".")));
        }
        public int GetROIAmp(int TargetDeviceNum)
        {
            string Result = Query(TargetDeviceNum, UCS30Commands.ROI_AMPLITUDE + "?");
            return int.Parse(ParseValue(Result.Replace(",", ".")));
        }
        public double GetStabilizeFactor(int TargetDeviceNum)
        {
            string Result = Query(TargetDeviceNum, UCS30Commands.STABILIZE_FACTOR + "?");

            return double.Parse(ParseValue(Result.Replace(",", "."))) / 100;
        }
        public int GetStabilizeCoefficient(int TargetDeviceNum)
        {
            string Result = Query(TargetDeviceNum, UCS30Commands.STABILIZE_COEFFICIENT + "?");

            int Value_T = int.Parse(ParseValue(Result.Replace(",", ".")));

            if (Value_T > Byte.MaxValue) Value_T -= 255;

            return Value_T / 10;
        }

        /*
        public bool IsAcquire(int TargetDeviceNum)
        {
            string Result = Query(TargetDeviceNum, UCS30Commands.ACQUIRE + "?");
            string[] SP = Result.Split(new string[] { },StringSplitOptions.RemoveEmptyEntries);

            if (int.Parse(SP[1]) == 1)
                return true;
            else
                return false;  
        }
        */
        public bool IsAcquire(int TargetDeviceNum) => IsAcq[TargetDeviceNum];

        public bool IsVoltOn(int TargetDeviceNum)
        {
            string Result = Query(TargetDeviceNum, UCS30Commands.VOLTAGE + "?");

            if (int.Parse(Result) == 1)
                return true;
            else
                return false;
        }
        public bool IsTempStabilizeOn(int TargetDeviceNum)
        {
            string Result = Query(TargetDeviceNum, UCS30Commands.STABILIZE_MODE + "?");

            if (int.Parse(Result) == 1)
                return true;
            else
                return false;
        }


        public bool ResetData(int TargetDeviceNum)
        {
            bool Result = true;

            Result = Result && Command(TargetDeviceNum, UCS30Commands.ERASE_CHANNELS);
            Result = Result && Command(TargetDeviceNum, UCS30Commands.ELAPSED_LIVE_TIME + " 0");
            Result = Result && Command(TargetDeviceNum, UCS30Commands.ELAPSED_REAL_TIME + " 0");
            Result = Result && Command(TargetDeviceNum, UCS30Commands.ELAPSED_INTEGRAL + " 0");

            return Result;
        }


        public double[] ReadChannel(int TargetDeviceNum, int StartCnl, int EndCnl)
        {
            double[] SpectrumCnl;

            bool bReturn_1 = new bool();
            byte[] inBytes = new byte[MAX_PATH];
            byte[] outBytes;

            int CnlCount;

            StringBuilder Sb = new StringBuilder();

            UCS30DeviceInfo TargetDevice = DeviceList[TargetDeviceNum];
            CnlCount = EndCnl - StartCnl + 1;
            SpectrumCnl = new double[CnlCount];

            if (!TargetDevice.IsDeviceOccupied)
                TargetDevice.IsDeviceOccupied = true;
            else
                return null;

            Sb.Append("RDCH " + StartCnl + "," + EndCnl);
            inBytes = StringBuilderToBytes(Sb);

            int Size = CnlCount * 4;

            bReturn_1 = Convert.ToBoolean(usxWriteMCAString(TargetDevice.Handle, inBytes));

            if (bReturn_1)
            {
                outBytes = new byte[CnlCount * 4];
                usxReadMCA_1All_Channels_Bytes(TargetDevice.Handle, outBytes, Size);

                for (int i = 0; i < CnlCount; i++)
                {
                    SpectrumCnl[i] = (double)DecodeChannelData(outBytes[i * 4 + 2], outBytes[i * 4 + 1], outBytes[i * 4]);
                    //SpectrumCnl[1,i] = UnsignedByteToInt(outBytes[i * 4 + 3]);

                }
            }

            TargetDevice.IsDeviceOccupied = false;

            return SpectrumCnl;
        }

        #endregion

        #region Basic Method
        public List<UCS30DeviceInfo> GetDeviceList() => DeviceList;

        public void ClearAll(int SelectedUCS30Num)
        {
            PrevLive = 0;
            PrevReal = 0;

            RealtimeBuffer.Clear();
            LivetimeBuffer.Clear();

            ResetData(SelectedUCS30Num);
        }

        public double GetDeadtime(double Live, double Real)
        {
            double Deadtime;

            double LValue = Live - PrevLive;
            double RValue = Real - PrevReal;

            if (LValue < 0) LValue = 0;
            if (RValue < 0) RValue = 0;

            PrevLive = Live;
            PrevReal = Real;

            if (LivetimeBuffer.Count() > DeadtimeBufferLength || RealtimeBuffer.Count() > DeadtimeBufferLength)
            {
                LivetimeBuffer.RemoveAt(0);
                RealtimeBuffer.RemoveAt(0);
            }

            LivetimeBuffer.Add(LValue);
            RealtimeBuffer.Add(RValue);

            if (RealtimeBuffer.Sum() > 0)
                Deadtime = (1 - LivetimeBuffer.Sum() / RealtimeBuffer.Sum()) * 100;
            else
                Deadtime = 0;
            return Deadtime;
        }

        private String Query(int TargetDeviceNum, string Cmd)
        {
            string rString = "";

            bool bReturn_1 = new bool();

            byte[] inBytes = new byte[MAX_PATH];
            byte[] outBytes = new byte[MAX_PATH];
            StringBuilder Sb = new StringBuilder();

            UCS30DeviceInfo TargetDevice = DeviceList[TargetDeviceNum];

            if (!TargetDevice.IsDeviceOccupied)
                TargetDevice.IsDeviceOccupied = true;
            else
                return "Query failed : Target Device is occupied.";

            Sb.Append(Cmd);
            inBytes = StringBuilderToBytes(Sb);

            try
            {
                bReturn_1 = Convert.ToBoolean(usxWriteMCAString(TargetDevice.Handle, inBytes));

                if (bReturn_1)
                {
                    usxReadMCAString(TargetDevice.Handle, outBytes, 64);
                    rString = BytesToString(outBytes);
                }
            }
            catch
            {
                return "Query failed : USX Read fail.";
            }

            Thread.Sleep(10);
            TargetDevice.IsDeviceOccupied = false;

            return rString;
        }

        private bool Command(int TargetDeviceNum, string Cmd)
        {
            bool bReturn_1 = new bool();
            bool bReturn_2 = new bool();

            byte[] inBytes = new byte[MAX_PATH];
            byte[] outBytes = new byte[MAX_PATH];
            StringBuilder Sb = new StringBuilder();

            UCS30DeviceInfo TargetDevice = DeviceList[TargetDeviceNum];

            if (!TargetDevice.IsDeviceOccupied)
                TargetDevice.IsDeviceOccupied = true;
            else
                return false;


            Sb.Append(Cmd);
            inBytes = StringBuilderToBytes(Sb);

            try
            {
                bReturn_1 = Convert.ToBoolean(usxWriteMCAString(TargetDevice.Handle, inBytes));

                if (bReturn_1)
                {
                    usxReadMCAString(TargetDevice.Handle, outBytes, 1);
                    bReturn_2 = true;
                }
            }
            catch
            {
                bReturn_2 = false;
            }

            Thread.Sleep(10);
            TargetDevice.IsDeviceOccupied = false;

            return bReturn_2;
        }

        private static long DecodeChannelData(Byte High, Byte Mid, Byte Low)
        {
            long Value = 0;
            Value += UnsignedByteToInt(Low);
            Value += UnsignedByteToInt(Mid) << 8;
            Value += UnsignedByteToInt(High) << 16;
            return Value;
        }

        private static int UnsignedByteToInt(byte b)
            => (int)b & 0xff;

        private byte[] StringBuilderToBytes(StringBuilder sb)
        {
            int ii = 0, nLen = sb.Length;
            byte[] byteRet = new byte[MAX_PATH];
            while (sb[ii] > 0)
            {
                byteRet[ii] = (byte)sb[ii];
                if (ii == nLen - 1)
                    break;
                ii++;
            }
            byteRet[ii + 1] = 0;
            return byteRet;
        }

        private string BytesToString(byte[] source)
        {
            int ii = 0;
            StringBuilder sb = new StringBuilder();
            char[] cArray = Encoding.ASCII.GetString(source).ToCharArray();
            while (ii < MAX_PATH && cArray[ii] > 0)
            {
                sb.Append(cArray[ii++]);
            }
            return sb.ToString();

        }

        private string ParseValue(string Result)
        {
            if (Result.EndsWith("\n"))
                return Result.Substring(5, Result.Length - 6);
            else
                return Result.Substring(5, Result.Length - 5);
        }
        #endregion

        private List<UCS30DeviceInfo> DeviceList;
        private const int MAX_PATH = 260;

        private double PrevLive;
        private double PrevReal;
        private const short DeadtimeBufferLength = 5;
        private List<double> RealtimeBuffer = new List<double>();
        private List<double> LivetimeBuffer = new List<double>();
        private List<bool> IsAcq = new List<bool>();

        public bool bIsDeviceReady;

        public InterfaceUCS30()
        {
            DeviceList = new List<UCS30DeviceInfo>();


            // Find Device...
            string DeviceCode;
            string[] StringSplit_1, StringSplit_2;

            byte[] inBytes = new byte[MAX_PATH];
            byte[] outBytes = new byte[MAX_PATH];

            usxFindUSXUCS30sString(outBytes);
            DeviceCode = BytesToString(outBytes);

            StringSplit_1 = DeviceCode.Split(':');

            for (int i = 0; i < StringSplit_1.Count(); i++)
            {
                StringSplit_2 = StringSplit_1[i].Split(';');

                if (StringSplit_2.Length != 2) break;
                else
                    DeviceList.Add(new UCS30DeviceInfo(StringSplit_2[0], Convert.ToInt32(StringSplit_2[1])));
            }
            // "Find Device... - " + "Found " + DeviceList.Count() + " Device(s)"


            if (DeviceList.Count() == 0) // Device connection failure. : No device found
                return;

            //Connect Device...
            StringBuilder Sb = new StringBuilder();

            for (int i = 0; i < DeviceList.Count(); i++)
            {
                int Handle;
                inBytes = new byte[MAX_PATH];
                outBytes = new byte[MAX_PATH];

                inBytes = StringBuilderToBytes(Sb.Append(DeviceList[i].ConnectionID));
                Handle = usxOpenUSXUCS30String(inBytes);

                if (Handle > 0)
                {
                    DeviceList[i].Handle = Handle;
                    DeviceList[i].IsConnected = true;

                    // Check Hex code is already uploaded
                    int Value;
                    if (int.TryParse(Query(i, UCS30Commands.IN_RAM + "?"), out Value))
                        continue;
                    else
                    {
                        //Upload Hex code
                        bool bIsHexLoaded = true;
                        float HexCnt = 0;

                        using (StreamReader srHex = new StreamReader(@".\UCS30v2.hex"))
                        {
                            string strLine;

                            while (true)
                            {
                                strLine = srHex.ReadLine();
                                if (strLine == null)
                                    break;
                                else
                                {
                                    string strLength = "0x" + strLine.Substring(1, 2);
                                    strLength = Convert.ToInt32(strLength, 16).ToString();

                                    string strAddress = "0x" + strLine.Substring(3, 4);
                                    strAddress = Convert.ToInt32(strAddress, 16).ToString();

                                    string strData = strLine.Substring(9, strLine.Length - 11);
                                    if (strData == "") break;

                                    HexCnt += strLine.Count();

                                    bIsHexLoaded = bIsHexLoaded && Command(i, UCS30Commands.LOAD_OP_CODE + " " + strLength + "," + strAddress + "\n" + strData);
                                    //"Connect Device... - Uploading Hex codes (" + HexCnt + ")"

                                    Thread.Sleep(1);
                                }


                            }

                            IsAcq.Add(false);

                        }

                        bool bIsRamUpdated = Command(i, UCS30Commands.JUMP_TO_RAM);

                        if (bIsRamUpdated)
                        {
                            // Ready.
                            bIsDeviceReady = true;

                        }
                        else
                        {
                            //Device connection failure. : Ram is not updated
                            bIsDeviceReady = false;
                        }


                    }
                }
            }
        }

        private static class UCS30Commands
        {

            public static String ADC_POINTER = "ADCB";
            public static String ACQUIRE = "ACQU";
            public static String PRESET_REAL_TIME = "PSRT";
            public static String ELAPSED_REAL_TIME = "ELRT";
            public static String PRESET_LIVE_TIME = "PSLT";
            public static String ELAPSED_LIVE_TIME = "ELLT";
            public static String PRESET_INTEGRAL = "PINT";
            public static String ELAPSED_INTEGRAL = "EINT";
            public static String PRESET_PASSES = "PPAS";
            public static String ELAPSED_PASSES = "EPAS";
            public static String CONVERSION_GAIN = "CVGN";
            public static String COARSE_GAIN = "CSGN";
            public static String FINE_GAIN = "FNGN";
            public static String HIGH_VOLTAGE = "DETV";
            public static String HIGH_VOLTAGE_FINE = "DETF";
            public static String VOLTAGE = "VOLT";
            public static String UPPER_LEVEL_DISCRIMINATOR = "ULDR";
            public static String LOWER_LEVEL_DISCRIMINATOR = "LLDR";
            public static String LOAD_OP_CODE = "LDOC";
            public static String SET_DATA_BLOCK = "SBLK";   // ??
            public static String GET_DATA_BLOCK = "GBLK";   // ??
            public static String READ_OPTIONAL_PANEL = "GOPT";   // ??
            public static String ACQUISITION_MODE = "MODE";
            public static String READ_CHANNELS = "RDCH";
            public static String SET_CHANNELS = "STCH";
            public static String SET_REGION_OF_INTEREST = "SROI";
            public static String SET_DWELL_TIME = "DWLL";
            public static String ERASE_CHANNELS = "ERAS";
            public static String GET_MCS_CHANNEL = "MCSC";
            public static String GET_CARD_PARAMETERS = "PARM";
            public static String JUMP_TO_RAM = "JRAM";
            public static String IN_RAM = "IRAM";

            //  Tempature Stabilization Commands
            public static String GET_TEMPERATURE = "GTMP";
            public static String STABILIZE_MODE = "STAB";
            public static String STABILIZE_COEFFICIENT = "SCOE";
            public static String STABILIZE_FACTOR = "SFAC";

            public static String VOLTAGE_POLARITY = "VPOL";
            public static String VOLTAGE_NEGATIVE = "VNEG"; // query to determine whether UCS35 supports negative voltage polarity
            public static String INPUT_VOLTAGE_POLARITY = "IPOL";
            public static String PEAK_TIME_SHAPING = "SHAP";
            public static String ROI_WIDTH = "ROIW";
            public static String ROI_AMPLITUDE = "ROIA";

        }
    }

    public class UCS30DeviceInfo
    {
        public string ConnectionID { get; set; }
        public int Handle { get; set; }
        public int SerialNum { get; set; }
        public bool IsConnected { get; set; }
        public bool IsDeviceOccupied { get; set; }


        public UCS30DeviceInfo(string _ConnectionID, int _SerialNum)
        {
            SerialNum = _SerialNum;
            ConnectionID = _ConnectionID;
        }
    }
}
