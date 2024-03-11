using MagicVilla_VillaAPI.Models;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {
        //Updates DB
        Task<VillaNumber> UpdateAsync(VillaNumber entity);
    }
}
