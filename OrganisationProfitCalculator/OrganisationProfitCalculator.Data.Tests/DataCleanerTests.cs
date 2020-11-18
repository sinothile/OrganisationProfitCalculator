using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace OrganisationProfitCalculator.Data.Tests
{
    [TestFixture]
    public class DataCleanerTests
    {
        [TestCase("Western Cape  ", "westerncape")]
        [TestCase("  Cape TOWN", "capetown")]
        [TestCase("Bellville", "bellville")]
        [TestCase("LONDON","london")]
        public void Given_officeName_Should_clean_the_name_to_make_it_uniform(string officeName, string expected)
        {
            //Arrange  
            var dataCleaner = new DataCleaner();    
                
            //Act           
            var actual = dataCleaner.CleanData(officeName);

            //Assert
            Assert.AreEqual(expected,actual);
        }
    }
}
