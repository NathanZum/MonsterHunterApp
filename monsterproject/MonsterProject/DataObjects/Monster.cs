using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace DataObjects
{
    public class Monster
    {
        [Key]
        public int MonsterID { get; set; }
        public int UserID { get; set; }

        [Required(ErrorMessage = "Please enter the monsters name.")]
        [MaxLength(30, ErrorMessage = "Name cannot be longer than 30 characters.")]
        [Display(Name = "Monster Name")]
        public string MonsterName { get; set; }

        [Required(ErrorMessage = "Please enter the type of monster.")]
        [MaxLength(15, ErrorMessage = "Type cannot be longer than 15 characters.")]
        [Display(Name = "Monster Type")]
        public string MonsterType { get; set; }
        public bool Active { get; set; }
        public List<Weakness> Weaknesses { get; set;}
    }

    public class MonsterVM : Monster
    {
        public List<Part> Parts { get; set; }
        public List<MaterialVM> Materials { get; set; }
        public List<Terrain> Terrains { get; set; }
    }
}
