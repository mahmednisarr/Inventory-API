using INV.Dto;
using INV.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INV.Services.Infrastructure.Masters
{
    public interface IRackServices
    {
        Task<TResponse> GetAllRacks();
        Task<TResponse> SaveOrUpdateRack(RackDto reqResponseDto, int usrID);
        Task<TResponse> DeleteRack(int ID, int usrID);
    }
}
