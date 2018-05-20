using System.Collections.Generic;
using System.IO;

namespace TWRPGHelper
{
    internal class FileComparer : IComparer<FileInfo>
    {
        int IComparer<FileInfo>.Compare(FileInfo x, FileInfo y)
        {
            return y.CreationTime.CompareTo(x.CreationTime);
        }
    }
}