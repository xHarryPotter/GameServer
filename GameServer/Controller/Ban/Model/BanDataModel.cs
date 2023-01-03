using GameServer.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Controller.Ban.Model
{
    public class BanDataModel
    {
        public bool isBanned { get; set; }
        public string banreason { get; set; }
        public DateTime expires { get; set; }
        public Account account { get; set; }
        public int adminId { get; set; }
        
        public BanDataModel(bool banned, DateTime expires, string banreason = "No Reason", Account account = null, int admin = 0)
        {
            this.isBanned = banned;
            this.banreason = banreason;
            this.expires = expires;
            this.account = account;
            this.adminId = admin;
        }
    }
}
