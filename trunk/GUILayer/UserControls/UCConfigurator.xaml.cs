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
using System.Threading.Tasks;

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
            Label label = new Label() { Content = user.Name + (user.IsAdmin?"(Admin)":"") };
            label.Width = 200;
            lbi.Content = label;
            return lbi;
        }

        /// <summary>
        /// Make the current user admin or reverse.
        /// </summary>
        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserDetail user = ((UserListBoxItem)lstUsers.SelectedItem).User;
                user.IsAdmin = !user.IsAdmin;
                LogicManager.Instance.ChangeAdmin(user.Id, user.IsAdmin);
                changeAdminText(user.IsAdmin);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Change the text of admin button
        /// </summary>
        /// <param name="isAdmin">The current admin value.</param>
        private void changeAdminText(bool isAdmin)
        {
            UserDetail user = ((UserListBoxItem)lstUsers.SelectedItem).User;
            ((Label)((ListBoxItem)lstUsers.SelectedItem).Content).Content = user.Name + (user.IsAdmin ? " (Admin)" : "");
            if (isAdmin)
            {
                btnAdmin.Content = "Unmake Admin";
            }
            else
            {
                btnAdmin.Content = "Make Admin";
            }
        }

        /// <summary>
        /// Delete the current user.
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserDetail user = ((UserListBoxItem)lstUsers.SelectedItem).User;
                LogicManager.Instance.DeleteUser(user.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            lstUsers.Items.Remove(lstUsers.SelectedItem);
        }

        /// <summary>
        /// Reload datadumps.
        /// </summary>
        private void btnDataDumps_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int num_records = 30;
                pbData.Maximum = num_records;
                Task t = new Task(new Action(() =>
                {
                    LogicManager.Instance.ReloadDataDumps(new UpdateProgressEvent(updateProgress), num_records);
                }));
                t.Start();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Reload actors datadumps.
        /// </summary>
        private void btnActorsDumps_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int num_records = 30;
                pbData.Maximum = num_records;
                Task t = new Task(new Action(() =>
                {
                    LogicManager.Instance.ReloadActorsDataDumps(new UpdateProgressEvent(updateProgress), num_records);
                }));
                t.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Update the progress.
        /// </summary>
        /// <param name="percent">The percent progress.</param>
        private void updateProgress(int percent)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                if (pbData.Value == 0)
                {
                    pbData.Visibility = System.Windows.Visibility.Visible;
                }
                pbData.Value = percent + 1;
                if (pbData.Value == 100)
                {
                    pbData.Visibility = System.Windows.Visibility.Collapsed;
                    pbData.Value = 0;
                }
            }));
        }

        /// <summary>
        /// Reload actors to free base.
        /// </summary>
        private void btcGetActor_Click(object sender, RoutedEventArgs e)
        {
            int num_records = 55500;
            pbData.Maximum = num_records;
            Task t = new Task(new Action(() =>
            {
                LogicManager.Instance.ReloadActorsDataDumps(new UpdateProgressEvent(updateProgress), num_records);
            }));
            t.Start();
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
