using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYTimesSearch.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Search news")]
        public string SearchItem { get; set; }
    }
}