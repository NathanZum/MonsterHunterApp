using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class Drop
    {
        [Key]
        public int MaterialID { get; set; }
        [Key]
        public int DropTypeID { get; set; }
        public string DropTypeName { get; set; }
        public decimal DropRate { get; set; }
    }
}
