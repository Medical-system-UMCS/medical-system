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
    /// Logika interakcji dla klasy AppointmentList.xaml
    /// </summary>
    public partial class AppointmentList : UserControl
    {
        private class CombinedAppointment
        {
            public Appointment appointment { get; set; }
            public string patient { get; set; }
        }
        private List<CombinedAppointment> appointments;
        public AppointmentList()
        {
            InitializeComponent();
            appointments = new List<CombinedAppointment>();
        }

        public void LoadAppointments(List<Appointment> appointments, List<Patient> patients)
        {            
            this.appointments.Clear();

            foreach (var item in appointments)
            {
                Patient patient = patients.Where(patient => patient.Id == item.PatientId).First();


                this.appointments.Add(new CombinedAppointment
                {
                    appointment = item,
                    patient = patient.FirstName + " " + patient.LastName
                });
            }

            appointmentHistoryDataGrid.ItemsSource = this.appointments;
        }
        public event Action OnCloseAppointmentList;

        public void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            OnCloseAppointmentList.Invoke();
        }
    }
}
