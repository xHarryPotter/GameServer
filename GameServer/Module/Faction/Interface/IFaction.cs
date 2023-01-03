using GameServer.Entities.SPlayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Module.Faction.Interface
{
    public interface IFaction
    {
         void InitFactionsIngame(SPlayer player);
    }
}
