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

            ApprovedDoctors = new List<Doctor>
            {
                // Add dummy data for doctors
                new() {
                    Id = 1,
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Specialization = "Kardiologia",
                    ScientificDegree = "Lek. n. med.",
                  
                    // Add more properties as needed
                },
                new () {
                    Id = 2,
                    FirstName = "Piotr",
                    LastName = "Nowak",
                    Specialization = "Neurochirurgia",
                    ScientificDegree = "Dr. n. med.",
                    
                    // Add more properties as needed
                }
            };

            
            doctorApprovalGrid.ItemsSource = ApprovedDoctors;
        }
        private void ConfirmDoctor_Click(object sender, RoutedEventArgs e)
        {
            if (doctorApprovalGrid.SelectedItem is Doctor selectedDoctor)
            {
               
                ApprovedDoctors.Remove(selectedDoctor);
                //user.verified = true
               
                doctorApprovalGrid.Items.Refresh();
            }

            CancelAddDoctor_Click(sender, e);
        }

        private void CancelAddDoctor_Click(object sender, RoutedEventArgs e)
        {
            //FirstName.Text = String.Empty;
            //LastName.Text = String.Empty;
            //SpecializationTextBox.Text = String.Empty;
            AddDoctorOverlay.IsOpen = false;
        }

        private void DoctorApprovalGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Enable/disable Confirm button based on whether a row is selected
            confirmButton.IsEnabled = doctorApprovalGrid.SelectedItems.Count == 1;
        }
    }
}
