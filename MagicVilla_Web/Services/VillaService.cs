using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class VillaService : BaseServices, IVillaService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string villaUrl;
        //IConfiguration is created to get value from appsettings.json
        public VillaService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPIURL");
        }
        public Task<T> CreateAsync<T>(VillaDTOCreate dto)
        {
            //Inside BaseService
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = villaUrl + "/api/VillaAPI" // route from VillaAPIController
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            //Inside BaseService
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = villaUrl + "/api/VillaAPI/" + id // route from VillaAPIController
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            //Inside BaseService
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "/api/VillaAPI" // route from VillaAPIController
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            //Inside BaseService
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "/api/VillaAPI/" + id // route from VillaAPIController
            });
        }

        public Task<T> UpdateAsync<T>(VillaDTOUpdate dto)
        {
            //Inside BaseService
            return SendAsync<T>(new APIRequest()
            {
                //Based on VillaAPIController
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = villaUrl + "/api/VillaAPI/" + dto.Id // route from VillaAPIController
            });
        }
    }
}
