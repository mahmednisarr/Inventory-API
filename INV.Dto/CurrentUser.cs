using System;

namespace INV.Dto
{
    public class CurrentUser
    {
        public int Id { get; set; }
        public string RoleIDs { get; set; }
        public string CompName { get; set; }
        public string LocIDs { get; set; }
        public int LocID { get; set; }
        public string AuthKey { get; set; }
    }


}