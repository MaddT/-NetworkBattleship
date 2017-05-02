using System;

namespace NetworkGame
{
    //сообщение в чате
    [Serializable]
    public class GameMessage
    {
        public string mess;
        public int gameID;

        public GameMessage(string str, int id)
        {
            mess = str;
            gameID = id;
        }
    }
}
