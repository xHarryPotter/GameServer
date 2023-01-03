using GameServer.Entities.SPlayer;
using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{

    public sealed class EventHandler : Script
    {

        [ServerEvent(Event.PlayerConnected)]
        private Task OnPlayerConnectAsync(SPlayer player) => GameServer.Events.PlayerConnectEvent.InvokeAsync(d => d(player));


        [ServerEvent(Event.PlayerDeath)]
        private Task OnPlayerDeath(SPlayer player, SPlayer killer, uint reason) => GameServer.Events.PlayerDeathEvent.InvokeAsync(d => d(player, killer, reason));
    }
}
