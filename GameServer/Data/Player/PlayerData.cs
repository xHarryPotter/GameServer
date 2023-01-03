using GameServer.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Data.Player
{
    public class PlayerData
    {
        public bool FFA { get; set; }
        public bool Dead { get; set; }
        public Languages Language { get; set; }
        public bool Login { get; set; }
        public bool LoginPressed { get; set; }


        public PlayerData(bool _ffa, bool _dead, Languages _language, bool _login, bool _loginPressed)
        {
            FFA = _ffa;
            Dead = _dead;
            Language = _language;
            Login = _login;
            LoginPressed = _loginPressed;


        }
    }
}
