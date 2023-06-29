using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursak.Models
{
    public class VoltageModel
    {
        public int MinVoltage { get; set; }
        public int MaxVoltage { get; set; }
        public int AverageVoltage { get; set; }
        public int CurrentVoltage { get; set; }

        public DateTime CollectedTime { get; set; }
    }
}
