using INV.Dto;
using INV.Dto.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INV.Services.Infrastructure.Masters
{
   public  interface IItemGroupServices
    {
        Task<TResponse> GetAllItemGroup(int LocID);
        Task<TResponse> GetItemGroups(int LocID);
        Task<TResponse> SaveOrUpdateItemGroup(ItemGroupDto reqResponseDto, int usrID, int LocID);
        Task<TResponse> DeleteItemGroup(int ID, int usrID);
    }
}
