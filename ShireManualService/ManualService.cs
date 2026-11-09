using System.ServiceProcess;

namespace ShireManualService
{
    // Written by hand from an empty project (task step 1): no designer, no template code.
    public class ManualService : ServiceBase
    {
        public const string Name = "ShireManualSvc";

        public ManualService()
        {
            ServiceName = Name;
        }
    }
}
