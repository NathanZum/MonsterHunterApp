using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class Terrain
    {
        [Key]
        public int TerrainID { get; set; }

        [Required(ErrorMessage = "Please enter the terrains name.")]
        [MaxLength(30, ErrorMessage = "Name cannot be longer than 30 characters.")]
        [Display(Name = "Terrain Name")]
        public string TerrainName { get; set; }
    }
}
