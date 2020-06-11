using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyDayApp.Models;


namespace MyDayApp.DataAccess
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> User { get; set; }

        public DbSet<ToDo> ToDo { get; set; }
        public DbSet<Label> Label { get; set; }
        public DbSet<Status> Status { get; set; }
    }
}