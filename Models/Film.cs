using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace test2019.Models
{
    public class Film
    {
        [Key]
        public int IDFilm { get; set; }
        public string Denumire { get; set; }
    }

    
}