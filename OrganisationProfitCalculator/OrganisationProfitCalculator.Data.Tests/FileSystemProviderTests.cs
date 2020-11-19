using NUnit.Framework;

namespace OrganisationProfitCalculator.Data.Tests
{
    [TestFixture]
    public class FileSystemProviderTests
    {
        [Test]
        public void Given_valid_fileName_Should_return_file_path()
        {
            //Arrange  
            var fileName = @"Documents\Question 1 input.csv";
            var fileSystemProvider = new FileSystemProvider();

            //Act   
            var actual = fileSystemProvider.GetFile(fileName);

            //Assert
            Assert.IsNotEmpty(actual);
        }

        [Test]
        public void Given_filePath_Should_read_file()   
        {
            //Arrange  
            var fileName = @"Documents\Question 1 input.csv";
            var fileSystemProvider = new FileSystemProvider();
            var filePath = fileSystemProvider.GetFile(fileName);

            //Act   
            var actual = fileSystemProvider.ReadFile(filePath);

            //Assert
            var expected = 3;
            Assert.AreEqual(actual.Columns.Count, expected);
        }
    }
}
