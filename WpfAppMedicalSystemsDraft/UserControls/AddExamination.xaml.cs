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
    /// Logika interakcji dla klasy AddExamination.xaml
    /// </summary>
    public partial class AddExamination : UserControl
    {
        public AddExamination()
        {
            InitializeComponent();
        }

        public void LoadAppointmentsWithExaminations(List<Appointment> appointments, List<Patient> patients)
        {
            ExaminationsList.Items.Clear();

            appointments.ForEach(el => {
                var patient = patients.First(patient => el.PatientId == patient.Id);
                ExaminationsList.Items.Add(string.Join(' ', el.Id, patient.FirstName, patient.LastName, el.Date));
            });
        }

        private void SubmitExamination_Click(object sender, RoutedEventArgs e)
        {
            SubmitExamination();
        }
        
        private void SubmitExamination()
        {
            if (ExaminationsList.SelectedIndex == -1)
            {
                MessageBox.Show("Wybierz wizytę!");
                return;
            }
            if (Symptoms.Text.Trim().Length == 0)
            {
                MessageBox.Show("Wprowadź objawy!");
                return;
            }
            if (Diagnosis.Text.Trim().Length == 0)
            {
                MessageBox.Show("Wprowadź diagnozę!");
                return;
            }
            if (Treatment.Text.Trim().Length == 0)
            {
                MessageBox.Show("Wprowadź leczenie!");
                return;
            }
            Examination examination = new Examination
            {
                Symptoms = Symptoms.Text.Trim(),
                Diagnosis = Diagnosis.Text.Trim(),
                Treatment = Treatment.Text.Trim(),
                AppointmentId = int.Parse(ExaminationsList.Text.Trim().First().ToString()),
            };
            OnSubmitExamination.Invoke(examination);
        }

        public event Action<Examination> OnSubmitExamination;
        public event Action OnCloseExamination;

        private void AddExamination_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SubmitExamination();
            }
            if (e.Key == Key.Escape)
            {
                OnCloseExamination.Invoke();
            }
        }
     
    }
}
