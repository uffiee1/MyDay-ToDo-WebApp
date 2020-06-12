using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDayApp.Models
{
    public class LoginViewModel
    {
        private string _Username;
        private string _Password;
        private bool _Remember;

        [Required]
        //[EmailAddress]
        public string Username
        {
            get { return this._Username; }
            set { _Username = value; }
        }

        [Required]
        [DataType(DataType.Password)]
        public string Password
        {
            get { return this._Password; }
            set { _Password = value; }
        }

        [Display(Name = "Onthoud mijn Email")]
        public bool Remember
        {
            get { return this._Remember; }
            set { _Remember = value; }
        }
    }
}
