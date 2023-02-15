using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class PartDrop
    {
        public int MaterialID { get; set; }
        public int PartID { get; set; }
        public string PartName { get; set; }
        public decimal DropRate { get; set; }
    }

}
