using MagicVilla_VillaAPI.Models.DTO;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        bool isUniqueUser(string username);
        
        //Login return type
        Task<LoginDTOResponse> Login(LoginDTORequest loginDTORequest);
        
        //Return user that has been created in DB
        Task<LocalUser>Register(RegisterationDTORequest registerationDTORequest);
    }
}
