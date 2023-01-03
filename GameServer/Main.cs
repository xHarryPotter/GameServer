using Autofac;
using Autofac.Core;
using GameServer.Controller.FFA;
using GameServer.Controller.Login;
using GameServer.Database;
using GameServer.Entities.SPlayer;
using GameServer.Interfaces;
using GameServer.Module;
using GameServer.Module.Player;
using GameServer.Services;
using GTANetworkAPI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer
{
    public class Main : Script
    {
        private readonly IConfiguration _config;
        private readonly IServiceProvider _serviceProvider;
        private readonly IContainer _container;
       

        public Main()
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;

      

            var services = new ServiceCollection();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .AddEnvironmentVariables();

            _config = builder.Build();

            InitializeServices(services);

            this.RegisterAssemblies<IResource>(services, false);
            this.RegisterAssemblies<ISingleton>(services, true);

            services.AddHostedService<PlayerService>();

            _serviceProvider = services.BuildServiceProvider();
       }


        private void InitializeServices(IServiceCollection services)
        {
            services
                .AddSingleton(_config)
                .AddSingleton<Logger>()
                .AddSingleton<LoginController>()
                .AddSingleton<LoginController>()
                .AddSingleton<FFAController>()
                .AddSingleton<BanController>()
                .AddSingleton<Context>();

        }


        [ServerEvent(Event.ResourceStart)]
        public async void OnResourceStart()
        {
            foreach (var service in _serviceProvider.GetServices<IHostedService>())
            {
                await service.StartAsync(CancellationToken.None);
            }
        }


        [ServerEvent(Event.ResourceStop)]
        public void OnResourceStop()
        {
            foreach (var service in _serviceProvider.GetServices<IHostedService>())
            {
                service.StopAsync(CancellationToken.None);
            }
        }

        public void RegisterAssemblies<T>(IServiceCollection services, bool singleton)
        {
            foreach (var type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
               .Where(x => typeof(T).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract))
            {
                if (singleton)
                {
                    services.AddSingleton(type);
                    services.AddSingleton(typeof(T), type);
                }
                else
                {
                    services.AddSingleton(typeof(T), type);
                    services.AddTransient(type);
                }
            }
        }



        static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;

            using (StreamWriter writer = new StreamWriter("server_logs.log", true))
            {
                writer.WriteLine(DateTime.Now);
                writer.WriteLine(ex.Message + "\n" + ex.StackTrace + "\n" + ex.ToString());
                writer.WriteLine();
            }
        }
    }
}


