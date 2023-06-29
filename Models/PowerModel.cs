using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursak.Models
{
    public class PowerModel
    {
        public int MinPower { get; set; }
        public int MaxPower { get; set; }
        public int AveragePower { get; set; }
        public int CurrentPower { get; set; }

        public DateTime CollectedTime { get; set; }
    }
}
