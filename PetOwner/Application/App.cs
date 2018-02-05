using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PetOwner.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace PetOwner.Application
{
    public class App
    {
        private IPetOwnerService _petOwnerService;
        private ILogger _logger;

        /// <summary>
        /// App constructor
        /// </summary>
        /// <param name="petOwnerService"></param>
        public App(IPetOwnerService petOwnerService, ILogger<App> logger)
        {
            _petOwnerService = petOwnerService;
            _logger = logger;
        }

        /// <summary>
        /// Entry point to run application
        /// 
        /// Calls pet owner service to retrieve pets grouped by gender of owner
        /// and write results to console
        /// </summary>
        public void Run()
        {
            try
            {
                var result = _petOwnerService.GetPetNamesByOwnerGender();
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred, see log for details");
                _logger.LogError($"Application aborted with exception: {ex.Message}");
            }
        }

        public async Task RunAsync()
        {
            try
            {
                var result = await _petOwnerService.GetPetNamesByOwnerGenderAsync();
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred, see log for details");
                _logger.LogError($"Application aborted with exception: {ex.Message}");
            }
        }
    }
}
