const MariaDB = require('mariadb');

const Host = "HOSTDB";
const ConnectionLimit = 5;

class Core_MariaDB {
    constructor() {
        ClientHdr = new MariaDB();
        bIsConnected = false;
        Pool;
        Conn;
    }

    async Login(ID, Pwd) {
        Pool = ClientHdr.createPool(Host, ID, Pwd, ConnectionLimit);

        try {
            Conn = await Pool.getConnection();
            bIsConnected = true;
        }
        catch (err) {
            console.error('ERR(E06) : MariaDB connection failure', err);
            bIsConnected = false;
            return false;
        }

        return true;
    }

    async SendQuery(QueryStr, Param) {
        let Rows;

        if (Param != null)
            Rows = await Conn.query(QueryStr, Param);
        else
            Rows = await Conn.query(QueryStr);
        
        return Rows;
    }

    async Logout() {
        Conn.end();
    }
}
