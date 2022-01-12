using Microsoft.AspNetCore.Mvc;
using INV.Core;

using INV.Dto;
using INV.Services.Infrastructure.Masters;
using System.Threading.Tasks;
using INV.Dto.Masters;
using INV.Helper.JWT;
using INV.Dto.Request;

namespace INV.Controllers
{
    public class DepartmentController : BaseController
    {

        #region Constructor
        
       
        public DepartmentController(IDepartmentServices DepartmentServices)
        {
            _departmentServices = DepartmentServices;
        }

        #endregion

        #region Members

        private readonly IDepartmentServices _departmentServices;
        private TResponse _response = new();

        #endregion

        #region ActionMethod


        [HttpGet]
        [Authorize]
        public async Task<TResponse> GetDepartments()
        {
            return await _departmentServices.GetAllDepartments(CurrentUser.LocID);
        }
        //[FromHeader(Name = "CompName")]
        [HttpPost]
        [Authorize]
        public async Task<TResponse> SaveOrUpdateDepartment(DepartmentDto reqResponseDto)
        {
            return await _departmentServices.SaveOrUpdateDepartment(reqResponseDto, CurrentUser.Id, CurrentUser.LocID);
        }

        [HttpDelete]
        [Authorize]
        public async Task<TResponse> DeleteDepartment(int ID)
        {
            return await _departmentServices.DeleteDepartment(ID, CurrentUser.Id);
        }

        #endregion
    }
}