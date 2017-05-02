using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using NetworkGame;

namespace Client
{
    public partial class MainForm : Form
    {
        static string userName;
        private const string host = "127.0.0.1";
        private const int port = 11000;
        static TcpClient client;
        bool connect = false;               //есть или нет подключения
        bool offer = false;                 //принято или нет предложение
        NetworkStream stream;
        List<UserInfo> users = new List<UserInfo>();
        UserInfo currUser = null;
        GameForm gameForm;
        AuthenticationForm authForm;
        GInfo currentGame;
        List<GInfo> openedGames = new List<GInfo>();
        GInfo openedGame;

        public MainForm()
        {
            InitializeComponent();
            btnLogout.Enabled = false;
            btnSendMessage.Enabled = false;
            btnJoinGame.Enabled = false;
            btnCreateGame.Enabled = false;
            btnOfferGame.Enabled = false;
            btnCancelGame.Enabled = false;
            gameForm = new GameForm(this);
            authForm = new AuthenticationForm(this);
        }

        // отправка сообщений
        private void SendData(byte[] data)
        {
            stream.Write(data, 0, data.Length);
        }

        // получение сообщений
        private void ReceiveMessage()
        {
            try
            {
                while (true)
                {
                    if (!connect)
                    {
                        this.Invoke(new MethodInvoker(() =>
                        {
                            textBoxChat.Clear();
                        }));
                        break;
                    }

                    byte[] data = new byte[64]; // буфер для получаемых данных                    
                    int bytes = 0;
                    MemoryStream memStream = new MemoryStream();
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        memStream.Write(data, 0, bytes);
                    }
                    while (stream.DataAvailable);

                    byte[] allData = memStream.ToArray();

                    if (allData.Length < 3) continue;

                    if (MessTypeUtil.MessageCheck(allData, MessTypeUtil.TypeMEnum.Message))
                    {
                        //MessageBox.Show("Hiiii");
                        string message = (string)SerializationUtils.DeSerialization(allData);
                        // добавляем полученное сообщение в текстовое поле 
                        this.Invoke(new MethodInvoker(() =>
                        {
                            textBoxChat.Text = DateTime.Now.ToShortTimeString() + " "
                                + message + "\r\n"
                                + textBoxChat.Text;
                        }));
                    }
                    else if (MessTypeUtil.MessageCheck(allData, MessTypeUtil.TypeMEnum.Entrance))
                    {
                        users = (List<UserInfo>)SerializationUtils.DeSerialization(allData);
                        this.Invoke(new MethodInvoker(() =>
                        {
                            lbUsers.Items.Clear();
                            foreach (UserInfo item in users)
                            {
                                lbUsers.Items.Add(item.UserNick + "(" + item.UserRating.ToString() + ")");
                            }
                        }));
                    }
                    else if (MessTypeUtil.MessageCheck(allData, MessTypeUtil.TypeMEnum.Exit))
                    {
                        users = (List<UserInfo>)SerializationUtils.DeSerialization(allData);
                        this.Invoke(new MethodInvoker(() =>
                        {
                            lbUsers.Items.Clear();
                            foreach (UserInfo item in users)
                            {
                                lbUsers.Items.Add(item.UserNick + "(" + item.UserRating.ToString() + ")");
                            }
                        }));
                    }
                    else if (MessTypeUtil.MessageCheck(allData, MessTypeUtil.TypeMEnum.Offer))
                    {
                        if (offer) return;
                        UserInfo userOpp = (UserInfo)SerializationUtils.DeSerialization(allData);
                        this.Invoke(new MethodInvoker(() =>
                        {
                            if (MessageBox.Show("Игрок " + userOpp.UserNick + " предлагает игру." +
                                "\n\rВступить в игру?",
                                this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                                == System.Windows.Forms.DialogResult.Yes)
                            {
                                offer = true;
                                UserInfo[] users1 = { userOpp, currUser };
                                byte[] message = SerializationUtils.Serialization(
                                    MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Accept], users1);
                                SendData(message);
                            }
                        }));
                    }
                    else if (MessTypeUtil.MessageCheck(allData, MessTypeUtil.TypeMEnum.NewGame))
                    {
                        currentGame = (GInfo)SerializationUtils.DeSerialization(allData);
                        this.Invoke(new MethodInvoker(() =>
                        {
                            offer = true;
                            gameForm.Show();
                            gameForm.Refresh();
                            gameForm.SetOpp(currentGame.userOpp.UserNick);
                            btnCancelGame.Enabled = false;
                            btnCreateGame.Enabled = true;
                            this.Hide();
                        }));
                    }
                    else if (MessTypeUtil.MessageCheck(allData, MessTypeUtil.TypeMEnum.Start))
                    {
                        UserInfo userToMove = (UserInfo)SerializationUtils.DeSerialization(allData);
                        this.Invoke(new MethodInvoker(() =>
                        {
                            if (currUser.UserNumber == userToMove.UserNumber)
                                gameForm.SetGame(true);
                            else
                                gameForm.SetGame(false);
                        }));
                    }
                    else if (MessTypeUtil.MessageCheck(allData, MessTypeUtil.TypeMEnum.Move))
                    {
                        Move move = (Move)SerializationUtils.DeSerialization(allData);
                        this.Invoke(new MethodInvoker(() =>
                        {
                            gameForm.AttackF(move);
                        }));
                    }
                    else if (MessTypeUtil.MessageCheck(allData, MessTypeUtil.TypeMEnum.Attack))
                    {
                        Move move = (Move)SerializationUtils.DeSerialization(allData);
                        this.Invoke(new MethodInvoker(() =>
                        {
                            gameForm.MarkF(move);
                        }));
                    }
                    else if (MessTypeUtil.MessageCheck(allData, MessTypeUtil.TypeMEnum.Win))
                    {
                        int newR = (int)SerializationUtils.DeSerialization(allData);
                        MessageBox.Show("Вы победили!");
                        this.Invoke(new MethodInvoker(() =>
                        {
                            currUser.UserRating = newR;
                            gameForm.Hide();
                            this.Show();
                        }));
                        offer = false;
                        currentGame = null;
                    }
                    else if (MessTypeUtil.MessageCheck(allData, MessTypeUtil.TypeMEnum.Lose))
                    {
                        int newR = (int)SerializationUtils.DeSerialization(allData);
                        MessageBox.Show("Вы проиграли!");
                        this.Invoke(new MethodInvoker(() =>
                        {
                            currUser.UserRating = newR;
                            gameForm.Hide();
                            this.Show();
                        }));
                        offer = false;
                        currentGame = null;
                    }
                    else if (MessTypeUtil.MessageCheck(allData, MessTypeUtil.TypeMEnum.GMessage))
                    {
                        GameMessage gMess = (GameMessage)SerializationUtils.DeSerialization(allData);
                        this.Invoke(new MethodInvoker(() =>
                        {
                            gameForm.NewMessage(gMess.mess);
                        }));
                    }
                    else if (MessTypeUtil.MessageCheck(allData, MessTypeUtil.TypeMEnum.UpdateGames))
                    {
                        openedGames = (List<GInfo>)SerializationUtils.DeSerialization(allData);
                        this.Invoke(new MethodInvoker(() =>
                        {
                            lbGames.Items.Clear();
                            foreach (GInfo item in openedGames)
                            {
                                if (item.userOpp.UserNumber == currUser.UserNumber)
                                {
                                    btnCreateGame.Enabled = false;
                                    openedGame = item;
                                }
                                lbGames.Items.Add(item.userOpp.UserNick);
                            }
                        }));
                    }
                    else if (MessTypeUtil.MessageCheck(allData, MessTypeUtil.TypeMEnum.LoginError))
                    {
                        this.Invoke(new MethodInvoker(() =>
                        {
                            authForm.LoginError();
                        }));
                    }
                    else if (MessTypeUtil.MessageCheck(allData, MessTypeUtil.TypeMEnum.LoginAccomplish))
                    {
                        //MessageBox.Show("Hiii");
                        currUser = (UserInfo)SerializationUtils.DeSerialization(allData);
                        this.Invoke(new MethodInvoker(() =>
                        {
                            lblUser.Text = "Игрок: " + currUser.UserNick;
                            authForm.Hide();
                            this.Show();
                        }));
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            //закрываем открытую игру
            if (openedGame != null)
                btnCancelGame.PerformClick();
            //удаляем с сервера
            SendData(MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Exit]);
            btnLogout.Enabled = false;
            btnSendMessage.Enabled = false;
            btnJoinGame.Enabled = false;
            btnCreateGame.Enabled = false;
            btnOfferGame.Enabled = false;
            btnCancelGame.Enabled = false;
            lbUsers.Items.Clear();
            lbGames.Items.Clear();
            currUser = null;
            connect = false;
            this.Hide();
            authForm.Show();
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            byte[] message = SerializationUtils.Serialization(
                MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Message], textBoxMessage.Text);
            SendData(message);
            textBoxMessage.Clear();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }

        private void btnOfferGame_Click(object sender, EventArgs e)
        {
            if (lbUsers.SelectedIndex == -1)
                return;
            if (currUser.UserNick == users[lbUsers.SelectedIndex].UserNick)
                return;
            UserInfo[] users1 = { users[lbUsers.SelectedIndex], currUser };
            byte[] message = SerializationUtils.Serialization(
                MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Offer], users1);
            SendData(message);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            authForm.Show();
        }

        private void lbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbUsers.SelectedIndex == -1) return;
            btnOfferGame.Enabled = true;
        }

        private void lbGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbGames.SelectedIndex == -1) return;
            btnJoinGame.Enabled = true;
        }

        public void SetField(byte[,] field)
        {
            FieldInfo fInfo = new FieldInfo(currUser, currentGame.gameID, field);
            byte[] message = SerializationUtils.Serialization(
                MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.SetField], fInfo);
            SendData(message);
        }

        public void NewMove(Move move)
        {
            move.gameID = currentGame.gameID;
            byte[] message = SerializationUtils.Serialization(
                MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Move], move);
            SendData(message);
        }

        public void SendGameMessage(string mess)
        {
            GameMessage gmess = new GameMessage(currUser.UserNick + ": " + mess, currentGame.gameID);
            byte[] message = SerializationUtils.Serialization(
                MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.GMessage], gmess);
            SendData(message);
        }

        private void btnCreateGame_Click(object sender, EventArgs e)
        {
            GInfo newGame = new GInfo(currUser, 0);
            byte[] message = SerializationUtils.Serialization(
                MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.OpenGame], newGame);
            SendData(message);
            btnCancelGame.Enabled = true;
        }

        private void btnJoinGame_Click(object sender, EventArgs e)
        {
            if (lbGames.SelectedIndex == -1) return;
            GInfo joinGame = openedGames[lbGames.SelectedIndex];
            if (joinGame.userOpp.UserNumber == currUser.UserNumber)
                return;
            byte[] message = SerializationUtils.Serialization(
                MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Join], joinGame);
            SendData(message);
        }

        private void btnCancelGame_Click(object sender, EventArgs e)
        {
            if (openedGame == null) return;
            byte[] message = SerializationUtils.Serialization(
                MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.CancelGame], openedGame);
            SendData(message);
            btnCancelGame.Enabled = false;
            btnCreateGame.Enabled = true;
            openedGame = null;
        }

        public void LoginUser(string Username, string Password)
        {
            if (!NewConnection()) return;
            AuthenticationInfo authInfo = new AuthenticationInfo(Username, Password);
            byte[] sendData = SerializationUtils.Serialization(
                MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.LogIn], authInfo);
            SendData(sendData);
        }

        public void RegisterUser(string Username, string Password)
        {
            if (!NewConnection()) return;
            AuthenticationInfo authInfo = new AuthenticationInfo(Username, Password);
            byte[] message = SerializationUtils.Serialization(
                MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.SignUp], authInfo);
            SendData(message);
        }

        public void AllClose()
        {
            this.Close();
        }

        private void Disconnect()
        {
            try
            {
                if (stream != null)
                {
                    SendData(MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Exit]);
                    stream.Close();
                }
                if (client != null)
                    client.Close();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private bool NewConnection()
        {
            if (connect) return true;
            try
            {
                if (stream != null)
                    stream.Close();
                if (client != null)
                    client.Close();

                client = new TcpClient();
                client.Connect(host, port);
                stream = client.GetStream();

                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start(); //старт потока
                receiveThread.IsBackground = true;

                btnLogout.Enabled = true;
                btnSendMessage.Enabled = true;
                btnCreateGame.Enabled = true;
                connect = true;
            }
            catch (SocketException)
            {
                MessageBox.Show("Не удается подключиться к серверу!" + "\r\nПовторите попытку попозже.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return true;
        }

        public void LeaveGame()
        {
            byte[] sendData = SerializationUtils.Serialization(
                MessTypeUtil.MessageTypes[(int)MessTypeUtil.TypeMEnum.Lose], currentGame);
            SendData(sendData);
        }
    }
}
