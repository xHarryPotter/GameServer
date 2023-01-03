using GameServer.Module.Translator;
using GameServer.Data;
using GTANetworkAPI;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GameServer.Core.Enums;
using GameServer.Data.Player;
using Newtonsoft.Json.Linq;

namespace GameServer.Entities.SPlayer
{
    public class SPlayer : Player
    {
        public int PlayerID { get; set; }
        public string Username { get; set; }
        public PlayerData PlayerData { get; set; }
        public PlayerFactionData FactionData { get; set; }
    
   
        public SPlayer(NetHandle handle) : base(handle)
        {
            PlayerID = 0;
            Username = this.Name;
            PlayerData = new PlayerData(false, false, Languages.German, false, false);
            FactionData = new PlayerFactionData(0);
        }


        public void Init(int _playerId, string _username    )
        {
            PlayerID = _playerId;
            Username = _username;

            PlayerData.Login = true;
            PlayerData.LoginPressed = true;

            FactionData = new PlayerFactionData(0);
         }

    
        public void Spawn(Vector3 pos) => NAPI.Task.Run(() => NAPI.Player.SpawnPlayer(this, pos));
 
        public async Task SetHealth(int value)
        {
            await NAPI.Task.WaitForMainThread(0);
            await Task.Run(() => Health = value);
        }

        public async Task SetArmour(int value) 
        {
            await NAPI.Task.WaitForMainThread(0);
            await Task.Run(() => Armor = value); 
        }
        public async Task SetDimension(uint value)
        {
            await NAPI.Task.WaitForMainThread(0);
            await Task.Run(() => Dimension = value);
        }

        public async Task SetPosition(Vector4 position, float rotation = 0)
        {
            NAPI.Task.Run(() => Position = position.ToVector3());
            NAPI.Task.Run(() => Rotation.Z = rotation);
        }

        public async Task SetFreezed(bool value)
        {
            TriggerEventSafe("Client:SetFreezed", value);
        }

        public async void TriggerEventSafe(string eventName, params object[] args)
        {
            await NAPI.Task.WaitForMainThread(0);
            TriggerEvent(eventName, args);
        }

        public void ShowLoginNotification(string message, bool error)
        {
            if (error)
            {
                this.TriggerEventSafe("Client:LoginError", message);
            }
            else
            {
                this.TriggerEventSafe("Client:LOginSuccess", message);
            }
        }

        public new async Task PlayAnimation(string animDict, string animName, int flag, int duration)
        {           
            NAPI.Task.Run(() => this.PlayAnimation(animDict, animName, flag));
            if (duration == -1) return;
            await Task.Delay(duration);
            NAPI.Task.Run(() => this.StopAnimation());
        }


        public async void SendTranslatedChatMessage(
            string deTranslation,
            string enTranslation,
            string ruTranslation,
            StatusTypes statusType)
        {

            var translation = new TranslatorModule(deTranslation, enTranslation, ruTranslation, statusType);

            await NAPI.Task.WaitForMainThread(0);

            if (translation.status == StatusTypes.Success)
            {
                TriggerEventSafe("Client:Success", translation.GetTranslatedMessage(this.PlayerData.Language));
            }
            else
            {
                TriggerEventSafe("Client:Error", translation.GetTranslatedMessage(this.PlayerData.Language));
            }
        }
    }
}
