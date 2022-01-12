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
        public string Code { get; set; }
        public string Phone { get; set; }
        public string Add { get; set; }
        public int CityID { get; set; }
        public string City { get; set; }
        public string Contname { get; set; }
        public string ContPhone { get; set; }
        public string DefaultOpeningBal { get; set; }
        public string TinNo { get; set; }
        public string Email { get; set; }
        public string PWD { get; set; }
        public string GSTIN { get; set; }
        public string Date { get; set; }
        public string UsrID { get; set; }
        public string CrLimit { get; set; }
        public string TDS { get; set; }
        public string ITPanNo { get; set; }
        public string Design { get; set; }
        public bool Status { get; set; }
    }
}
