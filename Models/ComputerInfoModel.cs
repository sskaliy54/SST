using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursak.Models
{
    public class ComputerInfoModel
    {
        public List<TemperatureModel> Temperatures { get; set; }
        public List<ClocksModel> Clocks { get; set; }
        public List<VoltageModel> Voltages { get; set; }
        public List<PowerModel> Powers { get; set; }
        public List<CoolingsFansModel> CoolFan{ get; set; }
        public List<UsageModel> Usage{ get; set; }
    }
}
