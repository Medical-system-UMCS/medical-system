using System;
using System.Collections.Generic;
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

namespace WpfAppMedicalSystemsDraft.UserControls
{
    /// <summary>
    /// Interaction logic for ManageControl.xaml
    /// </summary>
    public partial class ApproveDoctors : UserControl
    {
        public ApproveDoctors()
        {
            InitializeComponent();
        }
        private void AddDoctor_Click(object sender, RoutedEventArgs e)
        {


            CancelAddDoctor_Click(sender, e);
        }

        private void CancelAddDoctor_Click(object sender, RoutedEventArgs e)
        {
            FirstName.Text = String.Empty;
            LastName.Text = String.Empty;
            SpecializationTextBox.Text = String.Empty;
            AddDoctorOverlay.IsOpen = false;
        }
    }
}
