using GameServer.Entities.SPlayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Controller.FFA.Interface
{
    public interface IFFAController
    {
        void JoinFFA(SPlayer player, int ffaId);
        void LeaveFFA(SPlayer player);
    }
}
