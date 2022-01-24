using INV.Dto;
using INV.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INV.Dto.Masters;

namespace INV.Services.Infrastructure.Masters
{
    public interface ITaxServices
    {
        Task<TResponse> GetAllTax();
        Task<TResponse> SaveOrUpdateItem(Item reqDto, int usrID);
        Task<TResponse> DeleteTax(int ID, int usrID);
    }
}