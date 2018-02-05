using Newtonsoft.Json;
using PetOwner.Configuration;
using PetOwner.Models;
using PetOwner.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PetOwner.Repositories.Implementations
{
    /// <summary>
    /// PetOwnerRepository implements an HTTP web request
    /// to retrieve a list of pet owners and their pets
    /// </summary>
    public class PetOwnerRepository : IPetOwnerRepository
    {
        private PetOwnerRepositorySettings _settings;

        public PetOwnerRepository(PetOwnerRepositorySettings settings)
        {
            _settings = settings;
        }

        /// <summary>
        /// GetOwners synchronously calls the pet owner web service
        /// </summary>
        /// <returns>An enumeration of Owners and pets</returns>
        public IEnumerable<Owner> GetOwners()
        {
            var request = CreatePetOwnerRequest();
            var response = request.GetResponse();
            return DeserializePetOwnerResponse(response);
        }

        /// <summary>
        /// GetOwners asynchronously calls the pet owner web service
        /// </summary>
        /// <returns>An enumeration of Owners and pets</returns>
        public async Task<IEnumerable<Owner>> GetOwnersAsync()
        {
            var request = CreatePetOwnerRequest();
            var response = await request.GetResponseAsync();
            return DeserializePetOwnerResponse(response);
        }

        private WebRequest CreatePetOwnerRequest()
        {
            var uri = new Uri(_settings.PetOwnerUrl);
            var request = WebRequest.Create(uri);
            if (_settings.UseProxy)
            {
                WebProxy myproxy = new WebProxy(_settings.ProxyAddress, _settings.ProxyPort);
                myproxy.Credentials = new NetworkCredential(_settings.ProxyUsername, _settings.ProxyPassword);
                request.Proxy = myproxy;
            }

            request.Method = "Get";
            return request;
        }

        private IEnumerable<Owner> DeserializePetOwnerResponse(WebResponse response)
        {
            var stream = response.GetResponseStream();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var result = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<List<Owner>>(result);
        }
    }
}
