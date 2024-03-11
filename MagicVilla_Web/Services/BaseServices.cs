using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using Newtonsoft.Json;
using System.Text;

namespace MagicVilla_Web.Services
{
    public class BaseServices : IBaseService
    {
        public APIResponse responseModel { get; set; }
        //API Http call
        public IHttpClientFactory httpClientFactory { get; set; }
        public BaseServices(IHttpClientFactory httpClient)
        {
            this.responseModel = new();
            this.httpClientFactory = httpClient;
        }

        //Generic method to Call API endpoint
        //getting reponse back, deserializing and return it back
        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            //Create Client to call API
            try
            {
                var client = httpClientFactory.CreateClient("MagicAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                //Header Type
                message.Headers.Add("Accept", "application/json");
                //URL to call API
                message.RequestUri = new Uri(apiRequest.Url);
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }
                //Http Type
                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;

                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;

                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                //Response by default
                HttpResponseMessage apiResponse = null;

                // call API end Point to response API message
                //Logs can create in here
                //CHECK ERRORS
                apiResponse = await client.SendAsync(message);

                //Extract API Content
                var apiContent = await apiResponse.Content.ReadAsStringAsync();

                try
                {
                    //Deserialize it and model will be APIResponse APIResponse
                    APIResponse ApiResponse = JsonConvert.DeserializeObject<APIResponse>(apiContent);
                    if (apiResponse.StatusCode == System.Net.HttpStatusCode.BadRequest
                        || apiResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        ApiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        ApiResponse.IsSuccess = false;
                        var res = JsonConvert.SerializeObject(ApiResponse);
                        var returnObj = JsonConvert.DeserializeObject<T>(res);
                        return returnObj;
                    }
                }
                catch (Exception ex)
                {
                    //Deserialize it and model will be APIResponse T
                    var exAPIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                    return exAPIResponse;
                }

                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return APIResponse;

            }
            catch (Exception ex)
            {
                var dto = new APIResponse
                {
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false,
                };

                var res = JsonConvert.SerializeObject(dto);
                var ApiResponse = JsonConvert.DeserializeObject<T>(res);
                return ApiResponse;
            }
        }
    }
}
