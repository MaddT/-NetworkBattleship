using System;

namespace NetworkGame
{
    //информация для аутентификации пользователей
    [Serializable]
    public class AuthenticationInfo
    {
        public string Username;
        public string Password;

        public AuthenticationInfo(string username, string pass)
        {
            Username = username;
            Password = pass;
        }
    }
}
