using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INV.Dto.Request
{
    public class DepartmentDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int LocID { get; set; }
        public bool Status { get; set; }
    }
}
