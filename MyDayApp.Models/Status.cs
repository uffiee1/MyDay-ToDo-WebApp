using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyDayApp.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string Type { get; set; }
    }
}
