using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDayApp.Models
{
    public class ToDo
    {
        public int ID { get; set; }

        public string Event { get; set; }

        public string Location { get; set; }

        public int Status { get; set; }

        //public DataType StartDateTime { get; set; }

        //public DataType EndDateTime { get; set; }

        //public virtual ApplicationUser User { get; set; }
    }
}
