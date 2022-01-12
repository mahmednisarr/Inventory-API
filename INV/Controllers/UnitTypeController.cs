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
    public class UnitTypeController : BaseController
    {

        private IUnitTypeServices unitTypeServices;
        public UnitTypeController(IUnitTypeServices _unitTypeServices)
        {
            unitTypeServices = _unitTypeServices;
        }

        [HttpGet]
        [Authorize]
        public async Task<TResponse> GetAllUnitTypes() {
            return await unitTypeServices.GetAllUnitTypes();
        }

     

        [HttpPost]
        [Authorize]
        public async Task<TResponse> SaveOrUpdateUnitType(UnitTypeDto reqResponseDto)
        {
            return await unitTypeServices.SaveOrUpdateUnitType(reqResponseDto, CurrentUser.Id);
        }

        [HttpDelete]
        [Authorize]
        public async Task<TResponse> DeleteUnitType(int ID)
        {
            return await unitTypeServices.DeleteUnitType(ID, CurrentUser.Id);
        }

    }
}
