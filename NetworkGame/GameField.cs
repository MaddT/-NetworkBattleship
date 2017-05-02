using System;

namespace NetworkGame
{
    //модель игрового поля для игры "Морской Бой"
    public class GameField
    {
        //расположение корабля
        public enum ShipPos { Horizontal = 0, Vertical = 1 };
        //размер корабля
        public enum ShipSize { Tiny = 0, Small = 1, Medium = 2, Big = 3 };

        public const byte Empty = 0;
        public const byte Ship = 1;
        public const byte Hit = 2;
        public const byte Miss = 3;

        private const byte BigC = 1;
        private const byte MediumC = 2;
        private const byte SmallC = 3;
        private const byte TinyC = 4;

        private byte[,] field = new byte[10, 10];   //игровое поле

        public byte[,] Field
        {
            get { return field; }
        }

        public GameField() { }

        public GameField(byte[,] field)
        {
            if (10 != field.GetLength(1) || field.GetLength(0) != 10)
                throw new Exception("Данный массив не подходит для инициализации игрового поля!");
            this.field = field;
        }

        //поместить корабль в позицию (x, y)
        public bool PlaceShip(int x, int y, ShipSize ss, ShipPos sp)
        {
            x--; y--;
            //проверка границ
            if ((x < 0 || y < 0) ||
                (x > 9 || y > 9) ||
                (x + (int)ss * (sp == ShipPos.Horizontal ? 1 : 0) > 9) ||
                (y + (int)ss * (sp == ShipPos.Vertical ? 1 : 0)) > 9) return false;

            int x1 = x, y1 = y;
            for (int i = 0; i < (int)ss + 1; i++)
            {
                //есть ли корабли ближе чем одна клетка
                if (x1 - 1 >= 0 && field[x1 - 1, y1] == Ship) return false;
                if (x1 + 1 <= 9 && field[x1 + 1, y1] == Ship) return false;
                if (y1 - 1 >= 0 && field[x1, y1 - 1] == Ship) return false;
                if (y1 + 1 <= 9 && field[x1, y1 + 1] == Ship) return false;               
                if ((x1 - 1 >= 0 && y1 - 1 >= 0) && field[x1 - 1, y1 - 1] == Ship) return false;
                if ((x1 - 1 >= 0 && y1 + 1 <= 9) && field[x1 - 1, y1 + 1] == Ship) return false;
                if ((x1 + 1 <= 9 && y1 - 1 >= 0) && field[x1 + 1, y1 - 1] == Ship) return false;
                if ((x1 + 1 <= 9 && y1 + 1 <= 9) && field[x1 + 1, y1 + 1] == Ship) return false;
                if (field[x1, y1] == Ship) return false;
                if (sp == ShipPos.Horizontal) x1++;
                else y1++;
            }

            //поместить корабль
            for (int i = 0; i < (int)ss + 1; i++)
            {
                field[x, y] = Ship;
                if (sp == ShipPos.Horizontal) x++;
                else y++;
            }

            return true;
        }

        //атаковать позицию (x, y)
        public bool Attack(int x, int y)
        {
            x--; y--;
            if ((x < 0 || y < 0) ||
                (x > 9 || y > 9)) return false;

            if (field[x, y] == Ship)
                field[x, y] = Hit;
            else if (field[x, y] == Empty)
            {
                field[x, y] = Miss;
                return false;
            }
            else
                return false;

            return true;
        }

        //проверить: была ли атака в (x, y)
        public bool CheckCell(int x, int y)
        {
            x--; y--;
            if ((x < 0 || y < 0) ||
                (x > 9 || y > 9)) return false;

            if (field[x, y] == Hit || field[x, y] == Miss)
                return false;

            return true;
        }

        //попадание
        public void HitCell(int a, int b)
        {
            a--; b--;
            field[a, b] = Hit;
        }

        //промах
        public void MissCell(int a, int b)
        {
            a--; b--;
            field[a, b] = Miss;
        }

        //проверить условие победы
        public bool CheckWin()
        {
            int n = 0;
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    if (field[i, j] == Hit) n++;
            if (n == 20) return true;
            return false;
        }

        public void Clear()
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    field[i, j] = 0;
        }

        //заполнить поле кораблями случайно
        public void InitRandom()
        {
            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    field[i, j] = 0;
            int k = 0;
            while (true)
            {
                if (PlaceShip(rnd.Next(10) + 1, rnd.Next(10) + 1, ShipSize.Big, (ShipPos)rnd.Next(2)))
                    k++;
                if (k == BigC) break;
            }
            k = 0;
            while (true)
            {
                if (PlaceShip(rnd.Next(10) + 1, rnd.Next(10) + 1, ShipSize.Medium, (ShipPos)rnd.Next(2)))
                    k++;
                if (k == MediumC) break;
            }
            k = 0;
            while (true)
            {
                if (PlaceShip(rnd.Next(10) + 1, rnd.Next(10) + 1, ShipSize.Small, (ShipPos)rnd.Next(2)))
                    k++;
                if (k == SmallC) break;
            }
            k = 0;
            while (true)
            {
                if (PlaceShip(rnd.Next(10) + 1, rnd.Next(10) + 1, ShipSize.Tiny, (ShipPos)rnd.Next(2)))
                    k++;
                if (k == TinyC) break;
            }
        }
    }
}
