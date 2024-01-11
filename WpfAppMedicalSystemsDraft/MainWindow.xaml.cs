using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
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
using WpfAppMedicalSystemsDraft.Helpers;
using WpfAppMedicalSystemsDraft.Models;
using WpfAppMedicalSystemsDraft.Services;
using WpfAppMedicalSystemsDraft.UserControls;

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
        public string AccountTypeEnum { get; private set; } = AccountType.NOT_LOGGED;
        private User? currentUser;
        private Doctor? currentDoctor;
        private Patient? currentPatient;
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
            RegisterControl.OnRegisterDoctor += RegisterDoctorOnSubmit;
            RegisterControl.OnRegisterPatient += RegisterPacientOnSubmit;
            RegisterControl.OnRegisterClose += RegisterClose;
            LoginControl.OnSubmitLogin += LoginControlOnSubmit;
            LoginControl.OnCloseLogin += LoginControlClose;
            DoctorsListControl.OnCloseWindow += DoctorsListClose;

            NewAppointmentControl.OnNewAppointmentClose += NewAppointmentClose;
            NewAppointmentControl.OnMakeNewAppointment += MakeNewAppointmentOnSubmit;

            NewExaminationControl.OnCloseExamination += CloseNewExamination;
            NewExaminationControl.OnSubmitExamination += AddNewExamination;

            NewAppointmentHistory.OnCloseAppointmentHistory += CloseAppointmentHistory;

            medicalSystemsContext = new MedicalSystemsContext(settings.ConnectionString);           
            emailService = new EmailService(settings.SmtpApiKey);          
            DataContext = this;

            
            
        }

        private void AddNewExamination(Examination examination)
        {
            medicalSystemsContext.Examinations.Add(examination);
            medicalSystemsContext.SaveChanges();
            NewExaminationControl.Visibility = Visibility.Collapsed;
        }

        private void CloseNewExamination()
        {
            NewExaminationControl.Visibility = Visibility.Collapsed;
        }

        private void CloseAppointmentHistory()
        {
            NewAppointmentHistory.Visibility = Visibility.Collapsed; 
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
            LoginControl.Prepare();
            LoginControl.Visibility = Visibility.Visible;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegisterControl.Prepare();
            RegisterControl.Visibility = Visibility.Visible;
        }

        private void DoctorsList_Click(object sender, RoutedEventArgs e)
        {
            DoctorsListControl.LoadDoctors(medicalSystemsContext.Doctors.ToList());            
            DoctorsListControl.Visibility = Visibility.Visible;
        }

        private void AddAppointment_Click(object sender, RoutedEventArgs e)
        {
            //NewAppointmentControl.Prepare();
            NewAppointmentControl.LoadDoctors(medicalSystemsContext.Doctors.ToList());
            NewAppointmentControl.Visibility = Visibility.Visible;
        }

        private void AppointmentHistory_Click(object sender, RoutedEventArgs e)
        {
            if (currentPatient != null)
            {
                NewAppointmentHistory.LoadAppointments(currentPatient.Appointments.ToList());
                NewAppointmentHistory.Visibility = Visibility.Visible;
            }
        }

        private void ConfirmDoctor_Click(object sender, RoutedEventArgs e)
        {
            ApproveDoctorsControl.LoadDoctors(medicalSystemsContext.Doctors.ToList());
            ApproveDoctorsControl.AddDoctorOverlay.IsOpen = true;
            
        }

        private void ManageUsers_Click(object sender, RoutedEventArgs e)
        {
            if (ManageUsersControl.IsLoaded() == false)
            {
                ManageUsersControl.LoadUsers(medicalSystemsContext.Doctors.ToList(), medicalSystemsContext.Patients.ToList(), medicalSystemsContext.Users.ToList());
            }
           
            ManageUsersControl.ManageUsersOverlay.IsOpen = true;
        }

        private void ExitApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void RegisterClose()
        {
            RegisterControl.Visibility = Visibility.Hidden;
        }

        private void MakeNewAppointmentOnSubmit(Appointment appointment)
        {
            if (currentPatient == null)
            {
                return;
            }
            appointment.PatientId = currentPatient.Id;
            medicalSystemsContext.Appointments.Add(appointment);
            medicalSystemsContext.SaveChanges(); // Exception throw

            //currentUser = ;
            //currentPatient = ;

            string fullName = string.Join(' ', currentPatient.FirstName, currentPatient.LastName);
            string[] paramsValue = { currentUser.Login, fullName };
            emailService.SendEmail(currentUser.Email, fullName, EmailType.NEW_APPOINTMENT_CONFIRMATION, paramsValue);
            MessageBox.Show("Czekaj na potwierdzenie od administratora na podany email");
            NewAppointmentControl.Visibility = Visibility.Collapsed;

        }

        private void RegisterPacientOnSubmit(Patient patient, User user)
        {
            medicalSystemsContext.Users.Add(user);
            medicalSystemsContext.SaveChanges();
            patient.UserId = user.Id;
            medicalSystemsContext.Patients.Add(patient);
            medicalSystemsContext.SaveChanges();
            currentUser = user;
            currentPatient = patient;
            string fullName = string.Join(' ', patient.FirstName, patient.LastName);
            string[] paramsValue = { user.Login, fullName };
            emailService.SendEmail(user.Email, fullName, EmailType.ACCOUNT_CONFIRMATION, paramsValue);
            MessageBox.Show("Czekaj na potwierdzenie od administratora na podany email");
            RegisterControl.Visibility = Visibility.Collapsed;                      
            Appointments.Visibility = Visibility.Visible;
            Doctors.Visibility = Visibility.Visible;
            Register.Visibility = Visibility.Collapsed;
            LogIn.Visibility = Visibility.Collapsed;
            LogOut.Visibility = Visibility.Visible;
                        
        }

        private void RegisterDoctorOnSubmit(Doctor doctor, User user)
        {
            medicalSystemsContext.Users.Add(user);
            medicalSystemsContext.SaveChanges();
            doctor.UserId = user.Id;
            medicalSystemsContext.Doctors.Add(doctor);
            medicalSystemsContext.SaveChanges();
            currentUser = user;
            currentDoctor = doctor;
            string fullName = string.Join(' ', doctor.FirstName, doctor.LastName);
            string[] paramsValue = { user.Login, fullName };
            emailService.SendEmail(user.Email, fullName, EmailType.DOCTOR_REGISTRATION, paramsValue);
            MessageBox.Show("Czekaj na potwierdzenie od administratora na podany email");
            RegisterControl.Visibility = Visibility.Collapsed;
            Doctors.Visibility = Visibility.Visible;
            ManageExaminations.Visibility = Visibility.Visible;
            Register.Visibility = Visibility.Collapsed;
            LogIn.Visibility = Visibility.Collapsed;
            LogOut.Visibility = Visibility.Visible;
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

            string computedHash = HashHelper.GenerateHash(password);
            if (computedHash != user.Password)
            {
                MessageBox.Show("Użytkownik nie został znaleziony!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                LoginControl.Visibility = Visibility.Hidden;
                return;
            }
           
            AccountTypeEnum = user.AccountType;
            currentUser = user;
            LoginControl.Visibility = Visibility.Hidden;           
            if (AccountTypeEnum == AccountType.DOCTOR)
            {
                currentDoctor = medicalSystemsContext.Doctors.First(doctor => doctor.UserId == currentUser.Id);
            }
            if (AccountTypeEnum == AccountType.PACIENT) {
                currentPatient = medicalSystemsContext.Patients.First(patient => patient.UserId == currentUser.Id);                
            }           
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

        private void LoginControlClose()
        {
            LoginControl.Visibility = Visibility.Hidden;
        }

        private void DoctorsListClose()
        {
            DoctorsListControl.Visibility = Visibility.Collapsed;
        }

        private void NewAppointmentClose()
        {
            NewAppointmentControl.Visibility = Visibility.Hidden;
        }

    

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {                       
            MessageBoxResult res = MessageBox.Show("Czy na pewno chcesz się wylogować?", "Wyloguj się", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                // wylogowanie
                currentUser = null;
                if(AccountTypeEnum.Equals(AccountType.PACIENT))
                {
                    currentPatient = null;
                    Appointments.Visibility = Visibility.Collapsed;
                    Doctors.Visibility = Visibility.Collapsed;
                    Register.Visibility = Visibility.Visible;
                    LogIn.Visibility = Visibility.Visible;
                    LogOut.Visibility = Visibility.Collapsed;
                }
                if (AccountTypeEnum == AccountType.DOCTOR)
                {
                    currentDoctor = null;                    
                    ManageExaminations.Visibility = Visibility.Collapsed;
                    Doctors.Visibility = Visibility.Collapsed;
                    Register.Visibility = Visibility.Visible;
                    LogIn.Visibility = Visibility.Visible;
                    LogOut.Visibility = Visibility.Collapsed;
                }
                if (AccountTypeEnum == AccountType.ADMIN)
                {
                    ManageControl.Visibility = Visibility.Collapsed;
                    Register.Visibility = Visibility.Visible;
                    LogIn.Visibility = Visibility.Visible;
                    LogOut.Visibility = Visibility.Collapsed;
                }
                AccountTypeEnum = AccountType.NOT_LOGGED;
            }            
        }

        private void AddExamination_Click(object sender, RoutedEventArgs e)
        {
            var examinations = medicalSystemsContext.Appointments.Where(appointment => appointment.AppointmentType == VisitType.BADANIE).ToList();
            var patients = medicalSystemsContext.Patients.ToList().Where(patient => examinations.Any(el => el.PatientId == patient.Id)).ToList();
            NewExaminationControl.LoadAppointmentsWithExaminations(examinations, patients);
            NewExaminationControl.Visibility = Visibility.Visible;
        }


    }
}