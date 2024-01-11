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
        public AppointmentHistory()
        {
            InitializeComponent();
        }

        public void LoadAppointments(List<Appointment> appointments)
        {
            appointments.ForEach(appointment => appointmentHistoryDataGrid.Items.Add(appointment));
        }

        public event Action OnCloseAppointmentHistory;

        public void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            OnCloseAppointmentHistory.Invoke();
        }
    }
}
