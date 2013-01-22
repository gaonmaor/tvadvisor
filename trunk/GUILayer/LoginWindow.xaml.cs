using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TVAdvisor;
using LogicLayer;

namespace GUILayer
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void showError(string sError)
        {
            lblError.Content = sError;
            pnlError.Visibility = System.Windows.Visibility.Visible;
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int userId = LogicManager.Instance.GetUserID(txtName.Text, txtPassword.SecurePassword);
                if (userId >= 0)
                {
                    MainWindow.Instance.UserID = userId;
                    MainWindow.Instance.UserName = txtName.Text;
                    MainWindow.Instance.Title = "TVAdvisor - Welcome " + txtName.Text;
                    MainWindow.Instance.Show();
                    Close();
                }
                else
                {
                    showError("Incorrect user or password!");
                }
            }
            catch (Exception ex)
            {
                showError(ex.Message);
            }
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            pnlError.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            pnlError.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void lblSignup_Click(object sender, MouseButtonEventArgs e)
        {
            SignupWindow signup = new SignupWindow();
            signup.Title = "Sign up";
            signup.Show();
            Close();
        }
    }
}
