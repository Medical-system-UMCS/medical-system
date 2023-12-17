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

namespace WpfAppMedicalSystemsDraft.UserControls
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
        }

        public void SubmitLogin_Click(object sender, RoutedEventArgs e)
        {
            SubmitLogin();
        }

        private void SubmitLogin()
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            OnSubmitLogin.Invoke(username, password);
        }

        public event Action<string, string> OnSubmitLogin;
        public event Action OnCloseLogin;

        private void LoginOverlay_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                SubmitLogin();
            }
            if(e.Key == Key.Escape) {
                OnCloseLogin.Invoke();
            }
        }
    }
}
