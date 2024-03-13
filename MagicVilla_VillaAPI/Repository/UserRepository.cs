using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models.DTO;
using MagicVilla_VillaAPI.Repository.IRepository;

namespace MagicVilla_VillaAPI.Repository
{
    public class UserRepository :IUserRepository
    {
        private readonly AppDBContext _db;
        //to access the secret when we generate token
        private string secretKey;
        public UserRepository(AppDBContext db, IConfiguration configuration)
        {
            _db = db;
            //to access the secret when we generate token
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
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
            //to access the secret when we generate token 
            //for login request, get password and username from DB
            var user = _db.LocalUser.FirstOrDefault(u=>u.UserName.ToLower()==loginDTORequest.UserName.ToLower()
            && u.Password == loginDTORequest.Password);
            if (user == null) 
            {
                return null;
            }

            //if user was found generate JWT token

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
