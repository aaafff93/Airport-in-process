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
    class BoardingPassViewModel : INotifyPropertyChanged
    {
        #region ALL PROPERTIES
        private ObservableCollection<BoardingPass> allBoardingPasses;// = new ObservableCollection<BoardingPass>();
        public ObservableCollection<BoardingPass> AllBoardingPasses //свойство для всех рейсов
        {
            get
            {
                using (UserContext db = new UserContext())
                {
                    //foreach (BoardingPass boardingPass in BoardingPassRepository.selectedFlight.BoardingPasses)
                    //  allBoardingPasses.Add(boardingPass);
                    //allBoardingPasses = new ObservableCollection<BoardingPass>(BoardingPassRepository.selectedFlight.BoardingPasses);
                    return allBoardingPasses;
                }
            }
            set //
            {
                allBoardingPasses = value;
                OnPropertyChanged("AllBoardingPasses"); //уведомление
            }
        }

        public BoardingPass SelectedBoardingPass //выбранный рейс (из статического класса)
        {
            get
            {
                return BoardingPassRepository.selectedBoardingPass;
            }
            set
            {
                BoardingPassRepository.selectedBoardingPass = value;
                OnPropertyChanged("SelectedBoardingPass");
            }
        }

        private int boardingPassID;
        public int BoardingPassID
        {
            get
            {
                return boardingPassID;
            }
            set
            {
                boardingPassID = value;
                OnPropertyChanged("BoardingPassID");
            }
        }
        private string passengerName;
        public string PassengerName
        {
            get
            {
                return passengerName;
            }
            set
            {
                passengerName = value;
                OnPropertyChanged("PassengerName");
            }
        }
        private string passport;
        public string Passport
        {
            get
            {
                return passport;
            }
            set
            {
                passport = value;
                OnPropertyChanged("Passport");
            }
        }
        private string seat;
        public string Seat
        {
            get
            {
                return seat;
            }
            set
            {
                seat = value;
                OnPropertyChanged("Seat");
            }
        }

        private decimal price;
        public decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }
        private ObservableCollection<decimal> prices = new ObservableCollection<decimal>();
        public ObservableCollection<decimal> Prices
        {
            get
            {
                prices.Add(BoardingPassRepository.selectedFlight.Price.FirstClass.Value);
                prices.Add(BoardingPassRepository.selectedFlight.Price.BusinessClass.Value);
                prices.Add(BoardingPassRepository.selectedFlight.Price.EconomyClass);
                return prices;
            }
            set
            {
                prices = value;                
                OnPropertyChanged("Price");
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

        private Flight flight;
        public Flight Flight
        {
            get
            {
                return flight;
            }
            set
            {
                flight = value;
                OnPropertyChanged("Flight");
            }
        }

        private ObservableCollection<Flight> flights;
        public ObservableCollection<Flight> Flights
        {
            get
            {
                using (UserContext db = new UserContext())
                    flights = new ObservableCollection<Flight>(db.Flights.Include(a => a.Aircraft).Include(b => b.BoardingPasses).Include(p => p.Price));
                return flights;
            }
            set
            {
                flights = value;
                OnPropertyChanged("Flights");
            }
        }

        //private Flight selectedFlight;
        public Flight SelectedFlight
        {
            get
            {
                return BoardingPassRepository.selectedFlight;
            }
            set
            {
                BoardingPassRepository.selectedFlight = value;
                /*allBoardingPasses.Clear();
                foreach (BoardingPass boardingPass in BoardingPassRepository.selectedFlight.BoardingPasses)
                    allBoardingPasses.Add(boardingPass);
                BoardingPassRepository.selectedBoardingPass = null;*/

                allBoardingPasses = new ObservableCollection<BoardingPass>(BoardingPassRepository.selectedFlight.BoardingPasses);
                BoardingPassRepository.selectedBoardingPass = null;
                OnPropertyChanged("SelectedFlight");
                OnPropertyChanged("AllBoardingPasses");
            }
        }

        #endregion

        #region OPEN ALL WINDOWS
        private RelayCommand openAddNewBoardingPassWindowCommand;
        public RelayCommand OpenAddNewBoardingPassWindowCommand //команда для открытия окна добавления
        {
            get
            {
                return openAddNewBoardingPassWindowCommand ??
                  new RelayCommand(obj => { BoardingPassRepository.OpenAddNewFlightWindow(); }, 
                  param => BoardingPassRepository.selectedFlight != null); //если null, то вычисляется
            }
        }

        private RelayCommand openEditBoardingPassWindowCommand;
        public RelayCommand OpenEditBoardingPassWindowCommand //команда для открытия окна редактирования
        {
            get
            {
                return openEditBoardingPassWindowCommand ??
                  new RelayCommand(obj => { BoardingPassRepository.OpenEditBoardingPassWindow(); },
                  param => BoardingPassRepository.selectedBoardingPass != null); //если не null, то новая команда
            }
        }
        #endregion

        #region EDIT COMMANDS
        private RelayCommand addNewBoardingPass; //добавить билет
        public RelayCommand AddNewBoardingPass
        {
            get
            {
                return addNewBoardingPass ??
                    (addNewBoardingPass = new RelayCommand(obj =>
                    {
                        BoardingPassRepository.AddNewBoardingPass(passengerName, passport, seat, price);
                        //OnPropertyChanged("SelectedFlight");
                    }));
            }
        }
        private RelayCommand editBoardingPass; //добавить билет
        public RelayCommand EditBoardingPass
        {
            get
            {
                return editBoardingPass ??
                    (editBoardingPass = new RelayCommand(obj =>
                    {
                        BoardingPassRepository.EditBoardingPass();
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
