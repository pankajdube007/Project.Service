using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace Project.Service.Citi
{
    public class SftpRemoteClient : ISftpRemoteClient
    {
        #region Ctor

        public SftpRemoteClient(string host, string port, string username, string password, string keyFilePath)
        {
            _host = host;
            _port = Convert.ToInt32(port);
            _username = username;
            _password = password;

            var keyFile = new PrivateKeyFile(keyFilePath, _password);
            _keyFiles = new[] {keyFile};
        }

        #endregion

        #region Fields & Variables

        private readonly PrivateKeyFile[] _keyFiles;
        private readonly string _username;
        private readonly string _password;
        private readonly string _host;
        private readonly int _port;

        #endregion

        #region Utilities

        private SftpClient ConnectToServer()
        {
            var methods = new List<AuthenticationMethod>
            {
                new PasswordAuthenticationMethod(_username, _password),
                new PrivateKeyAuthenticationMethod(_username, _keyFiles)
            };

            var con = new ConnectionInfo(_host, _port, _username, methods.ToArray());

            var sftp = new SftpClient(con)
            {
                KeepAliveInterval = TimeSpan.FromSeconds(60),
                ConnectionInfo = {Timeout = TimeSpan.FromMinutes(180)},
                OperationTimeout = TimeSpan.FromMinutes(180)
            };
            sftp.Connect();
            return sftp;
        }

        private static void DisconnectFromServer(SftpClient sftp)
        {
            if (sftp.IsConnected) sftp.Disconnect();
            sftp.Dispose();
        }

        #endregion


        #region Implementation

        public List<string> ListFilesFromServer(string remoteDirectory)
        {
            var listFileList = new List<string>();
            if (string.IsNullOrWhiteSpace(remoteDirectory)) remoteDirectory = ".";

            var files = ListFilesFromRemoteServer(remoteDirectory);
            listFileList.AddRange(files.Select(file => file.Name));

            return listFileList;
        }

        public List<SftpFile> ListFilesFromRemoteServer(string remoteDirectory)
        {
            var files = new List<SftpFile>();
            if (string.IsNullOrWhiteSpace(remoteDirectory)) remoteDirectory = ".";

            try
            {
                var sftp = ConnectToServer();

                files = sftp.ListDirectory(remoteDirectory)?.OrderByDescending(s => s.LastWriteTime).ToList();

                DisconnectFromServer(sftp);
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception has been caught " + e);
                throw;
            }

            return files;
        }

        public void DownloadRemoteFile(string remoteFilePath, string localDirectory)
        {
            if (string.IsNullOrWhiteSpace(localDirectory))
                localDirectory = "~/App_Data";

            try
            {
                var sftp = ConnectToServer();

                var fileName = remoteFilePath.Split('/').LastOrDefault();
                fileName = string.IsNullOrWhiteSpace(fileName)
                    ? $"Unknown_{DateTime.Now.Millisecond}.txt"
                    : $"{fileName}.txt";

                var localFilePath = $"{localDirectory}/{fileName}";
                using (Stream fileStream =
                    File.Create(localFilePath))
                {
                    sftp.DownloadFile(remoteFilePath, fileStream);
                }

                DisconnectFromServer(sftp);
            }
            catch (Exception er)
            {
                Console.WriteLine("An exception has been caught " + er);
                throw;
            }
        }

        public void DownloadRemoteDirectory(string source, string destination, bool recursive = false)
        {
            // List the files and folders of the directory
            var files = ListFilesFromRemoteServer(source);

            // Iterate over them
            foreach (var file in files)
                // If is a file, download it
                if (!file.IsDirectory && !file.IsSymbolicLink)
                {
                    DownloadRemoteFileToDirectory(file, destination);
                }
                // If it's a symbolic link, ignore it
                else if (file.IsSymbolicLink)
                {
                    Console.WriteLine("Symbolic link ignored: {0}", file.FullName);
                }
                // If its a directory, create it locally (and ignore the .. and .=) 
                //. is the current folder
                //.. is the folder above the current folder -the folder that contains the current folder.
                else if (file.Name != "." && file.Name != "..")
                {
                    var dir = Directory.CreateDirectory(Path.Combine(destination, file.Name));
                    // and start downloading it's content recursively :) in case it's required
                    if (recursive) DownloadRemoteDirectory(file.FullName, dir.FullName);
                }
        }

        public void DownloadRemoteFileToDirectory(SftpFile file, string directory)
        {
            Console.WriteLine("Downloading {0}", file.FullName);
            try
            {
                var sftp = ConnectToServer();

                var fileName = file.Name;

                fileName = string.IsNullOrWhiteSpace(fileName)
                    ? $"Unknown_{DateTime.Now.Millisecond}.txt"
                    : $"{fileName}.txt";

                //if ((!file.Name.StartsWith(".")) && ((file.LastWriteTime.Date == DateTime.Today))
                using (Stream fileStream = File.Create(HttpContext.Current.Server.MapPath($"{directory}/{fileName}")))
                {
                    sftp.DownloadFile(file.FullName, fileStream);
                }

                DisconnectFromServer(sftp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void DeleteFileFromServer(string pathRemoteFileToDelete)
        {
            // Path to folder on SFTP server
            if (string.IsNullOrWhiteSpace(pathRemoteFileToDelete))
                pathRemoteFileToDelete = "/var/www/vhosts/folder/somefile.txt";

            try
            {
                var sftp = ConnectToServer();

                // Delete file
                sftp.DeleteFile(pathRemoteFileToDelete);

                DisconnectFromServer(sftp);
            }
            catch (Exception er)
            {
                Console.WriteLine("An exception has been caught " + er);
            }
        }

        public void MoveFolderToArchive(string ftpPathSrcFolder, string ftpPathDestFolder)
        {
            var sftp = ConnectToServer();
            var eachRemoteFile =
                sftp.ListDirectory(ftpPathSrcFolder).First(); //Get first file in the Directory            
            if (eachRemoteFile.IsRegularFile) //Move only file
            {
                var eachFileExistsInArchive = CheckIfRemoteFileExists(sftp, ftpPathDestFolder, eachRemoteFile.Name);

                //MoveTo will result in error if filename alredy exists in the target folder. Prevent that error by cheking if File name exists
                var eachFileNameInArchive = eachRemoteFile.Name;

                if (eachFileExistsInArchive)
                    eachFileNameInArchive =
                        eachFileNameInArchive + "_" +
                        DateTime.Now.ToString("MMddyyyy_HHmmss"); //Change file name if the file already exists
                eachRemoteFile.MoveTo(ftpPathDestFolder + eachFileNameInArchive);
            }

            DisconnectFromServer(sftp);
        }

        public bool CheckIfRemoteFileExists(SftpClient sftpClient, string remoteFolderName, string remoteFileName)
        {
            var isFileExists = sftpClient
                .ListDirectory(remoteFolderName)
                .Any(
                    f => f.IsRegularFile &&
                         string.Equals(f.Name, remoteFileName, StringComparison.CurrentCultureIgnoreCase)
                );
            return isFileExists;
        }

        public bool CheckIfRemoteFileExists(string remoteFolderName, string remoteFileName)
        {
            var isFileExists = false;
            try
            {
                var sftp = ConnectToServer();

                isFileExists = sftp
                    .ListDirectory(remoteFolderName)
                    .Any(
                        f => f.IsRegularFile &&
                             string.Equals(f.Name, remoteFileName, StringComparison.CurrentCultureIgnoreCase)
                    );


                DisconnectFromServer(sftp);
            }
            catch (Exception er)
            {
                Console.WriteLine("An exception has been caught " + er);
            }

            return isFileExists;
        }

        public void UploadFileToRemoteServer(string localFilePath, string remoteFilePath)
        {
            try
            {
                var sftp = ConnectToServer();

                var fileName = remoteFilePath.Split('/').Last();

                using (Stream localFile = File.OpenRead(localFilePath))
                {
                    sftp.UploadFile(localFile, remoteFilePath);
                }

                DisconnectFromServer(sftp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public List<RemoteFile> RemoteDirectories(SftpClient sftp, string path, string parentDirectory = "")
        {
            var files = new List<RemoteFile>();
            foreach (var file in sftp.ListDirectory(path))
            {
                if (file.Name == "." || file.Name == "..") continue;
                var remoteFile = new RemoteFile(file.Name, file.FullName, file.Length, parentDirectory,
                    file.IsDirectory, file.LastWriteTime, file.LastWriteTimeUtc);

                files.Add(remoteFile);
                if (!file.IsDirectory) continue;
                var path2 = path + "/" + file.Name;
                RemoteDirectories(sftp, path2, parentDirectory + "/" + file.Name);
            }

            return files;
        }

        #endregion
    }
}