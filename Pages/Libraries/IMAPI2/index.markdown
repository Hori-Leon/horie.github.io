---
# Feel free to add content and custom Front Matter to this file.
# To modify the layout, see https://jekyllrb.com/docs/themes/#overriding-theme-defaults

layout: default
title:  IMAPI2
parent: Libraries
has_children: false
---
'''CSharp
    static class IMAPI
    {
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = false, EntryPoint = "SHCreateStreamOnFileW")]
        internal static extern void SHCreateStreamOnFile(string fileName, uint mode, ref IMAPI2FS.IStream stream);
        internal const uint STGM_SHARE_DENY_WRITE = 0x00000020;
        internal const uint STGM_SHARE_DENY_NONE = 0x00000040;
        internal const uint STGM_READ = 0x00000000;
        private const string ClientName = "ClientName";
        public static List<MsftDiscRecorder2> FindAvailableDrive()
        {
            List<MsftDiscRecorder2> Out = new List<MsftDiscRecorder2>();
            MsftDiscMaster2 Dmaster = new MsftDiscMaster2();
            if (!Dmaster.IsSupportedEnvironment)
                return null;
            foreach (string UniqueId in Dmaster)
            {
                MsftDiscRecorder2 Recorder = new MsftDiscRecorder2();
                Recorder.InitializeDiscRecorder(UniqueId);
                Out.Add(Recorder);
            }
            Marshal.ReleaseComObject(Dmaster);
            return Out;
        }
        public static string ValidateSupportedMedia(MsftDiscRecorder2 _Recorder)
        {
            string ResultString = "";
            MsftDiscFormat2Data FormatData = new MsftDiscFormat2Data();
            if (!FormatData.IsCurrentMediaSupported(_Recorder))
            {
                ResultString = "This media is not supported";
            }
            else
            {
                FormatData.Recorder = _Recorder;
                var MediaType = FormatData.CurrentPhysicalMediaType;
                ResultString = GetMediaTypeString((IMAPI2FS.IMAPI_MEDIA_PHYSICAL_TYPE)MediaType);
                MsftFileSystemImage FsImage = new MsftFileSystemImage();
                FsImage.ChooseImageDefaultsForMediaType((IMAPI2FS.IMAPI_MEDIA_PHYSICAL_TYPE)MediaType);
            }
            return ResultString;
        }
        public static void FormatWrite(MsftDiscRecorder2 _Recorder, List<IMediaItem> _BurnFileInfo)
        {
            MsftDiscFormat2Data FormatData = null;
            IMAPI2FS.IDiscRecorder2 Drecorder = (IMAPI2FS.IDiscRecorder2)_Recorder;
            FormatData = new MsftDiscFormat2Data
            {
                Recorder = _Recorder,
                ClientName = ClientName,
                ForceMediaToBeClosed = true,
                ForceOverwrite = true,
            };
            // Verification
            ((IBurnVerification)FormatData).BurnVerificationLevel = IMAPI_BURN_VERIFICATION_LEVEL.IMAPI_BURN_VERIFICATION_FULL;
            // Create media file system
            MsftFileSystemImage Fsi = new MsftFileSystemImage();
            Fsi.ChooseImageDefaults(Drecorder);
            Fsi.FileSystemsToCreate = FsiFileSystems.FsiFileSystemJoliet | FsiFileSystems.FsiFileSystemISO9660;
            IFsiDirectoryItem RootItem = Fsi.Root;
            foreach (var Finfo in _BurnFileInfo)
            {
                if(Finfo.GetType() == typeof(ModelBurnFileInfo))
                {
                    IMAPI2FS.IStream stream = null;
                    SHCreateStreamOnFile(Finfo.Path, STGM_READ | STGM_SHARE_DENY_WRITE, ref stream);
                    RootItem.AddFile(Finfo.FileName, (FsiStream)stream);
                }
                else
                {
                    RootItem.AddTree(Finfo.Path, true);
                }
            }
            IMAPI2FS.IStream FsiStream = Fsi.CreateResultImage().ImageStream;
            Marshal.ReleaseComObject(Fsi);
            // Write media
            FormatData.Write((IMAPI2.IStream)FsiStream);
            _Recorder.EjectMedia();
            // Release com object
            Marshal.ReleaseComObject(FormatData);
        }
        public static void FormatErase(MsftDiscRecorder2 _Recorder, bool _IsQuickFormat)
        {
            MsftDiscFormat2Erase FormatErase = new MsftDiscFormat2Erase
            {
                Recorder = _Recorder,
                ClientName = ClientName,
                FullErase = !_IsQuickFormat
            };
            FormatErase.EraseMedia();
            Marshal.ReleaseComObject(FormatErase);
        }
        private static void FormatEraseUpdate(object @object, int elapsedSeconds, int estimatedTotalSeconds)
        {
            EvtFormatEraseUpdate.Invoke(elapsedSeconds * 100 / estimatedTotalSeconds, new EventArgs());
        }
    }
    '''