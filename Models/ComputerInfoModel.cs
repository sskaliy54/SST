using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursak.Models
{
    public class ComputerInfoModel
    {
        public ConcurrentBag<TemperatureModel> Temperatures { get; set; }
        public ConcurrentBag<ClocksModel> Clocks { get; set; }
        public ConcurrentBag<VoltageModel> Voltages { get; set; }
        public ConcurrentBag<PowerModel> Powers { get; set; }
        public ConcurrentBag<CoolingsFansModel> CoolFan { get; set; }
        public ConcurrentBag<UsageModel> Usage { get; set; }

        public ComputerInfoModel()
        {
            Temperatures = new ConcurrentBag<TemperatureModel>();
            Clocks = new ConcurrentBag<ClocksModel>();
            Voltages = new ConcurrentBag<VoltageModel>();
            Powers = new ConcurrentBag<PowerModel>();
            CoolFan = new ConcurrentBag<CoolingsFansModel>();
            Usage = new ConcurrentBag<UsageModel>();
        }
    }
}
