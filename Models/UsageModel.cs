using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursak.Models
{
   public class UsageModel
    {
        public int MinUsage { get; set; }
        public int MaxUsage { get; set; }
        public int AverageUsage { get; set; }
        public int CurrentUsage { get; set; }

        public DateTime CollectedTime { get; set; }
    }
}
