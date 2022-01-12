using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INV.Dto.Request
{
    public class RackDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int DeptID { get; set; }
        public string DeptName { get; set; }
        public bool Status { get; set; }
    }
}
