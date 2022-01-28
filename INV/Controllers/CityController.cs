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
    public class CityController : BaseController
    {

        private ICityService cityService;
        public CityController(ICityService _cityService)
        {
            cityService = _cityService;
        }

        
     

        [HttpGet]
        [Authorize]
        public async Task<TResponse> GetCityByPincode(int pincode)
        {
            return await cityService.GetCityByPincode(pincode);
        }

    }
}
