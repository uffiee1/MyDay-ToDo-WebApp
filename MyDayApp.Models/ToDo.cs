using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyDayApp.Models
{
    public class ToDo
    {
        public int ID { get; set; }

        [Required]
        public string Event { get; set; }

        public string Location { get; set; }

        public int Status { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        
        //[Required]
        public DataType StartDateTime { get; set; }

        public DataType EndDateTime { get; set; }
    }
}
