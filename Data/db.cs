using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDayApp.Data
{
    public class db
    {
        //Database Connection
        MySqlConnection con = new MySqlConnection(dbConnection.GetConnectionString());

        public UserInformation TryLogin(string QueryString)
        {
            UserInformation user = new UserInformation();

            MySqlCommand cmd = new MySqlCommand(QueryString, con);

            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                user.Username = reader.GetString("username");
                user.Password = reader.GetString("password");
                user.UserID = Convert.ToInt32(reader.GetString("id"));
            }
            con.Close();
            return user;
        }
    }
}
