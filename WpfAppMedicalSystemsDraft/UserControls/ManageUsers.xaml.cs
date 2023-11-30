using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpfAppMedicalSystemsDraft.Enums;
using WpfAppMedicalSystemsDraft.Models;

namespace WpfAppMedicalSystemsDraft.UserControls
{
    /// <summary>
    /// Interaction logic for ManageUsers.xaml
    /// </summary>
    public partial class ManageUsers : UserControl
    {
      
        public ObservableCollection<User> Users { get; set; }
        public ManageUsers()
        {
            InitializeComponent();
            Users = new ObservableCollection<User>(GetUsersFromDatabase());
            userGrid.ItemsSource = Users;       
        }

        private static IEnumerable<User> GetUsersFromDatabase()
        {
            // Replace this with your actual database query to get users
            // For demonstration, returning a dummy list
            return new List<User>
            {
                new() { Id = 1, Login = "user1", AccountType = AccountType.DOCTOR, Verified = true },
                new() { Id = 2, Login = "user2", AccountType = AccountType.NOT_LOGGED, Verified = false }

                // Add more users as needed
            };

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            ManageUsersOverlay.IsOpen = false;
        }


    }
}
