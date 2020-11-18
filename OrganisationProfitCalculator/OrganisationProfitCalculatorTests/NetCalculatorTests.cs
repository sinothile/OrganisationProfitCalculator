using NUnit.Framework;
using OrganisationProfitCalculator;

namespace OrganisationProfitCalculatorTests
{
    [TestFixture]
    public class NetCalculatorTests
    {
        [Test]
        public void Given_null_officeName_Should_return_nettProfit_zero()
        {
            //Arrange  
            var fileName = @"Documents\Question 1 input.csv";
            var nettCalculator = NettCalculator();

            //Act
            var actual = nettCalculator.GetNettProfit(fileName,null);

            //Assert
            var expected = 0;
            Assert.AreEqual(expected, actual);
        }

        [TestCase("invalid")]
        [TestCase("does not exist")]
        public void Given_invalid_officeName_Should_return_nettProfit_zero(string officeName)
        {
            //Arrange 
            var fileName = @"Documents\Question 1 input.csv";
            var nettCalculator = NettCalculator();

            //Act
            var actual = nettCalculator.GetNettProfit(fileName, officeName);

            //Assert
            var expected = 0;
            Assert.AreEqual(expected, actual);
        }

        [TestCase("HeadOffice", 627)]
        [TestCase("Western Cape",182)]
        [TestCase("Cape Town", 162)]
        [TestCase("Northern Suburbs", 15)]
        public void Given_officeName_Should_get_nettProfit(string officeName, double expected)
        {
            //Arrange
            var fileName = @"Documents\Question 1 input.csv";
            var nettCalculator = NettCalculator();

            //Act
            var actual = nettCalculator.GetNettProfit(fileName, officeName);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase("HEadOffiCE", 627)]
        [TestCase("western cape", 182)]
        [TestCase("Cape Town", 162)]
        [TestCase("NORTHERN SUBURBS", 15)]
        public void Given_officeName_with_any_cases_Should_get_nettProfit(string officeName, double expected)
        {
            //Arrange
            var fileName = @"Documents\Question 1 input.csv";
            var nettCalculator = NettCalculator();

            //Act
            var actual = nettCalculator.GetNettProfit(fileName, officeName);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase("HeadOffice     ", 627)]
        [TestCase("   Western Cape", 182)]
        [TestCase(" Cape Town", 162)]
        [TestCase("Northern Suburbs ", 15)]
        public void Given_officeName_with_trailing_spaces_Should_get_nettProfit(string officeName, double expected)
        {
            //Arrange 
            var fileName = @"Documents\Question 1 input.csv";
            var nettCalculator = NettCalculator();

            //Act
            var actual = nettCalculator.GetNettProfit(fileName, officeName);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase("WesternCape", 182)]
        [TestCase("CAPETOWN", 162)]
        [TestCase("NorthernSuburbs", 15)]
        [TestCase("northernsuburbs", 15)]
        public void Given_officeName_with_no_spaces_Should_get_nettProfit(string officeName, double expected)
        {   
            //Arrange   
            var fileName = @"Documents\Question 1 input.csv";
            var nettCalculator = NettCalculator();

            //Act
            var actual = nettCalculator.GetNettProfit(fileName, officeName);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Given_fileName_Should_return_office_with_largest_nett_profit()
        {
            //Arrange   
            var fileName = @"Documents\Question 2 input.csv";
            var nettCalculator = NettCalculator();

            //Act
            var actual = nettCalculator.FindLargestNettProfit(fileName);

            //Assert
            var expected = "Office With The Largest Nett Profit Is: HeadOffice With The Nett Profit Of: 177";
            Assert.AreEqual(expected, actual);
        }

        private NettCalculator NettCalculator()
        {
            return new NettCalculator();
        }
    }
}
