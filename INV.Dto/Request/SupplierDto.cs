using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INV.Dto.Request
{
    public class SupplierDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Telno{ get; set; }
        public string Email{ get; set; }
        public string Website{ get; set; }
        public string Fax{ get; set; }
        public string Address{ get; set; }
        public int  AreaID{ get; set; }
        public string PocName{ get; set; }
        public string PocDesign{ get; set; }
        public string PocPhone{ get; set; }
        public string PocEmail{ get; set; }
        public string License{ get; set; }
        public string GstNo{ get; set; }
        public string TinNo{ get; set; }
        public string VatNo{ get; set; }
        public string BankName { get; set; }
        public string BankIfsc{ get; set; }
        public string BankAcc{ get; set; }
        public string BankBenifName{ get; set; }
        public bool Status{ get; set; }

    }
}
