---
# Feel free to add content and custom Front Matter to this file.
# To modify the layout, see https://jekyllrb.com/docs/themes/#overriding-theme-defaults

layout: default
title:  TWAIN
parent: Libraries
has_children: false
---

```CSharp
   static class TWAIN
    {
        // TWAIN Properties
        public static string SavePath = "C:\\TWAINPATH";
        public static event EventHandler EvtGetScannedImage;
        static TwainSession TwainSess;
        static IntPtr WindowHandle;
        public static bool InitializeTwain(nint W_Handle)
        {
            WindowHandle = W_Handle;
            // 최신 DSM을 사용하지 않습니다. 호환성의 문제로 이 프로그램은 32비트에서만 돌아갑니다. 빌드도 32비트로 해야합니다.
            NTwain.PlatformInfo.Current.PreferNewDSM = false;
            // TWAIN Session을 만듭니다.
            var appId = TWIdentity.CreateFromAssembly(DataGroups.Image | DataGroups.Audio, Assembly.GetEntryAssembly());
            TwainSess = new TwainSession(appId);
            TwainSess.TransferError += TwainSess_TransferError;
            TwainSess.TransferReady += TwainSess_TransferReady;
            TwainSess.DataTransferred += TwainSess_DataTransferred;
            TwainSess.Open();
            return true;
        }
        public static List<ModelDataSource> GetTwainList()
        {
            List<ModelDataSource> Out = new List<ModelDataSource>();
            foreach (var T_DS in TwainSess)
                Out.Add(new ModelDataSource(T_DS));
            return Out;
        }
        public static void StartScan(ModelDataSource _Model)
        {
            if (_Model != null)
                _Model.Enable(WindowHandle);
        }
        static void TwainSess_TransferError(object sender, TransferErrorEventArgs e)
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (e.Exception != null)
                    Task.Run(() => MessageBox.Show("Transfer Error Exception"));
                else
                    Task.Run(() => MessageBox.Show(string.Format("Return Code: {0}\nCondition Code: {1}", e.ReturnCode, e.SourceStatus.ConditionCode)));
            }));
        }
        static void TwainSess_TransferReady(object sender, TransferReadyEventArgs e)
        {
            // 지금 장비는 TWAIN에서 어떤 전송 방식을 사용하고 있나요?
            var Mech = TwainSess.CurrentSource.Capabilities.ICapXferMech.GetCurrent();
            // 테스트했던 흑백 프린터는 전송방식을 Native로만 받습니다.
            // Native를 제외한 다른 코드들은 지금 테스트중인 프린터 드라이버에서 지원하질 않아서 확인할 수가 없습니다.
            switch (Mech)
            {
                case XferMech.Native:
                    // Native는 별도의 처리가 필요하지 않지만, 필요하다면 코드를 추가하시길 바랍니다.
                    break;
                case XferMech.File:
                    if (!Directory.Exists(SavePath))
                        Directory.CreateDirectory(SavePath);
                    var SupportFormat = TwainSess.CurrentSource.Capabilities.ICapImageFileFormat.GetValues();
                    var ScanFormat = SupportFormat.Contains(FileFormat.Tiff) ? FileFormat.Tiff : FileFormat.Bmp;
                    var Fsetup = new TWSetupFileXfer
                    {
                        Format = ScanFormat,
                        FileName = SavePath + "Scan" + DateTime.Now.ToShortTimeString() + ScanFormat
                    };
                    TwainSess.CurrentSource.DGControl.SetupFileXfer.Set(Fsetup);
                    break;
                case XferMech.MemFile:
                    // Obsoleted
                    break;
                case XferMech.Memory:
                    // Obsoleted
                    //TWSetupMemXfer Meminfo;
                    //TwainSess.CurrentSource.DGControl.SetupMemXfer.Get(out Meminfo);
                    break;
            }
        }
        static void TwainSess_DataTransferred(object sender, DataTransferredEventArgs e)
        {
            ImageSource TransferredImage = null;
            // TWAIN으로 데이터를 어떤 형식으로 받았나요?
            // Native를 제외한 다른 코드들은 지금 테스트중인 프린터 드라이버에서 지원하질 않아서 확인할 수가 없습니다.
            switch (e.TransferType)
            {
                case XferMech.Native:
                    using (var stream = e.GetNativeImageStream())
                    {
                        if (stream != null)
                            TransferredImage = stream.ConvertToWpfBitmap(300, 0);
                    }
                    break;
                case XferMech.File:
                    //Process.Start(e.FileDataPath);
                    TransferredImage = new BitmapImage(new Uri(e.FileDataPath));
                    if (TransferredImage.CanFreeze)
                        TransferredImage.Freeze();
                    break;
                case XferMech.MemFile:
                    // Obsoleted
                    byte[] ImageBytes = e.MemoryData;
                    break;
                case XferMech.Memory:
                    // Obsoleted
                    break;
            }
            if (TransferredImage != null)
                EvtGetScannedImage.Invoke(TransferredImage, new EventArgs());
        }
    
    }
  ```


{: .highlight }
```CSharp
    [ObservableObject]
    partial class ModelDataSource
    {
        private DataSource DS;
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private string version;
        [ObservableProperty]
        private string protocol;
        public ModelDataSource(DataSource _DS)
        {
            DS = _DS;
            Name = DS.Name;
            Version = DS.Version.Info;
            Protocol = DS.ProtocolVersion.ToString();
        }
        public void Open()
            => DS.Open();
        public void Close()
            => DS.Close();
        public void Enable(IntPtr W_Handle)
            => DS.Enable(SourceEnableMode.NoUI, false, W_Handle);
    }
```