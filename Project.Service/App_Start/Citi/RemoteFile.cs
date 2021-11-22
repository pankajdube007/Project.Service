using System;

namespace Project.Service.Citi
{
    public class RemoteFile
    {
        public RemoteFile(string fileName, string fileFullName, long fileLength, string parentDirectory,
            bool fileIsDirectory, DateTime fileLastWriteTime, DateTime fileLastWriteTimeUtc)
        {
            FileName = fileName;
            FileFullName = fileFullName;
            FileLength = fileLength;
            ParentDirectory = parentDirectory;
            FileIsDirectory = fileIsDirectory;
            FileLastWriteTime = fileLastWriteTime;
            FileLastWriteTimeUtc = fileLastWriteTimeUtc;
        }

        public string FileName { get; }
        public string FileFullName { get; }
        public long FileLength { get; }
        public string ParentDirectory { get; }
        public bool FileIsDirectory { get; }
        public DateTime FileLastWriteTime { get; }
        public DateTime FileLastWriteTimeUtc { get; }
    }
}