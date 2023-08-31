using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class Part
    {
        [Key]
        public int PartID { get; set; }
        public int MonsterID { get; set; }

        [Required(ErrorMessage = "Please enter the parts name.")]
        [MaxLength(30, ErrorMessage = "Name cannot be longer than 30 characters.")]
        [Display(Name = "Part Name")]
        public string PartName { get; set; }

        [Required(ErrorMessage = "Please enter the Fire weakness of the part.")]
        [Range(0, 100, ErrorMessage = "Fire weakness must be between 0 and 100")]
        public int Fire { get; set; }

        [Required(ErrorMessage = "Please enter the Water weakness of the part.")]
        [Range(0, 100, ErrorMessage = "Water weakness must be between 0 and 100")]
        public int Water { get; set; }

        [Required(ErrorMessage = "Please enter the Thunder weakness of the part.")]
        [Range(0, 100, ErrorMessage = "Thunder weakness must be between 0 and 100")]
        public int Thunder { get; set; }

        [Required(ErrorMessage = "Please enter the Ice weakness of the part.")]
        [Range(0, 100, ErrorMessage = "Ice weakness must be between 0 and 100")]
        public int Ice { get; set; }

        [Required(ErrorMessage = "Please enter the Dragon weakness of the part.")]
        [Range(0, 100, ErrorMessage = "Dragon weakness must be between 0 and 100")]
        public int Dragon { get; set; }

        [Required(ErrorMessage = "Please enter the Cut weakness of the part.")]
        [Range(0, 100, ErrorMessage = "Cut weakness must be between 0 and 100")]
        public int Cut { get; set; }

        [Required(ErrorMessage = "Please enter the Blunt weakness of the part.")]
        [Range(0, 100, ErrorMessage = "Blunt weakness must be between 0 and 100")]
        public int Blunt { get; set; }

        [Required(ErrorMessage = "Please enter the Ammo weakness of the part.")]
        [Range(0, 100, ErrorMessage = "Ammo weakness must be between 0 and 100")]
        public int Ammo { get; set; }
    }

    public class PartVM : Part
    {
        [Display(Name = "Monster Name")]
        public string MonsterName { get; set; }
    }
}
