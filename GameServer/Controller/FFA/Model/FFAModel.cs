using GameServer.Core.Enums;
using GameServer.Entities.SPlayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Module.FFA.Model
{
    public class FFAModel
    {
        public int Id { get; set; }
        public FFATypes Type { get; set; }
        public List<SPlayer> Players { get; set; } = new List<SPlayer>();
        public int MaxPlayers { get; set; }

        public FFAModel(int _id, FFATypes _type, List<SPlayer> _players, int _maxPlayers)
        {
            Id = _id;
            Type = _type;
            Players = _players;
            MaxPlayers = _maxPlayers;
        }
    }
}
