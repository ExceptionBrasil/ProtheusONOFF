using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace ProtheusONOFF
{
    class Program
    {
        static void Main(string[] args)
        {
            NameValueCollection services = ConfigurationManager.AppSettings;


            Console.WriteLine("Escolha uma opção");
            Console.WriteLine("----------------------------");
            Console.WriteLine("[1] Inicializar Serviços ");
            Console.WriteLine("[2] Parar Serviços ");
            Console.WriteLine("[3] Lista de Serviços ");
            Console.Write(">> ");
            var key = Console.ReadKey();

            if (key.KeyChar == '1')
            {
                Parallel.ForEach(services.AllKeys, (service) =>
                {
                    Console.WriteLine($"Iniciando ... {service}");
                    ServiceController sc = new ServiceController();

                    sc.ServiceName = services[service].ToString();
                    Console.WriteLine($"Status Atual ... {Enum.GetName(typeof(ServiceControllerStatus),sc.Status)}");

                    if (sc.Status == ServiceControllerStatus.Stopped)
                        sc.Start();

                  
                });
            }

            if (key.KeyChar == '2')
            {
                Parallel.ForEach(services.AllKeys, (service) =>
                {

                    Console.WriteLine($"Iniciando ... {service}");
                    ServiceController sc = new ServiceController();

                    sc.ServiceName = services[service].ToString();
                    Console.WriteLine($"Status Atual ... {Enum.GetName(typeof(ServiceControllerStatus), sc.Status)}");

                    if (sc.Status == ServiceControllerStatus.Running)
                        sc.Stop();
                });
            }

            if (key.KeyChar == '3')
            {
                Console.WriteLine();
                Parallel.ForEach(services.AllKeys, (service) =>
                {
                    ServiceController sc = new ServiceController();
                    sc.ServiceName = services[service].ToString();
                    Console.WriteLine($"Serviço: {services[service].ToString()} Status: {Enum.GetName(typeof(ServiceControllerStatus), sc.Status)}");
                });
            }

            Console.ReadKey();

        }
    }
}
