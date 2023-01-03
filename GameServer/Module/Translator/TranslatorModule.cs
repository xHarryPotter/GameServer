using GameServer.Core.Enums;
using GameServer.Database;
using GameServer.Entities.SPlayer;
using GameServer.Module.Player;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module.Translator
{
    public class TranslatorModule
    {
        public string deTranslation { get; set; }
        public string enTranslation { get; set; }
        public string ruTranslation { get; set; }
        public StatusTypes status { get; set; }

        public TranslatorModule(string _de, string _en, string _ru, StatusTypes _status)
        {
            deTranslation = _de;
            enTranslation = _en;
            ruTranslation = _ru;
            status = _status;
        }

        public string GetTranslatedMessage(Languages language)
        {
            switch ((Languages)language)
            {
                case Languages.German:
                    return deTranslation.ToString();

                case Languages.English:
                    return enTranslation.ToString();

                case Languages.Russian:
                    return  ruTranslation.ToString();

                default: return deTranslation.ToString();
            }
        }
    }
}
