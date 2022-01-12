using INV.Dto;
using INV.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INV.Services.Infrastructure.Masters
{
    public interface IShelfServices
    {
        Task<TResponse> GetAllShelfs();
        Task<TResponse> SaveOrUpdateShelf(ShelfDto reqResponseDto, int usrID);
        Task<TResponse> DeleteShelf(int ID, int usrID);
    }
}
