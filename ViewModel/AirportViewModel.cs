using Airport.Models;
using Airport.View; //для окон
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Airport.ViewModel
{
    class AirportViewModel : INotifyPropertyChanged //интерфейс, чтобы всё менялось при привязке
    {    

        public ObservableCollection<object> allViewModels = UpdateDB.UpdateAllDB();
        public ObservableCollection<object> AllViewModels //свойство для возврата всех vm
        {
            get
            {
                return allViewModels;
            }
            set///убрать?
            {
                allViewModels = value;
                OnPropertyChanged("AllViewModels");
            }
        }             
       
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) //параметр - имя свойства
        {
            MessageBox.Show(propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); //? (оператор условного null) - проверка на null. Invoke - вызов делегата
        }
    }
}
