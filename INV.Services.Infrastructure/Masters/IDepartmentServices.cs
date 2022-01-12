using INV.Dto;
using INV.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INV.Services.Infrastructure.Masters
{
    public interface IDepartmentServices
    {
        Task<TResponse> GetAllDepartments(int LocID);
        Task<TResponse> SaveOrUpdateDepartment(DepartmentDto reqResponseDto, int usrID, int LocID);
        Task<TResponse> DeleteDepartment(int ID, int usrID);
    }
}
