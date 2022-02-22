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
    /// Логика взаимодействия для AddAircraft.xaml
    /// </summary>
    public partial class AddNewAircraftWindow : Window
    {
        public AddNewAircraftWindow()
        {
            InitializeComponent();
            DataContext = new AircraftViewModel();
            //DataContext = new AirportViewModel();
        }
    }
}
