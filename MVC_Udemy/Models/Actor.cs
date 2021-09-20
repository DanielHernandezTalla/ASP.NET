﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Udemy.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "Profile Picture is required")]
        public string ProfilePictureURL { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "The full name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full name must be between 3 and 50 chars")]
        public string FullName { get; set; }
    
        [Display(Name = "Biography")]
        [Required(ErrorMessage = "The Biography is required")]
        public string Bio { get; set; }

        // Relationships 
        public List<Actor_Movie> Actor_Movies { get; set; }
    }

}
