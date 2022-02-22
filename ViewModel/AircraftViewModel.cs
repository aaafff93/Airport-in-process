using Airport.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Airport.ViewModel
{
    class AircraftViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Aircraft> allAircrafts;
        public ObservableCollection<Aircraft> AllAircrafts //свойство для всех самолётов
        {
            get
            {
                using (UserContext db = new UserContext())
                {
                    //allAircrafts = new ObservableCollection<Aircraft>(db.Aircrafts.ToList()); //?
                    allAircrafts = new ObservableCollection<Aircraft>(db.Aircrafts.Include(a=>a.Flights)); //?
                    return allAircrafts;
                }
            }
            set
            {
                allAircrafts = value;
                OnPropertyChanged("AllAircrafts"); //уведомление
            }
        }

        private int aircraftID;
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
        }
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }

        }
        private string tailNumber;
        public string TailNumber
        {
            get
            {
                return tailNumber;
            }
            set
            {
                tailNumber = value;
                OnPropertyChanged("TailNumber");
            }
        }
        private short maximumCapacity;
        public short MaximumCapacity
        {
            get
            {
                return maximumCapacity;
            }
            set
            {
                maximumCapacity = value;
                OnPropertyChanged("MaximumCapacity");
            }
        }
        private string airlane;
        public string Airlane
        {
            get
            {
                return airlane;
            }
            set
            {
                airlane = value;
                OnPropertyChanged("Airlane");
            }
        }

        public byte[] image { get; set; }
        public byte[] Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }

        public Aircraft SelectedAircraft //выбранный самолёт (из статического класса)
        {
            get
            {
                    return AircraftRepository.selectedAircraft;
            }
            set
            {
                AircraftRepository.selectedAircraft = value;
                OnPropertyChanged("SelectedAircraft");
            }
        }

        #region OPEN ALL WINDOWS AND DIALOGS COMMANDS

        private RelayCommand openFileDialogCommandForAdd; //диалог при добавлении
        public RelayCommand OpenFileDialogCommandForAdd
        {
            get
            {
                return openFileDialogCommandForAdd ??
                    new RelayCommand(obj => { image = AircraftRepository.OpenDialog(image); OnPropertyChanged("Image"); }); //делегат?
            }
        }

        private RelayCommand openFileDialogCommandForEdit; //диалог при редактировании
        public RelayCommand OpenFileDialogCommandForEdit
        {
            get
            {
                return openFileDialogCommandForEdit ??
                    new RelayCommand(obj => { AircraftRepository.selectedAircraft.Image = AircraftRepository.OpenDialog(AircraftRepository.selectedAircraft.Image); OnPropertyChanged("SelectedAircraft"); }); //делегат?
            }
        }

        private RelayCommand openFileDialogCommandDeleteImageForAdd; //удалить картинку при добавлении
        public RelayCommand OpenFileDialogCommandDeleteImageForAdd
        {
            get
            {
                return openFileDialogCommandDeleteImageForAdd ??
                    new RelayCommand(obj => { image = null; OnPropertyChanged("Image"); }); //делегат?
            }
        }

        private RelayCommand openFileDialogCommandDeleteImageForEdit; //удалить картинку при редактировании
        public RelayCommand OpenFileDialogCommandDeleteImageForEdit
        {
            get
            {
                return openFileDialogCommandDeleteImageForEdit ??
                    new RelayCommand(obj => { AircraftRepository.selectedAircraft.Image = null; OnPropertyChanged("SelectedAircraft"); }); //делегат?
            }
        }

        private RelayCommand openAddNewAircraftWindowCommand;
        public RelayCommand OpenAddNewAircraftWindowCommand //команда для открытия окна добавления
        {
            get
            {
                return openAddNewAircraftWindowCommand ?? 
                  new RelayCommand(obj => { AircraftRepository.OpenAddNewAircraftWindow(); }); //если null, то вычисляется
            }
        }

        private RelayCommand openEditAircraftWindowCommand;
        public RelayCommand OpenEditAircraftWindowCommand //команда для открытия окна редактирования
        {
            get
            {
                return openEditAircraftWindowCommand ??
                  new RelayCommand(obj => { AircraftRepository.OpenEditAircraftWindow(); }, 
                  param => AircraftRepository.selectedAircraft != null); //если не null, то новая команда
            }
        }

        private RelayCommand openDeleteAircraftWindowCommand;
        public RelayCommand OpenDeleteAircraftWindowCommand //команда для открытия окна удаления
        {
            get
            {
                return openDeleteAircraftWindowCommand ??
                  new RelayCommand(obj => { AircraftRepository.OpenDeleteAircraftWindow(); }, 
                  param => AircraftRepository.selectedAircraft != null); //если не null, то новая команда
            }
        }
        #endregion

        #region EDIT COMMANDS
        private RelayCommand addNewAircraft;
        public RelayCommand AddNewAircraft
        {
            get
            {
                return addNewAircraft ??
                    (addNewAircraft = new RelayCommand(obj =>
                    {
                        AircraftRepository.AddNewAircraft(aircraftID, name, tailNumber, maximumCapacity, airlane, image);
                    }));
            }
        }

        private RelayCommand editAircraft;
        public RelayCommand EditAircraft
        {
            get
            {
                return editAircraft ??
                    (editAircraft = new RelayCommand(obj =>
                    {
                        AircraftRepository.EditAircraft();
                    }));
            }
        }

        private RelayCommand deleteAircraft;
        public RelayCommand DeleteAircraft
        {
            get
            {               
                return deleteAircraft ??
                    (deleteAircraft = new RelayCommand(obj =>
                    {
                        AircraftRepository.DeleteAircraft();
                    }));
            }
        }

        private RelayCommand deleteAircraftCancel;
        public RelayCommand DeleteAircraftCancel
        {
            get
            {
                return deleteAircraftCancel ??
                    (deleteAircraftCancel = new RelayCommand(obj =>
                    {
                        AircraftRepository.deleteAircraftWindow.Close();
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
