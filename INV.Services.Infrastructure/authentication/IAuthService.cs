using System.Threading.Tasks;
using INV.Dto;
using INV.Dto.Auth;

namespace INV.Services.Infrastructure.authentication
{
    public interface IAuthService
    {
        TResponse Index();

        Task<(AuthenticateResponse, int, string)> Authenticate(AuthenticateRequest model, string ipAddress);

        Task<bool> ValidateLogin(string authKey, int userId, string compName);
        Task<bool> ValidateRole(string roles, string path);
        
    }
}