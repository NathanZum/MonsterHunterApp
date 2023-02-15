using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Drop
    {
        public int MaterialID { get; set; }
        public int DropTypeID { get; set; }
        public string DropTypeName { get; set; }
        public decimal DropRate { get; set; }
    }
}
