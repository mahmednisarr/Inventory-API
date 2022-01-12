using INV.Dto;
using INV.Dto.Request;
using INV.Helper.JWT;
using INV.Services.Infrastructure.Masters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace INV.Controllers
{
    public class SupplierController : BaseController
    {

        #region Constructor
        
        public SupplierController(ISupplierServices supplierServices)
        {
            _supplierServices = supplierServices;
        }

        #endregion

        #region Members

        private readonly ISupplierServices _supplierServices;
        private TResponse _response = new();

        #endregion

        
        #region ActionMethod

        [HttpGet]
        [Authorize]
        public async Task<TResponse> GetAllSupplier()
        {
            return await _supplierServices.GetAllSupplier(CurrentUser.LocID);
        }


        [HttpPost]
        [Authorize]
        public async Task<TResponse> SaveOrUpdateSupplier(SupplierDto reqResponseDto)
        {
            return await _supplierServices.SaveOrUpdateSupplier(reqResponseDto, CurrentUser.Id, CurrentUser.LocID);
        }


        [HttpDelete]
        [Authorize]
        public async Task<TResponse> DeleteSupplier(int ID)
        {
            return await _supplierServices.DeleteSupplier(ID, CurrentUser.Id);
        }

        #endregion
    }
}