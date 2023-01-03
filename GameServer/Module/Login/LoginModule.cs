using GameServer.Controller.Login;
using GameServer.Core.Enums;
using GameServer.Data.Player;
using GameServer.Database;
using GameServer.Database.Models;
using GameServer.Entities.SPlayer;
using GameServer.Interfaces;
using GameServer.Module.Faction;
using GTANetworkAPI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module.Login
{
    public class LoginModule : ISingleton
    {
        private readonly Logger _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly LoginController _loginController;
        private readonly Context _context;
        private readonly FactionModule _factionModule;
 
        public LoginModule(IServiceProvider services, Logger logger, LoginController loginController, Context context, FactionModule factionModule)
        {
            _logger = logger;
            _serviceProvider = services;
            _loginController = loginController;
            _context = context;
            _factionModule = factionModule;


            NAPI.ClientEvent.Register<SPlayer, string>("Server:Login", this, Login);
        }

        public async void Login(SPlayer player, string username)
        {
            if (player == null || !player.Exists) return;
            if (string.IsNullOrEmpty(username)) return;
            if (player.PlayerData.LoginPressed) return;
            
            player.PlayerData.LoginPressed = true;

            var response = await _loginController.CheckLogin(player, username);
            if (response == null) return;

            if (response.loginResponse == LoginResponse.Success)
            {
                if (response.account == null) return;

                player.Init(response.account.Id, response.account.username);
                player.TriggerEventSafe("Client:LOginSuccess");
                player.SendTranslatedChatMessage("Du hast dich erfolgreich eingeloggt!", "", "Bameninghong", StatusTypes.Success);
                _factionModule.InitFactionsIngame(player);
                player.SetPosition(new Vector4(-425.6301, 1123.3588, 325.85425, 150), 160);
            }


            if (response.loginResponse == LoginResponse.Error)
            {
                player.PlayerData.LoginPressed = false;
            }
        
        
            if (response.loginResponse == LoginResponse.Banned)
            {           
                if (response.banData== null) return;

                player.SendTranslatedChatMessage("Du bist gebannt! Grund: " + response.banData.banreason + " bis zum " + response.banData.expires.ToLongDateString() + 
                    " " + 
                    response.banData.expires.ToLongTimeString(),
                    "",
                    "Bameninghong",
                    StatusTypes.Error);

                player.PlayerData.LoginPressed = false;
            }

          

            if (response.loginResponse == LoginResponse.AlreadyLogin)
            {          
                player.SendTranslatedChatMessage("Du bist bereits eingeloggt!", "Already logged in!", "Bameninghong", StatusTypes.Error);
            }
           
            if (response.loginResponse == LoginResponse.CreateAccount)
            {
                await _context.Accounts.AddAsync(new Database.Models.Account()
                {
                    address = player.Address,
                    Serial = player.Serial,
                    socialClubId = (int)player.SocialClubId,
                    socialClubName = player.SocialClubName,
                    username = username,
                });

                await _context.SaveChangesAsync();
                player.SendTranslatedChatMessage("Du hast dir erfolgreich einen Account erstellt! Logge dich nun ein", "null", "Bameninghong", StatusTypes.Success);
                player.PlayerData.LoginPressed = false;
            }
        }
    }
}
