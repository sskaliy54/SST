using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursak.Models
{
    public class TemperatureModel
    {
        public int MinTemperature { get; set; }
        public int MaxTemperature { get; set; }
        public int AverageTemperature { get; set; }
        public int CurrentTemperature { get; set; }

        public DateTime CollectedTime { get; set; }
    }
}
