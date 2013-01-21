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

namespace TVAdvisor
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
        }
    }
}
