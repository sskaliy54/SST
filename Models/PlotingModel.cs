using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursak.Models
{
    public class PlotingModel
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public int Average { get; set; }
        public int Current { get; set; }

        public DateTime CollectedTime { get; set; }
    }
}
