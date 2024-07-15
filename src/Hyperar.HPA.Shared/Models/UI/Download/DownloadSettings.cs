namespace Hyperar.HPA.Shared.Models.UI.Download
{
    public class DownloadSettings
    {
        public bool DownloadFullMatchArchive { get; set; }

        public bool DownloadHattrickArenaMatches { get; set; }

        public bool ReDownloadMatchEvents { get; set; }

        public bool SaveXmlFileToDisk { get; set; } = true;
    }
}