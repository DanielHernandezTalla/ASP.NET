﻿using MVC_Udemy.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Udemy.Models
{
    public class Cinema: IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Cinema Logo")]
        public string Logo { get; set; }
     
        [Display(Name = "Name")]
        public string Name{ get; set; }
        
        [Display(Name = "Description")]
        public string Description{ get; set; }

        // Relationships
        public List<Movie> Movies { get; set;  }

    }
}
