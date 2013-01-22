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
        /// <summary>
        /// The singleton object.
        /// </summary>
        private static SignupWindow m_instance = null;

        /// <summary>
        /// Get the singleton instance.
        /// </summary>
        public static SignupWindow Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new SignupWindow();
                }

                return m_instance;
            }
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        private void txtPassword2_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "")
            {
            }
        }
    }
}
