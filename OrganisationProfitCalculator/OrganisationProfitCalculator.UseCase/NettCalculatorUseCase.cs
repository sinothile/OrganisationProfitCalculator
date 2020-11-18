﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
         public double CalculateNettProfit(string fileName, string officeName)
        {   
            var path = _fileSystemProvider.GetFile(fileName);
            var file = _fileSystemProvider.ReadFile(path);
            var fileData = _fileSystemProvider.PopulateTheData(file);
            var descendants = GetDescendants(officeName, fileData);

            return GetNettProfit(fileData, descendants);
        }

        //This method will get all the descendants
        private List<string> GetDescendants(string officeName, List<Office> fileData)
        {
            if (string.IsNullOrWhiteSpace(officeName))
            {
                return new List<string>();
            }

            var cleanedOfficeName = _dataCleaner.CleanData(officeName);
            var descendants = new List<string>() { cleanedOfficeName };

            foreach (var data in fileData)
            {
                for (int i = 0; i < descendants.Count; i++)
                {
                    if (_dataCleaner.CleanData(data.Parent).Equals(descendants[i]))
                    {
                        descendants.Add(_dataCleaner.CleanData(data.Name));
                    }
                }
            }

            return descendants;
        }

        //This method will calculate the nett profit for office including its descendants
        private double GetNettProfit(List<Office> fileData, List<string> descendants)
        {
            var total = new List<double>();

            foreach (var descendant in descendants)
            {
                var amount = (fileData.Where(x => _dataCleaner.CleanData(x.Name) == descendant).Select(y => y.Amount)).ToList();
                if (amount.Any())
                {
                    total.Add(amount.ElementAt(0));
                }
            }

            return total.Sum();
        }

        /*
          Question 2
          Because i had done question 1 and it had all the methods i can use to find the office with the largest nett profit,
          i then reused those methods inorder to avoid duplicates and also to show that my code is generic and reusable
         */
        public string FindLargestNettProfit(string fileName)
        {
            var path = _fileSystemProvider.GetFile(fileName);
            var file = _fileSystemProvider.ReadFile(path);
            var officeData = _fileSystemProvider.PopulateTheData(file);
            var officeWithLargestNettProfit = "";
            double maxNettProfit = 0;

            foreach (var office in officeData)
            {
                var descendants = GetDescendants(office.Name, officeData);
                var total = GetNettProfit(officeData, descendants);

                if (!(total > maxNettProfit)) continue;
                maxNettProfit = total;
                officeWithLargestNettProfit = office.Name;
            }

            return $"Office With The Largest Nett Profit Is: {officeWithLargestNettProfit} With The Nett Profit Of: {maxNettProfit}";
        }
    }
}