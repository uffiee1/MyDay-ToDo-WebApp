using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyDayApp.Models
{
    public class Role
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }


        public const string Administrator = "Admin";
        public const string Gebruiker = "User";
    }
}
