using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for ManageControl.xaml
    /// </summary>
    public partial class ApproveDoctors : UserControl
    {
        public List<Doctor> Doctors { get; set; }
        private List<Doctor> approvedDoctors { get; set; } = new List<Doctor>();

        public event Action<List<Doctor>> OnCloseApproveDoctors;

        public ApproveDoctors()
        {
            InitializeComponent();
            

            Table.ItemsSource = Doctors; 
        }

        public void LoadDoctors(List<Doctor> doctors)
        {
            this.Doctors = new List<Doctor>(doctors);

            Table.ItemsSource = this.Doctors;
        }

        private void ConfirmDoctor_Click(object sender, RoutedEventArgs e)
        {
            if (Table.SelectedItem is Doctor selectedDoctor)
            {

                Doctors.Remove(selectedDoctor);
                approvedDoctors.Add(selectedDoctor);
                Table.Items.Refresh();
            }
        }

        private void CancelAddDoctor_Click(object sender, RoutedEventArgs e)
        {
            OnCloseApproveDoctors.Invoke(approvedDoctors);
            
        }

        private void Table_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            confirmButton.IsEnabled = Table.SelectedItems.Count == 1;
        }

        public List<Doctor> GetApprovedDoctors()
        {
            return Doctors;
        }
    }
}
