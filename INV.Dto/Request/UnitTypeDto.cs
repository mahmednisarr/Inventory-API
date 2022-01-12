using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INV.Dto.Request
{
    public class UnitTypeDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string SIName { get; set; }
        public bool Status { get; set; }
    }
}
