using Kursak.Help;
using Kursak.Models;
using Kursak.Service;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kursak.ViewModel
{
    public class SSTViewModel : INotifyPropertyChanged
    {
        private readonly StressTestService _stressTestService;

        private bool CPU;
        private bool FPU;
        private bool Cashe;
        private bool GPU;
        private bool Disk;
        private bool RAM;
        private bool RAM2;
        
        public bool Checkbox1
        {
            get { return CPU; }
            set { CPU = value; OnPropertyChangedCheckbox(); }
        }

       public bool Checkbox2
        {
            get { return FPU; }
            set {FPU= value; OnPropertyChangedCheckbox(); }
        }

        public bool Checkbox3
        {
            get { return Cashe; }
            set { Cashe = value; OnPropertyChangedCheckbox(); }
        }

        public bool Checkbox4
        {
            get { return RAM; }
            set { RAM = value; OnPropertyChangedCheckbox(); }
        }

        public bool Checkbox5
        {
            get { return Disk; }
            set { Disk = value; OnPropertyChangedCheckbox(); }
        }
        public bool Checkbox6
        {
            get { return GPU; }
            set { GPU = value; OnPropertyChangedCheckbox(); }
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
            _stressTestService = new StressTestService();
            StartCommand = new RelayCommand(Start);
            StopCommand = new RelayCommand(Stop);
        }

        public bool IsCPUTestEnabled { get; set; }
        public bool IsFPUTestEnabled { get; set; }
        public bool IsCasheTestEnabled { get; set; }
        public bool IsRAMTestEnabled { get; set; }
        public bool IsDiskTestEnabled { get; set; }
        public bool IsGPUTestEnabled { get; set; }


        public void Start()
        {
            if (Checkbox1)
            {
                Global.IsStartCPU = true;
                _stressTestService.CPUStressTest();
            }
            if (Checkbox2)
            {
                Global.IsStartFPU = true;
                _stressTestService.FPUStressTest();
            }

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

            }
        private TestSettingsModel GetTestSettingsModel() 
        {
            var model = new TestSettingsModel()
            {
            IsCPUTestEnabled = this.IsCPUTestEnabled,
            IsFPUTestEnabled = this.IsFPUTestEnabled,
            IsCasheTestEnabled = this.IsCasheTestEnabled,
            IsRAMTestEnabled = this.IsRAMTestEnabled,
            IsDiskTestEnabled = this.IsDiskTestEnabled,
            IsGPUTestEnabled = this.IsGPUTestEnabled
            };
            return model;
        }

    }


    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute?.Invoke();
        }
    }

}
