using System;
namespace Andersc.CodeLib.DoubanDownloader
{
    public interface IConfigManager
    {
        string AlbumDownloadTips { get; }
        string LastSelectedPath { get; set; }
        void Refresh();
        void Save();
    }
}
