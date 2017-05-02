using System;

namespace NetworkGame
{
    //ход игрока
    [Serializable]
    public class Move
    {
        public int a;
        public int b;
        public byte result;
        public int gameID;

        public Move(int a, int b, byte result, int id)
        {
            this.a = a;
            this.b = b;
            this.result = result;
            gameID = id;
        }
    }
}
