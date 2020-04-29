using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyDayApp.Models
{
    public class RegisterViewModel
    {

        private int _UserID;
        private string _FirstName;
        private string _Lastname;
        private string _Username;
        private string _Email;
        private string _Password;


        [Key]
        public int UserID
        {
            get { return this._UserID; }
            set { _UserID = value; }
        }

        [Display(Name ="First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage ="First name is required")]
        public string FirstName
        {
            get { return this._FirstName; }
            set { _FirstName = value; }
        }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required")]
        public string LastName
        {
            get { return this._Lastname; }
            set { _Lastname = value; }
        }

        public string Username
        {
            get { return this._Username; }
            set { _Username = value; }
        }

        [Display(Name ="Email")]
        [Required(AllowEmptyStrings =false, ErrorMessage ="Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email
        {
            get { return this._Email; }
            set { _Email = value; }
        }

        [Required(AllowEmptyStrings =false, ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        public string Password
        {
            get { return this._Password; }
            set { _Password = value; }
        }

    }
}
