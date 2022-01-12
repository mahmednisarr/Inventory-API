using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INV.Dto.Masters
{
   public  class ItemGroupDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }
    }
}
