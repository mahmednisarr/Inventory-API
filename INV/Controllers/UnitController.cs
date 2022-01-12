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
    public class UnitController : BaseController
    {

        private IUnitServices unitServices;
        public UnitController(IUnitServices _unitServices)
        {
            unitServices = _unitServices;
        }

        [HttpGet]
        [Authorize]
        public async Task<TResponse> GetAllUnits() {
            return await unitServices.GetAllUnits();
        }

     

        [HttpPost]
        [Authorize]
        public async Task<TResponse> SaveOrUpdateUnit(UnitDto reqResponseDto)
        {
            return await unitServices.SaveOrUpdateUnit(reqResponseDto, CurrentUser.Id);
        }

        [HttpDelete]
        [Authorize]
        public async Task<TResponse> DeleteUnit(int ID)
        {
            return await unitServices.DeleteUnit(ID, CurrentUser.Id);
        }

    }
}
