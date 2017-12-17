using Main.Common;
using System;
using System.Collections.Generic;
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

namespace Main
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = mainWindowViewModel;
        }
        
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                mainWindowViewModel.IsWhile = true;
                mainWindowViewModel.BeginRefsherData();
            });
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.IsWhile = false;
        }
    }
}
