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
using GUILayer.Properties;

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
            lstUsers.Items.Clear();
            try
            {
                List<UserDetail> users = LogicManager.Instance.GetUsers();
                foreach (UserDetail user in users)
                {
                    lstUsers.Items.Add(GetUserItem(user));
                }
            }
            catch (Exception)
            {
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
        private void btnProgramDumps_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int num_records = 30;
                Task t = new Task(new Action(() =>
                {
                    try
                    {
                        LogicManager.Instance.ReloadProgramDumps(new UpdateProgressEvent(updateProgress), num_records,
                            Settings.Default.ProgramsDumpURL, Settings.Default.DumpFolder);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }));
                t.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                Task t = new Task(new Action(() =>
                {
                    try
                    {
                        LogicManager.Instance.ReloadActorsDataDumps(new UpdateProgressEvent(updateProgress), num_records,
                            Settings.Default.ActorsDumpURL, Settings.Default.DumpFolder);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                    
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
        private void updateProgress(string info, int percent)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                lblTitle.Content = "Progress Bar: " + info; 
                if (pbData.Value == 0)
                {
                    pbData.Visibility = System.Windows.Visibility.Visible;
                }
                pbData.Value = percent + 1;
                if (pbData.Value + 1 >= 100)
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
            Task t = new Task(new Action(() =>
            {
                LogicManager.Instance.ReloadActorsDataDumps(new UpdateProgressEvent(updateProgress), num_records,
                    Settings.Default.ActorsDumpURL, Settings.Default.DumpFolder);
            }));
            t.Start();
        }

        private void lstUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstUsers.SelectedItem != null)
            {
                UserDetail user = ((UserListBoxItem)lstUsers.SelectedItem).User;
                changeAdminText(user.IsAdmin);
            }
        }

        private void btnFullDumps_Click(object sender, RoutedEventArgs e)
        {
            int num_records = 55500;
            Task t = new Task(new Action(() =>
            {
                try
                {
                    LogicManager.Instance.ReloadFullDataDumps(new UpdateProgressEvent(updateProgress), num_records,
                    new List<string>{ Settings.Default.ProgramsDumpURL, 
                                      Settings.Default.ActorsDumpURL,
                                      Settings.Default.RegTVDumpURL}, Settings.Default.DumpFolder);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
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
