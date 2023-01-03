using GameServer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer.Services
{

    public sealed class PlayerService : IHostedService
    {
        private readonly Logger _logger;
        private readonly IConfiguration _config;
        private readonly IServiceProvider _serviceProvider;
        public PlayerService(IServiceProvider services, Logger logger)
        {
            _logger = logger;
            _serviceProvider = services;
            _config = services.GetRequiredService<IConfiguration>();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            foreach (var service in _serviceProvider.GetServices<IResource>())
            {
                await service.OnStart();
            }

            Console.WriteLine("");
            foreach (var singleton in _serviceProvider.GetServices<ISingleton>())
            {           
                _logger.LogDebug("Successfully loaded " + singleton.ToString().Replace("GameServer.", "") + ".cs");
            }
            Console.WriteLine("");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInfo("Stopped service...");

            foreach (var service in _serviceProvider.GetServices<IResource>())
            {
                await service.OnStop();
            }
        }
    }
}
