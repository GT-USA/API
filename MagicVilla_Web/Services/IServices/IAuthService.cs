using MagicVilla_Web.Models.DTO;
using MagicVilla_WEB.Models.DTO;

namespace MagicVilla_Web.Services.IServices
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginDTORequest loginDTORequest);
        Task<T> RegisterAsync<T>(RegisterDTORequest localUserDTO);
    }
}
