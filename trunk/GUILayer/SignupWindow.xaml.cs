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
using LogicLayer;
using MySql.Data.MySqlClient;

namespace GUILayer
{
    /// <summary>
    /// Interaction logic for SignupWindow.xaml
    /// </summary>
    public partial class SignupWindow : Window
    {
        public SignupWindow()
        {
            InitializeComponent();
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            pnlError.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            pnlError.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void txtPassword2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            pnlError.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void showError(string sError)
        {
            lblError.Content = sError;
            pnlError.Visibility = System.Windows.Visibility.Visible;
        }

        private void BackToLogin()
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            Close();
        }

        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "" || txtPassword.Password == "" || txtPassword2.Password == "")
            {
                showError("Please fill all the fileds.");
                return;
            }
            if (txtPassword.Password != txtPassword2.Password)
            {
                showError("The password fileds don't match.");
                return;
            }
            try
            {
                LogicManager.Instance.userRegister(txtName.Text, txtPassword.Password);
                MessageBox.Show("Registration succeeded!", "Welcome", MessageBoxButton.OK, MessageBoxImage.Information);
                BackToLogin();
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    showError("The user " + txtName.Text + " is already exists!");
                }
            }
            catch (Exception ex)
            {
                showError(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            BackToLogin();
        }
    }
}
