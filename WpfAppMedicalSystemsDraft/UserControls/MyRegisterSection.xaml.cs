﻿using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfAppMedicalSystemsDraft.Enums;
using WpfAppMedicalSystemsDraft.Helpers;
using WpfAppMedicalSystemsDraft.Models;

namespace WpfAppMedicalSystemsDraft.UserControls
{
    
    public partial class MyRegisterSection : UserControl
    {


        public MyRegisterSection()
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



        public void SubmitRegister_Click(object sender, RoutedEventArgs e)
        {

            if(!Regex.IsMatch(FirstNameTextBox.Text, @"^[a-zA-Z]{3,30}$"))
            {
                MessageBox.Show("Podaj poprawne imie!");
                FirstNameTextBox.Focus();
            }
            else if (!Regex.IsMatch(LastNameTextBox.Text, @"^[a-zA-Z]{3,40}$"))
            {
                MessageBox.Show("Podaj poprawne nazwisko!");
                LastNameTextBox.Focus();
            }   
            else if(!Regex.IsMatch(EmailTextBox.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                MessageBox.Show("Podaj poprawny email!");
                EmailTextBox.Focus();
            }
            else if (DateOfBirthPicker.SelectedDate == null)
            {
                MessageBox.Show("Podaj datę!");
                DateOfBirthPicker.Focus();
            }   
            else if (!Regex.IsMatch(PasswordBox.Password, @"^[a-zA-Z0-9!@#$%^&]{6,32}$"))
            {
                MessageBox.Show("Podaj poprawne hasło!");
                //PasswordBox.BorderBrush = new SolidColorBrush(Colors.Red);
                PasswordBox.Focus();
            }
            else if (PasswordBox.Password != RepeatPasswordBox.Password)
            {
                MessageBox.Show("Hasła muszą być jednakowe!");
                RepeatPasswordBox.Focus();
            } 
            else if (typeIsPatient)
            {
                if (!Regex.IsMatch(TypeTextBox1.Text, @"^[0-9]{2,3}$"))
                {
                    MessageBox.Show("Podaj poprawną wagę!");
                    TypeTextBox1.Focus();
                }
                else if (!Regex.IsMatch(TypeTextBox2.Text, @"^[0-9]{2,3}$"))
                {
                    MessageBox.Show("Podaj poprawny wzrost");
                    TypeTextBox2.Focus();
                }
                else
                {
                    Patient patient = new Patient
                    {
                        FirstName = FirstNameTextBox.Text,
                        LastName = LastNameTextBox.Text,
                        Dayofbirth = DateOnly.Parse(DateOfBirthPicker.Text),
                        Weight = int.Parse(TypeTextBox1.Text),
                        Height = int.Parse(TypeTextBox2.Text)
                    };
                    User user = new User
                    {
                        AccountType = AccountType.PACIENT,
                        Login = string.Join("_", patient.FirstName.ToLower(), patient.LastName.ToLower()),
                        Password = HashHelper.GenerateHash(PasswordBox.Password),
                        Verified = true,
                        Email = EmailTextBox.Text
                    };
                    MessageBox.Show(user.Email);
                    OnRegisterPatient.Invoke(patient, user);
                }
            }
            else if (!typeIsPatient)
            {
                if (!Regex.IsMatch(TypeTextBox1.Text, @"^[a-zA-Z0-9]{3,50}$"))
                {
                    MessageBox.Show("Podaj poprawny stopień naukowy!");
                    TypeTextBox1.Focus();
                }
                else if (!Regex.IsMatch(TypeTextBox2.Text, @"^[a-zA-Z0-9]{3,50}$"))
                {
                    MessageBox.Show("Podaj poprawną specjalizację!");
                    TypeTextBox2.Focus();
                }
                else
                {
                    RegisterOverlay.Visibility = Visibility.Collapsed;
                }
            } 
        }


        public string AccTypeVar1
        {
            get { return (string)GetValue(AccTypeVar1Property); }
            set { SetValue(AccTypeVar1Property, value); }
        }

        public static readonly DependencyProperty AccTypeVar1Property =
            DependencyProperty.Register("AccTypeVar1", typeof(string), typeof(MyRegisterSection), new PropertyMetadata(null));

        public string AccTypeVar2
        {
            get { return (string)GetValue(AccTypeVar2Property); }
            set { SetValue(AccTypeVar2Property, value); }
        }

        public static readonly DependencyProperty AccTypeVar2Property =
            DependencyProperty.Register("AccTypeVar2", typeof(string), typeof(MyRegisterSection), new PropertyMetadata(null));



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
            //e.ThrowException = true;
        }

        public event Action<Doctor, User> OnRegisterDoctor;
        public event Action<Patient, User> OnRegisterPatient;
    }
}
