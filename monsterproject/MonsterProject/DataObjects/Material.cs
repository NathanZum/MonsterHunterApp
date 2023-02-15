using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Material
    {
        public int MaterialID { get; set; }
        public int MonsterID { get; set; }
        public string MaterialName { get; set; }
        public int Price { get; set; }
        public bool Active { get; set; }
    }
    
    public class MaterialVM : Material
    {
        public List<Drop> DropRates { get; set; }
        public List<PartDrop> PartDropRates { get; set; }
        public string MonsterName { get; set; }
    }
}
