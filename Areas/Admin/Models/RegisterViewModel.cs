﻿using System;
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
        private bool _IsEmailVerified;

        public System.DateTime DateOfBirth { get; set; }
        //Could have
        public System.Guid ActivationCode { get; set; }

        [Key]
        public int UserID
        {
            get { return this._UserID; }
            set { _UserID = value; }
        }

        public string FirstName
        {
            get { return this._FirstName; }
            set { _FirstName = value; }
        }

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

        public string Email
        {
            get { return this._Email; }
            set { _Email = value; }
        }

        public string Password
        {
            get { return this._Password; }
            set { _Password = value; }
        }

        public bool IsEmailVerified
        {
            get { return this._IsEmailVerified; }
            set { _IsEmailVerified = value; }
        }

        public string ConfirmPassword { get; internal set; }

        //public User()
        //{
        //}
    }
}