using GameServer.Entities.SPlayer;
using GameServer.Interfaces;
using GTANetworkAPI;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace GameServer.Factories
{
    public class Factorie : ISingleton
    {

        public Factorie()
        {
            RAGE.Entities.Players.CreateEntity = handle => _(handle);
        }


        public Player _(NetHandle handle) 
        {
            SPlayer player = (SPlayer)Activator.CreateInstance(typeof(SPlayer), handle);
            if (player == null)
            {
                throw new Exception("Error at " + nameof(Factorie) + " Player is null");
            }
            return player!;
        }
    }
}
