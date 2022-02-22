using Airport.View;
using Airport.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Airport.Models
{    
    public static class AircraftRepository
    {
        public static Aircraft selectedAircraft;

        #region EDIT METHODS
        public static void AddNewAircraft
            (int aircraftID, string name, string tailNumber, short maximumCapacity, string airlane, byte[] image) //метод добавления
        {
            try
            {
                using (UserContext db = new UserContext())
                {
                    Aircraft newAircraft = new Aircraft();
                    newAircraft.AircraftID = aircraftID;
                    newAircraft.Name = name;
                    newAircraft.TailNumber = tailNumber;
                    newAircraft.MaximumCapacity = maximumCapacity;
                    newAircraft.Airlane = airlane;
                    newAircraft.Image = image;
                    db.Entry(newAircraft).State = EntityState.Added;
                    //db.Aircrafts.Add(newAircraft);
                    db.SaveChanges();
                    MessageBox.Show("Вставлено");
                }
                addNewAircraftWindow.Close(); //в finally?                
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
                //string errors = "";
                /*foreach (InnerException dbEntityEntry in e.Entries)
                {

                }*/
                MessageBox.Show(ex.InnerException.InnerException.Message); //возможна коллекция?
            }
            finally
            {
                //allAircrafts = GetAllAircrafts(); //обновление
                UpdateDB.UpdateAllDB();
            }
        }

        public static void EditAircraft() //метод редактирования
        {
            try
            {
                using (UserContext db = new UserContext())
                {
                    //MessageBox.Show(image[0].ToString());
                    //selectedAircraft.Image = image;//
                    //MessageBox.Show(image[0].ToString());
                    db.Entry(selectedAircraft).State = EntityState.Modified; //так как в разных контекстах                              
                    db.SaveChanges();
                    MessageBox.Show("Изменено");
                }               
                editAircraftWindow.Close();
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
                string errors = "";
                /*foreach (InnerException dbEntityEntry in e.Entries)
                {

                }*/
                MessageBox.Show(ex.InnerException.InnerException.Message); //возможна коллекция?
            }
            finally
            {
                //allAircrafts = GetAllAircrafts();
                UpdateDB.UpdateAllDB();
            }
        }

        public static void DeleteAircraft()
        {
            try
            {
                using (UserContext db = new UserContext())
                {
                    //db.Aircrafts.Remove(selectedAircraft);
                    db.Entry(selectedAircraft).State = EntityState.Deleted; //так как в разных контекстах                              
                    db.SaveChanges();
                }
                MessageBox.Show("Удалено");
                deleteAircraftWindow.Close();
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
                string errors = "";
                /*foreach (InnerException dbEntityEntry in e.Entries)
                {

                }*/
                MessageBox.Show(ex.InnerException.InnerException.Message); //возможна коллекция?
            }
            finally
            {
                //allAircrafts = GetAllAircrafts();
                UpdateDB.UpdateAllDB();
            }
        }
        #endregion

        #region OPEN ALL WINDOWS AND DIALOGS

        public static OpenFileDialog openFileDialog;

        public static byte[] OpenDialog(byte[] image)
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != "")
                return File.ReadAllBytes(openFileDialog.FileName);
            else
                return image;            
        }

        public static AddNewAircraftWindow addNewAircraftWindow;
        public static void OpenAddNewAircraftWindow() //открытие окна добавления самолёта
        {
            addNewAircraftWindow = new AddNewAircraftWindow();
            addNewAircraftWindow.ShowDialog();
        }

        public static EditAircraftWindow editAircraftWindow;
        public static void OpenEditAircraftWindow() //открытие окна редактирования самолёта
        {
            editAircraftWindow = new EditAircraftWindow();
            editAircraftWindow.ShowDialog();
        }

        public static DeleteAircraftWindow deleteAircraftWindow;
        public static void OpenDeleteAircraftWindow() //открытие окна редактирования самолёта
        {
            deleteAircraftWindow = new DeleteAircraftWindow();
            deleteAircraftWindow.ShowDialog();
        }
        #endregion
    }
}
