using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class Material
    {
        [Key]
        public int MaterialID { get; set; }

        public int MonsterID { get; set; }

        [Required(ErrorMessage = "Please enter the materials name.")]
        [MaxLength(30, ErrorMessage = "Name cannot be longer than 30 characters.")]
        [Display(Name = "Material Name")]
        public string MaterialName { get; set; }

        [Required(ErrorMessage = "Please enter the price of the material.")]
        [Range(0, 100000, ErrorMessage = "Price range must be between 0 and 100000")]
        public int Price { get; set; }

        public bool Active { get; set; }
    }
    
    public class MaterialVM : Material
    {
        public List<Drop> DropRates { get; set; }
        public List<PartDrop> PartDropRates { get; set; }
        [Display(Name = "Monster Name")]
        public string MonsterName { get; set; }
    }
}
