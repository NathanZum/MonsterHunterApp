using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class Weakness
    {
        [Key]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the weaknesses effectivness.")]
        [Range(0, 1000, ErrorMessage ="Weakness must be between 0 and 1000")]
        public int Effectiveness { get; set; }
    }
}
