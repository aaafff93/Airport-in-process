using Airport.ViewModel;
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
using System.Windows.Shapes;

namespace Airport.View
{
    /// <summary>
    /// Логика взаимодействия для DeleteFlightWindow.xaml
    /// </summary>
    public partial class DeleteFlightWindow : Window
    {
        public DeleteFlightWindow()
        {
            InitializeComponent();
            DataContext = new FlightViewModel();
        }
    }
}
