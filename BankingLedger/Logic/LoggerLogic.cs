using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;

namespace BankingLedger.Logic
{
    public class LoggerLogic
    {
        public static ILog CreateLogger()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());

            XmlConfigurator.Configure(logRepository, new FileInfo("C:\\GitHub\\BankingLedger\\BankingLedger\\log4net.config"));

            return LogManager.GetLogger(typeof(Program));
        }
    }
}
