using System;

namespace INV.Dto.Auth
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string UserCode { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Roles { get; set; }
        public string LocIDs { get; set; }
        public string LocID { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Token { get; set; }
    }
}

