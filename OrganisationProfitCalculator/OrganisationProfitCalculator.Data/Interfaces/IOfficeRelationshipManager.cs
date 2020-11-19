using System.Collections.Generic;
using OrganisationProfitCalculator.Data.Models;

namespace OrganisationProfitCalculator.Data.Interfaces
{
    public interface IOfficeRelationshipManager
    {
        List<string> GetDescendants(string officeName, List<Office> officeData);
    }
}
