using NetworkGame;

namespace Server
{
    //представляет игру на сервере
    public class Game
    {
        private enum State { Prepare, Go }

        //игровые поля обоих игроков
        public GameField gameFieldP1 { get; private set; }
        public GameField gameFieldP2 { get; private set; }
        private State GameState = State.Prepare;

        public GameInfo gameInfo;

        public Game(int id, UserInfo[] users)
        {
            gameInfo = new GameInfo(id, users[0], users[1]);
        }

        public void SetGField1(GameField gf)
        {
            if (GameState == State.Go || gameFieldP1 != null) return;
            gameFieldP1 = gf;
            if (gameFieldP2 != null)
                GameState = State.Go;
        }

        public void SetGField2(GameField gf)
        {
            if (GameState == State.Go || gameFieldP2 != null) return;
            gameFieldP2 = gf;
            if (gameFieldP1 != null)
                GameState = State.Go;
        }

        public bool CheckState()
        {
            return GameState == State.Go;
        }
    }
}
