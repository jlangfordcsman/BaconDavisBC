using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaconDavisBC.ViewModels
{
    public class MainViewModel
    {

        public Period CurrentWageDay { get; set; }

        public Period AvailableWageDays { get; set; }
    }
}
