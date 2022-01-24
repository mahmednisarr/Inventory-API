using INV.Dto;
using INV.Dto.Masters;
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
    public class TaxController : BaseController
    {

        private ITaxServices taxServices;
        public TaxController(ITaxServices _taxServices)
        {
            taxServices = _taxServices;
        }

        [HttpGet]
        [Authorize]
        public async Task<TResponse> GetAllTax() {
            return await taxServices.GetAllTax();
        }

        [HttpPost]
        [Authorize]
        public async Task<TResponse> SaveOrUpdateUnit(Item reqDto)
        {
            return await taxServices.SaveOrUpdateItem(reqDto, CurrentUser.Id);
        }

        [HttpDelete]
        [Authorize]
        public async Task<TResponse> DeleteUnit(int ID)
        {
            return await taxServices.DeleteTax(ID, CurrentUser.Id);
        }
    }
}
