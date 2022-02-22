using Airport.View;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Airport.Models
{
    public static class BoardingPassRepository
    {
        public static BoardingPass selectedBoardingPass;
        public static Flight selectedFlight;       

        #region OPEN ALL WINDOWS
        public static AddNewBoardingPassWindow addNewBoardingPassWindow;
        public static void OpenAddNewFlightWindow()
        {
            addNewBoardingPassWindow = new AddNewBoardingPassWindow();
            addNewBoardingPassWindow.ShowDialog();
        }
        public static EditBoardingPassWindow editBoardingPassWindow;
        public static void OpenEditBoardingPassWindow()
        {
            editBoardingPassWindow = new EditBoardingPassWindow();
            editBoardingPassWindow.ShowDialog();
        }
        #endregion

        #region EDIT METHODS
        public static void AddNewBoardingPass(string passengerName, string passport, string seat, decimal price)
        {
            try
            {
                using (UserContext db = new UserContext())
                {

                    BoardingPass newBoardingPass = new BoardingPass();
                    //newBoardingPass.BoardingPassID = boardingPassID;
                    newBoardingPass.PassengerName = passengerName;
                    newBoardingPass.Passport = passport;
                    newBoardingPass.Seat = seat;
                    newBoardingPass.Price = price;
                    newBoardingPass.FlightID = selectedFlight.FlightID;

                    db.BoardingPasses.Add(newBoardingPass);

                    db.SaveChanges();
                    MessageBox.Show("Вставлено");
                    UpdateDB.UpdateAllDB();
                }
                addNewBoardingPassWindow.Close();                
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
                              
                //UpdateDB.UpdateAllDB();
                //using (UserContext db = new UserContext())
                  //  selectedFlight = db.Flights.Include(b => b.BoardingPasses).Single(b => b.FlightID == selectedFlight.FlightID);
            }
        }
        public static void EditBoardingPass()
        {
            try
            {
                using (UserContext db = new UserContext())
                {

                    

                    db.Entry(selectedBoardingPass).State = EntityState.Modified;
                    db.Entry(selectedFlight).State = EntityState.Modified;

                    db.SaveChanges();
                    MessageBox.Show("Изменено");
                    UpdateDB.UpdateAllDB();
                }
                editBoardingPassWindow.Close();
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
            /*catch (DbUpdateException ex)
            {
                MessageBox.Show(ex.InnerException.InnerException.Message); //возможна коллекция?
            }*/
            catch (NullReferenceException)
            {
                MessageBox.Show("Заполнены не все поля!");
            }
            finally
            {
                UpdateDB.UpdateAllDB();               
                //UpdateDB.UpdateAllDB();
                //using (UserContext db = new UserContext())
                //  selectedFlight = db.Flights.Include(b => b.BoardingPasses).Single(b => b.FlightID == selectedFlight.FlightID);

            }
        }
        #endregion
    }
}
