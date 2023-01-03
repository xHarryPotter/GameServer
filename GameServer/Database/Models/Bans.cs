using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Database.Models
{
    public class Bans
    {
        public int Id { get; set; }
        public string socialClubName { get; set; }
        public int socialClubId { get; set; }
        public string address { get; set; }
        public string Serial { get; set; }
        public string reason { get; set; }
        public DateTime banDate { get; set; }
        public DateTime expires { get; set; }
        public int adminId { get; set; }
    }
}
