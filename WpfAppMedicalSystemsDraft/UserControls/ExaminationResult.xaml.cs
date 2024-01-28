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
   
    public partial class ExaminationResult : UserControl
    {
        public ExaminationResult()
        {
            InitializeComponent();

            DataContext = this;
        }

        
        public void LoadAppointments(List<Appointment> appointments, List<Patient> patients)
        {
            AppointmentList.Items.Clear();
            appointments.ForEach(el => {
                var patient = patients.First(patient => el.PatientId == patient.Id);
                AppointmentList.Items.Add(string.Join(' ', el.Id, "  |  ", el.Date));
            });
        }
        

        public void SubmitExaminationDownload_Click(object sender, RoutedEventArgs e)
        {
            SubmitDownload();
        }

        public void SubmitDownload()
        {
            if (AppointmentList.SelectedItem == null)
            {
                MessageBox.Show("Wybierz wizytę", "Alert");
            }
            else
            {

                int appointmentID = int.Parse(AppointmentList.Text.Substring(0, AppointmentList.Text.IndexOf(" ")));
                string appointmentDate = AppointmentList.Text.Substring(appointmentID.ToString().Length + 7);       

                OnDownloadExaminationResult.Invoke(appointmentID, appointmentDate);
            }
        }


        public event Action OnExaminationResultClose;
        public event Action<int, string> OnDownloadExaminationResult;

        private void ExaminationResult_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SubmitDownload();
            }
            if (e.Key == Key.Escape)
            {
                OnExaminationResultClose.Invoke();
            }
        }



    }
}
