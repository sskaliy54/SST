using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursak.Models
{
    public class CoolingsFansModel
    {
        public int MinCoolingsFans { get; set; }
        public int MaxCoolingsFans { get; set; }
        public int AverageCoolingsFans { get; set; }
        public int CurrentCoolingsFans { get; set; }

        public DateTime CollectedTime { get; set; }
    }
}
