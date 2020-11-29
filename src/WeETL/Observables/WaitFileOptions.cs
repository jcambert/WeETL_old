using System.IO;

namespace WeETL.Observables
{
    public class WaitFileOptions
    {
        private int _stopOn=0;
        #region public properties
        public bool StopOnFirst { get => StopOn == 1; set { StopOn = value ? 1 : 0; } }

        public int StopOn { get => _stopOn; set { _stopOn = value; } }
        public NotifyFilters NotifyFilters { get; set; } = NotifyFilters.LastWrite;
        public string Path { get; set; } = Extensions.BaseLocation;
        public string Filter { get; set; } = "*.*";
        public bool IncludeSubDirectories { get; set; } = false;

        #endregion
    }
}
