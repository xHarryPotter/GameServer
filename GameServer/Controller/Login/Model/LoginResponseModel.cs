using GameServer.Controller.Ban.Model;
using GameServer.Core.Enums;
using GameServer.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Controller.Login.Model
{
    public class LoginResponseModel
    {
        public LoginResponse loginResponse { get; set; }
        public Account account { get; set; }
        public BanDataModel banData { get; set; }

        public LoginResponseModel(LoginResponse loginResponse, Account account = null, BanDataModel banData = null)
        {
            this.loginResponse = loginResponse;
            this.account = account;
            this.banData = banData;
        
        }
    }
}
