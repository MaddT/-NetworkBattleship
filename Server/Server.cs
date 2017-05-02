using System;
using System.Collections.Generic;
using System.Threading;
using NetworkGame;

namespace Server
{
    public class Server
    {
        private List<Client> Clients = new List<Client>();
        private List<UserInfo> users = new List<UserInfo>();
        private List<Game> Games = new List<Game>();
        private List<Game> OpenGames = new List<Game>();
        private DatabaseManager usersDatabase = new DatabaseManager();

        public List<UserInfo> Users
        {
            get
            {
                return users;
            }
            private set
            {
                users = value;
            }
        }

        public int gameID { get; private set; }

        public Server(int id)
        {
            if (!usersDatabase.SuccessfulConnection)
            {
                Console.WriteLine("Подключение к базе данных не удалось.");
                Thread.Sleep(3000);
                Environment.Exit(0);
            }
            gameID = id;
        }

        public void SendToUser(UserInfo user, byte[] data)
        {
            Client dUser = Clients[0];
            foreach (Client item in Clients)
                if (item.numb == user.UserNumber)
                {
                    dUser = item;
                    break;
                }
            dUser.SendMessage(data);
        }

        public void SendAll(byte[] data)
        {
            foreach (Client item in Clients)
                item.SendMessage(data);
        }

        public void AddClient(Client co)
        {
            Clients.Add(co);
        }

        public void AddUser(UserInfo ui)
        {
            Users.Add(ui);
        }

        public void AddGame(Game gi)
        {
            Games.Add(gi);
            gameID++;
        }

        private void AddOpenGame(Game gn)
        {
            OpenGames.Add(gn);
            gameID++;
        }

        public void RemoveClient(Client co)
        {
            Clients.Remove(co);
        }

        public void RemoveUser(UserInfo ui)
        {
            Users.Remove(ui);
        }

        public void AddField(FieldInfo fInfo)
        {
            Game currGame = null;
            foreach (Game item in Games)
                if (item.gameInfo.gameID == fInfo.gameID)
                {
                    currGame = item;
                    break;
                }
            if (currGame.gameInfo.user1.UserNumber == fInfo.user.UserNumber)
                currGame.SetGField1(new GameField(fInfo.field));
            else
                currGame.SetGField2(new GameField(fInfo.field));
            if (currGame.CheckState())
            {
                byte[] sendData = SerializationUtils.Serialization(
                    MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Start], currGame.gameInfo.currentUser);
                SendToUser(currGame.gameInfo.user1, sendData);
                SendToUser(currGame.gameInfo.user2, sendData);
            }
        }

        public void PlayerMove(Move move, UserInfo user)
        {
            Game currGame = null;
            foreach (Game item in Games)
                if (item.gameInfo.gameID == move.gameID)
                {
                    currGame = item;
                    break;
                }
            if (user.UserNumber != currGame.gameInfo.currentUser.UserNumber) return;
            if (user.UserNumber == currGame.gameInfo.user1.UserNumber)
            {
                if (currGame.gameFieldP2.Attack(move.a, move.b))
                {
                    move.result = GameField.Hit;
                    byte[] sendData = SerializationUtils.Serialization(
                        MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Move], move);
                    SendToUser(currGame.gameInfo.user2, sendData);
                    move.result = GameField.Hit;
                    sendData = SerializationUtils.Serialization(
                        MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Attack], move);
                    SendToUser(currGame.gameInfo.user1, sendData);
                }
                else
                {
                    move.result = GameField.Miss;
                    byte[] sendData = SerializationUtils.Serialization(
                        MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Move], move);
                    SendToUser(currGame.gameInfo.user2, sendData);
                    move.result = GameField.Miss;
                    sendData = SerializationUtils.Serialization(
                        MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Attack], move);
                    SendToUser(currGame.gameInfo.user1, sendData);
                    currGame.gameInfo.SwapActiveUser();
                }
            }
            else
            {
                if (currGame.gameFieldP1.Attack(move.a, move.b))
                {
                    move.result = GameField.Hit;
                    byte[] sendData = SerializationUtils.Serialization(
                        MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Move], move);
                    SendToUser(currGame.gameInfo.user1, sendData);
                    move.result = GameField.Hit;
                    sendData = SerializationUtils.Serialization(
                        MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Attack], move);
                    SendToUser(currGame.gameInfo.user2, sendData);
                }
                else
                {
                    move.result = GameField.Miss;
                    byte[] sendData = SerializationUtils.Serialization(
                        MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Move], move);
                    SendToUser(currGame.gameInfo.user1, sendData);
                    move.result = GameField.Miss;
                    sendData = SerializationUtils.Serialization(
                        MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Attack], move);
                    SendToUser(currGame.gameInfo.user2, sendData);
                    currGame.gameInfo.SwapActiveUser();
                }
            }

            //проверка условия победы, и определение победителя если необходимо
            UserInfo winner;
            UserInfo loser;
            if (currGame.gameFieldP1.CheckWin())
            {
                winner = currGame.gameInfo.user2;
                loser = currGame.gameInfo.user1;

            }
            else if (currGame.gameFieldP2.CheckWin())
            {
                winner = currGame.gameInfo.user1;
                loser = currGame.gameInfo.user2;
            }
            else return;

            int[] ratings = GetElo(winner.UserRating, loser.UserRating);
            winner.UserRating = ratings[0];
            loser.UserRating = ratings[1];

            foreach (UserInfo item in users)
            {
                if (item.UserNumber == winner.UserNumber)
                    item.UserRating = winner.UserRating;
                if (item.UserNumber == loser.UserNumber)
                    item.UserRating = loser.UserRating;
            }

            byte[] sendData1 = SerializationUtils.Serialization(
                        MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Win], winner.UserRating);
            SendToUser(winner, sendData1);
            sendData1 = SerializationUtils.Serialization(
                        MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Lose], loser.UserRating);
            SendToUser(loser, sendData1);
            Console.WriteLine("Игра завершена");
            Console.WriteLine("Победитель: " + winner.UserNick);
            Console.WriteLine("Проигравший: " + loser.UserNick);

            //обновляем рейтинг в базе данных
            usersDatabase.UpdateRating(winner, winner.UserRating);
            usersDatabase.UpdateRating(loser, loser.UserRating);

            Games.Remove(currGame);
            //обновляем список пользователей на клиенте
            byte[] sendData2 = SerializationUtils.Serialization(
                MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Entrance],
                Users);
            SendAll(sendData2);
        }

        public Game GetGame(int id)
        {
            foreach (Game item in Games)
                if (item.gameInfo.gameID == id)
                    return item;
            return null;
        }

        public void OpenGame(GInfo game)
        {
            Game g = new Game(this.gameID, new UserInfo[] { game.userOpp, null });
            AddOpenGame(g);
            UpdateOpenedGames();
        }

        public void RemoveGame(GInfo game)
        {
            Game rGame = null;
            foreach (Game item in OpenGames)
                if (game.gameID == item.gameInfo.gameID)
                {
                    rGame = item;
                    break;
                }
            OpenGames.Remove(rGame);
            UpdateOpenedGames();
        }

        public void UpdateOpenedGames()
        {
            List<GInfo> openGames = new List<GInfo>();
            foreach (Game item in OpenGames)
            {
                GInfo gi = new GInfo(item.gameInfo.user1, item.gameInfo.gameID);
                openGames.Add(gi);
            }
            byte[] games = SerializationUtils.Serialization(
                MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.UpdateGames],
                openGames);
            SendAll(games);
        }

        public void JoinToGame(UserInfo user, GInfo game)
        {
            UserInfo[] users = new UserInfo[] { game.userOpp, user };
            Game jGame = new Game(game.gameID, users);
            this.AddGame(jGame);

            //посылаем игрокам информацию для начала игры
            GInfo gameI = new GInfo(users[0], jGame.gameInfo.gameID);
            byte[] sendData = SerializationUtils.Serialization(
                MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.NewGame],
                gameI);
            this.SendToUser(users[1], sendData);

            gameI.userOpp = users[1];
            gameI.gameID = jGame.gameInfo.gameID;
            sendData = SerializationUtils.Serialization(
                MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.NewGame],
                gameI);
            this.SendToUser(users[0], sendData);

            //удаляем из списка открытых игр
            this.RemoveGame(game);
        }

        public UserInfo Login(AuthenticationInfo authInfo)
        {
            UserInfo user = usersDatabase.LoginAccount(authInfo.Username, authInfo.Password);
            return user;
        }

        public UserInfo Register(AuthenticationInfo authInfo)
        {
            UserInfo user = usersDatabase.RegisterNewAccount(authInfo.Username, authInfo.Password);
            return user;
        }

        private int[] GetElo(int ratingW, int ratingL)
        {
            int[] ratings = new int[2];
            ratings[0] = ratingW + (int)(15 * (1 - 1 / (1.0 + Math.Pow(10, (ratingL - ratingW) / 400))));
            ratings[1] = ratingL + (int)(15 * (0 - 1 / (1.0 + Math.Pow(10, (ratingW - ratingL) / 400))));
            return ratings;
        }

        public void InterruptGame(UserInfo loser, GInfo game)
        {
            Game currGame = null;
            foreach (Game item in Games)
                if (item.gameInfo.gameID == game.gameID)
                {
                    currGame = item;
                    break;
                }

            UserInfo winner;
            if (currGame.gameInfo.user1.UserNumber == loser.UserNumber)
                winner = currGame.gameInfo.user2;
            else
                winner = currGame.gameInfo.user1;

            int[] ratings = GetElo(winner.UserRating, loser.UserRating);
            winner.UserRating = ratings[0];
            loser.UserRating = ratings[1];

            byte[] sendData1 = SerializationUtils.Serialization(
                        MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Win], winner.UserRating);
            SendToUser(winner, sendData1);
            sendData1 = SerializationUtils.Serialization(
                        MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Lose], loser.UserRating);
            SendToUser(loser, sendData1);

            Console.WriteLine("Игра завершена");
            Console.WriteLine("Победитель: " + winner.UserNick);
            Console.WriteLine("Проигравший: " + loser.UserNick);


            foreach (UserInfo item in users)
            {
                if (item.UserNumber == winner.UserNumber)
                    item.UserRating = winner.UserRating;
                if (item.UserNumber == loser.UserNumber)
                    item.UserRating = loser.UserRating;
            }

            //обновляем рейтинг в базе данных
            usersDatabase.UpdateRating(winner, winner.UserRating);
            usersDatabase.UpdateRating(loser, loser.UserRating);

            Games.Remove(currGame);
            //обновляем список пользователей на клиенте
            byte[] sendData2 = SerializationUtils.Serialization(
                MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Entrance],
                Users);
            SendAll(sendData2);
        }
    }
}
