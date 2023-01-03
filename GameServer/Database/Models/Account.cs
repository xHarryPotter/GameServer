using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Database.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string username { get; set; }
        public string socialClubName { get; set; }
        public int socialClubId { get; set; }
        public string address { get; set; }
        public string Serial { get; set; }
    }
}
