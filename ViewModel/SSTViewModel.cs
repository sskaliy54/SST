using GalaSoft.MvvmLight.Command;
using Kursak.Help;
using Kursak.Models;
using Kursak.Service;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Wpf;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kursak.ViewModel
{
    public class SSTViewModel : INotifyPropertyChanged
    {
        private readonly StressTestService _stressTestService;
        private readonly ComputerInfoService _computerInfoService;


        public bool Checkbox1
        {
            get { return IsCPUTestEnabled; }
            set { IsCPUTestEnabled = value; OnPropertyChangedCheckbox(); }
        }

        public bool Checkbox2
        {
            get { return IsFPUTestEnabled; }
            set { IsFPUTestEnabled = value; OnPropertyChangedCheckbox(); }
        }

        public bool Checkbox3
        {
            get { return IsCasheTestEnabled; }
            set { IsCasheTestEnabled = value; OnPropertyChangedCheckbox(); }
        }

        public bool Checkbox4
        {
            get { return IsRAMTestEnabled; }
            set { IsRAMTestEnabled = value; OnPropertyChangedCheckbox(); }
        }

        public bool Checkbox5
        {
            get { return IsDiskTestEnabled; }
            set { IsDiskTestEnabled = value; OnPropertyChangedCheckbox(); }
        }
        public bool Checkbox6
        {
            get { return IsGPUTestEnabled; }
            set { IsGPUTestEnabled = value; OnPropertyChangedCheckbox(); }
        }
        public event PropertyChangedEventHandler PropertyChangedCheckbox;

        protected virtual void OnPropertyChangedCheckbox([CallerMemberName] string propertyName = null)
        {
            PropertyChangedCheckbox?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }

        private bool _isGridVisible;

        public bool IsGridVisible
        {
            get { return _isGridVisible; }
            set
            {
                if (_isGridVisible != value)
                {
                    _isGridVisible = value;
                    OnPropertyChanged(nameof(IsGridVisible));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SSTViewModel()
        {
            _computerInfoService = new ComputerInfoService();
            _stressTestService = new StressTestService();
            StartCommand = new RelayCommand(Start);
            StopCommand = new RelayCommand(Stop);
            GraphsButtonCommand = new RelayCommand(ExecuteGraphsButton);
            HomeButtonCommand = new RelayCommand(ExecuteHomeButton);
            HelpButtonCommand = new RelayCommand(ExecuteHelpButton);
            ChangeComputerInfoTypeCommand = new RelayCommand<ComputerInfoType>(ChangeComputerInfoType);
            PlotModel = new PlotModel();
           
            // Create axes
            var xAxis = new DateTimeAxis
            {
                Position = AxisPosition.Bottom,

                MinorIntervalType = DateTimeIntervalType.Milliseconds,
                IntervalType = DateTimeIntervalType.Minutes,
                Minimum = DateTimeAxis.ToDouble(DateTime.Now),
                Maximum = DateTimeAxis.ToDouble(DateTime.Now.AddMinutes(3)),
                AxisDistance = 2,
                StringFormat = "mm:ss",
                Title = "Time",

            };
            PlotModel.Axes.Add(xAxis);

            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Value",
                Minimum = 0,
                Maximum = 100,
               
            };
            PlotModel.Axes.Add(yAxis);
            //_computerInfoService.PropertyChanged += (sender, args) =>
            //{
            //    PlotInfo(SelectedComputerInfoType);
            //};

            // Set up the timer
            timer = new Timer { Interval = 1000 }; // Refresh every second
            timer.Elapsed += Timer_Tick;
            timer.Start();
        }
   

        private ComputerInfoType selectedComputerInfoType;
        public ComputerInfoType SelectedComputerInfoType
        {
            get { return selectedComputerInfoType; }
            set
            {
                if (selectedComputerInfoType != value)
                {
                    selectedComputerInfoType = value;
                    OnPropertyChanged(nameof(SelectedComputerInfoType));
                }
            }
        }
        public ICommand ChangeComputerInfoTypeCommand { get; }

        public void ChangeComputerInfoType(ComputerInfoType newType)
        {
            SelectedComputerInfoType = newType;
            PlotInfo(newType);

        }

        private PlotModel _plotModel;

        public PlotModel PlotModel
        {
            get { return _plotModel; }
            set
            {
                _plotModel = value;
                OnPropertyChanged(nameof(PlotModel));
            }
        }


        private Timer timer;
        private void Timer_Tick(object sender, EventArgs e)
        {
            PlotInfo(SelectedComputerInfoType);
            // Redraw the plot
            
        }


        public void PlotInfo(ComputerInfoType infoType)
        {
            // Create the PlotModel

            PlotModel.Series.Clear();
            var lineSeries = new LineSeries
            {
                Title = "Data",
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
            
            };

            // Add data points to the series
            var data = _computerInfoService.GetPlotInfo(infoType);
            data.OrderBy(x => x.CollectedTime);
            foreach (var item in data)
            {
                var dataPoint = DateTimeAxis.CreateDataPoint(item.CollectedTime, item.Current);
                lineSeries.Points.Add(dataPoint);
            }

           
            PlotModel.Series.Add(lineSeries);
            PlotModel.InvalidatePlot(true);
        }




        public bool IsCPUTestEnabled { get; set; }
        public bool IsFPUTestEnabled { get; set; }
        public bool IsCasheTestEnabled { get; set; }
        public bool IsRAMTestEnabled { get; set; }
        public bool IsDiskTestEnabled { get; set; }
        public bool IsGPUTestEnabled { get; set; }


        public async void Start()
        {
            Task.Run(async () => await _stressTestService.RunHardwareTests(GetTestSettingsModel()));


            //if (Checkbox1)
            //{
            //    Global.IsStartCPU = true;
            //    _stressTestService.CPUStressTest();
            //}
            //if (Checkbox2)
            //{
            //    Global.IsStartFPU = true;
            //    _stressTestService.FPUStressTest();
            //}
        }
        public void Stop()
        {
            if (Checkbox1)
            {
                //Debug.WriteLine("Skalii");
                Global.IsStartCPU = false;
            }
            if (Checkbox2)
            {
                Global.IsStartFPU = false;
            }
            if (Checkbox3)
            {
                Global.IsStartCashe = false;
            }
            if (Checkbox4)
            {
                Global.IsStartRAM = false;
            }
            if (Checkbox5)
            {
                Global.IsStartDisk = false;
            }
            if (Checkbox6)
            {
                Global.IsStartGPU = false;
            }

        }
        private TestSettingsModel GetTestSettingsModel()
        {
            var model = new TestSettingsModel()
            {
                IsCPUTestEnabled = IsCPUTestEnabled,
                IsFPUTestEnabled = IsFPUTestEnabled,
                IsCasheTestEnabled = IsCasheTestEnabled,
                IsRAMTestEnabled = IsRAMTestEnabled,
                IsDiskTestEnabled = IsDiskTestEnabled,
                IsGPUTestEnabled = IsGPUTestEnabled
            };
            return model;
        }

        private Visibility _grid1Visibility = Visibility.Visible;
        public Visibility Grid1Visibility
        {
            get { return _grid1Visibility; }
            set
            {
                _grid1Visibility = value;
                OnPropertyChanged(nameof(Grid1Visibility));
            }
        }

        private Visibility _grid2Visibility = Visibility.Collapsed;
        public Visibility Grid2Visibility
        {
            get { return _grid2Visibility; }
            set
            {
                _grid2Visibility = value;
                OnPropertyChanged2(nameof(Grid2Visibility));
            }
        }

        private Visibility _stackPanelVisibility = Visibility.Collapsed;
        public Visibility StackPanelVisibility
        {
            get { return _stackPanelVisibility; }
            set
            {
                _stackPanelVisibility = value;
                OnPropertyChanged2(nameof(StackPanelVisibility));
            }
        }

        // Implement the ICommand properties for button click events
        public ICommand GraphsButtonCommand { get; private set; }
        public ICommand HomeButtonCommand { get; private set; }
        public ICommand HelpButtonCommand { get; private set; }


        

        // Implement the click event methods
        public void ExecuteGraphsButton()
        {
            Grid1Visibility = Visibility.Collapsed;
            Grid2Visibility = Visibility.Visible;
            StackPanelVisibility = Visibility.Collapsed;
            
        }

        private void ExecuteHomeButton()
        {
            Grid1Visibility = Visibility.Visible;
            Grid2Visibility = Visibility.Collapsed;
            StackPanelVisibility = Visibility.Collapsed;
        }

        private void ExecuteHelpButton()
        {
            Grid1Visibility = Visibility.Collapsed;
            Grid2Visibility = Visibility.Collapsed;
            StackPanelVisibility = Visibility.Visible;
        }

        // Implement the INotifyPropertyChanged interface
        public event PropertyChangedEventHandler PropertyChanged2;

        protected virtual void OnPropertyChanged2(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }


    //public class RelayCommand : ICommand
    //{
    //    private readonly Action _execute;
    //    private readonly Func<bool> _canExecute;

    //    public event EventHandler CanExecuteChanged
    //    {
    //        add { CommandManager.RequerySuggested += value; }
    //        remove { CommandManager.RequerySuggested -= value; }
    //    }

    //    public RelayCommand(Action execute, Func<bool> canExecute = null)
    //    {
    //        _execute = execute;
    //        _canExecute = canExecute;
    //    }

    //    public bool CanExecute(object parameter)
    //    {
    //        return _canExecute == null || _canExecute();
    //    }

    //    public void Execute(object parameter)
    //    {
    //        _execute?.Invoke();
    //    }
    //}

}
