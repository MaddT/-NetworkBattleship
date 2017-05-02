using System;

namespace NetworkGame
{
    //информация о игре
    [Serializable]
    public class GameInfo
    {
        //игроки
        public UserInfo user1;
        public UserInfo user2;
        //игрок, которому принадлежит следующий ход
        public UserInfo currentUser;

        public int gameID;  //идентификатор игры

        public GameInfo(int id, UserInfo u1, UserInfo u2)
        {
            gameID = id;
            user1 = u1;
            user2 = u2;
            currentUser = u1;
        }

        //передать очередность хода
        public UserInfo SwapActiveUser()
        {
            if (currentUser == null)
                currentUser = user1;
            if (currentUser == user1)
                currentUser = user2;
            else
                currentUser = user1;
            return currentUser;
        }
    }
}
