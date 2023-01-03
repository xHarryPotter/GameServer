using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Module.Faction.Models
{
    public class FactionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool isPrivate { get; set; }
        public bool isActive { get; set; }
        public string Position { get; set; }
        public string Clothes { get; set; }
        public string Color { get; set; } // HEX

        public FactionModel() 
        { 
        }
    }
}
