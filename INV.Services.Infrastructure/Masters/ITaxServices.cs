using INV.Dto;
using INV.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INV.Services.Infrastructure.Masters
{
    public interface ITaxServices
    {
        Task<TResponse> GetAllTax(int LocID);
        Task<TResponse> SaveOrUpdateItem(ItemDto reqResponseDto, int usrID, int LocID);
        Task<TResponse> DeleteTax(int ID, int usrID);
    }
}