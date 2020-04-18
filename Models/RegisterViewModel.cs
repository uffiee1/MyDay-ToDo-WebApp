using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDayApp.Models
{
    public class RegisterViewModel
    {
        private string _FirstName;
        private string _LastName;
        private string _Email;
        private string _Password;
        private string _ConfirmPassword;

        [Required]
        public string FirstName
        {
            get { return this._FirstName; }
            set { _FirstName = value; }
        }

        [Required]
        public string LastName
        {
            get { return this._LastName; }
            set { _LastName = value; }
        }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email
        {
            get { return this._Email; }
            set { _Email = value; }
        }

        [Required]
        [DataType(DataType.Password)]
        public string Password
        {
            get { return this._Password; }
            set { _Password = value; }
        }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword
        {
            get { return this._ConfirmPassword; }
            set { _ConfirmPassword = value; }
        }
    }
}
