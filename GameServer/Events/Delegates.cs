using GameServer.Entities.SPlayer;
using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class Delegates
    {
        public delegate Task PlayerConnectAsyncDelegate(SPlayer player);
        public delegate Task PlayerDeathAsyncDelegate(SPlayer player, SPlayer killer, uint reason);
        public delegate Task PlayerDisconnectedAsyncDelegate(SPlayer player, DisconnectionType type, string reason);
    }
}
