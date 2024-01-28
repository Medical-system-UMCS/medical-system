using System;
using System.Collections.Generic;
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
using WpfAppMedicalSystemsDraft.Models;

namespace WpfAppMedicalSystemsDraft.UserControls
{
    /// <summary>
    /// Interaction logic for AppointmentHistory.xaml
    /// </summary>
    public partial class AppointmentHistory : UserControl
    {
        private class CombinedAppointment
        {
            public Appointment appointment { get; set; }
            public string doctor { get; set; }
        }

        private List<CombinedAppointment> appointments;

        public AppointmentHistory()
        {
            InitializeComponent();
            appointments = new List<CombinedAppointment>();
        }

        public void LoadAppointments(List<Appointment> appointments, List<Doctor> doctors)
        {
            //appointmentHistoryDataGrid.ItemsSource = appointments;
            //var first = appointments.First();

            //MessageBox.Show(first.Doctor.FirstName);
            //MessageBox.Show(first.Doctor.LastName);

            foreach (var item in appointments)
            {
                Doctor doctor = doctors.Where(doctor => doctor.Id == item.DoctorId).First();


                this.appointments.Add(new CombinedAppointment
                {
                    appointment = item,
                    doctor = doctor.FirstName + " " + doctor.LastName
                });
            }

            appointmentHistoryDataGrid.ItemsSource = this.appointments;
        }



        public event Action OnCloseAppointmentHistory;

        public void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            OnCloseAppointmentHistory.Invoke();
        }
    }
}
