using MagicVilla_Web.Models.DTO;

namespace MagicVilla_WEB.Models.DTO
{
    public class LoginDTOResponse
    {
        public LocalUserDTO User { get; set; }
        public string Token { get; set; }
    }
}
