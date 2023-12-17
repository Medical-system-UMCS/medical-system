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
        public List<Doctor> ApprovedDoctors { get; set; }
        public ApproveDoctors()
        {
            InitializeComponent();
            

            Table.ItemsSource = ApprovedDoctors; 
        }

        public void LoadDoctors(List<Doctor> doctors)
        {
            ApprovedDoctors = new List<Doctor>(doctors);

            Table.ItemsSource = ApprovedDoctors;
        }

        private void ConfirmDoctor_Click(object sender, RoutedEventArgs e)
        {
            if (Table.SelectedItem is Doctor selectedDoctor)
            {
                ApprovedDoctors.Remove(selectedDoctor);

                Table.Items.Refresh();
            }
        }

        private void CancelAddDoctor_Click(object sender, RoutedEventArgs e)
        {
            AddDoctorOverlay.IsOpen = false;
        }

        private void Table_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            confirmButton.IsEnabled = Table.SelectedItems.Count == 1;
        }

        public List<Doctor> GetApprovedDoctors()
        {
            return ApprovedDoctors;
        }
    }
}
