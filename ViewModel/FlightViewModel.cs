using Airport.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Airport.ViewModel
{
    class FlightViewModel : INotifyPropertyChanged
    {
        #region ALL PROPERTIES
        private ObservableCollection<Flight> allFlights;
        public ObservableCollection<Flight> AllFlights //свойство для всех рейсов
        {
            get
            {
                using (UserContext db = new UserContext())
                {
                    //allFlights = new ObservableCollection<Flight>(db.Flights.Include(f=>f.Price));
                    allFlights = new ObservableCollection<Flight>(db.Flights.Include(f => f.Price).Include(a => a.Aircraft));
                    return allFlights;
                }

            }
            set
            {
                allFlights = value;
                OnPropertyChanged("AllFlights"); //уведомление
            }
        }

        public Flight SelectedFlight //выбранный рейс (из статического класса)
        {
            get
            {
                return FlightRepository.selectedFlight;
            }
            set
            {
                FlightRepository.selectedFlight = value;
                OnPropertyChanged("SelectedFlight");
            }
        }

        private int flightID;
        public int FlightID
        {
            get
            {
                return flightID;
            }
            set
            {
                flightID = value;
                OnPropertyChanged("FlightID");
            }
        }
        private DateTime date = DateTime.Now; //edit?
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }     

        private string departure;
        public string Departure
        {
            get
            {
                return departure;
            }
            set
            {
                departure = value;
                OnPropertyChanged("Departure");
            }
        }
        private string arrival;
        public string Arrival
        {
            get
            {
                return arrival;
            }
            set
            {
                arrival = value;
                OnPropertyChanged("Arrival");
            }
        }
        private TimeSpan boardingTime;
        public TimeSpan BoardingTime
        {
            get
            {
                return boardingTime;
            }
            set
            {
                boardingTime = value;
                OnPropertyChanged("BoardingTime");
            }
        }
        private TimeSpan lastCallTime;
        public TimeSpan LastCallTime
        {
            get
            {
                return lastCallTime;
            }
            set
            {
                lastCallTime = value;
                OnPropertyChanged("LastCallTime");
            }
        }
        private TimeSpan outTime;
        public TimeSpan OutTime
        {
            get
            {
                return outTime;
            }
            set
            {
                outTime = value;
                OnPropertyChanged("OutTime");
            }
        }
        private TimeSpan arrivalTime;
        public TimeSpan ArrivalTime
        {
            get
            {
                return arrivalTime;
            }
            set
            {
                arrivalTime = value;
                OnPropertyChanged("ArrivalTime");
            }
        }
        private short availableSeats;
        public short AvailableSeats
        {
            get
            {
                return availableSeats;               
            }
            set
            {
                availableSeats = value;
                OnPropertyChanged("AvailableSeats");
            }
        }
        private short soldSeats;
        public short SoldSeats
        {
            get
            {
                return soldSeats;
            }
            set
            {
                soldSeats = value;
                OnPropertyChanged("SoldSeats");
            }
        }
        /*private int aircraftID;
        public int AircraftID
        {
            get
            {
                return aircraftID;
            }
            set
            {
                aircraftID = value;
                OnPropertyChanged("AircraftID");
            }
        }*/
        /*private int priceID; //убрать
        public int PriceID
        {
            get
            {
                return priceID;
            }
            set
            {
                priceID = value;
                OnPropertyChanged("PriceID");
            }
        }*/
        private Price price;
        public Price Price
        {
            get
            {
                return price = new Price();
            }
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }
        /*private Aircraft aircraft;
        public Aircraft Aircraft
        {
            get
            {
                return aircraft;
            }
            set
            {
                aircraft = value;
                OnPropertyChanged("Aircraft");
            }
        }*/


        public Aircraft SelectedFromComboBoxAircraft
        {
            get
            {
                return FlightRepository.selectedFromComboBoxAircraft;
            }
            set
            {
                FlightRepository.selectedFromComboBoxAircraft = value;
                OnPropertyChanged("SelectedFromComboBoxAircraft");
            }
        }
        public ObservableCollection<Aircraft> Aircrafts => FlightRepository.GetAllAircrafts(); //самолёты для выбора в combobox
        #endregion

        #region OPEN ALL WINDOWS
        private RelayCommand openAddNewFlightWindowCommand;
        public RelayCommand OpenAddNewFlightWindowCommand //команда для открытия окна добавления
        {
            get
            {
                return openAddNewFlightWindowCommand ??
                  new RelayCommand(obj => { FlightRepository.OpenAddNewFlightWindow(); }); //если null, то вычисляется
            }
        }

        private RelayCommand openEditFlightWindowCommand;
        public RelayCommand OpenEditFlightWindowCommand //команда для открытия окна редактирования
        {
            get
            {
                return openEditFlightWindowCommand ??
                  new RelayCommand(obj => { FlightRepository.OpenEditFlightWindow(); }, 
                  param => FlightRepository.selectedFlight != null); //если не null, то новая команда
            }
        }

        private RelayCommand openDeleteFlightWindowCommand;
        public RelayCommand OpenDeleteFlightWindowCommand //команда для открытия окна удаления
        {
            get
            {
                return openDeleteFlightWindowCommand ??
                  new RelayCommand(obj => { FlightRepository.OpenDeleteFlightWindow(); }, 
                  param => FlightRepository.selectedFlight != null); //если не null, то новая команда
            }
        }
        #endregion

        #region EDIT COMMANDS
        private RelayCommand addNewFlight; //добавить рейс
        public RelayCommand AddNewFlight
        {
            get
            {
                return addNewFlight ??
                    (addNewFlight = new RelayCommand(obj =>
                    {
                        FlightRepository.AddNewFlight
                        (flightID, date, departure, arrival, boardingTime, lastCallTime, outTime, arrivalTime, price);
                    }));
            }
        }

        private RelayCommand editFlight; //редактировать рейс
        public RelayCommand EditFlight
        {
            get
            {
                return editFlight ??
                    (editFlight = new RelayCommand(obj =>
                    {
                        FlightRepository.EditFlight();                        
                    }));
            }
        }

        private RelayCommand deleteFlight; //редактировать рейс
        public RelayCommand DeleteFlight
        {
            get
            {
                return deleteFlight ??
                    (deleteFlight = new RelayCommand(obj =>
                    {
                        FlightRepository.DeleteFlight();
                    }));
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "") //параметр - имя свойства
        {
            //MessageBox.Show(propertyName); //чтобы можно было следить за изменениями
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); //? (оператор условного null) - проверка на null. Invoke - вызов делегата
        }
    }
}
