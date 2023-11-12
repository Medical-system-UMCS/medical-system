using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
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
using WpfAppMedicalSystemsDraft.Enums;
using WpfAppMedicalSystemsDraft.Models;

namespace WpfAppMedicalSystemsDraft
{
    class AppSettings
    {
        public string ConnectionString { get; set; }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string AccountTypeEnum { get; set; } = AccountType.DOCTOR;
        private MedicalSystemsContext medicalSystemsContext;
        public MainWindow()
        {
            var settings = ReadSettings();
            if (settings == null)
            {
                MessageBox.Show("Error: Cannot load settings!");
                Application.Current.Shutdown();
                return;
            }
            InitializeComponent();
            LoginControl.OnSubmitLogin += LoginControlOnSubmit;
            medicalSystemsContext = new MedicalSystemsContext(settings.ConnectionString);
            DataContext = this;
        }

        private static AppSettings? ReadSettings()
        {
            string filePath = "data.bin";
            string? connectionString;

            if (File.Exists(filePath))
            {

                FileStream fs1 = new FileStream(filePath, FileMode.Open);
                BinaryReader br = new BinaryReader(fs1);
                try
                {
                    connectionString = br.ReadString();
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
                fs1.Close();
            }
            else
            {
                return null;
            }
            return new AppSettings { ConnectionString = connectionString };
        }

        public void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginControl.Visibility = Visibility.Visible;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Okno rejestracji");
        }

        private void DoctorsList_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Lista lekarzy");
        }

        private void AddAppointment_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Umów się na wizytę");
        }

        private void ExitApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LoginControlOnSubmit(string username, string password)
        {          
            LoginControl.Visibility = Visibility.Hidden;
        }
    }
}