using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDayApp.Data
{
    public class UserInformation
    {
        private MySqlConnection con = new MySqlConnection("host=localhost;user=root;password=;database=mydaydb;");

        private int UserId;
        private string email;
        private string username;
        private string password;
        private string name;
        private string surname;

        public int UserID
        {
            get { return this.UserId; }
            set { UserId = value; }
        }

        public string Email
        {
            get { return this.email; }
            set { email = value; }
        }

        public string Username
        {
            get { return this.username; }
            set { username = value; }
        }

        public string Password
        {
            get { return this.password; }
            set { password = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { name = value; }
        }

        public string Surname
        {
            get { return this.surname; }
            set { surname = value; }
        }

        public UserInformation()
        {

        }
        private UserInformation(string GetUsername, string GetPassword)
        {
            Username = GetUsername;
            Password = GetPassword;
        }

        public Task<UserInformation> TryLogin(string username, string password)
        {
            UserInformation CurrentUser = new UserInformation();
            string QueryString = $"SELECT* FROM `user` WHERE `username`= '{username}' AND `password`= '{password}'";

            MySqlCommand cmd = new MySqlCommand(QueryString, con);

            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                CurrentUser.email = reader.GetString("email");
                CurrentUser.Password = reader.GetString("password");
                CurrentUser.UserID = Convert.ToInt32(reader.GetString("id"));
            }
            con.Close();
            return null;
        }
    }
}
