using System.Collections.Generic;
using Renci.SshNet.Sftp;

namespace Project.Service.Citi
{
    public interface ISftpRemoteClient
    {
        List<string> ListFilesFromServer(string remoteDirectory);
        List<SftpFile> ListFilesFromRemoteServer(string remoteDirectory);
        void DownloadRemoteFile(string remoteFilePath, string localDirectory);
        void DownloadRemoteFileToDirectory(SftpFile file, string directory);
        void DownloadRemoteDirectory(string source, string destination, bool recursive = false);
        void DeleteFileFromServer(string pathRemoteFileToDelete);
        void UploadFileToRemoteServer(string localFilePath, string remoteFilePath);
        bool CheckIfRemoteFileExists(string remoteFolderName, string remoteFileName);
    }
}