using Kursak.Help;
using Kursak.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kursak
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
/*        private void ABCCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Debug.WriteLine("Skalii");
            Global.IsStartCPU = true;
            var viewModel = DataContext as SSTViewModel;
            viewModel?.ABC();
        }
*/

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
        private List<DateTime> Start = new List<DateTime>();


        private void ShowDateTime(object sender, RoutedEventArgs e)
        {
            if (StartListBox.Items.Count > 0)
            {
                StartListBox.Items.Insert(StartListBox.Items.Count, "---------------------");
                StatusListBox.Items.Insert(StatusListBox.Items.Count, "---------------------");

            }

            StartListBox.Items.Insert(StartListBox.Items.Count, DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
            StatusListBox.Items.Add("Test is started");
            Start.Add(DateTime.Now);
        }


        private void StopTimer(object sender, RoutedEventArgs e)
        {

            if (Start.Count > 0)
            {
                StartListBox.Items.Insert(StartListBox.Items.Count, "---------------------");
                StatusListBox.Items.Insert(StatusListBox.Items.Count, "---------------------");

            }

            StartListBox.Items.Insert(StartListBox.Items.Count, DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
            StatusListBox.Items.Add("Test is stopped");
            Start.Add(DateTime.Now);

        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            // Очищення ListBox
            StartListBox.Items.Clear();
            StatusListBox.Items.Clear();

            CPU.IsChecked = false;
            FPU.IsChecked = false;
            GPU.IsChecked = false;
            Cashe.IsChecked = false;
            RAM.IsChecked = false;
            Disk.IsChecked = false;
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is SSTViewModel viewModel)
            {
                viewModel.IsGridVisible = !viewModel.IsGridVisible;
            }
        }
    }
}
