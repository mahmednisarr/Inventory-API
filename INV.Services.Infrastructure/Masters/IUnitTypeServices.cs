using INV.Dto;
using INV.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INV.Services.Infrastructure.Masters
{
    public interface IUnitTypeServices
    {
        Task<TResponse> GetAllUnitTypes();
        Task<TResponse> SaveOrUpdateUnitType(UnitTypeDto reqResponseDto, int usrID);
        Task<TResponse> DeleteUnitType(int ID, int usrID);
    }
}
