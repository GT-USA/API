using MagicVilla_Web.Models;

namespace MagicVilla_Web.Services.IServices
{
    public interface IBaseService
    {
        //Call API Response
        APIResponse responseModel { get; set; }
        //Call the API
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
