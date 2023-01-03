using GameServer.Database.Models;
using GameServer.Database;
using GameServer.Entities.SPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GameServer.Data.Player;
using GameServer.Controller.Ban.Model;
using System.Runtime.CompilerServices;
using System.Security.Principal;

namespace GameServer.Module.Player
{
    public class BanController : ISingleton
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Logger _logger;
        private readonly Context _context;

        public BanController(IServiceProvider services, Logger logger, Context context)
        {
            _serviceProvider = services;
            _logger = logger;
            _context = context;
        }

        public async Task<BanDataModel> GetBanData(SPlayer player)
        {
            var scIdData = _context.Bans.FirstOrDefault(x => x.socialClubId == (int)player.SocialClubId);


            if (scIdData != null)
            {

                if (scIdData.expires > DateTime.Now)
                {
                    return await Task.FromResult(new BanDataModel(banned: true, banreason: scIdData.reason, expires: scIdData.expires, account: _context.Accounts.FirstOrDefault(x => x.socialClubId == (int)player.SocialClubId), admin: scIdData.adminId));
                }
            }

            var scNameData = _context.Bans.FirstOrDefault(x => x.socialClubName == player.SocialClubName);

            if (scNameData != null)
            {
                if (scNameData.expires > DateTime.Now)
                {
                    return await Task.FromResult(new BanDataModel(banned: true, banreason: scNameData.reason, expires: scNameData.expires, account: _context.Accounts.FirstOrDefault(x => x.socialClubName == player.SocialClubName), admin: scNameData.adminId));
                }
            }


            var scSerialData = _context.Bans.FirstOrDefault(x => x.Serial == player.Serial);

            if (scSerialData != null)
            {
                if (scSerialData.expires > DateTime.Now)
                {
                    return await Task.FromResult(new BanDataModel(banned: true, banreason: scSerialData.reason, expires: scSerialData.expires, account: _context.Accounts.FirstOrDefault(x => x.Serial == player.Serial), admin: scSerialData.adminId));
                }
            }

            var scAddressData = _context.Bans.FirstOrDefault(x => x.address == player.Address);

            if (scAddressData != null)
            {
                if (scAddressData.expires > DateTime.Now)
                {
                    return await Task.FromResult(new BanDataModel(banned: true, banreason: scAddressData.reason, expires: scAddressData.expires, account: _context.Accounts.FirstOrDefault(x => x.address == player.Address), admin: scAddressData.adminId));
                }
            }

            return await Task.FromResult(new BanDataModel(banned: false, expires: DateTime.Now.AddYears(10)));

        }

        public Task<bool> BanPlayerIngame(SPlayer player)
        {
            return Task.FromResult(true);
        }
        public Task<bool> BanPlayer(int Id)
        {
            return Task.FromResult(true);
        }
        public Task<bool> BanPlayer(string socialClubName)
        {
            return Task.FromResult(true);
        }
        public Task<bool> BanPlayer(ulong socialClubId)
        {
            return Task.FromResult(true);
        }
        public Task<bool> BanPlayerName(string Name)
        {
            return Task.FromResult(true);
        }
        public Task<bool> BanPlayerAddress(string Address)
        {
            return Task.FromResult(true);
        }
        public Task<bool> BanPlayerHWID(string Hwid)
        {
            return Task.FromResult(true);
        }
       
    }
}
