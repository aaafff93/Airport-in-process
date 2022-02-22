using Airport.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Airport.Models
{
    class FlightRepository
    {
        public static Flight selectedFlight;

        public static Aircraft selectedFromComboBoxAircraft;

        //public static short availableSeats;
        /*public static short GetAvailableSeats() ///////////////////////
        {
            using (UserContext db = new UserContext())
            {
                var v = db.Aircrafts.Single(c => c.AircraftID == selectedFromComboBoxAircraft.AircraftID);
                return v.MaximumCapacity;
            }
        }*/
        public static ObservableCollection<Aircraft> GetAllAircrafts()
        {
            using (UserContext db = new UserContext())
            {
                //MessageBox.Show("aircrafts");
                return new ObservableCollection<Aircraft>(db.Aircrafts);
            }
        }

        //public Price price;

        #region OPEN ALL WINDOWS
        public static AddNewFlightWindow addNewFlightWindow;
        public static void OpenAddNewFlightWindow()
        {
            addNewFlightWindow = new AddNewFlightWindow();
            addNewFlightWindow.ShowDialog();
        }

        public static EditFlightWindow editFlightWindow;
        public static void OpenEditFlightWindow()
        {
            editFlightWindow = new EditFlightWindow();
            editFlightWindow.ShowDialog();
        }

        public static DeleteFlightWindow deleteFlightWindow;
        public static void OpenDeleteFlightWindow()
        {
            deleteFlightWindow = new DeleteFlightWindow();
            deleteFlightWindow.ShowDialog();
        }
        #endregion

        #region EDIT METHODS
        public static void AddNewFlight
            (int flightID, DateTime date, string departure, string arrival, TimeSpan boardingTime,
            TimeSpan lastCallTime, TimeSpan outTime, TimeSpan arrivalTime, Price price)
        {
            try
            {
                using (UserContext db = new UserContext())
                {
                    Flight newFlight = new Flight();

                    newFlight.FlightID = flightID;
                    newFlight.Date = date;
                    newFlight.Departure = departure;
                    newFlight.Arrival = arrival;
                    newFlight.BoardingTime = boardingTime;
                    newFlight.LastCallTime = lastCallTime;
                    newFlight.OutTime = outTime;
                    newFlight.ArrivalTime = arrivalTime;
                    newFlight.AvailableSeats = selectedFromComboBoxAircraft.MaximumCapacity;
                    newFlight.SoldSeats = 0;
                    //newFlight.AircraftID = selectedFromComboBoxAircraft.AircraftID;
                    newFlight.Price = price;
                    newFlight.Aircraft = db.Aircrafts.Single(c => c.AircraftID == selectedFromComboBoxAircraft.AircraftID);

                    db.Flights.Add(newFlight);
                    //db.Entry(newFlight).State = EntityState.Added;
                    //db.Entry(price).State = EntityState.Added;
                    db.SaveChanges();
                    //selectedFromComboBoxAircraft = null;/////////////
                    MessageBox.Show("Вставлено");
                }
                addNewFlightWindow.Close(); //в finally?                
            }
            catch (DbEntityValidationException ex)
            {
                string errors = "";
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        errors += err.ErrorMessage + "\n";
                    }
                }
                MessageBox.Show("Ошибка:\n" + errors);
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show(ex.InnerException.InnerException.Message); //возможна коллекция?
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Заполнены не все поля!");
            }
            finally
            {
                selectedFromComboBoxAircraft = null;
                UpdateDB.UpdateAllDB();
            }
        }

        public static void EditFlight()           
        {
            try
            {
                using (UserContext db = new UserContext())
                {
                    if (selectedFromComboBoxAircraft != null)
                    {
                        selectedFlight.Aircraft = selectedFromComboBoxAircraft;
                        selectedFlight.AircraftID = selectedFromComboBoxAircraft.AircraftID;
                        selectedFlight.AvailableSeats = selectedFromComboBoxAircraft.MaximumCapacity;
                    }                    
                    db.Entry(selectedFlight).State = EntityState.Modified;
                    db.Entry(selectedFlight.Price).State = EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show("Изменено");                   
                }
                editFlightWindow.Close(); //в finally?                
            }
            catch (DbEntityValidationException ex)
            {
                string errors = "";
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        errors += err.ErrorMessage + "\n";
                    }
                }
                MessageBox.Show("Ошибка:\n" + errors);
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show(ex.InnerException.InnerException.Message); //возможна коллекция?
            }
            finally
            {
                selectedFromComboBoxAircraft = null;
                UpdateDB.UpdateAllDB();
            }
        }

        public static void DeleteFlight()
        {
            try
            {
                using (UserContext db = new UserContext())
                {
                    db.Flights.Remove(db.Flights.Single(f=>f.FlightID == selectedFlight.FlightID));
                    //db.Entry(selectedFlight).State = EntityState.Deleted;
                    db.SaveChanges();
                    MessageBox.Show("Удалено");
                }
                deleteFlightWindow.Close(); //в finally?                
            }
            catch (DbEntityValidationException ex)
            {
                string errors = "";
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        errors += err.ErrorMessage + "\n";
                    }
                }
                MessageBox.Show("Ошибка:\n" + errors);
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show(ex.InnerException.InnerException.Message); //возможна коллекция?
            }
            finally
            {
                //selectedFromComboBoxAircraft = null;
                UpdateDB.UpdateAllDB();
            }
        }
        #endregion
    }
}
