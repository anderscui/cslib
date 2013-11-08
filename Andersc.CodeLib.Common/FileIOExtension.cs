using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Andersc.CodeLib.Common
{
    public static class FileIOExtension
    {
        public static bool IsReadOnly(this FileInfo fileInfo)
        {
            return ((fileInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly);
        }

        public static void MakeEditable(this FileInfo fileInfo)
        {
            fileInfo.Attributes &= ~FileAttributes.ReadOnly;
        }
    }
}
