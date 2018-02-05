using Newtonsoft.Json;
using PetOwner.Configuration;
using PetOwner.Repositories.Implementations;
using PetOwnerTests;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PetOwner
{
    public class PetOwnerRepositoryTests
    {
        private string MockPetOwnerUrl = "test://agl-developer-test.azurewebsites.net/people.json";

        private string ExpectedResponse = 
@"[
  {
    ""Name"": ""Bob"",
    ""Gender"": ""Male"",
    ""Age"": 23,
    ""Pets"": [
      {
        ""Name"": ""Garfield"",
        ""Type"": ""Cat""
      },
      {
        ""Name"": ""Fido"",
        ""Type"": ""Dog""
      }
    ]
  },
  {
    ""Name"": ""Jennifer"",
    ""Gender"": ""Female"",
    ""Age"": 18,
    ""Pets"": [
      {
        ""Name"": ""Garfield"",
        ""Type"": ""Cat""
      }
    ]
  },
  {
    ""Name"": ""Steve"",
    ""Gender"": ""Male"",
    ""Age"": 45,
    ""Pets"": null
  },
  {
    ""Name"": ""Fred"",
    ""Gender"": ""Male"",
    ""Age"": 40,
    ""Pets"": [
      {
        ""Name"": ""Tom"",
        ""Type"": ""Cat""
      },
      {
        ""Name"": ""Max"",
        ""Type"": ""Cat""
      },
      {
        ""Name"": ""Sam"",
        ""Type"": ""Dog""
      },
      {
        ""Name"": ""Jim"",
        ""Type"": ""Cat""
      }
    ]
  },
  {
    ""Name"": ""Samantha"",
    ""Gender"": ""Female"",
    ""Age"": 40,
    ""Pets"": [
      {
        ""Name"": ""Tabby"",
        ""Type"": ""Cat""
      }
    ]
  },
  {
    ""Name"": ""Alice"",
    ""Gender"": ""Female"",
    ""Age"": 64,
    ""Pets"": [
      {
        ""Name"": ""Simba"",
        ""Type"": ""Cat""
      },
      {
        ""Name"": ""Nemo"",
        ""Type"": ""Fish""
      }
    ]
  }
]";

        private void SetupRequest()
        {
            WebRequest.RegisterPrefix("test", new MockWebRequestCreate());
            MockWebRequestCreate.CreateTestRequest(ExpectedResponse);
        }

        [Fact]
        public void GetOwners_ExpectedResponse_Returned()
        {
            // Arrange
            SetupRequest();
            var settings = new PetOwnerRepositorySettings { PetOwnerUrl = MockPetOwnerUrl };
            var repo = new PetOwnerRepository(settings);

            // Act
            var owners = repo.GetOwners();

            // Assert
            var jsonResult = JsonConvert.SerializeObject(owners, Formatting.Indented);
            Assert.Equal(jsonResult, ExpectedResponse);
        }
        
        [Fact]
        public async Task GetOwnersAsync_ExpectedResponse_Returned()
        {
            // Arrange
            SetupRequest();
            var settings = new PetOwnerRepositorySettings { PetOwnerUrl = MockPetOwnerUrl };
            var repo = new PetOwnerRepository(settings);

            // Act
            var owners = await repo.GetOwnersAsync();

            // Assert
            var jsonResult = JsonConvert.SerializeObject(owners, Formatting.Indented);
            Assert.Equal(jsonResult, ExpectedResponse);
        }
    }
}
