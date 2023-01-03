using GameServer.Controller.FFA.Interface;
using GameServer.Core.Enums;
using GameServer.Database;
using GameServer.Entities.SPlayer;
using GameServer.Interfaces;
using GameServer.Module.FFA.Model;
using GameServer.Module.Player;
using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Controller.FFA
{
    public class FFAController : ISingleton, IFFAController, IResource
    {
        public static List<FFAModel> models = new List<FFAModel>();

        private readonly Logger _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly Context _context;
        private readonly BanController _banController;
        private readonly Events _events;
        public FFAController(IServiceProvider services, Logger logger, Context context, BanController banController, Events events)
        {
            _logger = logger;
            _serviceProvider = services;
            _context = context;
            _banController = banController;
            _events = events;

            _events.OnPlayerDeath += OnPlayerDeath;
        }

        public async Task OnStart()
        {
            models.Add(
                new FFAModel(1, FFATypes.Würfelpark, new List<Entities.SPlayer.SPlayer>(), 10)
                );
        }

        public async Task OnStop() { }
        

        public async Task OnPlayerDeath(SPlayer player, SPlayer killer, uint reason)
        {
            if (player == null || !player.Exists) return;
            if (!player.PlayerData.Login || !player.PlayerData.FFA) return;
            if (player.Dead) return;

            try
            {
                player.PlayerData.Dead = true;
                player.Spawn(player.Position);
                player.StopAnimation();
                _logger.LogError("Death Event");
                await player.SetFreezed(true);
                await player.PlayAnimation("missarmenian2", "corpse_search_exit_ped", 1, -1);

                NAPI.Task.Run(async () =>
                {
                    await player.SetPosition(Enums.WürfelparkPositions[new Random().Next(0, Enums.WürfelparkPositions.Count)]);
                    await player.SetHealth(100);
                    await player.SetArmour(100);
                    await player.SetFreezed(false);
                    player.PlayerData.Dead = false;

                    NAPI.Task.Run(() => player.StopAnimation());
                }, 3000);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async void JoinFFA(SPlayer player, int ffaId)
        {
            if (player == null || !player.Exists || !player.PlayerData.Login) return;

            if (player.PlayerData.FFA) return;

            var ffaData = models.FirstOrDefault(x => x.Id == ffaId);

            try
            {
                if (ffaData == null)
                {
                    _logger.LogError("FFA Arena is null! Id: " + ffaId);
                }
                else
                {

                    if (ffaData.Players.Count > ffaData.MaxPlayers)
                    {
                        player.SendChatMessage("!{RED}MAX PLAYERS");
                        return;
                    }

                    ffaData.Players.Add(player);
                    _logger.LogMessage("FFA Arena is not null! Id: " + ffaId);
                    await player.SetPosition(Enums.WürfelparkPositions[new Random().Next(0, Enums.WürfelparkPositions.Count)]);
                    await player.SetHealth(100);
                    await player.SetArmour(100);
                    await player.SetDimension((uint)6811 + (uint)ffaData.Id);
                    await player.SetFreezed(false);

                    NAPI.Task.Run(() => player.SendChatMessage("!{GREEN}Entered FFA"));
                    player.PlayerData.FFA = !player.PlayerData.FFA;

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }


        public void LeaveFFA(SPlayer player)
        {

        }
    }
}
