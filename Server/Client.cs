using System;
using System.IO;
using System.Net.Sockets;
using NetworkGame;

namespace Server
{
    public class Client
    {
        public TcpClient client;
        public int numb { get; private set; }
        NetworkStream stream;
        string nick = String.Empty;
        UserInfo User = null;
        int n = 0;
        private Server server;

        public Client(TcpClient tcpClient, int num, Server serv)
        {
            client = tcpClient;
            numb = num;
            server = serv;
        }

        public void SendMessage(byte[] data)
        {
            stream.Write(data, 0, data.Length);
        }

        public void Process()
        {
            stream = client.GetStream();
            string code = string.Empty;
            byte[] data = new byte[64]; // буфер для получаемых данных
            while (true)
            {
                try
                {
                    // получаем сообщение                    
                    int bytes = 0;
                    MemoryStream memStream = new MemoryStream();
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        memStream.Write(data, 0, bytes);
                    }
                    while (stream.DataAvailable);

                    byte[] allData = memStream.ToArray();

                    if (MessTypeUtil.MessageCheck(allData, MessTypeUtil.TypeMEnum.Entrance))
                        UserEntrance(allData);
                    else if (MessTypeUtil.MessageCheck(allData, MessTypeUtil.TypeMEnum.Exit))
                        UserLogout();
                    else if (MessTypeUtil.MessageCheck(allData, MessTypeUtil.TypeMEnum.Message))
                        MainMessage(allData);
                    else if (MessTypeUtil.MessageCheck(allData, MessTypeUtil.TypeMEnum.Offer))
                        OfferGame(allData);
                    else if (MessTypeUtil.MessageCheck(allData, MessTypeUtil.TypeMEnum.Accept))
                        AcceptGame(allData);
                    else if (MessTypeUtil.MessageCheck(allData, MessTypeUtil.TypeMEnum.SetField))
                        SetField(allData);
                    else if (MessTypeUtil.MessageCheck(allData, MessTypeUtil.TypeMEnum.Move))
                        NewMove(allData);
                    else if (MessTypeUtil.MessageCheck(allData, MessTypeUtil.TypeMEnum.GMessage))
                        SendGameMessage(allData);
                    else if (MessTypeUtil.MessageCheck(allData, MessTypeUtil.TypeMEnum.OpenGame))
                        OpenNewGame(allData);
                    else if (MessTypeUtil.MessageCheck(allData, MessTypeUtil.TypeMEnum.Join))
                        JoinToGame(allData);
                    else if (MessTypeUtil.MessageCheck(allData, MessTypeUtil.TypeMEnum.CancelGame))
                        CancelGame(allData);
                    else if (MessTypeUtil.MessageCheck(allData, MessTypeUtil.TypeMEnum.LogIn))
                        Login(allData);
                    else if (MessTypeUtil.MessageCheck(allData, MessTypeUtil.TypeMEnum.SignUp))
                        Register(allData);
                    else if (MessTypeUtil.MessageCheck(allData, MessTypeUtil.TypeMEnum.Lose))
                        LeaveGame(allData);
                }
                catch (ObjectDisposedException)
                {
                    Console.WriteLine("соединение с " + User.UserNick + " разорвано");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
            }
        }

        private void LeaveGame(byte[] data)
        {
            GInfo game = (GInfo)SerializationUtils.DeSerialization(data);
            server.InterruptGame(User, game);
        }

        private void Register(byte[] data)
        {
            AuthenticationInfo authInfo = (AuthenticationInfo)SerializationUtils.DeSerialization(data);
            User = server.Register(authInfo);
            if (User == null)
            {
                byte[] sendData = SerializationUtils.Serialization(
                    MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.LoginError], 1);
                SendMessage(sendData);
            }
            else
            {
                //добавляем нового пользователя на сервер и отправляем подтверждение клиенту
                User.UserNumber = this.numb;
                server.AddUser(User);
                byte[] sendData = SerializationUtils.Serialization(
                    MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.LoginAccomplish], User);
                SendMessage(sendData);

                //обновляем список пользователей на клиенте
                sendData = SerializationUtils.Serialization(
                    MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Entrance],
                    server.Users);
                server.SendAll(sendData);
                server.UpdateOpenedGames();
            }
        }

        private void Login(byte[] data)
        {
            if (User != null)
            {
                return;
            }
            AuthenticationInfo authInfo = (AuthenticationInfo)SerializationUtils.DeSerialization(data);
            Console.WriteLine(authInfo.Username + " - пытается подключиться\nпароль: " + authInfo.Password);
            User = server.Login(authInfo);
            if (User != null) Console.WriteLine(User.UserNick + " подключился");
            else Console.WriteLine("такого пользователя нет");

            if (User == null)
            {
                byte[] sendData = SerializationUtils.Serialization(
                    MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.LoginError], 1);
                SendMessage(sendData);
            }
            else
            {
                //добавляем нового пользователя на сервер и отправляем подтверждение клиенту
                User.UserNumber = this.numb;
                server.AddUser(User);
                byte[] sendData = SerializationUtils.Serialization(
                    MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.LoginAccomplish], User);
                SendMessage(sendData);

                //обновляем список пользователей на клиенте
                sendData = SerializationUtils.Serialization(
                    MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Entrance],
                    server.Users);
                server.SendAll(sendData);

                server.UpdateOpenedGames();
            }
        }

        private void CancelGame(byte[] data)
        {
            GInfo cGame = (GInfo)SerializationUtils.DeSerialization(data);
            Console.WriteLine(cGame.userOpp.UserNick + " хочет отменить игру " + cGame.gameID.ToString());
            server.RemoveGame(cGame);
        }

        private void JoinToGame(byte[] data)
        {
            GInfo jGame = (GInfo)SerializationUtils.DeSerialization(data);
            Console.WriteLine(User.UserNick + " хочет подключиться к игре " + jGame.userOpp.UserNick);
            server.JoinToGame(User, jGame);
        }

        private void OpenNewGame(byte[] data)
        {
            GInfo newG = (GInfo)SerializationUtils.DeSerialization(data);
            server.OpenGame(newG);
        }

        private void SendGameMessage(byte[] data)
        {
            GameMessage mess = (GameMessage)SerializationUtils.DeSerialization(data);
            Game cGame = server.GetGame(mess.gameID);
            if (cGame == null) return;
            server.SendToUser(cGame.gameInfo.user2, data);
            server.SendToUser(cGame.gameInfo.user1, data);
        }

        private void NewMove(byte[] data)
        {
            Move move = (Move)SerializationUtils.DeSerialization(data);
            server.PlayerMove(move, User);
        }

        private void SetField(byte[] data)
        {
            FieldInfo fInfo = (FieldInfo)SerializationUtils.DeSerialization(data);
            server.AddField(fInfo);
        }

        private void AcceptGame(byte[] data)
        {
            UserInfo[] users = (UserInfo[])SerializationUtils.DeSerialization(data);

            Game game = new Game(server.gameID, users);
            server.AddGame(game);

            GInfo gameI = new GInfo(users[0], game.gameInfo.gameID);
            byte[] sendData = SerializationUtils.Serialization(
                MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.NewGame],
                gameI);
            server.SendToUser(users[1], sendData);
            gameI.userOpp = users[1];
            gameI.gameID = game.gameInfo.gameID;
            sendData = SerializationUtils.Serialization(
                MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.NewGame],
                gameI);
            server.SendToUser(users[0], sendData);
        }

        private void UserEntrance(byte[] data)
        {
            string message = (string)SerializationUtils.DeSerialization(data);
            User = new UserInfo(message, this.numb, 100);
            server.AddUser(User);
            Console.WriteLine(message + " подключился");
            nick = message;

            //отправить список пользователей
            byte[] sendData = SerializationUtils.Serialization(
                MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Entrance],
                server.Users);
            server.SendAll(sendData);
            server.UpdateOpenedGames();
        }

        private void UserLogout()
        {
            //Console.WriteLine(nick + " отключился");
            server.RemoveUser(this.User);
            server.RemoveClient(this);
            stream.Close();
            client.Close();

            byte[] sendData = SerializationUtils.Serialization(
                MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Exit],
                server.Users);
            server.SendAll(sendData);
        }

        private void MainMessage(byte[] data)
        {
            string message = (string)SerializationUtils.DeSerialization(data);
            Console.WriteLine(message);
            byte[] sendData = SerializationUtils.Serialization(
                MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Message],
                User.UserNick + ": " + message);
            server.SendAll(sendData);
        }

        private void OfferGame(byte[] data)
        {
            UserInfo[] users = (UserInfo[])SerializationUtils.DeSerialization(data);
            byte[] sendData = SerializationUtils.Serialization(
                MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Offer],
                users[1]);
            server.SendToUser(users[0], sendData);
        }
    }
}
