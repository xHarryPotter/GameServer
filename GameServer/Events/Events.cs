using Microsoft.Extensions.DependencyInjection;
using GameServer.Module;
using System;
using System.Collections.Generic;
using System.Text;
using GameServer.Interfaces;
using static GameServer.Delegates;
using GTANetworkAPI;
using GameServer.Entities.SPlayer;

namespace GameServer
{
    public sealed class Events : ISingleton
    {   
        public static AsyncEventHandler<PlayerConnectAsyncDelegate> PlayerConnectEvent;
        public static AsyncEventHandler<PlayerDeathAsyncDelegate> PlayerDeathEvent;
        
        public Events()
        {
            PlayerConnectEvent = new AsyncEventHandler<PlayerConnectAsyncDelegate>();
            PlayerDeathEvent = new AsyncEventHandler<PlayerDeathAsyncDelegate>();
        }

        public event PlayerConnectAsyncDelegate OnPlayerConnect
        {
            add => PlayerConnectEvent.Add(value);
            remove => PlayerConnectEvent.Remove(value);
        }

        public event PlayerDeathAsyncDelegate OnPlayerDeath
        {
            add => PlayerDeathEvent.Add(value);
            remove => PlayerDeathEvent.Remove(value);
        }



    }
}
