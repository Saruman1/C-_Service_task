using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;

namespace ShireLogService
{
    public partial class LogService : ServiceBase
    {
        // Shared with ShireServiceManager (it reads this log) - keep the names in sync.
        public const string LogName = "ShireLog";
        public const string SourceName = "ShireLogSource";

        private readonly Timer timer = new Timer(30000);
        private EventLog log;

        public LogService()
        {
            InitializeComponent();
            timer.Elapsed += (s, e) => log.WriteEntry("Shire Log Service is running.");
        }

        protected override void OnStart(string[] args)
        {
            // Runs as LocalSystem, so it has the admin rights CreateEventSource needs.
            if (!EventLog.SourceExists(SourceName))
                EventLog.CreateEventSource(SourceName, LogName);

            log = new EventLog(LogName) { Source = SourceName };
            log.WriteEntry("Shire Log Service started.");
            timer.Start();
        }

        protected override void OnStop()
        {
            timer.Stop();
            log.WriteEntry("Shire Log Service stopped.");
        }
    }
}
