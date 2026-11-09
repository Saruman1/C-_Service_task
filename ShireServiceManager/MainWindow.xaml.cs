using System;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Windows;

namespace ShireServiceManager
{
    public partial class MainWindow : Window
    {
        // Must match ShireLogService (ServiceName in its designer, LogName in LogService).
        private const string ServiceName = "ShireLogSvc";
        private const string LogName = "ShireLog";

        private readonly ServiceController service = new ServiceController(ServiceName);

        public MainWindow()
        {
            InitializeComponent();
            UpdateStatus();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e) =>
            ChangeState(service.Start, ServiceControllerStatus.Running);

        private void StopButton_Click(object sender, RoutedEventArgs e) =>
            ChangeState(service.Stop, ServiceControllerStatus.Stopped);

        private void ChangeState(Action action, ServiceControllerStatus target)
        {
            try
            {
                action();
                // shortcut: blocks the UI thread for up to 30 s; move to Task.Run if that ever bites.
                service.WaitForStatus(target, TimeSpan.FromSeconds(30));
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is System.ServiceProcess.TimeoutException)
            {
                MessageBox.Show(ex.Message, "Shire Service Manager", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            try
            {
                service.Refresh();
                var status = service.Status;
                StatusLabel.Content = status;
                StartButton.IsEnabled = status == ServiceControllerStatus.Stopped;
                StopButton.IsEnabled = status == ServiceControllerStatus.Running;
            }
            catch (InvalidOperationException)
            {
                StatusLabel.Content = "Not installed";
                StartButton.IsEnabled = StopButton.IsEnabled = false;
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            if (!EventLog.Exists(LogName))
            {
                LogBox.Text = $"Event log '{LogName}' does not exist yet - start the service once.";
                return;
            }
            using (var log = new EventLog(LogName))
            {
                LogBox.Text = string.Join(Environment.NewLine, log.Entries.Cast<EventLogEntry>()
                    .Select(entry => $"{entry.TimeGenerated:G}  {entry.EntryType,-12} {entry.Message}"));
            }
            LogBox.ScrollToEnd();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e) => LogBox.Clear();
    }
}
