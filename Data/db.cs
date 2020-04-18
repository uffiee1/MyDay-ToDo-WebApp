using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MyDayApp.Models;

namespace MyDayApp.Data
{
    public class db
    {

        public partial class MyDatabaseEntities : System.Data.Entity.DbContext
        {
            public MyDatabaseEntities() : base("name=EUCOM")
            {
            }
    
            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                throw new UnintentionalCodeFirstException();
            }
    
            public virtual System.Data.Entity.DbSet<RegisterViewModel> Users { get; set; }
        }




        //////Database Connection
        //MySqlConnection con = new MySqlConnection(AppDbContext.GetConnectionString("mydaydb"));

        //public User TryLogin(string QueryString)
        //{
        //    User user = new User();

        //    MySqlCommand cmd = new MySqlCommand(QueryString, con);

        //    con.Open();
        //    MySqlDataReader reader = cmd.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        user.Username = reader.GetString("Username");
        //        user.Password = reader.GetString("Password");
        //        user.UserID = Convert.ToInt32(reader.GetString("id"));
        //    }
        //    con.Close();
        //    return user;
        //}
    }
}
