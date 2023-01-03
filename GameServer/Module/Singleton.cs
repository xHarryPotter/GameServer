using GameServer.Interfaces;
using Microsoft.Extensions.Configuration;
using System;

namespace GameServer.Module
{
    public class Singleton : ISingleton
    {       
        private readonly Logger _logger;
        private readonly IServiceProvider _serviceProvider;

        public Singleton(IServiceProvider services, Logger logger)
        {
            _logger = logger;
            _serviceProvider = services;
        }
    }
}
