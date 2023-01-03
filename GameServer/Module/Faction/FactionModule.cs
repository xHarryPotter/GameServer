using GameServer.Database;
using GameServer.Entities.SPlayer;
using GameServer.Interfaces;
using GameServer.Module.Faction.Models;
using GTANetworkAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module.Faction
{
    public class FactionModule : ISingleton, IResource
    {
        private readonly Logger _logger;
        private readonly Context _context;
        private readonly Events _events;
        public static List<FactionModel> _factions = new List<FactionModel>();

        public FactionModule(Logger logger, Context context, Events events, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _context = context;
            _events = events;
         
            NAPI.ClientEvent.Register<SPlayer>("Server:MainMenu:SelectStartGame", this, StartGame);
            NAPI.ClientEvent.Register<SPlayer, int>("Server:MainMenu:SelectTeam", this, SelectTeam);
        }

        public async Task OnStart()
        {
            _logger.LogMessage(nameof(FactionModule));
        }


        public async Task OnStop()
        {

        }



        public void InitFactionsIngame(SPlayer player)
        {
            player.TriggerEventSafe("Client:MainMenu:Open");
        }

        public void InitClothes(SPlayer player, int factionId)
        {
            var clothes = NAPI.Util.FromJson<FactionClothesModel>(_context.Factions.FirstOrDefault(x => x.Id == factionId));
            if (clothes == null) return;

            //player.SetClothes()
        }

        //[RemoteEvent("Server:MainMenu:SelectStartGame")]
        public void StartGame(SPlayer player)
        {
            player.TriggerEventSafe("Client:MainMenu:PushTeams", NAPI.Util.ToJson(_context.Factions.Select(x => new
            {
                name = x.Name,
                id = x.Id,
                clothes = x.Clothes,
                color = x.Color
            }).ToList()), _context.Factions.Count<FactionModel>());
        }


        public async void SelectTeam(SPlayer player, int factionId)
        {
            try
            {
                var factions = _context.Factions.FirstOrDefault(x => x.Id == factionId);
                if (factions == null) return;

                await player.SetPosition(NAPI.Util.FromJson<Vector4>(factions.Position));
                var clothes = NAPI.Util.FromJson<FactionClothesModel>(factions.Clothes);

                player.SetClothes(1, clothes.MaskDrawable, clothes.MaskTexture);
                player.SetClothes(3, clothes.TorsoDrawable, clothes.TorsoTexture);
                player.SetClothes(4, clothes.LegsDrawable, clothes.LegsTexture);
                player.SetClothes(5, clothes.BagsNParachuteDrawable, clothes.BagsNParachuteTexture);
                player.SetClothes(6, clothes.ShoeDrawable, clothes.ShoeTexture);
                player.SetClothes(7, clothes.AccessiorDrawable, clothes.AccessiorTexture);
                player.SetClothes(8, clothes.UndershirtDrawable, clothes.UndershirtTexture);
                player.SetClothes(9, clothes.BodyArmorDrawable, clothes.BodyArmorTexture);
                player.SetClothes(11, clothes.TopDrawable, clothes.TopTexture);

                player.TriggerEventSafe("Client:MainMenu:Close");
                player.SetFreezed(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
            }
        }
    }
}
