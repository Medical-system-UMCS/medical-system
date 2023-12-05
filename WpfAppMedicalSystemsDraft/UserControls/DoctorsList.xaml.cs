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
    /// Logika interakcji dla klasy DoctorsList.xaml
    /// </summary>
    public partial class DoctorsList : UserControl
    {
        public List<Doctor> Doctors { get; set; } = new List<Doctor>();
        public DoctorsList()
        {
            InitializeComponent();
            Table.ItemsSource = Doctors;          
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            OnCloseWindow.Invoke();
        }
        
        public event Action OnCloseWindow;
    }
}
