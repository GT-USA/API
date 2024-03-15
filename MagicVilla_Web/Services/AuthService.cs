using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Services.IServices;
using MagicVilla_WEB.Models.DTO;

namespace MagicVilla_Web.Services
{
    public class AuthService : BaseServices, IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string villaUrl;

        public AuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration):base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPIURL");
        }

        // LOGIN AND REGISTER IS HTTPGET !!!!!!
        public Task<T> LoginAsync<T>(LoginDTORequest loginDTORequest)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = loginDTORequest,
                Url = villaUrl + "/api/UserAuth/login" // route from UsersAPIController + http postname
            });
        }

        public Task<T> RegisterAsync<T>(RegisterDTORequest localUserDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = localUserDTO,
                Url = villaUrl + "/api/UserAuth/register" // route from UsersAPIController + http postname
            });
        }
    }
}
