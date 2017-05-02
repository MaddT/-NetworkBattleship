using System;
using System.Data.OleDb;
using NetworkGame;

namespace Server
{
    //класс для работы с БД
    public class DatabaseManager
    {
        private OleDbConnection connection;

        public bool SuccessfulConnection
        {
            get;
            private set;
        }

        //строка подключения к БД
        private const string connString = @"Provider=Microsoft.ACE.OLEDB.12.0;
                                                Data Source=../../files/players.accdb";

        public DatabaseManager()
        {
            this.connection = new OleDbConnection(connString);
            try
            {
                connection.Open();
                SuccessfulConnection = true;
            }
            catch (OleDbException)
            {
                SuccessfulConnection = false;
            }
        }

        //регистрация новоого игрока
        public UserInfo RegisterNewAccount(string Username, string Password)
        {
            string command = "INSERT INTO Users (Username, [Password], Rating, Loses, WIns) VALUES (";
            command += "'" + Username + "'";
            command += ", '" + Password + "'";
            command += ", " + (1000).ToString();
            command += ", " + (0).ToString();
            command += ", " + (0).ToString() + ")";
            OleDbCommand comm = new OleDbCommand(command, connection);
            try
            {
                comm.ExecuteNonQuery();
            }
            catch (OleDbException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            UserInfo user = new UserInfo(Username, 0, 1000);
            return user;
        }

        //авторизация
        public UserInfo LoginAccount(string Username, string Password)
        {
            string command = "SELECT * FROM Users WHERE ";
            command += "Users.Username = '" + Username + "' ";
            command += " AND Users.[Password] = '" + Password + "'";
            OleDbCommand comm = new OleDbCommand(command, connection);
            OleDbDataReader reader;
            try
            {
                reader = comm.ExecuteReader();
            }
            catch (OleDbException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            if (!reader.Read()) return null;
            UserInfo user = new UserInfo(Username, 0, int.Parse(reader["Rating"].ToString()));
            return user;
        }

        //изменение райтинга в БД
        public void UpdateRating(UserInfo user, int newRating)
        {
            string command = "UPDATE Users SET Rating = ";
            command += newRating.ToString();
            command += " WHERE Username = '" + user.UserNick + "'";
            OleDbCommand comm = new OleDbCommand(command, connection);
            OleDbDataReader reader;
            try
            {
                reader = comm.ExecuteReader();
            }
            catch (OleDbException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
