using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursak.Models
{
   public class ClocksModel
    {
        public int MinClocks { get; set; }
        public int MaxClocks { get; set; }
        public int AverageClocks { get; set; }
        public int CurrentClocks { get; set; }

        public DateTime CollectedTime { get; set; }
    }
}
