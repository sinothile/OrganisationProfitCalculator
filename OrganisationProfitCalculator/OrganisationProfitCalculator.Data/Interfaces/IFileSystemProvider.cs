using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganisationProfitCalculator.Data.Models;

namespace OrganisationProfitCalculator.Data.Interfaces
{
    public interface IFileSystemProvider
    {
       string GetFile(string fileName);
       DataTable ReadFile(string filePath);
       List<Office> PopulateTheData(DataTable csvTable);
    }
}
