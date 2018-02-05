using Microsoft.Extensions.Logging;
using PetOwner.Application;
using PetOwner.Models;
using PetOwner.Repositories.Contracts;
using PetOwner.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Services.Implementations
{
    public class PetOwnerService : IPetOwnerService
    {
        private IPetOwnerRepository _repository;
        private ILogger _logger;

        /// <summary>
        /// PetOwnerService provides services to manipulate pet owner database
        /// </summary>
        /// <param name="repository">Pet owner repository</param>
        /// <param name="logger"></param>
        public PetOwnerService(IPetOwnerRepository repository, ILogger<App> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// GetPetNamesByOwnerGender
        /// Queries the pet owner database for a list of all the pet names
        /// grouped by the gender of the owner
        /// </summary>
        /// <returns>Enumeration of PetNamesByOwnerGender</returns>
        public IEnumerable<PetNamesByOwnerGender> GetPetNamesByOwnerGender()
        {
            try
            {
                var owners = _repository.GetOwners();
                return QueryPetNamesByOwnerGender(owners);
            }

            catch (Exception ex)
            {
                _logger.LogError($"GetPetNamesByOwner failed: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// GetPetNamesByOwnerGenderAsync
        /// Asynchronously queries the pet owner database for a list of all the pet names
        /// grouped by the gender of the owner
        /// </summary>
        /// <returns>Enumeration of PetNamesByOwnerGender</returns>
        public async Task<IEnumerable<PetNamesByOwnerGender>> GetPetNamesByOwnerGenderAsync()
        {
            try
            {
                var owners = await _repository.GetOwnersAsync();
                return QueryPetNamesByOwnerGender(owners);
            }

            catch (Exception ex)
            {
                _logger.LogError($"GetPetNamesByOwnerAsync failed: {ex.Message}");
                throw;
            }
        }

        private IEnumerable<PetNamesByOwnerGender> QueryPetNamesByOwnerGender(IEnumerable<Owner> owners)
        {
            // Flatten out the gender and pet name (SelectMany)
            // then group the names by the gender (GroupBy)
            // The names in each gender group might have duplicates
            // so a distinct list of names is retrieved

            return owners
                .Where(po => po.Pets != null)
                .SelectMany(po => po.Pets, (owner, pet) => new { owner.Gender, pet.Name })
                .GroupBy(po => po.Gender, p => p.Name, (key, g) => new PetNamesByOwnerGender { Gender = key, Names = g.OrderBy(p => p).Distinct() })
                .OrderBy(po => po.Gender);
        }
    }
}
