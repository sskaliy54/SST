using Kursak.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursak.Help
{
    public class MappingHelper
    {
        public static PlotingModel MapToPlotingModel(TemperatureModel temperatureModel)
        {
            return new PlotingModel
            {
                Min = temperatureModel.MinTemperature,
                Max = temperatureModel.MaxTemperature,
                Average = temperatureModel.AverageTemperature,
                Current = temperatureModel.CurrentTemperature,
                CollectedTime = temperatureModel.CollectedTime
                
            };
        }

        public static PlotingModel MapToPlotingModel(ClocksModel clocksModel)
        {
            return new PlotingModel
            {
                Min = clocksModel.MinClocks,
                Max = clocksModel.MaxClocks,
                Average = clocksModel.AverageClocks,
                Current = clocksModel.CurrentClocks,
                CollectedTime = clocksModel.CollectedTime
            };
        }

        public static PlotingModel MapToPlotingModel(PowerModel powerModel)
        {
            return new PlotingModel
            {
                Min = powerModel.MinPower,
                Max = powerModel.MaxPower,
                Average = powerModel.AveragePower,
                Current = powerModel.CurrentPower,
                CollectedTime = powerModel.CollectedTime
            };
        }

        public static PlotingModel MapToPlotingModel(VoltageModel powerModel)
        {
            return new PlotingModel
            {
                Min = powerModel.MinVoltage,
                Max = powerModel.MaxVoltage,
                Average = powerModel.AverageVoltage,
                Current = powerModel.CurrentVoltage,
                CollectedTime = powerModel.CollectedTime
            };
        }

        public static PlotingModel MapToPlotingModel(CoolingsFansModel coolingsFansModel)
        {
            return new PlotingModel
            {
                Min = coolingsFansModel.MinCoolingsFans,
                Max = coolingsFansModel.MaxCoolingsFans,
                Average = coolingsFansModel.AverageCoolingsFans,
                Current = coolingsFansModel.CurrentCoolingsFans,
                CollectedTime = coolingsFansModel.CollectedTime
            };
        }

        public static PlotingModel MapToPlotingModel(UsageModel usageModel)
        {
            return new PlotingModel
            {
                Min = usageModel.MinUsage,
                Max = usageModel.MaxUsage,
                Average = usageModel.AverageUsage,
                Current = usageModel.CurrentUsage,
                CollectedTime = usageModel.CollectedTime
            };
        }
        public static List<PlotingModel> MapToPlotingModels(ConcurrentBag<TemperatureModel> temperatureModels)
        {
            return temperatureModels.Select(MapToPlotingModel).ToList();
        }

        public static List<PlotingModel> MapToPlotingModels(ConcurrentBag<ClocksModel> clocksModels)
        {
            return clocksModels.Select(MapToPlotingModel).ToList();
        }

        public static List<PlotingModel> MapToPlotingModels(ConcurrentBag<PowerModel> powerModels)
        {
            return powerModels.Select(MapToPlotingModel).ToList();
        }

        public static List<PlotingModel> MapToPlotingModels(ConcurrentBag<CoolingsFansModel> coolingsFansModels)
        {
            return coolingsFansModels.Select(MapToPlotingModel).ToList();
        }

        public static List<PlotingModel> MapToPlotingModels(ConcurrentBag<UsageModel> usageModels)
        {
            return usageModels.Select(MapToPlotingModel).ToList();
        }

        public static List<PlotingModel> MapToPlotingModels(ConcurrentBag<VoltageModel> usageModels)
        {
            return usageModels.Select(MapToPlotingModel).ToList();
        }

    }
}
