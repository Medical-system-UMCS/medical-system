using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAppMedicalSystemsDraft.Enums;
using WpfAppMedicalSystemsDraft.Models;

namespace WpfAppMedicalSystemsDraft.UserControls
{
    /// <summary>
    /// Interaction logic for ManageUsers.xaml
    /// </summary>
    public partial class ManageUsers : UserControl
    {
        private class CombinedDoctor
        { 
           public Doctor doctor { get; set; }
           public bool isVerified { get; set; }
        }

        private class CombinedPatient
        {
            public Patient patient { get; set; }
            public bool isVerified { get; set; }
        }

        private bool loaded = false;

        private List<CombinedDoctor> Doctors;
        private List<CombinedPatient> Patients;

        public ManageUsers()
        {
            InitializeComponent();
            Doctors = new List<CombinedDoctor>();
            Patients = new List<CombinedPatient>();
        }

        public void LoadUsers(List<Doctor> doctors, List<Patient> patients, List<User> users)
        {
            Doctors.Clear();
            Patients.Clear();

            foreach (Doctor doctor in doctors)
            {
                Doctors.Add(new CombinedDoctor
                {
                    doctor = doctor,
                    isVerified = doctor.User.Verified
                });
            }

            doctorGrid.ItemsSource = Doctors;

            foreach (Patient patient in patients)
            {
                Patients.Add(new CombinedPatient { patient = patient, isVerified = patient.User.Verified });
            }

            patientGrid.ItemsSource = Patients;

            loaded = true;  
        }

        public event Action OnCloseWindow;
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            OnCloseWindow.Invoke();
        }
        public List<Doctor> GetDoctors()
        {
            List<Doctor> result = new();

            foreach (var combined_doctor in this.Doctors)
            {
                result.Add(combined_doctor.doctor);
            }

            return result;
        }

        public List<Patient> GetPatients()
        {
            List<Patient> result = new();

            foreach (var combined_patient in this.Patients)
            {
                result.Add(combined_patient.patient);
            }

            return result;
        }

        public new bool IsLoaded()
        {
            return loaded;
        }
    }
}
