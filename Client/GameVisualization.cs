using System.Drawing;
using System.Windows.Forms;
using NetworkGame;

namespace Client
{
    //класс отвечающий за отрисовку игрового поля
    public class GameVisualization : PictureBox
    {

        public GameField gameField1 { get; private set; }
        public GameField gameField2 { get; private set; }

        private char[] bukv = { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ж', 'З', 'И', 'К' };

        private Graphics gr;

        public GameVisualization(GameField gf1, GameField gf2)
        {
            gameField1 = gf1;
            gameField2 = gf2;

            gr = this.CreateGraphics();
        }

        public bool GetCellCoordinates(int x, int y, out int a, out int b)
        {
            a = 0;
            b = 0;

            int height = this.ClientSize.Height;
            int width = this.ClientSize.Width;
            int t = this.ClientSize.Height / 100;

            x = x - (t / 2 + t * 10) - t / 2;
            y = y - t / 2 - t * 10;
            if (y > 0)
                b = (int)(y / ((height - t * 10 - t) / 10.0)) + 1;
            else
                b = 0;
            if (b == 11) b = 10;

            if (x > 0 && x < height - t * 10 - t)
            {
                a = (int)(x / ((height - t * 10 - t) / 10.0)) + 1;
                return true;
            }
            else if (x > width - t * 2.5 - height && x < width - t * 2.5 - t * 10)
                a = (int)((x - (int)(width - t * 2.5 - height)) / ((height - t * 10 - t) / 10.0)) + 1;
            else
                a = 0;

            return false;
        }

        private void PaintFields(Graphics g)
        {
            Pen p = new Pen(Brushes.Black);
            p.Width = this.ClientSize.Height / 100;
            Font f = new Font("Consolas", p.Width * 4, FontStyle.Bold | FontStyle.Regular);
            int t = (int)p.Width;
            int height = this.ClientSize.Height;
            int width = this.ClientSize.Width;

            g.DrawRectangle(p, new Rectangle(new Point(t / 2 + t * 10, t / 2 + t * 10),
                new Size(height - t - t * 10, height - t - t * 10)));
            g.DrawRectangle(p, new Rectangle(new Point(width - t - height - t + t * 10, t / 2 + t * 10),
                new Size(height - t * 10, height - t - t * 10)));
            for (int i = 1; i <= 10; i++)
            {
                g.DrawString(bukv[i - 1].ToString(), f, Brushes.Black, new Point(0 + i * (height - t - t * 10) / 10 + 5, (height - t - t * 10) / 30));
                g.DrawString(bukv[i - 1].ToString(), f, Brushes.Black, new Point(width - height - t + i * (height - t - t * 10) / 10 + 5, (height - t - t * 10) / 30));
                g.DrawString((i).ToString(), f, Brushes.Black, new Point(t, i * (height - t * 10) / 10 + t));
                g.DrawString((i).ToString(), f, Brushes.Black, new Point(width - height - t, i * (height - t * 10) / 10 + t));
                if (i == 10) continue;
                g.DrawLine(p, new Point(0 + t * 10, 0 + i * (height - t - t * 10) / 10 + t * 10),
                    new Point(height - t, 0 + i * (height - t - t * 10) / 10 + t * 10));
                g.DrawLine(p, new Point(width - height - t * 2 + t * 10, 0 + i * (height - t - t * 10) / 10 + t * 10),
                    new Point(width - t * 2, 0 + i * (height - t - t * 10) / 10 + t * 10));
                g.DrawLine(p, new Point(t * 10 + i * (height - t - t * 10) / 10, t * 10),
                    new Point(t * 10 + i * (height - t - t * 10) / 10, height - t));
                g.DrawLine(p, new Point(t * 10 + width - height + i * (height - t - t * 10) / 10 - t, t * 10),
                    new Point(t * 10 + width - height + i * (height - t - t * 10) / 10 - t, height - t));
            }
        }

        private void PaintCells(Graphics g)
        {
            int t = this.ClientSize.Height / 100;
            int height = this.ClientSize.Height;
            int width = this.ClientSize.Width;

            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    switch (gameField1.Field[i, j])
                    {
                        case GameField.Empty:
                            g.FillRectangle(Brushes.LightBlue,
                                new Rectangle(new Point(0 + t + t * 10 + (int)((height - t * 10) / 9.8) * i, t * 10 + t + (int)((height - t * 10) / 9.8) * j), new Size((int)((height - t * 10) / 9.8) - t * 3, (int)((height - t * 10) / 9.8) - t * 3)));
                            break;
                        case GameField.Ship:
                            g.FillRectangle(Brushes.DarkOliveGreen,
                                new Rectangle(new Point(0 + t + t * 10 + (int)((height - t * 10) / 9.8) * i, t * 10 + t + (int)((height - t * 10) / 9.8) * j), new Size((int)((height - t * 10) / 9.8) - t * 3, (int)((height - t * 10) / 9.8) - t * 3)));
                            break;
                        case GameField.Hit:
                            g.FillRectangle(Brushes.Red,
                                new Rectangle(new Point(0 + t + t * 10 + (int)((height - t * 10) / 9.8) * i, t * 10 + t + (int)((height - t * 10) / 9.8) * j), new Size((int)((height - t * 10) / 9.8) - t * 3, (int)((height - t * 10) / 9.8) - t * 3)));
                            break;
                        case GameField.Miss:
                            g.FillRectangle(Brushes.LightBlue,
                                new Rectangle(new Point(0 + t + t * 10 + (int)((height - t * 10) / 9.8) * i, t * 10 + t + (int)((height - t * 10) / 9.8) * j), new Size((int)((height - t * 10) / 9.8) - t * 3, (int)((height - t * 10) / 9.8) - t * 3)));
                            g.FillEllipse(Brushes.Black,
                                new Rectangle(new Point(0 + t + t * 10 + (int)((height - t * 10) / 9.8) * i + t, t + t * 10 + t + (int)((height - t * 10) / 9.8) * j), new Size((int)((height - t * 10) / 9.8) - t * 5, (int)((height - t * 10) / 9.8) - t * 5)));
                            break;
                    }
                }

            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    switch (gameField2.Field[i, j])
                    {
                        case GameField.Empty:
                            g.FillRectangle(Brushes.LightBlue,
                                new Rectangle(new Point(width - height - t / 2 + t * 10 + (int)((height - t * 10) / 9.8) * i, t * 10 + t + (int)((height - t * 10) / 9.8) * j), new Size((int)((height - t * 10) / 9.8) - t * 3, (int)((height - t * 10) / 9.8) - t * 3)));
                            break;
                        case GameField.Ship:
                            g.FillRectangle(Brushes.DarkOliveGreen,
                                new Rectangle(new Point(width - height - t / 2 + t * 10 + (int)((height - t * 10) / 9.8) * i, t * 10 + t + (int)((height - t * 10) / 9.8) * j), new Size((int)((height - t * 10) / 9.8) - t * 3, (int)((height - t * 10) / 9.8) - t * 3)));
                            break;
                        case GameField.Hit:
                            g.FillRectangle(Brushes.Red,
                                new Rectangle(new Point(width - height - t / 2 + t * 10 + (int)((height - t * 10) / 9.8) * i, t * 10 + t + (int)((height - t * 10) / 9.8) * j), new Size((int)((height - t * 10) / 9.8) - t * 3, (int)((height - t * 10) / 9.8) - t * 3)));
                            break;
                        case GameField.Miss:
                            g.FillRectangle(Brushes.LightBlue,
                                new Rectangle(new Point(width - height - t / 2 + t * 10 + (int)((height - t * 10) / 9.8) * i, t * 10 + t + (int)((height - t * 10) / 9.8) * j), new Size((int)((height - t * 10) / 9.8) - t * 3, (int)((height - t * 10) / 9.8) - t * 3)));
                            g.FillEllipse(Brushes.Black,
                                new Rectangle(new Point(width - height - t / 2 + t * 10 + (int)((height - t * 10) / 9.8) * i + t, t + t * 10 + t + (int)((height - t * 10) / 9.8) * j), new Size((int)((height - t * 10) / 9.8) - t * 5, (int)((height - t * 10) / 9.8) - t * 5)));
                            break;
                    }
                }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            PaintFields(pe.Graphics);
            PaintCells(pe.Graphics);
        }
    }
}
