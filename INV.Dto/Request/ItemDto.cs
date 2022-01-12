using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INV.Dto.Request
{
    public class ItemDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string HSNCode { get; set; }
        public int GrpID { get; set; }
        public string Grp { get; set; }
        public string UnitType { get; set; }
        public int UnitTypeid { get; set; }
        public int LocID { get; set; }
        public int UsrID { get; set; }
        public string Usr { get; set; }
        public string Date { get; set; }
        public bool Status { get; set; }
    }
}
