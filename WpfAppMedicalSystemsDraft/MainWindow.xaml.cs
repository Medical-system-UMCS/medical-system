using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
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
using WpfAppMedicalSystemsDraft.Services;

namespace WpfAppMedicalSystemsDraft
{
    class AppSettings
    {
        public string ConnectionString { get; set; }
        public string SmtpApiKey { get; set; }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string AccountTypeEnum { get; private set; } = AccountType.ADMIN;
        private MedicalSystemsContext medicalSystemsContext;
        private EmailService emailService;
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
            emailService = new EmailService(settings.SmtpApiKey);
            DataContext = this;
        }


        private void UserControlUsers_CloseClicked(object sender, EventArgs e)
        {
            // Handle the close logic here
            Close();
        }

        private static AppSettings? ReadSettings()
        {
            string filePath = "data.bin";
            string? connectionString;
            string? smtpApiKey;
            if (File.Exists(filePath))
            {

                FileStream fs1 = new FileStream(filePath, FileMode.Open);
                BinaryReader br = new BinaryReader(fs1);
                try
                {
                    connectionString = BinaryToString(br.ReadString());
                    smtpApiKey = BinaryToString(br.ReadString());
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
            return new AppSettings { ConnectionString = connectionString, SmtpApiKey = smtpApiKey };
        }

        static string BinaryToString(string data)
        {
            List<byte> byteList = new List<byte>();

            for (int i = 0; i < data.Length; i += 8)
            {
                byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
            }
            return Encoding.ASCII.GetString(byteList.ToArray());
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

        private void ConfirmDoctor_Click(object sender, RoutedEventArgs e)
        {
            ApproveDoctorsControl.AddDoctorOverlay.IsOpen = true;
            ApproveDoctorsControl.FirstName.Focus();
        }

        private void ManageUsers_Click(object sender, RoutedEventArgs e)
        {
            ManageUsersControl.ManageUsersOverlay.IsOpen = true;

        
        }

        private void ExitApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LoginControlOnSubmit(string username, string password)
        {
            User? user = medicalSystemsContext.Users.FirstOrDefault(user => user.Login.Equals(username));
            if (user == null)
            {
                MessageBox.Show("Użytkownik nie został znaleziony!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                LoginControl.Visibility = Visibility.Hidden;
                return;
            }
            using (SHA256 sHA256 = SHA256.Create())
            {
                byte[] inputBytes = sHA256.ComputeHash(Encoding.UTF8.GetBytes(password));
                string computedHash = BitConverter.ToString(inputBytes).Replace("-", string.Empty).ToLower();
                if (computedHash != user.Password)
                {
                    MessageBox.Show("Użytkownik nie został znaleziony!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    LoginControl.Visibility = Visibility.Hidden;
                    return;
                }
            }
            AccountTypeEnum = user.AccountType;
            LoginControl.Visibility = Visibility.Hidden;

            if (AccountTypeEnum.Equals(AccountType.ADMIN))
            {
                ManageControl.Visibility = Visibility.Visible;
                Register.Visibility = Visibility.Collapsed;
                LogIn.Visibility = Visibility.Collapsed;
                LogOut.Visibility = Visibility.Visible;
            }
            if (AccountTypeEnum.Equals(AccountType.PACIENT))
            {
                Appointments.Visibility = Visibility.Visible;
                Doctors.Visibility = Visibility.Visible;
                Register.Visibility = Visibility.Collapsed;
                LogIn.Visibility = Visibility.Collapsed;
                LogOut.Visibility = Visibility.Visible;
            }
            if (AccountTypeEnum.Equals(AccountType.DOCTOR))
            {
                Doctors.Visibility = Visibility.Visible;
                ManageExaminations.Visibility = Visibility.Visible;
                Register.Visibility = Visibility.Collapsed;
                LogIn.Visibility = Visibility.Collapsed;
                LogOut.Visibility = Visibility.Visible;
            }
        }

    }
}