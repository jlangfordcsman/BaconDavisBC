using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlRepository
{
    public class PeriodRepository
    {
        private const string applicationFolder = "BaconDavisBC";

        public ICollection<Period> GetAvailablePeriods(string project)
        {
            string filesPath = string.Format(@"{0}\{1}\Projects\{2}", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), PeriodRepository.applicationFolder, project);


        }
    }
}
