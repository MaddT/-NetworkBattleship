using System;

namespace NetworkGame
{
    [Serializable]
    public class GInfo
    {
        public UserInfo userOpp;
        public int gameID;

        public GInfo(UserInfo usr, int id)
        {
            userOpp = usr;
            gameID = id;
        }
    }
}
