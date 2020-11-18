using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganisationProfitCalculator.Data.Interfaces;

namespace OrganisationProfitCalculator.Data
{
    public class DataCleaner : IDataCleaner
    {
        /*
          This method will clean the officeName.
          Remove the spaces.
          also change office name to small caps.
          all of this clean up is to make the data uniform
         */
        public string CleanData(string officeName)
        {
            return officeName.Replace(" ", string.Empty).ToLower().Trim();
        }
    }
}
