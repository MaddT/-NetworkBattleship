using System;

namespace NetworkGame
{
    //игровое поле игрока
    [Serializable]
    public class FieldInfo
    {
        public UserInfo user;   //информация о игроке
        public int gameID;      //идентификатор игры
        public byte[,] field;   //расположение кораблей на поле

        public FieldInfo(UserInfo usr, int id, byte[,] f)
        {
            user = usr;
            gameID = id;
            field = f;
        }
    }
}
