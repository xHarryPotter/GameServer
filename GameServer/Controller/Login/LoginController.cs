using GameServer.Controller.Login.Interface;
using GameServer.Controller.Login.Model;
using GameServer.Core.Enums;
using GameServer.Data.Player;
using GameServer.Database;
using GameServer.Entities.SPlayer;
using GameServer.Interfaces;
using GameServer.Module.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Controller.Login
{
    public class LoginController : ISingleton, ILoginController
    {
        private readonly Logger _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly Context _context;
        private readonly BanController _banController;
        public LoginController(IServiceProvider services, Logger logger, Context context, BanController banController)
        {
            _logger = logger;
            _serviceProvider = services;
            _context = context;
            _banController = banController;
        }

        public async Task<LoginResponseModel> CheckLogin(SPlayer player, string username)
        {
            if (player == null || !player.Exists) return await Task.FromResult(new LoginResponseModel(loginResponse: LoginResponse.Error));
            if (string.IsNullOrEmpty(username)) return await Task.FromResult(new LoginResponseModel(loginResponse: LoginResponse.Error));
            
            if (_banController.GetBanData(player).Result.isBanned)
            {
                return await Task.FromResult(new LoginResponseModel(loginResponse: LoginResponse.Banned, banData: _banController.GetBanData(player).Result)) ;
            }
          
            if (player.PlayerData.Login)
            {
                return await Task.FromResult(new LoginResponseModel(loginResponse: LoginResponse.AlreadyLogin));
            }
         
            try
            {
                var account = _context.Accounts.FirstOrDefault(x => x.socialClubId == (int)player.SocialClubId);
              
                if (account == null)
                {               
                    if (username.Length < 3)
                    {
                        player.SendTranslatedChatMessage("Dein Nutzername ist zu kurz!", "Username tooooo short!", "Bameninghong", StatusTypes.Error);
                        return await Task.FromResult(new LoginResponseModel(loginResponse: LoginResponse.Error));
                    }         

                    if (_context.Accounts.FirstOrDefault(x => x.username == username) != null)
                    {
                        player.SendTranslatedChatMessage("Nutzername bereits vorhanden!", "", "Bameninghong", StatusTypes.Error);
                        return await Task.FromResult(new LoginResponseModel(loginResponse: LoginResponse.Error));
                    }
                   
                    if (_context.Accounts.FirstOrDefault(x => x.socialClubName == player.SocialClubName) != null)
                    {
                        player.SendTranslatedChatMessage("Du besitzt bereits einen Account!", "", "Bameninghong", StatusTypes.Error);
                        return await Task.FromResult(new LoginResponseModel(loginResponse: LoginResponse.Error));
                    }
                  
                    if (_context.Accounts.FirstOrDefault(x => x.Serial == player.Serial) != null)
                    {
                        player.SendTranslatedChatMessage("Du besitzt bereits einen Account!", "", "Bameninghong", StatusTypes.Error);
                        return await Task.FromResult(new LoginResponseModel(loginResponse: LoginResponse.Error));
                    }
                   
                    if (_context.Accounts.FirstOrDefault(x => x.address == player.Address) != null)
                    {
                        player.SendTranslatedChatMessage("Du besitzt bereits einen Account!", "", "Bameninghong", StatusTypes.Error);
                        return await Task.FromResult(new LoginResponseModel(loginResponse: LoginResponse.Error));
                    }
                  
                    return await Task.FromResult(new LoginResponseModel(loginResponse: LoginResponse.CreateAccount));
                }
                else
                {          
                    // MAYBE REMOVE
                    if (account.socialClubName != player.SocialClubName)
                    {
                        player.SendTranslatedChatMessage("Account stimmt nicht überein!", "", "Bameninghong", StatusTypes.Error);
                        return await Task.FromResult(new LoginResponseModel(loginResponse: LoginResponse.Error));
                    }               

                    if (account.username != username)
                    {
                        player.SendTranslatedChatMessage("Nutzername stimmt nicht überein!", "", "Bameninghong", StatusTypes.Error);
                        return await Task.FromResult(new LoginResponseModel(loginResponse: LoginResponse.Error));
                    }
                    
                    return await Task.FromResult(new LoginResponseModel(loginResponse: LoginResponse.Success, account));

                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
            }

            return await Task.FromResult(new LoginResponseModel(LoginResponse.Error));
        }
    }
}
