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
    /// Логика взаимодействия для AddNewFlightWindow.xaml
    /// </summary>
    public partial class AddNewFlightWindow : Window
    {
        public AddNewFlightWindow()
        {
            InitializeComponent();
            DataContext = new FlightViewModel();
        }
    }
}
