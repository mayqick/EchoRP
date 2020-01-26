using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo_ClientSide.Models
{
    public class AccountModel
    {
        public int id { get; set; }
        public string license { get; set; }
        public string mail { get; set; }
        public int status { get; set; }
        public int donate { get; set; }
    }
}
