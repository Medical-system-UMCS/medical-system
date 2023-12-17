using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfAppMedicalSystemsDraft.Enums;
using WpfAppMedicalSystemsDraft.Helpers;
using WpfAppMedicalSystemsDraft.Models;

namespace WpfAppMedicalSystemsDraft.UserControls
{
    
    public partial class Registration : UserControl
    {


        public Registration()
        {
            InitializeComponent();
            this.DataContext = this;
            this.AccTypeVar1 = "Waga (kg):";
            this.AccTypeVar2 = "Wzrost (cm):";
        }

        private bool typeIsPatient = true;

        public string Email { get; set; }
        public string Nazwisko { get; set; }

        public string SelectedDate { get; set; }


        public void Prepare()
        {
            FirstNameTextBox.Clear();
            LastNameTextBox.Clear();
            EmailTextBox.Clear();
            PasswordBox.Clear();
            RepeatPasswordBox.Clear();
            WeightDegree.Clear();
            HeightSpecialization.Clear();            
        }

        public void SubmitRegister_Click(object sender, RoutedEventArgs e)
        {
            SubmitRegister();
        }

        private void SubmitRegister()
        {
            if (!Regex.IsMatch(FirstNameTextBox.Text, @"^[\p{L}]{3,30}$"))
            {
                MessageBox.Show("Podaj poprawne imię!");
                FirstNameTextBox.Focus();
            }
            else if (!Regex.IsMatch(LastNameTextBox.Text, @"^[\p{L}]{3,40}$"))
            {
                MessageBox.Show("Podaj poprawne nazwisko!");
                LastNameTextBox.Focus();
            }
            else if (!Regex.IsMatch(EmailTextBox.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                MessageBox.Show("Podaj poprawny email!");
                EmailTextBox.Focus();
            }
            else if (!Regex.IsMatch(PasswordBox.Password, @"^[a-zA-Z0-9!@#$%^&]{6,32}$"))
            {
                MessageBox.Show("Podaj poprawne hasło!");
                PasswordBox.Focus();
            }
            else if (PasswordBox.Password != RepeatPasswordBox.Password)
            {
                MessageBox.Show("Hasła muszą być jednakowe!");
                RepeatPasswordBox.Focus();
            }
            else if (typeIsPatient)
            {
                if (!Regex.IsMatch(WeightDegree.Text, @"^[0-9]{2,3}$"))
                {
                    MessageBox.Show("Podaj poprawną wagę!");
                    WeightDegree.Focus();
                }
                else if (!Regex.IsMatch(HeightSpecialization.Text, @"^[0-9]{2,3}$"))
                {
                    MessageBox.Show("Podaj poprawny wzrost");
                    HeightSpecialization.Focus();
                }
                else
                {
                    Patient patient = new Patient
                    {
                        FirstName = FirstNameTextBox.Text.Trim(),
                        LastName = LastNameTextBox.Text.Trim(),
                        Dayofbirth = DateOnly.Parse(DateOfBirthPicker.Text),
                        Weight = int.Parse(WeightDegree.Text.Trim()),
                        Height = int.Parse(HeightSpecialization.Text.Trim())
                    };
                    User user = new User
                    {
                        AccountType = AccountType.PACIENT,
                        Login = string.Join('_', PolishLettersHelper.ReplacePolishLetters(patient.FirstName.Trim().ToLower()),
                        PolishLettersHelper.ReplacePolishLetters(patient.LastName.Trim().ToLower())),
                        Password = HashHelper.GenerateHash(PasswordBox.Password),
                        Verified = true,
                        Email = EmailTextBox.Text.Trim()
                    };
                    OnRegisterPatient.Invoke(patient, user);
                }
            }
            else if (!typeIsPatient)
            {
                if (!Regex.IsMatch(WeightDegree.Text, @"^[\p{L}\s.]{3,50}$"))
                {
                    MessageBox.Show("Podaj poprawny stopień naukowy!");
                    WeightDegree.Focus();
                }
                else if (!Regex.IsMatch(HeightSpecialization.Text, @"^[\p{L}\s.]{3,50}$"))
                {
                    MessageBox.Show("Podaj poprawną specjalizację!");
                    HeightSpecialization.Focus();
                }
                else
                {
                    Doctor doctor = new Doctor
                    {
                        FirstName = FirstNameTextBox.Text.Trim(),
                        LastName = LastNameTextBox.Text.Trim(),
                        ScientificDegree = WeightDegree.Text.Trim(),
                        Specialization = HeightSpecialization.Text.Trim()
                    };
                    User user = new User
                    {
                        AccountType = AccountType.DOCTOR,
                        Login = string.Join('_', PolishLettersHelper.ReplacePolishLetters(doctor.FirstName.Trim().ToLower()),
                        PolishLettersHelper.ReplacePolishLetters(doctor.LastName.Trim().ToLower())),
                        Password = HashHelper.GenerateHash(PasswordBox.Password),
                        Verified = false,
                        Email = EmailTextBox.Text.Trim()
                    };
                    OnRegisterDoctor.Invoke(doctor, user);
                }
            }
        }

        public string AccTypeVar1
        {
            get { return (string)GetValue(AccTypeVar1Property); }
            set { SetValue(AccTypeVar1Property, value); }
        }

        public static readonly DependencyProperty AccTypeVar1Property =
            DependencyProperty.Register("AccTypeVar1", typeof(string), typeof(Registration), new PropertyMetadata(null));

        public string AccTypeVar2
        {
            get { return (string)GetValue(AccTypeVar2Property); }
            set { SetValue(AccTypeVar2Property, value); }
        }

        public static readonly DependencyProperty AccTypeVar2Property =
            DependencyProperty.Register("AccTypeVar2", typeof(string), typeof(Registration), new PropertyMetadata(null));



        private void PatientButton_Checked(object sender, RoutedEventArgs e)
        {
            AccTypeVar1 = "Waga (kg):";
            AccTypeVar2 = "Wzrost (cm):";
            typeIsPatient = true;
            DateOfBirthPicker.Visibility = Visibility.Visible;
            DateOfBirthHeadline.Visibility = Visibility.Visible;
        }

        private void DoctorButton_Checked(object sender, RoutedEventArgs e)
        {
            AccTypeVar1 = "Stopień naukowy:";
            AccTypeVar2 = "Specjalizacja:";
            typeIsPatient = false;
            DateOfBirthPicker.Visibility = Visibility.Collapsed;
            DateOfBirthHeadline.Visibility = Visibility.Collapsed;
        }

        private void DateOfBirthPicker_Validation(object sender, DatePickerDateValidationErrorEventArgs e)
        {
            MessageBox.Show("Podaj poprawną datę!");
        }

        public event Action<Doctor, User> OnRegisterDoctor;
        public event Action<Patient, User> OnRegisterPatient;
        public event Action OnRegisterClose;

        private void RegisterOverlay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SubmitRegister();
            }
            if (e.Key == Key.Escape)
            {
                OnRegisterClose.Invoke();
            }
        }
    }
}
