//
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MyDayApp.Models;

namespace MyDayApp.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        
        
        
        
        //public dbConnection(string value) => Value = value;

        //public string Value { get; }

        //private static string ConnectionString;

        //public static void SetConnectionString(string connection)
        //{
        //    ConnectionString = connection;
        //}
        //public static string GetConnectionString()
        //{
        //    return ConnectionString;
        //}

        /// <summary>
        /// The User for the application
        /// </summary>
        public DbSet<RegisterViewModel> User { get; set; } 

    }
}
