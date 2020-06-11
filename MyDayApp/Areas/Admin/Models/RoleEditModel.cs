using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDayApp.Areas.Admin.Models
{
    public class RoleEditModel
    {
        public RoleEditModel()
        {
            UserList = new List<string>();
        }

        public string RoleId { get; set; }

        [Required(ErrorMessage = "Role Name is required")]
        public string RoleName { get; set; }

        public List<string> UserList { get; set; }
    }
}
