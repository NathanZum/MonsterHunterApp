using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class WPFUser
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public bool Active { get; set; }
        public List<string> Roles { get; set; }
    }
}
