using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Entities
{
    public enum CuisineType
    {
        none,
        Itallian,
        French,
        Japanese,
        American
    }
    public class Restaurant
    {
        public int Id { get; set; }

        [Required, MaxLength(80)]
        [Display(Name ="Restaurant Name"] //on the html, don't display the text "Name" on the input, display "Restaurant Name"
        public string Name { get; set; }
        public CuisineType Cuisine { get; set; }
    }
}
