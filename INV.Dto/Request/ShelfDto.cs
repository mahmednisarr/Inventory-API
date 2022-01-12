using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INV.Dto.Request
{
    public class ShelfDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int RackID { get; set; }
        public string RackName { get; set; }
        public bool Status { get; set; }
    }
}
