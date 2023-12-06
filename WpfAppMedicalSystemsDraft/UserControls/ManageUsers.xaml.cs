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

        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<Patient> Patients { get; set; }

        public ManageUsers()
        {
            InitializeComponent();
            Doctors = new ObservableCollection<Doctor>();
            Patients = new ObservableCollection<Patient>();

            //test doctor
            Doctors.Add(new Doctor
            {
                Id = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
                Specialization = "Kardiologia",
                ScientificDegree = "Lek. n. med.",
                UserId = 101 
            });

            Doctors.Add(new Doctor
            {
                Id = 2,
                FirstName = "sdsd",
                LastName = "sdf",
                Specialization = "Kardiologia",
                ScientificDegree = "Lek. n. med.",
                UserId = 101
            });

            //test patient
            Patients.Add(new Patient
            {
                Id = 1,
                FirstName = "Anna",
                LastName = "Nowak",
                Dayofbirth = new DateOnly(1990, 1, 1),
                Height = 170.5, 
                Weight = 65.0, 
                UserId = 102
            });

            Patients.Add(new Patient
            {
                Id = 2,
                FirstName = "foo",
                LastName = "bar",
                Dayofbirth = new DateOnly(2003, 11, 12),
                Height = 170.5,
                Weight = 65.0,
                UserId = 102
            });


            doctorGrid.ItemsSource = Doctors;
            patientGrid.ItemsSource = Patients;
        }

        //private static IEnumerable<User> GetUsersFromDatabase()
        //{
        //    // Replace this with your actual database query to get users
        //    // For demonstration, returning a dummy list
           

        //}

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            ManageUsersOverlay.IsOpen = false;
        }
    }
}
