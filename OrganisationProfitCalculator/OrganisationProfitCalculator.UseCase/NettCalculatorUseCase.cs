using System;
using System.Collections.Generic;
using System.Linq;
using OrganisationProfitCalculator.Data.Interfaces;
using OrganisationProfitCalculator.Data.Models;

namespace OrganisationProfitCalculator.UseCase
{
    public class NettCalculatorUseCase
    {
        private readonly IFileSystemProvider _fileSystemProvider;
        private readonly IDataCleaner _dataCleaner;
        public NettCalculatorUseCase(IFileSystemProvider fileSystemProvider, IDataCleaner dataCleaner)
        {
            _fileSystemProvider = fileSystemProvider;
            _dataCleaner = dataCleaner;
        }

        //This method calls all the methods to be executed in order to get nett profit
        public decimal CalculateNettProfit(string fileName, string officeName)
        {
            if (string.IsNullOrWhiteSpace(officeName))
            {
                return 0;
            }

            var fileData = ProcessFile(fileName);

            if (OfficeNameDoesNotExist(officeName, fileData))
            {
                return 0;
            }
            var descendants = GetDescendants(officeName, fileData);

            return GetNettProfit(descendants);        
        }
            
        private bool OfficeNameDoesNotExist(string officeName, List<Office> fileData)
        {   
            return !fileData.Any(x => _dataCleaner.CleanData(x.Name).Equals(_dataCleaner.CleanData(officeName)));
        }

        //This method will get all the descendants
        private List<Office> GetDescendants(string officeName, List<Office> officeData)
        {
            var cleanedOfficeName = _dataCleaner.CleanData(officeName);
            var currentOffice = officeData.FirstOrDefault(x => _dataCleaner.CleanData(x.Name).Equals(cleanedOfficeName));

            var descendants = new List<Office>() { currentOffice };

            foreach (var office in officeData)
            {
                for (int i = 0; i < descendants.Count; i++)
                {
                    var parent = _dataCleaner.CleanData(office.Parent);
                    var officeInQuestion = _dataCleaner.CleanData(descendants[i].Name);
                    if (parent.Equals(officeInQuestion))
                    {
                        descendants.Add(office);
                    }
                }
            }
            return descendants;
        }

        //This method will calculate the nett profit for office including its descendants
        private decimal GetNettProfit(List<Office> descendants)
        {
            var total = descendants.Select(x => x.Amount).Sum();

            return total;
        }

        /*
          Question 2
          Because i had done question 1 and it had all the methods i can use to find the office with the largest nett profit,
          i then reused those methods inorder to avoid duplicates and also to show that my code is generic and reusable.
          I am getting the nett profit for each office and putting that in a temporary variable, then i compare if
          the current nett is the largest i then store it in temporary variable till i get the largest.

         */
        public string FindLargestNettProfit(string fileName)
        {
            var offices = ProcessFile(fileName);
            var officeWithLargestNettProfit = "";
            decimal maxNettProfit = 0;

            foreach (var office in offices)
            {
                var descendants = GetDescendants(office.Name, offices);
                var total = GetNettProfit(descendants);

                if (!(total > maxNettProfit)) continue;
                maxNettProfit = total;
                officeWithLargestNettProfit = office.Name;
            }

            return $"Office With The Largest Nett Profit Is: {officeWithLargestNettProfit} With The Nett Profit Of: {maxNettProfit}";
        }

        private List<Office> ProcessFile(string fileName)
        {
            var path = _fileSystemProvider.GetFile(fileName);
            var file = _fileSystemProvider.ReadFile(path);
            var fileData = _fileSystemProvider.GetOffices(file);
            return fileData;
        }
    }
}
