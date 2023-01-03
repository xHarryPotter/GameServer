using GameServer.Controller.Login.Model;
using GameServer.Core.Enums;
using GameServer.Database.Models;
using GameServer.Entities.SPlayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Controller.Login.Interface
{
    public interface ILoginController
    {
        Task<LoginResponseModel> CheckLogin(SPlayer player, string username);
    }
}
