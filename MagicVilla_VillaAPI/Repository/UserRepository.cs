using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models.DTO;
using MagicVilla_VillaAPI.Repository.IRepository;

namespace MagicVilla_VillaAPI.Repository
{
    public class UserRepository :IUserRepository
    {
        private readonly AppDBContext _db;
        public UserRepository(AppDBContext db)
        {
            _db = db;
        }

        public bool isUniqueUser(string username)
        {
            throw new NotImplementedException();
        }

        public Task<LoginDTOResponse> Login(LoginDTORequest loginDTORequest)
        {
            throw new NotImplementedException();
        }

        public Task<LocalUser> Register(RegisterationDTORequest registerationDTORequest)
        {
            throw new NotImplementedException();
        }
    }
}
