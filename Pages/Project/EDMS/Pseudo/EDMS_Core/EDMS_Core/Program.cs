using Renci.SshNet;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.IsolatedStorage;


class Program
{
  
    public static ScanResult Main(string[] args)
    {
        string FilePath = args[0];
        string[] Spl =  FilePath.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
        
        string Filename = Spl[Spl.Length - 1];

        // Initialize SFTP
       ConnectionInfo Conn_SFTP =
            new ConnectionInfo("Host IP", "USER", new PasswordAuthenticationMethod("USER", "PWD"));
        SftpClient Client_SFTP = new SftpClient(Conn_SFTP);


       // Create Isolation
       IsolatedStorageFile IsoStore
            = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null);

        if(!IsoStore.DirectoryExists("Isolated"))
            IsoStore.CreateDirectory("Isolated");


        IsolatedStorageFileStream IsoStream = new IsolatedStorageFileStream(Filename, FileMode.Create);

        // Get file from path to Isolated directory
        StreamWriter StreamW = new StreamWriter(IsoStream);
        StreamW.WriteLine(Client_SFTP.ReadAllLines(FilePath));
        StreamW.Close();

        
        // Malware Detection
        var Process_Info = new ProcessStartInfo("C:\\program files\\windows defender\\mpcmdrun.exe")
        {
            Arguments = string.Format("-Scan -ScanType 3 -File \"{0}\"", IsoStream.Name),
            CreateNoWindow = true,
            ErrorDialog = false,
            WindowStyle = ProcessWindowStyle.Hidden,
            UseShellExecute = false
        };

        var Process = new Process();

        Process.StartInfo = Process_Info;
        Process.Start();
        Process.WaitForExit(3000);

        if (!Process.HasExited)
        {
            Process.Kill();
            return ScanResult.Timeout;
        }

        // Disposal
        IsoStream.Dispose();
        IsoStore.DeleteFile(IsoStream.Name);

        // Return malware detection result
        switch (Process.ExitCode)
        {
            case 0:
                return ScanResult.NoThreatFound;
            case 2:
                return ScanResult.ThreatFound;
            default:
                return ScanResult.Error;
        }
    }


    public enum ScanResult
    {
        [Description("No threat found")]
        NoThreatFound,

        [Description("Threat found")]
        ThreatFound,

        [Description("The file could not be found")]
        FileNotFound,

        [Description("Timeout")]
        Timeout,

        [Description("Error")]
        Error
    }

}

