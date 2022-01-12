using INV.Dto;
using INV.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INV.Services.Infrastructure.Masters
{
    public interface IUnitServices
    {
        Task<TResponse> GetAllUnits();
        Task<TResponse> SaveOrUpdateUnit(UnitDto reqResponseDto, int usrID);
        Task<TResponse> DeleteUnit(int ID, int usrID);
    }
}
