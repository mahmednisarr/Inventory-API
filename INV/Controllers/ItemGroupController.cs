using Microsoft.AspNetCore.Mvc;
using INV.Core;

using INV.Dto;
using INV.Services.Infrastructure.Masters;
using System.Threading.Tasks;
using INV.Dto.Masters;
using INV.Helper.JWT;

namespace INV.Controllers
{
    public class ItemGroupController : BaseController
    {

        #region Constructor
        
       
        public ItemGroupController(IItemGroupServices itemGroupServices)
        {
            _itemGroupServices = itemGroupServices;
        }

        #endregion

        #region Members

        private readonly IItemGroupServices _itemGroupServices;
        private TResponse _response = new();

        #endregion

        #region ActionMethod

        [HttpGet]
        [Authorize]
        public async Task<TResponse> GetAllItemGroup()
        {
            return await _itemGroupServices.GetAllItemGroup(CurrentUser.LocID);
        }

        [HttpGet]
        [Authorize]
        public async Task<TResponse> GetItemGroups()
        {
            return await _itemGroupServices.GetItemGroups(CurrentUser.LocID);
        }

        [HttpPost]
        [Authorize]
        public async Task<TResponse> SaveOrUpdateItemGroup(ItemGroupDto reqResponseDto)
        {
            return await _itemGroupServices.SaveOrUpdateItemGroup(reqResponseDto, CurrentUser.Id, CurrentUser.LocID);
        }

        [HttpDelete]
        [Authorize]
        public async Task<TResponse> DeleteItemGroup(int ID)
        {
            return await _itemGroupServices.DeleteItemGroup(ID, CurrentUser.Id);
        }


        #endregion
    }
}