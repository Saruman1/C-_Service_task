using System.ServiceProcess;

namespace ShireManualService
{
    static class Program
    {
        static void Main() => ServiceBase.Run(new ManualService());
    }
}
