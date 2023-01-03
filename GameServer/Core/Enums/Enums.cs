using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Core.Enums
{
    public enum TranslationTypes : int
    {
        AlreadyLoggedIn = 1,
        AccountAlreadyExist = 2,
        NotRegistered = 3,
        NotSamePassword = 4,
        AlreadyExistByName = 5,
        AccountCreated = 6,
        UsernameNotMatch = 7,
        LoginMessage = 8,
        BanMessageLogin = 9,
        UsernameTooShort,
        UsernameNotAllowed
    }

    public enum StatusTypes : int
    {
        Success = 0,
        Error = 1,
        Warning = 2,
        Information = 3
    }

    public enum Languages
    {
        German = 0,
        English = 1,
        Russian = 2
    }

    public enum LoginResponse
    {
        Success,
        Error,
        Banned,
        AlreadyLogin,
        CreateAccount
    }


    public enum FFATypes
    {
        Würfelpark = 1
    }

    public class Enums
    {
        public static string GetStatusString(StatusTypes statusTypes)
        {
            switch (statusTypes)
            {
                case StatusTypes.Success:
                    return "!{GREEN}";

                case StatusTypes.Error:
                    return "!{RED}";

                case StatusTypes.Warning:
                    return "!{ORANGE}";

                case StatusTypes.Information:
                    return "!{BLUE}";

                default: return "!{MAGENTA}";
            }
        }

        public static List<Vector4> WürfelparkPositions = new List<Vector4>
        {
          new Vector4(211.32306, -945.462, 30.686779, 55.050186),
            new Vector4(234.76306, -876.2825, 30.49208, 140.44954),
            new Vector4(181.8819, -855.51044, 31.130713, 156.21736),
            new Vector4(162.04382, -911.1257, 30.2309, -147.92009),
            new Vector4(150.71957, -964.0796, 30.0919,-71.81984),
            new Vector4(200.12329, -972.18445, 30.091902, 6.428997)
        };
    }
}
