using GameServer.Controller.Ban.Model;
using GameServer.Data.Player;
using GameServer.Entities.SPlayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Controller.Ban.Interface
{
    public interface IBanController
    {
        Task<BanDataModel> GetBanData(SPlayer player);
        Task<bool> BanPlayerIngame(SPlayer player);
        Task<bool> BanPlayer(int Id);
        Task<bool> BanPlayer(string socialClubName);
        Task<bool> BanPlayer(ulong socialClubId);
        Task<bool> BanPlayerName(string Name);
        Task<bool> BanPlayerAddress(string Address);
        Task<bool> BanPlayerHWID(string Hwid);
    }
}
