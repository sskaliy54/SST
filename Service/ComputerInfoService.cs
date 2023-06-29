using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kursak.Help;
using Kursak.Models;
using OpenHardwareMonitor.Hardware;

namespace Kursak.Service
{
    public class ComputerInfoService: IDisposable
    {
        private ComputerInfoModel _computerInfoModel;
        Computer _computer = new Computer() {
            CPUEnabled = true,
            RAMEnabled = true,
            GPUEnabled = true,
            FanControllerEnabled = true,
            HDDEnabled = true,
            MainboardEnabled = true
        };
        UpdateVisitor updateVisitor = new UpdateVisitor();


        public ComputerInfoService()
        {
            _computerInfoModel = new ComputerInfoModel();

            _computer.Open();
             _computer.GetReport();
            Task.Run(async()=>await GetComputerInfoAsync());
        }

        public List<PlotingModel> GetPlotInfo(ComputerInfoType infoType)
        {
            switch (infoType)
            {
                case ComputerInfoType.Clock:
                    return MappingHelper.MapToPlotingModels(_computerInfoModel.Clocks);

                case ComputerInfoType.Cooling:
                    return MappingHelper.MapToPlotingModels(_computerInfoModel.CoolFan);

                case ComputerInfoType.Power:
                    return MappingHelper.MapToPlotingModels(_computerInfoModel.Powers);

                case ComputerInfoType.Temperature:
                    return MappingHelper.MapToPlotingModels(_computerInfoModel.Temperatures);

                case ComputerInfoType.Usage:
                    return MappingHelper.MapToPlotingModels(_computerInfoModel.Usage);

                case ComputerInfoType.Voltage:
                    return MappingHelper.MapToPlotingModels(_computerInfoModel.Voltages);

                default:
                    throw new ArgumentException("Invalid ComputerInfoType value");
            }
        }


        public async Task GetComputerInfoAsync()
        {
            _computer.Accept(updateVisitor);
            while (true)
            {
               
                // Iterate through each hardware component
                foreach (var hardware in _computer.Hardware)
                {
                    hardware.Update();
                   
                    foreach(var sensor in hardware.SubHardware)
                    {
                        sensor.Update();
                    }

                    // Check if the hardware component represents a CPU
                    if (hardware.HardwareType == HardwareType.CPU)
                    {
                        // Retrieve the clock-related sensors
                        var minClocksSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Clock);
                        var maxClocksSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Clock);
                        var averageClocksSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Clock);
                        var currentClocksSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Clock);

                        // Create a new ClocksModel object
                        ClocksModel clocksData = new ClocksModel();

                        // Update the ClocksModel object with the sensor values
                        clocksData.MinClocks = (int)(minClocksSensor?.Value ?? 0);
                        clocksData.MaxClocks = (int)(maxClocksSensor?.Value ?? 0);
                        clocksData.AverageClocks = (int)(averageClocksSensor?.Value ?? 0);
                        clocksData.CurrentClocks = (int)(currentClocksSensor?.Value ?? 0);

                        // Set the collected time
                        clocksData.CollectedTime = DateTime.Now;

                        // Add the ClocksModel object to the list
                        _computerInfoModel.Clocks.Add(clocksData);


                        // Retrieve the temperature sensors
                        var minTempSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature);
                        var maxTempSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature);
                        var averageTempSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature);
                        var currentTempSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature);

                        // Create a new TemperatureModel object
                        TemperatureModel tempData = new TemperatureModel();

                        // Update the TemperatureModel object with the sensor values
                        tempData.MinTemperature = (int)(minTempSensor?.Value ?? 0);
                        tempData.MaxTemperature = (int)(maxTempSensor?.Value ?? 0);
                        tempData.AverageTemperature = (int)(averageTempSensor?.Value ?? 0);
                        tempData.CurrentTemperature = (int)(currentTempSensor?.Value ?? 0);

                        // Set the collected time
                        tempData.CollectedTime = DateTime.Now;

                        // Add the TemperatureModel object to the list
                        _computerInfoModel.Temperatures.Add(tempData);

                        // Retrieve the power-related sensors
                        var minPowerSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Power);
                        var maxPowerSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Power);
                        var averagePowerSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Power);
                        var currentPowerSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Power);

                        // Create a new PowerModel object
                        PowerModel powerData = new PowerModel();

                        // Update the PowerModel object with the sensor values
                        powerData.MinPower = (int)(minPowerSensor?.Value ?? 0);
                        powerData.MaxPower = (int)(maxPowerSensor?.Value ?? 0);
                        powerData.AveragePower = (int)(averagePowerSensor?.Value ?? 0);
                        powerData.CurrentPower = (int)(currentPowerSensor?.Value ?? 0);

                        // Set the collected time
                        powerData.CollectedTime = DateTime.Now;

                        // Add the PowerModel object to the list
                        _computerInfoModel.Powers.Add(powerData);

                        // Retrieve the fan-related sensors
                        var minFanSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Fan);
                        var maxFanSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Fan);
                        var averageFanSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Fan);
                        var currentFanSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Fan);

                        // Create a new CoolingsFansModel object
                        CoolingsFansModel fanData = new CoolingsFansModel();

                        // Update the CoolingsFansModel object with the sensor values
                        fanData.MinCoolingsFans = (int)(minFanSensor?.Value ?? 0);
                        fanData.MaxCoolingsFans = (int)(maxFanSensor?.Value ?? 0);
                        fanData.AverageCoolingsFans = (int)(averageFanSensor?.Value ?? 0);
                        fanData.CurrentCoolingsFans = (int)(currentFanSensor?.Value ?? 0);

                        // Set the collected time
                        fanData.CollectedTime = DateTime.Now;

                        // Add the CoolingsFansModel object to the list
                        _computerInfoModel.CoolFan.Add(fanData);

                        // Retrieve the usage-related sensors
                        var minUsageSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Load );
                        var maxUsageSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Load );
                        var averageUsageSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Load);
                        var currentUsageSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Load);

                        // Create a new UsageModel object
                        UsageModel usageData = new UsageModel();

                        // Update the UsageModel object with the sensor values
                        usageData.MinUsage = (int)(minUsageSensor?.Value ?? 0);
                        usageData.MaxUsage = (int)(maxUsageSensor?.Value ?? 0);
                        usageData.AverageUsage = (int)(averageUsageSensor?.Value ?? 0);
                        usageData.CurrentUsage = (int)(currentUsageSensor?.Value ?? 0);

                        // Set the collected time
                        usageData.CollectedTime = DateTime.Now;

                        // Add the UsageModel object to the list
                        _computerInfoModel.Usage.Add(usageData);

                    }
                }

                OnPropertyChanged(nameof(_computerInfoModel));
                await Task.Delay(3000);
            }

        }

        public event EventHandler<PropertyChangedEventArgs> PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            _computer.Close();
        }
    }
}
