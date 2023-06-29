using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kursak.Models;

namespace Kursak.Service
{
    public class ComputerInfoService
    {
        private ComputerInfoModel _computerInfoModel;

        public ComputerInfoService()
        {
            _computerInfoModel = new ComputerInfoModel();
        }

        public async Task GetComputerInfoAsync()
        {
            while (true)
            {
                _computerInfoModel.Clocks.Add(new ClocksModel()
                {
                    AverageClocks = 1,
                    CollectedTime = DateTime.UtcNow
                });

                _computerInfoModel.Powers.Add(new PowerModel()
                {
                    CollectedTime = DateTime.UtcNow
                });
            }
        }

    }
}
