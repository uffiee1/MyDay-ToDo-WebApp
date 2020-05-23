using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDayApp.Areas.User.Models
{
    public partial class CreateRoleModel
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleNamee { get; set; }
    }
}
