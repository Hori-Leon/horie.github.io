const Client = require('ssh2-sftp-client');

class Core_SFTP {
  constructor() {
    bIsConnected = false;
    ClientHdr = new Client();
  }

  async Connect(CONNECTIONINFO) {
    console.log(`Connecting to ${CONNECTIONINFO.Host}:${CONNECTIONINFO.Port}`);
    try {
      await ClientHdr.connect(CONNECTIONINFO);
      bIsConnected = true;
    } catch (err) {
        console.error('ERR(E00) : Connection failure', err);
        bIsConnected = false;
        return false;
    }
  }

  async Disconnect() {
    await ClientHdr.end();
  }

  async Upload(FROMFILE, TOFILE) {
    console.log(`Uploading ${FROMFILE} to ${TOFILE} ...`);
    try {
      await SFTPClient.put(FROMFILE, TOFILE);
    } catch (err) {
        console.error('ERR(E01) : Cannot upload this file', err);
        return false;
    }

    return true;
  }

  async Download(FROMFILE, TOFILE) {
    console.log(`Downloading ${FROMFILE} to ${TOFILE} ...`);
    try {
      await ClientHdr.get(FROMFILE, TOFILE);
    } catch (err) {
      console.error('ERR(E03) : Cannot download this file', err);
      return false;
    }
  }

  async Delete(FROMFILE) {
    console.log(`Deleting ${FROMFILE}`);
    try {
      await ClientHdr.delete(FROMFILE);
    } catch (err) {
      console.error('ERR(E04) : Cannot delete this file', err);
      return false;
    }
    return true;
  }

  async GetFileList(DIR) {
    console.log(`Get a list of files from ${DIR} ...`);

    let List;

    try {
        List = await this.client.list(DIR);
    } catch (err) {
        console.error('ERR(E05) : Cannot get a list of this directory', err);
        return null;
    }
    return List;
  }

}


