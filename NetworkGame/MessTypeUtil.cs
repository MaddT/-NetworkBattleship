namespace NetworkGame
{
    //коды сообщений между сервером и клиентом
    public static class MessTypeUtil
    {
        public enum TypeMEnum
        {
            Entrance = 0, Exit = 1, Message = 2, Offer = 3,
            Accept = 4, NewGame = 5, SetField = 6, Start = 7,
            Move = 8, Attack = 9, Win = 10, Lose = 11, GMessage = 12,
            OpenGame = 13, UpdateGames = 14, Join = 15, CancelGame = 16,
            LogIn = 17, SignUp = 18, LoginError = 19, LoginAccomplish = 20
        };

        public static byte[][] MessageTypes = {
            new byte[] {1, 1, 1},
            new byte[] {3, 3, 3},
            new byte[] {7, 8, 9},
            new byte[] {5, 3, 7},
            new byte[] {7, 3, 5},

            new byte[] {7, 7, 7},
            new byte[] {5, 5, 5},
            new byte[] {1, 4, 8},
            new byte[] {4, 7, 9},
            new byte[] {4, 6, 1},

            new byte[] {5, 6, 1},
            new byte[] {6, 6, 1},
            new byte[] {6, 6, 3},
            new byte[] {6, 6, 5},
            new byte[] {6, 6, 7},

            new byte[] {6, 6, 9},
            new byte[] {7, 6, 1},
            new byte[] {7, 6, 3},
            new byte[] {7, 6, 5},
            new byte[] {7, 6, 7},

            new byte[] {7, 6, 9}
                                              };

        public static bool MessageCheck(byte[] code, TypeMEnum type)
        {
            if (code[0] == MessageTypes[(int)type][0]
                && code[1] == MessageTypes[(int)type][1]
                && code[2] == MessageTypes[(int)type][2])
                return true;
            return false;
        }
    }
}
