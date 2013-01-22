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

using DataLayer;

namespace TVAdvisor
{
    /// <summary>
    /// Interaction logic for UCConfigurator.xaml
    /// </summary>
    public partial class UCConfigurator : UserControl
    {
        public UCConfigurator()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }

        private void btcGetData_Click(object sender, RoutedEventArgs e)
        {
            int num_records = 30;
            pbData.Maximum = num_records;
            Task t = new Task(new Action(()=>{
                DataManager.Instance.GetFreebaseData(new UpdateProgressEvent(updateProgress), num_records);
            }));
            t.Start();
        }

        private void updateProgress(int percent)
        {
            Dispatcher.Invoke(new Action(()=>{
                pbData.Value = percent + 1;
            }));
        }

        private void btcGetActor_Click(object sender, RoutedEventArgs e)
        {
            int num_records = 55500;
            pbData.Maximum = num_records;
            Task t = new Task(new Action(() =>
            {
                DataManager.Instance.GetFreebaseActor(new UpdateProgressEvent(updateProgress), num_records);
            }));
            t.Start();
        }
 
    }
}
