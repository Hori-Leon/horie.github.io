const Core_MariaDB = require('./Core_MariaDB');
const Core_SFTP = require('./Core_SFTP');
const NodeCmd = require('node-cmd');

const Client_MariaDB = new Core_MariaDB();
const Client_SFTP = new Core_SFTP();


// Callbacks
CallbackLoginAttept = function (ID, Pwd) {
    Client_MariaDB.Login(ID, Pwd);
}

CallbackUploadDocument = function (FILEPATH, FileInfoObj) {
    if (Client_SFTP.bIsConnected === false) return false;
    if (Client_MariaDB.bIsConnected === false) return false;

    // Obtain filename.
    let PathSplit = FILEPATH.split("/");
    let Filename = PathSplit[PathSplit.length - 1];

    // Upload file in isolation area.
    Client_SFTP.Upload(FILEPATH, String.concat("./Isolation/", Filename));

    // CLI Command for Malware detection
    let CommandLine = String.concat("cd C:\\program files\\windows defender&&mpcmdrun.exe -Scan -ScanType 3 -File ", Filename);

    const PM = new Promise((resolve, reject) => {
        nodeCmd.run('CommandLine', (err, data, stderr) => {
            if (err) {
                reject(err);
            } else {
                resolve(data, stderr);

                let ResultSplit = data.split("\n", " ");
                let Result = String.concat(
                    ResultSplit[ResultSplit.length - 3], ResultSplit[ResultSplit.length - 2], ResultSplit[ResultSplit.length - 1]);

                // Is it safe?
                if (Result === "found no threats") {
                    // How many files are in this directory?
                    Row = Client_MariaDB.SendQuery(
                        String.concat(
                            "SELECT COUNT(*) FROM EDMS.", FileInfoObj.Dept
                            , " WHERE "
                            , "Dept = ", FileInfoObj.Dept, " AND "
                            , "MainSort = ", FileInfoObj.MainSort, " AND "
                            , "SubSort = ", FileInfoObj.SubSort
                            )
                    ,null)

                    FileNum = parseInt(Row) + 1;

                    // Upload it

                    Path = String.concat(
                        "/", FileInfoObj.Dept,
                        "/", FileInfoObj.MainSort,
                        "/", FileInfoObj.SubSort
                    );

                    UploadName = String.concat(
                        "회사이름"
                        , "-", FileInfoObj.MainSort
                        , "-", FileInfoObj.SubSort
                        , "-", typeof (FileNum));

                    Client_SFTP.Upload(
                        String.concat("./Isolation/", Filename),
                        String.concat( Path, "/", UploadName)
                    );

                    // assign its info on DB
                    Client_MariaDB.SendQuery(
                        String.concat(
                            "INSERT INTO EDMS.", FileInfoObj.Dept
                            ," VALUE(Name, OriginName, Date, MainSort, SubSort, Path, Verified)"
                    )
                    , [UploadName, Filename, Date.now(), FileInfoObj.MainSort, FileInfoObj.SubSort, Path, FILESTATUS.AwaitVerfi]
                    ); 
                }
            }
        });
    });

}


// Model classes
class INFOCOLLECTION {
    constructor() {
        this.Items = [];
    }
    Add(Model) {
        if (Array.isArray(this.Items)) {
            this.Items.push(Model);
        }
    }
    RemoveAt(Idx) {
        if (Array.isArray(this.Items)) {
            this.Items = this.Items.filter(i => i.Idx !== Idx);
        }
    }
}

class FILEINFOMODEL {
    constructor(Name, OriginName, Date, Dept, MainSort, SubSort, Path, Comment) {
        this.Name = Name;
        this.OriginName = OriginName;
        this.Date = Date;
        this.Dept = Dept;
        this.MainSort = MainSort;
        this.SubSort = SubSort;
        this.Path = Path;
        this.Verified = FILESTATUS.NotVerified;
        this.Comment = Comment;
    }
}

class COMMONINFOMODEL {
    constructor() {
        this.OriginName = "";
        this.Dept = "";
        this.MainSort = "";
        this.SubSort = "";
    }
}


// Enums
const FILESTATUS = {
    AwaitVerfi: 0,
    VirusDetected: 1,
    Verified: 2,
    Dismissed: 3,
    Deleted: 4
}
