using System.Collections.Generic;
using System.Data;
using OrganisationProfitCalculator.Data.Models;

namespace OrganisationProfitCalculator.Data.Interfaces
{
    public interface IFileSystemProvider
    {
       string GetFile(string fileName);
       DataTable ReadFile(string filePath);
       List<Office> GetOffices(DataTable csvTable);
    }   
}
