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

        public bool IsUniqueUser(string username)
        {
            //retrieve user from DB based on user
            var user = _db.LocalUser.FirstOrDefault(x=>x.UserName == username);
            if (user == null) 
            {
                return true;
            }
            return false;
        }

        public Task<LoginDTOResponse> Login(LoginDTORequest loginDTORequest)
        {
            throw new NotImplementedException();
        }

        public async Task<LocalUser> Register(RegisterationDTORequest registerationDTORequest)
        {
            //Add new user in Local DB and populate user based on RegistrationDTORequest
            LocalUser user = new()
            {
                UserName = registerationDTORequest.UserName,
                Password = registerationDTORequest.Password,
                Name = registerationDTORequest.Name,
                Role = registerationDTORequest.Role,
            };

            //Add database
            _db.LocalUser.Add(user);
            //Save changes
            await _db.SaveChangesAsync();

            //before return empty the password
            user.Password = "";
            //return user object
            return user;
        }
    }
}
