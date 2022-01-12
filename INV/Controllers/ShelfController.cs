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
    public class ShelfController : BaseController
    {

        private IShelfServices shelfServices;
        public ShelfController(IShelfServices _shelfServices)
        {
            shelfServices = _shelfServices;
        }

        [HttpGet]
        [Authorize]
        public async Task<TResponse> GetAllShelfs() {
            return await shelfServices.GetAllShelfs();
        }

     

        [HttpPost]
        [Authorize]
        public async Task<TResponse> SaveOrUpdateShelf(ShelfDto reqResponseDto)
        {
            return await shelfServices.SaveOrUpdateShelf(reqResponseDto, CurrentUser.Id);
        }

        [HttpDelete]
        [Authorize]
        public async Task<TResponse> DeleteShelf(int ID)
        {
            return await shelfServices.DeleteShelf(ID, CurrentUser.Id);
        }

    }
}
