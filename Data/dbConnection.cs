using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MyDayApp.Data
{
    public class dbConnection : IdentityDbContext
    {
        //public dbConnection(string value) => Value = value;

        //public string Value { get; }

        private static string ConnectionString;

        public static void SetConnectionString(string connection)
        {
            ConnectionString = connection;
        }
        public static string GetConnectionString()
        {
            return ConnectionString;
        }
    }
}
