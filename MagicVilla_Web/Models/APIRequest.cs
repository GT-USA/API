using Microsoft.AspNetCore.Mvc;
using static MagicVilla_Utility.SD;

namespace MagicVilla_Web.Models
{
    public class APIRequest
    {
        // request type of api
        public ApiType ApiType { get; set; } = ApiType.GET;
        // request url
        public string Url { get; set; }
        //request data object
        public object Data { get; set; }
    }
}
