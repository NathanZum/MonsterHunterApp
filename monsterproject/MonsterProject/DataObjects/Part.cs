using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Part
    {
        public int PartID { get; set; }
        public int MonsterID { get; set; }
        public string PartName { get; set; }
        public int Fire { get; set; }
        public int Water { get; set; }
        public int Thunder { get; set; }
        public int Ice { get; set; }
        public int Dragon { get; set; }
        public int Cut { get; set; }
        public int Blunt { get; set; }
        public int Ammo { get; set; }
    }

    public class PartVM : Part
    {
        public string MonsterName { get; set; }
    }
}
