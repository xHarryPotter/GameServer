using GameServer.Controller.FFA;
using GameServer.Controller.FFA.Interface;
using GameServer.Database;
using GameServer.Entities.SPlayer;
using GameServer.Interfaces;
using GameServer.Module.FFA.Model;
using GameServer.Module.Player;
using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module.FFA
{
    public class FFAModule : ISingleton
    {
        private readonly Logger _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly Context _context;
        private readonly BanController _banController;
        private readonly Events _events;
        private readonly FFAController _ffaController;
      
        public FFAModule(IServiceProvider services, Logger logger, Context context, BanController banController, Events events, FFAController ffAController)
        {
            _logger = logger;
            _serviceProvider = services;
            _context = context;
            _banController = banController;
            _events = events;
            _ffaController = ffAController;
        }

        [Command("ffa")]
        public async void FFA(SPlayer player, int ffaId)
        {
            if (player == null || !player.Exists) return;

            _ffaController.JoinFFA(player, ffaId);
        }
    }
}
