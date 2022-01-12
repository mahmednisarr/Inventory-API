using INV.Dto;
using INV.Dto.Request;
using INV.Helper.JWT;
using INV.Services.Infrastructure.Masters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INV.Controllers
{
    public class RackController : BaseController
    {

        private IRackServices rackServices;
        public RackController(IRackServices _rackServices)
        {
            rackServices = _rackServices;
        }

        [HttpGet]
        [Authorize]
        public async Task<TResponse> GetAllRacks() {
            return await rackServices.GetAllRacks();
        }

     

        [HttpPost]
        [Authorize]
        public async Task<TResponse> SaveOrUpdateRack(RackDto reqResponseDto)
        {
            return await rackServices.SaveOrUpdateRack(reqResponseDto, CurrentUser.Id);
        }

        [HttpDelete]
        [Authorize]
        public async Task<TResponse> DeleteRack(int ID)
        {
            return await rackServices.DeleteRack(ID, CurrentUser.Id);
        }

    }
}
