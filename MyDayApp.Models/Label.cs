using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyDayApp.Models
{
    public class Label
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Label")]
        public string Type { get; set; }
    }
}