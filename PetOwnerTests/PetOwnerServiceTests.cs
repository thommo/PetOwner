using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using PetOwner.Application;
using PetOwner.Models;
using PetOwner.Repositories.Contracts;
using PetOwner.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PetOwnerTests
{
    public class PetOwnerServiceTests
    {
        private IPetOwnerRepository CreateRepo()
        { 
            var owners = new List<Owner>()
            {
                new Owner {
                    Gender = "Male",
                    Age = 20,
                    Name = "Tom",
                    Pets = new List<Pet> {
                        new Pet {Name="Cujo", Type = "Dog"},
                        new Pet{Name="Tweetie", Type = "Bird"}
                    }
                },
                new Owner {
                    Gender = "Female",
                    Age = 22,
                    Name = "Mary",
                    Pets = new List<Pet> {
                        new Pet {Name="Lily", Type = "Cat"},
                        new Pet {Name="Fluffy", Type = "Rabbit"}
                    }
                },
                new Owner {
                    Gender = "Female",
                    Age = 32,
                    Name = "Jane",
                    Pets = new List<Pet> {
                        new Pet {Name="Tweetie", Type = "Bird"}
                    }
                },
             new Owner {
                    Gender = "Male",
                    Age = 42,
                    Name = "Dick",
                    Pets = new List<Pet> {
                        new Pet {Name="Cujo", Type = "Dog"},
                        new Pet {Name="Milo", Type = "Rabbit"},
                        new Pet {Name="Bella", Type = "Dog"}
                    }
                },
            };

            var repo = new Mock<IPetOwnerRepository>();
            repo.Setup(r => r.GetOwners()).Returns(() => owners);
            repo.Setup(r => r.GetOwnersAsync()).Returns(async () => owners);
            return repo.Object;
        }

        private ILogger<App> CreateLogger()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<App>();
            return logger;
        }

        [Fact]
        public void GetPetNamesByOwnerGender_NameSort_CorrectOrder()
        {
            // Arrange
            var service = new PetOwnerService(CreateRepo(), CreateLogger());

            // Act
            var result = service.GetPetNamesByOwnerGender();

            // Assert
            Assert.Equal(result.Count(), 2);
            var elem = result.First();
            var names = new List<string> { "Fluffy", "Lily", "Tweetie" };
            Assert.Equal(elem.Gender, "Female");
            Assert.Equal(elem.Names.Count(), names.Count());
            Assert.True(Enumerable.SequenceEqual(elem.Names, names));

            elem = result.Last();
            names = new List<string> { "Bella", "Cujo", "Milo", "Tweetie" };
            Assert.Equal(elem.Gender, "Male");
            Assert.Equal(elem.Names.Count(), names.Count());
            Assert.True(Enumerable.SequenceEqual(elem.Names, names));
        }

        [Fact]
        public async Task GetPetNamesByOwnerGenderAsync_NameSort_CorrectOrder()
        {
            // Arrange
            var service = new PetOwnerService(CreateRepo(), CreateLogger());

            // Act
            var result = await service.GetPetNamesByOwnerGenderAsync();

            // Assert
            Assert.Equal(result.Count(), 2);
            var elem = result.First();
            var names = new List<string> { "Fluffy", "Lily", "Tweetie" };
            Assert.Equal(elem.Gender, "Female");
            Assert.Equal(elem.Names.Count(), names.Count());
            Assert.True(Enumerable.SequenceEqual(elem.Names, names));

            elem = result.Last();
            names = new List<string> { "Bella", "Cujo", "Milo", "Tweetie" };
            Assert.Equal(elem.Gender, "Male");
            Assert.Equal(elem.Names.Count(), names.Count());
            Assert.True(Enumerable.SequenceEqual(elem.Names, names));
        }

        [Fact]
        public void GetPetNamesByOwnerGender_NullRepo_Throws_NullReferencException()
        {
            // Arrange
            var service = new PetOwnerService(null, CreateLogger());

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => service.GetPetNamesByOwnerGender());
        }
        [Fact]
        public void GetPetNamesByOwnerGenderAsync_NullRepo_Throws_NullReferencException()
        {
            // Arrange
            var service = new PetOwnerService(null, CreateLogger());

            // Act & Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await service.GetPetNamesByOwnerGenderAsync());
        }
    }
}
