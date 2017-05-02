using System;

namespace NetworkGame
{
    //информация о игроке
    [Serializable]
    public class UserInfo
    {
        public string UserNick;
        public int UserNumber;
        public int UserRating;

        public UserInfo(string nick, int numb, int rating)
        {
            UserNick = nick;
            UserNumber = numb;
            UserRating = rating;
        }
    }
}
