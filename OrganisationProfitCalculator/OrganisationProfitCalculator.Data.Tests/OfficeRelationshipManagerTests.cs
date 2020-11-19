using System.Collections.Generic;
using NUnit.Framework;
using OrganisationProfitCalculator.Data.Models;

namespace OrganisationProfitCalculator.Data.Tests
{
    [TestFixture]
    public class OfficeRelationshipManagerTests
    {
        [Test]
        public void Given_valid_officeName_and_all_offices_Should_return_descendants()
        {   
            //Arrange  
            var officeName = "KZN"; 
            var offices = GetOffices();

            var officeRelationshipManager = new OfficeRelationshipManager(new DataCleaner());

            //Act   
            var actual = officeRelationshipManager.GetDescendants(officeName, offices);

            //Assert
            var expected = new List<string>() { "kzn", "ethekwini", "pinetown" };
            Assert.AreEqual(expected,actual);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Given_empty_or_null_officeName_and_all_offices_Should_return_no_descendants(string officeName)
        {
            //Arrange  
            var offices = GetOffices();

            var officeRelationshipManager = new OfficeRelationshipManager(new DataCleaner());

            //Act   
            var actual = officeRelationshipManager.GetDescendants(officeName, offices);

            //Assert
            var expected = new List<string>{};
            Assert.AreEqual(expected, actual);
        }

        private static List<Office> GetOffices()
        {
            return new List<Office>()
            {
                new Office()
                {
                    Name = "KZN",
                    Parent = "South Africa",
                    Amount = 30
                },
                new Office()
                {
                    Name = "eThekwini",
                    Parent = "KZN",
                    Amount = 10
                },
                new Office()
                {
                    Name = "Pinetown",
                    Parent = "eThekwini",
                    Amount = 20
                }
            };
        }
    }
}
