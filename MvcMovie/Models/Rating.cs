using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcMovie.Models
{
    public class Rating
    {
        public string userName { get; set; }
        public int ID { get; set; }
        public int MovieId { get; set; }
        [Range(1, 5), Required]
        public int OneToFive { get; set; }
        public virtual Movie Movie { get; set; }
    }

    
}
