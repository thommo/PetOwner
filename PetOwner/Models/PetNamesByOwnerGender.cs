using System.Collections.Generic;

namespace PetOwner.Models
{
    public class PetNamesByOwnerGender
    {
        public string Gender { get; set; }
        public IEnumerable<string> Names { get; set; }
    }
}
