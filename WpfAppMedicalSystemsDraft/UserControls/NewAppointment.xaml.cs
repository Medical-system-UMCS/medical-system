using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        List<string> hours = new List<string>
            {
              "08:00", "08:30", "09:00", "09:30",
              "10:00", "10:30", "11:00", "11:30",
              "12:00", "12:30", "13:00", "13:30",
              "14:00", "14:30", "15:00", "15:30",
              "16:00", "16:30", "17:00", "17:30",
              "18:00", "18:30", "19:00", "19:30"
            };

        List<string> hoursToRemoveTrimmed = new List<string>();
        List<string> hoursToRemove = new List<string>();

        public void LoadDoctors(List<Doctor> doctors)
        {
            DoctorPicker.Items.Clear();
            doctors.ForEach(doctor => DoctorPicker.Items.Add(doctor.Id + "  |  " + doctor.FirstName + " " + doctor.LastName + "  |  " + doctor.Specialization));
        }

        public void LoadDateTime(List<Appointment> appointments)
        {
            appointments.ForEach(appointment => hoursToRemove.Add(appointment.Date.ToString()));
        }

        private void UpdateHours(object sender, EventArgs e)
        {
            DateTime selectedDate = DateOfAppointmentPicker.SelectedDate ?? DateTime.MinValue;
            string selectedDateString = selectedDate.ToString();

            hoursToRemove.ForEach(hour =>
            {
                if(hour.Substring(0, 10) == selectedDateString.Substring(0, 10))
                {
                    hoursToRemoveTrimmed.Add(hour.Substring(11, 5));
                }
            });

            TimePicker.Items.Clear();
            hours.ForEach(hour =>
            {
                int hourFlag = 0;
                hoursToRemoveTrimmed.ForEach(hoursToRemove =>
                {
                    if (hoursToRemove == hour) hourFlag++;
                });
                if (hourFlag == 0) TimePicker.Items.Add(hour);
            });
            
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

                ComboBoxItem comboBoxItem = (ComboBoxItem)AppointmentTypePicker.SelectedItem;

                Appointment appointment = new Appointment
                {
                    DoctorId = int.Parse(DoctorPicker.Text.Substring(0, DoctorPicker.Text.IndexOf(" "))),
                    Date = DateTime.Parse(DateOfAppointmentPicker.Text) + TimeSpan.Parse(TimePicker.Text),
                    ExaminRoom = random.Next(1, 10).ToString(),
                    AppointmentType = comboBoxItem.Name
                };

                OnMakeNewAppointment.Invoke(appointment);

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
