using PetOwner.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetOwner.Services.Contracts
{
    public interface IPetOwnerService
    {
        IEnumerable<PetNamesByOwnerGender> GetPetNamesByOwnerGender();
        Task<IEnumerable<PetNamesByOwnerGender>> GetPetNamesByOwnerGenderAsync();
    }
}
