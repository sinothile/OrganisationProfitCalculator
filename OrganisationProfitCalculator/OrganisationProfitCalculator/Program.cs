using System;
using OrganisationProfitCalculator.Data;
using OrganisationProfitCalculator.Data.Interfaces;
using OrganisationProfitCalculator.UseCase;

namespace OrganisationProfitCalculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IFileSystemProvider fileSystemProvider = new FileSystemProvider();  
            IDataCleaner dataCleaner = new DataCleaner();

            var nettCalculator = new NettCalculatorUseCase(fileSystemProvider, dataCleaner);

            Console.WriteLine("Enter Office to calculate nett profit for: ");
            var office = Console.ReadLine();
            var nettProfit = nettCalculator.CalculateNettProfit("Question 1 input.csv", office);
            Console.WriteLine("Nett Profit: " + nettProfit);
            Console.WriteLine("Press Enter to see the office with the largest nett profit");
            Console.ReadKey();

            var officeWitMaxnettProfit = nettCalculator.FindLargestNettProfit("Question 2 input.csv");
            Console.WriteLine(officeWitMaxnettProfit);
            Console.ReadKey();
        }   
    }
}
