using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(String[] args)
        {
            if (args.Any())
            {
                if (args[0].Equals("c", StringComparison.OrdinalIgnoreCase))
                {   // Run as a console application
                    IdentityServer.Default.Start();
                    Console.WriteLine("Push any key to terminate the service...");
                    Console.Read();
                    IdentityServer.Default.Stop();
                }
                else if (args[0].Equals("i", StringComparison.OrdinalIgnoreCase))
                {   // Install service
                    SelfInstaller.InstallMe();
                }
                else if (args[0].Equals("u", StringComparison.OrdinalIgnoreCase))
                {   // Uninstall service
                    SelfInstaller.UninstallMe();
                }
                else
                {   // Wrong argument
                    Console.WriteLine("Wrong argument!");
                }
            }
            else
            {
                var servicesToRun = new ServiceBase[]
                {
                    new Service()
                };
                ServiceBase.Run(servicesToRun);
            }
        }
    }
}
