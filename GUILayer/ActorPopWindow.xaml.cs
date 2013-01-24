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
using CommonLayer;
using System.Threading.Tasks;

namespace GUILayer
{
    /// <summary>
    /// Interaction logic for ActorPopWindowxaml.xaml
    /// </summary>
    public partial class ActorPopWindow : Window
    {
        public ActorPopWindow()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Close();
                    break;
            }
        }

        /// <summary>
        ///  Load the content of the actor from the database.
        /// </summary>
        /// <param name="actorName">The actor name.</param>
        public void LoadActor(string actorName)
        {
            lblName.Content = actorName;
            try
            {
                //Task t = new Task(new Action(()=>{
                //    try
                //    {
                //        ActorDetail detail = LogicManager.Instance.LoadActor(actorName);
                //        Dispatcher.Invoke(new Action(() => lblBiography.Text = detail.Biography));
                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show(ex.Message);
                //    }
                //}));
                //t.Start();
            }
            catch (Exception)
            {
                lblBiography.Text = "";
            }
             
        }
    }
}
