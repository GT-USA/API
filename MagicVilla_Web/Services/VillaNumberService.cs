using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class VillaNumberService : BaseServices, IVillaNumberService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string villaNumberUrl;
        //IConfiguration is created to get value from appsettings.json
        public VillaNumberService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            villaNumberUrl = configuration.GetValue<string>("ServiceUrls:VillaAPIURL");
        }
        public Task<T> CreateAsync<T>(VillaNumberDTOCreate dto)
        {
            //Inside BaseService
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = villaNumberUrl + "/api/VillaNumberAPI" // route from VillaNumberAPIController
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            //Inside BaseService
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = villaNumberUrl + "/api/VillaNumberAPI/" + id // route from VillaNumberAPIController
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            //Inside BaseService
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villaNumberUrl + "/api/VillaNumberAPI" // route from VillaNumberAPIController
            });

        }

        public Task<T> GetAsync<T>(int id)
        {
            //Inside BaseService
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villaNumberUrl + "/api/VillaNumberAPI/" + id // route from VillaNumberAPIController
            });
        }

        public Task<T> UpdateAsync<T>(VillaNumberDTOUpdate dto)
        {
            //Inside BaseService
            return SendAsync<T>(new APIRequest()
            {
                //Based on VillaAPIController
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = villaNumberUrl + "/api/VillaNumberAPI/" + dto.VillaNo // route from VillaNumberAPIController
            });
        }
    }
}
