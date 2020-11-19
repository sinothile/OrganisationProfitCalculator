using System.Collections.Generic;
using OrganisationProfitCalculator.Data.Interfaces;
using OrganisationProfitCalculator.Data.Models;

namespace OrganisationProfitCalculator.Data
{
    public class OfficeRelationshipManager : IOfficeRelationshipManager
    {
        private readonly IDataCleaner _dataCleaner; 
        public OfficeRelationshipManager(IDataCleaner dataCleaner)
        {
            _dataCleaner = dataCleaner; 
        }

        //This method will get all the descendants
        public List<string> GetDescendants(string officeName, List<Office> officeData)
        {
            if (string.IsNullOrWhiteSpace(officeName))
            {
                return new List<string>();
            }

            var cleanedOfficeName = _dataCleaner.CleanData(officeName);
            var descendants = new List<string>() { cleanedOfficeName };

            foreach (var office in officeData)
            {
                for (int i = 0; i < descendants.Count; i++)
                {
                    if (_dataCleaner.CleanData(office.Parent).Equals(descendants[i]))
                    {
                        descendants.Add(_dataCleaner.CleanData(office.Name));
                    }
                }
            }

            return descendants;
        }

    }
}
