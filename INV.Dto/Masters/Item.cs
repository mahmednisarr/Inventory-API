using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INV.Dto.Masters
{
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal value { get; set; }
        public string type { get; set; }
        public bool status { get; set; }
        public int usrid { get; set; }
    }
}
