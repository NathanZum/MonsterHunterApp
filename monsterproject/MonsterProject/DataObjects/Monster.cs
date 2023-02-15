using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataObjects
{
    public class Monster
    {
        public int MonsterID { get; set; }
        public int UserID { get; set; }
        public string MonsterName { get; set; }
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
