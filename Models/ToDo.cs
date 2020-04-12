using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDayApp.Models
{
    public class ToDo
    {
        private int _ID;
        private string _Event;
        private DateTime _startDateTime;
        private DateTime _endDateTime;
        private string _Location;
        private int _Status;

        public int ID
        {
            get { return this._ID; }
            set { _ID = value; }
        }

        public string Event
        {
            get { return this._Event; }
            set { _Event = value; }
        }

        public DateTime StartDateTime
        {
            get { return this._startDateTime; }
            set { _startDateTime = value; }
        }

        public DateTime EndDateTime
        {
            get { return this._endDateTime; }
            set { _endDateTime = value; }
        }

        public string Location
        {
            get { return this._Location; }
            set { _Location = value; }
        }

        public int Status
        {
            get { return this._Status; }
            set { _Status = value; }
        }
    }
}
