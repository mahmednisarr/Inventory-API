using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INV.Dto.Request
{
    public class UnitDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int UnitTypeID { get; set; }
        public string UnitType { get; set; }
        public string  Conversion { get; set; }
        public bool Status { get; set; }
    }
}
