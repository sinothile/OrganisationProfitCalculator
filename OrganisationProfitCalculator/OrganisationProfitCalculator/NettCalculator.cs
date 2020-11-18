using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using LumenWorks.Framework.IO.Csv;
using OrganisationProfitCalculator.Models;
using System.Reflection;

namespace OrganisationProfitCalculator
{
    public class NettCalculator
    {
        /*
         Question 1
         This method calls all the methods to be executed in order to get nett profit
         */
        public double GetNettProfit(string fileName, string officeName)
        {
            var path = GetFile(fileName);
            var file = ReadFile(path);
            var fileData = PopulateTheData(file);
            var descendants = GetDescendants(officeName, fileData);

            return CalculateNettProfit(fileData, descendants);
        }

        //This method will get the file
        private string GetFile(string fileName)
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), fileName);
        }

        //This method will read the file
        public DataTable ReadFile(string filePath)
        {   
            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(File.OpenRead(filePath)), true))
            {
                csvTable.Load(csvReader);
            }   

            return csvTable;
        }

        //This method will populate all the data to the model
        public List<Columns> PopulateTheData(DataTable csvTable)
        {
            List<Columns> columns = new List<Columns>();
            for (int i = 0; i < csvTable.Rows.Count; i++)
            {
                columns.Add(new Columns { Office = csvTable.Rows[i][0].ToString(), Parent = csvTable.Rows[i][1].ToString(), Amount = Convert.ToDouble(csvTable.Rows[i][2]) });
            }

            return columns;
        }

        //This method will get all the descendants
        private List<string> GetDescendants(string officeName, List<Columns> fileData)
        {
            if (string.IsNullOrWhiteSpace(officeName))
            {
                return new List<string>();
            }

            var cleanedOfficeName = CleanData(officeName);
            var descendants = new List<string>() { cleanedOfficeName };

                foreach (var data in fileData)
                {
                    for (int i = 0; i < descendants.Count; i++)
                    {
                        if (CleanData(data.Parent).Equals(descendants[i]))
                        {
                            descendants.Add(CleanData(data.Office));
                        }
                    }
                }

                return descendants;
        }

        //This method will calculate the nett profit for office including its descendants
        private double CalculateNettProfit(List<Columns> fileData, List<string> descendants)
        {
            var total = new List<double>();

            foreach (var descendant in descendants)
            {
                var amount = (fileData.Where(x => CleanData(x.Office) == descendant).Select(y => y.Amount)).ToList();
                if (amount.Any())
                {
                    total.Add(amount.ElementAt(0));
                }
            }

            return total.Sum();
        }

        /*
          This method will clean the officeName.
          Remove the spaces.
          also change office name to small caps
         */
        private string CleanData(string officeName)
        {
            return officeName.Replace(" ", string.Empty).ToLower().Trim();
        }

        /*
          Question 2
          Because i had done question 1 and it had all the methods i can use to find the office with the largest nett profit,
          i then reused those methods inorder to avoid duplicates and also to show that my code is generic and reusable
         */
        public string FindLargestNettProfit(string fileName)
        {
            var path = GetFile(fileName);
            var file = ReadFile(path);
            var fileData = PopulateTheData(file);
            string officeWithLargestNettProfit = "";
            double maxNettProfit = 0;   

            foreach (var data in fileData)
            {
                var descendants = GetDescendants(data.Office, fileData);
                var total = CalculateNettProfit(fileData, descendants);

                if (total > maxNettProfit)
                {
                    maxNettProfit = total;
                    officeWithLargestNettProfit = data.Office;
                }
            }

            return $"Office With The Largest Nett Profit Is: {officeWithLargestNettProfit} With The Nett Profit Of: {maxNettProfit}";
        }
    }
}
