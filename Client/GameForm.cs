using System;
using System.Drawing;
using System.Windows.Forms;
using NetworkGame;

namespace Client
{
    public partial class GameForm : Form
    {
        private GameVisualization pbGame;
        MainForm mainForm;
        bool Move = false;

        public GameForm(MainForm form)
        {
            InitializeComponent();
            mainForm = form;
        }

       new public void Refresh()
        {
            pbGame.gameField1.InitRandom();
            pbGame.gameField2.Clear();
            pbGame.Invalidate();
            tbGameChat.Clear();
            tbGameMessage.Clear();
            btnStartGame.Enabled = true;
            btnGenerateNewField.Enabled = true;
            btnCancelStart.Enabled = false;
            Move = false;
            lblStatus.Text = "Статус:\n\r\t настройка игры.";
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            GameField gf = new GameField();
            gf.InitRandom();
            pbGame = new GameVisualization(gf, new GameField());
            pbGame.Parent = this;
            pbGame.Size = new Size(720, 350);
            pbGame.Location = new Point(50, 0);
            pbGame.MouseDown += pbGame_MouseDown;
        }

        void pbGame_MouseDown(object sender, MouseEventArgs e)
        {
            if (!Move) return;
            int a, b;
            bool bc = pbGame.GetCellCoordinates(e.X, e.Y, out a, out b);
            if (!bc && a != 0 && b != 0)
            {
                if (!pbGame.gameField2.CheckCell(a, b)) return;
                Move move = new Move(a, b, 0, 0);
                mainForm.NewMove(move);
            }
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //mainForm.Show();
        }

        public void SetOpp(string str)
        {
            lblOpp.Text = "Cоперник:\n\r\t " + str;
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            mainForm.SetField(pbGame.gameField1.Field);
            btnCancelStart.Enabled = true;
            btnStartGame.Enabled = false;
            btnGenerateNewField.Enabled = false;
            lblStatus.Text = "Статус:\n\r\t ожидание.";
        }

        public void SetGame(bool m)
        {
            Move = m;
            if (Move)
                lblStatus.Text = "Статус:\r\n\t ваш ход.";
            else
                lblStatus.Text = "Статус:\r\n\t ход соперника.";
        }

        private void btnGenerateNewField_Click(object sender, EventArgs e)
        {
            pbGame.gameField1.InitRandom();
            pbGame.gameField2.Clear();
            pbGame.Invalidate();
        }

        public void AttackF(Move move)
        {
            SetGame(!pbGame.gameField1.Attack(move.a, move.b));
            pbGame.Invalidate();
        }

        public void MarkF(Move move)
        {
            if (move.result == GameField.Hit)
                pbGame.gameField2.HitCell(move.a, move.b);
            if (move.result == GameField.Miss)
            {
                pbGame.gameField2.MissCell(move.a, move.b);
                SetGame(false);
            }
            pbGame.Invalidate();
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            mainForm.SendGameMessage(tbGameMessage.Text);
            tbGameMessage.Clear();
        }

        public void NewMessage(string str)
        {
            tbGameChat.Text = DateTime.Now.ToShortTimeString() + " "
                                + str + "\r\n"
                                + tbGameChat.Text;
        }

        private void btnCancelStart_Click(object sender, EventArgs e)
        {
            mainForm.LeaveGame();
        }
    }
}
