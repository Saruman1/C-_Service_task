namespace ShireLogService
{
    partial class ProjectInstaller
    {
        private System.ComponentModel.IContainer components = null;
        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller1;
        private System.ServiceProcess.ServiceInstaller serviceInstaller1;

        protected override void Dispose(bool disposing)
        {
            if (disposing) components?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.serviceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstaller1 = new System.ServiceProcess.ServiceInstaller();
            //
            // serviceProcessInstaller1
            //
            this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            //
            // serviceInstaller1
            //
            this.serviceInstaller1.ServiceName = "ShireLogSvc";
            this.serviceInstaller1.DisplayName = "Shire Log Service";
            this.serviceInstaller1.Description = "Template-based Windows service that writes to the ShireLog event log every 30 seconds.";
            this.serviceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Manual;
            //
            // ProjectInstaller
            //
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
                this.serviceProcessInstaller1,
                this.serviceInstaller1});
        }
    }
}
