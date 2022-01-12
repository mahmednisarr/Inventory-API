using INV.Dto;
using INV.Dto.Masters;
using INV.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INV.Services.Infrastructure.Masters
{
   public  interface ISupplierServices
    {
        Task<TResponse> GetAllSupplier(int LocID);
        Task<TResponse> SaveOrUpdateSupplier(SupplierDto reqResponseDto, int usrID, int LocID);
        Task<TResponse> DeleteSupplier(int ID, int usrID);
    }
}
