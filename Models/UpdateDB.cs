using Airport.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Airport.Models
{
    static class UpdateDB
    {
        public static ObservableCollection<object> allViewModels = new ObservableCollection<object>(); //private? поле класса       
        public static ObservableCollection<object> UpdateAllDB()
        {
            //MessageBox.Show("getall");
            allViewModels.Clear();
            allViewModels.Add(new AircraftViewModel());
            allViewModels.Add(new FlightViewModel());
            allViewModels.Add(new BoardingPassViewModel());
            return allViewModels;
        }
    }
}
