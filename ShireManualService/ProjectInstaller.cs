using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace ShireManualService
{
    // Picked up by installutil.exe and by the setup project's custom actions.
    [RunInstaller(true)]
    public class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            Installers.Add(new ServiceProcessInstaller { Account = ServiceAccount.LocalSystem });
            Installers.Add(new ServiceInstaller
            {
                ServiceName = ManualService.Name,
                DisplayName = "Shire Manual Service",
                Description = "Hand-written Windows service created from an empty project (Shire lab, task 1).",
                StartType = ServiceStartMode.Manual
            });
        }
    }
}
