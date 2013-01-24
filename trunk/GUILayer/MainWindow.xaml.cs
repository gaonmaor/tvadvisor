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
using System.Threading.Tasks;
using CommonLayer;
using EPGGruberService;
using LogicLayer;

namespace GUILayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The connected user.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// The name of the user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The singleton object.
        /// </summary>
        private static MainWindow m_instance = null;

        /// <summary>
        /// Get the singleton instance.
        /// </summary>
        public static MainWindow Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new MainWindow();
                }

                return m_instance;
            }
        }

        /// <summary>
        /// The main constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            PleaseWaitWindow pw = new PleaseWaitWindow();
            pw.lblMessage.Content = "Please wait while the epg is been generated.";
            MessageBoxResult rest = MessageBox.Show("Do you want to reload the epg data?", "Reload data.", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (rest == MessageBoxResult.Yes)
            {
                Task t = new Task(new Action(() =>
                {
                    try
                    {
                        EPGUpdater updater = new EPGUpdater(UserID, Environment.CurrentDirectory);
                        updater.start();
                        Dispatcher.Invoke(new Action(() => pw.Close()));
                    }
                    catch (Exception ex)
                    {
                        Dispatcher.Invoke(new Action(() =>
                        {
                            pw.Close();
                            MessageBox.Show(ex.Message);
                        }));

                    }

                }));
                t.Start();
                pw.ShowDialog();
            }
        }

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                List<UserDetail> users = LogicManager.Instance.GetUsers();
                UserDetail user = (from u in users where u.Id == UserID select u).First();
                if (user.IsAdmin)
                {
                    tiConfigurator.Visibility = System.Windows.Visibility.Visible;
                }
            }
            catch (Exception)
            {
            }

            
            wbHelp.Navigate("file:///" + Environment.CurrentDirectory + "/help.pdf");
        }
    }
}
