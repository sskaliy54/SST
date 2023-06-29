using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursak.Models
{
    public class TestSettingsModel
    {
        public bool IsCPUTestEnabled { get; set; }
        public bool IsFPUTestEnabled { get; set; }
        public bool IsCasheTestEnabled { get; set; }
        public bool IsRAMTestEnabled { get; set; }
        public bool IsDiskTestEnabled { get; set; }
        public bool IsGPUTestEnabled { get; set; }
    }
}
