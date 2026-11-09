using System.ServiceProcess;

namespace ShireLogService
{
    static class Program
    {
        static void Main() => ServiceBase.Run(new ServiceBase[] { new LogService() });
    }
}
