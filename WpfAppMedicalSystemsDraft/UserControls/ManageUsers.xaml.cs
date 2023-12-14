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

        //public List<Doctor> Doctors { get; set; }
        //public List<Patient> Patients { get; set; }

        public ManageUsers()
        {
            InitializeComponent();
        }

        public void LoadUsers(List<Doctor> doctors, List<Patient> patients)
        {
            patientGrid.ItemsSource = patients;
            doctorGrid.ItemsSource = doctors;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            ManageUsersOverlay.IsOpen = false;
        }
    }
}
