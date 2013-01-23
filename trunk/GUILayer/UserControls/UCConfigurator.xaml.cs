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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LogicLayer;
using CommonLayer;

namespace GUILayer
{
    /// <summary>
    /// Interaction logic for UCConfigurator.xaml
    /// </summary>
    public partial class UCConfigurator : UserControl
    {
        /// <summary>
        /// The default constructor.
        /// </summary>
        public UCConfigurator()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handle user control loading.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            List<UserDetail> users = LogicManager.Instance.GetUsers();
            foreach (UserDetail user in users)
            {
                lstUsers.Items.Add(GetUserItem(user));
            }            
        }

        /// <summary>
        /// Get user panel.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private UserListBoxItem GetUserItem(UserDetail user)
        {
            UserListBoxItem lbi = new UserListBoxItem();
            lbi.User = user;
            Grid grd = new Grid();
            CheckBox cbx = new CheckBox() { Content = "cbxAdmin" };
            cbx.IsChecked = user.IsAdmin;
            cbx.Checked += new RoutedEventHandler(cbx_Checked);
            Label label = new Label() { Content = user.Name };
            Button btn = new Button() { Content = "Delete" };
            btn.Click += new RoutedEventHandler(btn_Click);

            grd.Children.Add(label);
            grd.Children.Add(cbx);
            grd.Children.Add(btn);
            lbi.Content = grd;
            return lbi;
        }

        /// <summary>
        /// Handle delete click.
        /// </summary>
        void btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogicManager.Instance.DeleteUser(((UserListBoxItem)(((Button)sender).Parent)).User.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            lstUsers.Items.Remove(((Button)sender).Parent);
        }

        /// <summary>
        /// Handle admin checkbox changes.
        /// </summary>
        void cbx_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                LogicManager.Instance.ChangeAdmin(((UserListBoxItem)(((CheckBox)sender).Parent)).User.Id, ((CheckBox)sender).IsChecked.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    /// <summary>
    /// User list box item.
    /// </summary>
    class UserListBoxItem : ListBoxItem
    {
        /// <summary>
        /// The user connected to the list box.
        /// </summary>
        public UserDetail User { get; set; }
    }
}
