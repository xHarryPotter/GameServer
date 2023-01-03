using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Data.Player
{
    public class PlayerFactionData
    {
        public int factionId { get; set; }

        public PlayerFactionData(int _factionId)
        {
            factionId = _factionId;
        }
    }
}
