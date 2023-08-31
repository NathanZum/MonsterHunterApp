using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class PartDrop
    {
        [Key]
        public int MaterialID { get; set; }
        [Key]
        public int PartID { get; set; }

        [Display(Name = "Part Name")]
        public string PartName { get; set; }

        [Display(Name = "Drop Rate")]
        public decimal DropRate { get; set; }
    }

}
