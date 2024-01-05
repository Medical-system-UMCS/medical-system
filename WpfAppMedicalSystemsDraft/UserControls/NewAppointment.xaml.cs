using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfAppMedicalSystemsDraft.Enums;
using WpfAppMedicalSystemsDraft.Helpers;
using WpfAppMedicalSystemsDraft.Models;

namespace WpfAppMedicalSystemsDraft.UserControls
{

    public partial class NewAppointment : UserControl
    {

        public NewAppointment()
        {
            InitializeComponent();

            DataContext = this;
            
        }

        public void LoadDoctors(List<Doctor> doctors)
        {
            DoctorPicker.Items.Clear();
            doctors.ForEach(doctor => DoctorPicker.Items.Add(doctor.Id + "  |  " + doctor.FirstName + " " + doctor.LastName + "  |  " + doctor.Specialization));
        }


        public void SubmitAppointment_Click(object sender, RoutedEventArgs e)
        {
            SubmitAppointment();
        }

        public void SubmitAppointment()
        {
            //OnCloseNewAppointment.Invoke();


            if (AppointmentTypePicker.SelectedItem == null)
            {
                MessageBox.Show("Wybierz typ wizyty!", "Alert");
            }
            else if(DoctorPicker.SelectedItem == null)
            {
                MessageBox.Show("Wybierz lekarza!" ,"Alert");
            }
            else if(DateOfAppointmentPicker.SelectedDate == null) 
            {
                MessageBox.Show("Wybierz datę!", "Alert");
            }
            else if (TimePicker.SelectedItem == null)
            {
                MessageBox.Show("Wybierz godzinę!", "Alert");
            }
            else
            {
                var random = new Random();

                Appointment appointment = new Appointment
                {
                    DoctorId = int.Parse(DoctorPicker.Text.Substring(0, DoctorPicker.Text.IndexOf(" "))),
                    Date = DateTime.Parse(DateOfAppointmentPicker.Text) + TimeSpan.Parse(TimePicker.Text),
                    ExaminRoom = random.Next(1, 10).ToString(),
                    AppointmentType = AppointmentTypePicker.Text.Trim().ToLower()
                };

                OnMakeNewAppointment.Invoke(appointment);

                //MessageBox.Show("DoctorID: " + appointment.DoctorId.ToString() + "\nTermin: " + appointment.Date.ToString() + "\nPokój: " + appointment.ExaminRoom.ToString() +
                //                "\nTyp: " + appointment.AppointmentType.ToString(), "Dane Wizyty");
            } 
        }

        public event Action OnNewAppointmentClose;
        public event Action<Appointment> OnMakeNewAppointment;

        private void NewAppointmentOverlay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SubmitAppointment();
            }
            if (e.Key == Key.Escape)
            {
                OnNewAppointmentClose.Invoke();
            }
        }

    }
}
