using PetOwner.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetOwner.Repositories.Contracts
{
    public interface IPetOwnerRepository
    {
        IEnumerable<Owner> GetOwners();
        Task<IEnumerable<Owner>> GetOwnersAsync();
    }
}
