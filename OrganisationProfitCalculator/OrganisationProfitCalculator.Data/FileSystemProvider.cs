using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using LumenWorks.Framework.IO.Csv;
using OrganisationProfitCalculator.Data.Interfaces;
using OrganisationProfitCalculator.Data.Models;

namespace OrganisationProfitCalculator.Data
{
    public class FileSystemProvider : IFileSystemProvider
    {
        //This method will get the file
        public string GetFile(string fileName)
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),"Documents", fileName);
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

        //This method will populate all the data to the model and return offices
        public List<Office> GetOffices(DataTable csvTable)
        {
            var offices = new List<Office>();
            for (int i = 0; i < csvTable.Rows.Count; i++)
            {
                var rows = csvTable.Rows;
                offices.Add(new Office { Name = rows[i][0].ToString(), Parent = rows[i][1].ToString(), Amount =  decimal.Parse(rows[i][2].ToString(), CultureInfo.InvariantCulture) });
            }
                    
            return offices;
        }
    }
}
