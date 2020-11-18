using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganisationProfitCalculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var nettCalculator = new NettCalculator();

            Console.WriteLine("Enter Office to calculate nett profit for: ");
            var office = Console.ReadLine();
            var nettProfit = nettCalculator.GetNettProfit(@"Documents\Question 1 input.csv", office);
            Console.WriteLine("Nett Profit: " + nettProfit);
            Console.WriteLine("Press Enter to see the office with the largest nett profit");
            Console.ReadKey();

            var officeWitMaxnettProfit = nettCalculator.FindLargestNettProfit(@"Documents\Question 2 input.csv");
            Console.WriteLine(officeWitMaxnettProfit);
            Console.ReadKey();
        }   
    }
}
