using INV.Dto;
using INV.Dto.Request;
using INV.Helper.JWT;
using INV.Services.Infrastructure.Masters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace INV.Controllers
{
    public class ItemController : BaseController
    {

        #region Constructor
        
        public ItemController(IItemServices itemServices)
        {
            _itemServices = itemServices;
        }

        #endregion

        #region Members

        private readonly IItemServices _itemServices;
        private TResponse _response = new();

        #endregion

        
        #region ActionMethod

        [HttpGet]
        [Authorize]
        public async Task<TResponse> GetAllItem()
        {
            return await _itemServices.GetAllItems(CurrentUser.LocID);
        }


        [HttpPost]
        [Authorize]
        public async Task<TResponse> SaveOrUpdateItem(ItemDto reqResponseDto)
        {
            return await _itemServices.SaveOrUpdateItem(reqResponseDto, CurrentUser.Id, CurrentUser.LocID);
        }


        [HttpDelete]
        [Authorize]
        public async Task<TResponse> DeleteItem(int ID)
        {
            return await _itemServices.DeleteItem(ID, CurrentUser.Id);
        }

        #endregion
    }
}