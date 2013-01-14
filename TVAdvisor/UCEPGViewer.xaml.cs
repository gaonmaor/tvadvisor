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
using CommonLayer;
using System.Timers;
using System.IO;

namespace TVAdvisor
{
    /// <summary>
    /// Interaction logic for EPGViewerxaml.xaml
    /// </summary>
    public partial class UCEPGViewer : UserControl
    {
        #region Enums

        /// <summary>
        /// The direction of movement.
        /// </summary>
        enum Dir
        {
            Up,
            Down,
            Left,
            Right,
            None
        }

        #endregion

        #region Fields

        /// <summary>
        /// The skiped channel index.
        /// </summary>
        private int m_chanIdx;

        #endregion

        #region Properties

        /// <summary>
        /// The ordered brush.
        /// </summary>
        public Brush OrderedBrush { get; set; }

        /// <summary>
        /// The EPG of the main class.
        /// </summary>
        public tv Epg { get; set; }

        /// <summary>
        /// The window start.
        /// </summary>
        public DateTime WindowStart { get; set; }

        /// <summary>
        /// The window stop.
        /// </summary>
        public DateTime WindowStop { get; set; }

        /// <summary>
        /// The last valid window start.
        /// </summary>
        public DateTime LastWindowStart { get; set; }

        /// <summary>
        /// The last valid window stop.
        /// </summary>
        public DateTime LastWindowStop { get; set; }

        /// <summary>
        /// A single step.
        /// </summary>
        public double WindowStep { get; set; }

        /// <summary>
        /// The current selected program.
        /// </summary>
        public programme SelectedProgram
        {
            get { return (programme)GetValue(SelectedProgramProperty); }
            private set { SetValue(SelectedProgramProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedProgram.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedProgramProperty =
            DependencyProperty.Register("SelectedProgram", typeof(programme), typeof(UCEPGViewer), new UIPropertyMetadata(null));

        /// <summary>
        /// Te current selected channel.
        /// </summary>
        public channel SelectedChannel
        {
            get { return (channel)GetValue(SelectedChannelProperty); }
            set { SetValue(SelectedChannelProperty, value); }
        }

        /// <summary>
        /// The selected inex.
        /// </summary>
        public int SearchIndex { get; set; }

        /// <summary>
        /// The search result of the current string.
        /// </summary>
        public List<programme> SearchResults { get; set; }

        // Using a DependencyProperty as the backing store for SelectedChannel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedChannelProperty =
            DependencyProperty.Register("SelectedChannel", typeof(channel), typeof(UCEPGViewer), new UIPropertyMetadata(null));

        #endregion

        #region Constructors

        /// <summary>
        /// The main constructor.
        /// </summary>
        public UCEPGViewer()
        {
            m_chanIdx = 0;
            InitializeComponent();
            SearchResults = new List<programme>();
            SearchIndex = -1;
            OrderedBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFC66E"));
            //WindowStart = new DateTime(2012, 12, 14, 15, 0, 0);
            //WindowStop = new DateTime(2012, 12, 14, 16, 30, 0);
            WindowStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, (DateTime.Now.Minute > 30) ? 30 : 0, 0);
            WindowStop = new DateTime(WindowStart.Ticks);
            WindowStep = 30;
            WindowStop = WindowStop.AddMinutes(WindowStep * 3);

            try
            {
                Epg = Utils.DeserializeXml<tv>(Environment.CurrentDirectory +  @"\epg.xml");
                // Fix null stops.
                if (Epg.programme[0].stop == null)
                {
                    foreach (var chan in Epg.channel)
                    {
                        var chanProg = (from p in Epg.programme
                                        where p.channel == chan.id
                                        select p).ToArray();

                        for (int i = 0; i < chanProg.Length - 1; i++)
                        {
                            chanProg[i].stop = chanProg[i + 1].start;
                        }
                        chanProg[chanProg.Length - 1].stop = chanProg[chanProg.Length - 1].start;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handle window resize.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void epgViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            updateEPG(Dir.Left);
        }

        /// <summary>
        /// Update the EPG table.
        /// </summary>
        /// <param name="dir">True left False right null none.</param>
        private void updateEPG(Dir dir)
        {
            string selectedChan = null;
            ListBoxItem selectedItem = null;

            try
            {
                if (lstChannels.SelectedItem != null)
                {
                    selectedChan = ((EPGItemContent)lstChannels.SelectedItem).Program.channel;
                }

                lstChannels.Items.Clear();
                grdChannels.Children.Clear();
                lblDate.Content = WindowStart.ToLongDateString();
                lblStart.Content = String.Format("{0:00}:{1:00}", WindowStart.Hour, WindowStart.Minute);
                DateTime mid = new DateTime((WindowStart.Ticks + WindowStop.Ticks) / 2);
                lblMid.Content = String.Format("{0:00}:{1:00}", mid.Hour, mid.Minute);
                lblStop.Content = String.Format("{0:00}:{1:00}", WindowStop.Hour, WindowStop.Minute);

                int count = 0;
                int chnIdx = 0;
                foreach (var chan in Epg.channel)
                {
                    if (chnIdx < m_chanIdx)
                    {
                        ++chnIdx;
                        continue;
                    }

                    if (count == Properties.Settings.Default.ChannelsInWindow)
                    {
                        break;
                    }


                    grdChannels.Children.Add(new Label() 
                    {
                        BorderThickness = new Thickness(0.5),
                        BorderBrush = Brushes.Gray,
                        HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center,
                        Content = chan.displayname[0].Value 
                    });
                    var chanProg = from p in Epg.programme
                                   where p.channel == chan.id && p.start != p.stop && !((Utils.GetEPGDate(p.stop) <= WindowStart) ||
                                   (Utils.GetEPGDate(p.start) >= WindowStop))
                                   select p;
                    if (chanProg.Count() > 0)
                    {
                        long div = 0;
                        foreach (var p in chanProg)
                        {
                            div += Math.Min(Utils.GetEPGDate(p.stop).Ticks, WindowStop.Ticks) -
                                Math.Max(Utils.GetEPGDate(p.start).Ticks, WindowStart.Ticks);
                        }

                        double mult = (lstChannels.ActualWidth - 10) / div;

                        programme[] programs = chanProg.ToArray();
                        ListBoxItem lbiFirst = null;
                        ListBoxItem lbiLast = null;
                        for (int i = 0; i < programs.Length; ++i)
                        {
                            programme p = programs[i];
                            EPGItemContent lbi = null;
                            foreach (OrderedItemContent o in lstOrders.Items)
                            {
                                if (o.LinkedContent.Program == p)
                                {
                                    lbi = o.LinkedContent;
                                }
                            }

                            if (lbi == null)
                            {
                                StackPanel spLBI = new StackPanel() { Orientation = Orientation.Horizontal };
                                spLBI.Children.Add(new Image()
                                {
                                    Visibility = System.Windows.Visibility.Collapsed,
                                    Width = 20,
                                    Height = 20,
                                    Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\clock.jpg"))
                                });
                                spLBI.Children.Add(new Label() { Content = p.title[0].Value });

                                lbi = new EPGItemContent()
                                {
                                    BorderThickness = new Thickness(0.5),
                                    BorderBrush = Brushes.Gray,
                                    HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center,
                                    Program = p,
                                    Channel = chan,
                                    IsStart = i == 0,
                                    FlowDirection = System.Windows.FlowDirection.RightToLeft,
                                    IsStop = (i + 1) == programs.Length,
                                    IsTop = count == 0,
                                    IsBottom = count + 1 == Properties.Settings.Default.ChannelsInWindow,
                                    Content = spLBI,
                                    Width = (Math.Min(Utils.GetEPGDate(p.stop).Ticks, WindowStop.Ticks) -
                                    Math.Max(Utils.GetEPGDate(p.start).Ticks, WindowStart.Ticks)) * mult
                                };
                            }
                            lstChannels.Items.Add(lbi);

                            if (selectedChan == p.channel && (
                                ((dir == Dir.Right) && i == 0) ||
                                ((dir == Dir.Left) && i + 1 == programs.Length) ||
                                ((dir == Dir.Up) && count == 0) ||
                                ((dir == Dir.Down) && (count + 2) == Properties.Settings.Default.ChannelsInWindow)))
                            {
                                selectedItem = lbi;
                            }

                            if (i == 0)
                            {
                                lbiFirst = lbi;
                            }
                            if (i + 1 == programs.Length)
                            {
                                lbiLast = lbi;
                            }
                        }

                        if (selectedChan == chan.id && selectedItem == null)
                        {
                            if (dir == Dir.Right)
                            {
                                selectedItem = lbiFirst;
                                selectedItem.Focus();
                            }
                            if (dir == Dir.Left)
                            {
                                selectedItem = lbiLast;
                                selectedItem.Focus();
                            }
                        }
                    }
                    ++count;
                }

                if (selectedItem != null)
                {
                    lstChannels.SelectedItem = selectedItem;
                    selectedItem.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Handle keyboard on epg panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstChannels_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            EPGItemContent item;
            if (lstChannels.SelectedItem != null)
            {
                item = (EPGItemContent)lstChannels.SelectedItem;
                if (e.Key == Key.Left && item.IsStart)
                {
                    LastWindowStart = WindowStart;
                    LastWindowStop = WindowStop;
                    WindowStart = WindowStart.AddMinutes(-WindowStep);
                    WindowStop = WindowStop.AddMinutes(-WindowStep);
                    updateEPG(Dir.Left);
                }
                else if (e.Key == Key.Right && item.IsStop)
                {
                    LastWindowStart = WindowStart;
                    LastWindowStop = WindowStop;
                    WindowStart = WindowStart.AddMinutes(WindowStep);
                    WindowStop = WindowStop.AddMinutes(WindowStep);
                    updateEPG(Dir.Right);
                }
                if (e.Key == Key.PageUp)
                {
                    LastWindowStart = WindowStart;
                    LastWindowStop = WindowStop;
                    WindowStart = WindowStart.AddDays(-1);
                    WindowStop = WindowStop.AddDays(-1);
                    updateEPG(Dir.Right);
                }
                else if (e.Key == Key.PageDown)
                {
                    LastWindowStart = WindowStart;
                    LastWindowStop = WindowStop;
                    WindowStart = WindowStart.AddDays(1);
                    WindowStop = WindowStop.AddDays(1);
                    updateEPG(Dir.Left);
                }
                else if (e.Key == Key.H)
                {
                    WindowStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, (DateTime.Now.Minute > 30) ? 30 : 0, 0);
                    WindowStop = new DateTime(WindowStart.Ticks);
                    WindowStep = 30;
                    WindowStop = WindowStop.AddMinutes(WindowStep * 3);
                    updateEPG(Dir.Left);
                }
                else if (e.Key == Key.Space)
                {
                    if (item.IsOrdered)
                    {
                        item.Ordered.UnsetTimer();
                        lstOrders.Items.Remove(item.Ordered);
                        item.Ordered = null;
                        //item.Background = Brushes.Transparent;
                        ((StackPanel)item.Content).Children[0].Visibility = System.Windows.Visibility.Collapsed;
                    }
                    else
                    {
                        DateTime start = Utils.GetEPGDate(item.Program.start);
                        if (start < DateTime.Now)
                        {
                            return;
                        }
                        OrderedItemContent ordered = new OrderedItemContent()
                        {
                            ParentWindow = epgViewer,
                            LinkedContent = item,
                            Content = Utils.GetEPGDate(item.Program.start).ToShortTimeString() + " : " + item.Channel.displayname[0].Value +  " : " + item.Program.title[0].Value,
                            WindowStart = this.WindowStart,
                            WindowStop = this.WindowStop,
                            ChanIdx = m_chanIdx
                        };
                        //item.Background = OrderedBrush;
                        item.Ordered = ordered;
                        int i = 0;
                        for (; i < lstOrders.Items.Count; ++i)
                        {
                            if (Utils.GetEPGDate(((OrderedItemContent)lstOrders.Items[i]).LinkedContent.Program.start) > start)
                            {
                                break;
                            }
                        }
                        lstOrders.Items.Insert(i, ordered);
                        ordered.SetTimer();
                        ((StackPanel)item.Content).Children[0].Visibility = System.Windows.Visibility.Visible;
                    }
                }
                else if (e.Key == Key.Up && item.IsTop)
                {
                    m_chanIdx = (m_chanIdx > 0) ? (m_chanIdx - 1) : 0;
                    updateEPG(Dir.Up);
                }
                else if (e.Key == Key.Down && item.IsBottom)
                {
                    m_chanIdx = (m_chanIdx < Epg.channel.Count()) ? (m_chanIdx + 1) : Epg.channel.Count();
                    updateEPG(Dir.Down);
                }
            }

            if (lstChannels.Items.Count == 0)
            {
                WindowStart = LastWindowStart;
                WindowStop = LastWindowStop;
                updateEPG(Dir.Left);
            }
        }

        /// <summary>
        /// Update the selected program.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstChannels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                EPGItemContent content = (EPGItemContent)e.AddedItems[0];
                SelectedProgram = content.Program;
                if (SelectedProgram == null)
                {
                    return;
                }
                foreach (var chan in Epg.channel)
                {
                    if (SelectedProgram.channel == chan.id)
                    {
                        SelectedChannel = chan;
                    }
                }
                grdDetails.Children.Clear();
                if (SelectedProgram.channel != null)
                    AddDetailPanel("ערוץ:", SelectedChannel.displayname[0].Value);
                AddDetailPanel("זמן:", Utils.GetEPGDate(SelectedProgram.stop).ToShortTimeString() + " - " +
                    Utils.GetEPGDate(SelectedProgram.start).ToShortTimeString());
                if (SelectedProgram.category != null)
                    AddDetailPanel("קטגוריה:", SelectedProgram.category[0].Value);
                if (SelectedProgram.desc != null && SelectedProgram.desc.Count() > 0 && SelectedProgram.desc[0].Value != null)
                    AddDetailPanel("תיאור:", SelectedProgram.desc[0].Value);
            }
            else
            {
                SelectedProgram = null;
            }
        }

        /// <summary>
        /// Get stack pannel for the given property.
        /// </summary>
        /// <param name="title">The title of the propery.</param>
        /// <param name="desc">The description of the property.</param>
        private void AddDetailPanel(string title, string desc)
        {
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;
            sp.Children.Add(new Label() { Content = title });
            sp.Children.Add(new Label() { Content = desc });

            grdDetails.Children.Add(new Label()
            {
                Content = sp
            });
        }

        /// <summary>
        /// Handle selected order change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OrderedItemContent ordered = ((OrderedItemContent)lstOrders.SelectedItem);
            if (ordered != null)
            {
                WindowStart = ordered.WindowStart;
                WindowStop = ordered.WindowStop;
                m_chanIdx = ordered.ChanIdx;
                updateEPG(Dir.None);
                lstChannels.SelectedItem = ordered.LinkedContent;
            }
        }

        /// <summary>
        /// Handle search change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text.Length >= 3)
            {
                SearchResults.Clear();

                foreach (programme p in Epg.programme)
                {
                    if ((p.title != null && (p.title[0].Value).Contains(txtSearch.Text)) ||
                        (p.desc != null && (p.desc[0].Value).Contains(txtSearch.Text)))
                    {
                        SearchResults.Add(p);
                    }
                }

                if (SearchResults.Count > 0)
                {
                    SearchIndex = 0;
                    lblSearchCount.Content = "(" + SearchResults.Count + ")";
                }
                else
                {
                    SearchIndex = -1;
                    lblSearchCount.Content = "";
                }
            }
            else
            {
                if (SearchResults.Count > 0)
                {
                    SearchResults.Clear();
                    lblSearchCount.Content = "";
                    SearchIndex = -1;
                }
            }
        }

        /// <summary>
        /// Handle button previewes search.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (SearchResults.Count > 0)
            {
                UpdateSelectedSearch();
                SearchIndex = SearchIndex > 0 ? SearchIndex - 1 : 0;
            }
        }

        /// <summary>
        /// Update the selected search item.
        /// </summary>
        private void UpdateSelectedSearch()
        {
            programme selectedProgram = SearchResults[SearchIndex];
            WindowStart = Utils.GetEPGDate(selectedProgram.start);
            WindowStart.AddMinutes(-WindowStart.Minute);
            WindowStop = new DateTime(WindowStart.Ticks);
            WindowStop = WindowStop.AddMinutes(WindowStep * 3);

            for (int chnIdx = 0; chnIdx < Epg.channel.Length; ++chnIdx)
            {
                if (selectedProgram.channel == Epg.channel[chnIdx].id)
                {
                    m_chanIdx = chnIdx;
                    break;
                }
            }

            updateEPG(Dir.Left);

            foreach (EPGItemContent item in lstChannels.Items)
            {
                if (item.Program == selectedProgram)
                {
                    lstChannels.SelectedItem = item;
                    item.Focus();
                }
            }
        }

        /// <summary>
        /// Handle button next search.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (SearchResults.Count > 0)
            {
                UpdateSelectedSearch();
                SearchIndex = SearchIndex < SearchResults.Count - 1 ? SearchIndex + 1 : SearchResults.Count - 1;
            }
        }

        #endregion
    }

    /// <summary>
    /// Represent a channel row.
    /// </summary>
    public class EPGChannelRow
    {
        public UCEPGViewer Viewer { get; set; }

        /// <summary>
        /// The internal channel.
        /// </summary>
        public channel Channel { get; set; }

        /// <summary>
        /// The previews program.
        /// </summary>
        public programme PreProg
        {
            get
            {
                try
                {
                    return (from p in Viewer.Epg.programme
                            where Utils.GetEPGDate(p.stop) < DateTime.Now
                            select p).Last();
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// The current program.
        /// </summary>
        public programme CurProg
        {
            get
            {
                try
                {
                    return (from p in Viewer.Epg.programme
                            where Utils.GetEPGDate(p.start) <= DateTime.Now &&
                            DateTime.Now < Utils.GetEPGDate(p.stop)
                            select p).First();
                }
                catch (InvalidOperationException)
                {
                    return null;
                }

            }
        }

        /// <summary>
        /// The next program.
        /// </summary>
        public programme NextProg
        {
            get
            {
                try
                {
                    return (from p in Viewer.Epg.programme
                            where DateTime.Now <= Utils.GetEPGDate(p.start)
                            select p).First();
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Create the channel row.
        /// </summary>
        /// <param name="viewer">The controling viewer.</param>
        /// <param name="chan">The channel.</param>
        public EPGChannelRow(UCEPGViewer viewer, channel chan)
        {
            Viewer = viewer;
            Channel = chan;
        }
    }

    /// <summary>
    /// The content of each list item.
    /// </summary>
    public class EPGItemContent : ListBoxItem
    {
        /// <summary>
        /// The inner program.
        /// </summary> 
        public programme Program { get; set; }

        /// <summary>
        /// The channel of the program.
        /// </summary>
        public channel Channel { get; set; }

        /// <summary>
        /// The ordered item if ordered.
        /// </summary>
        public OrderedItemContent Ordered { get; set; }

        /// <summary>
        /// Indicate if the current item is leftmost.
        /// </summary>
        public bool IsStart { get; set; }

        /// <summary>
        /// Indicate if the current item is rightmost.
        /// </summary>
        public bool IsStop { get; set; }

        /// <summary>
        /// Check if the program is ordered to watch.
        /// </summary>
        public bool IsOrdered { get { return Ordered != null; } }

        /// <summary>
        /// Get string representation of the content.
        /// </summary>
        /// <returns>The string to return.</returns>
        public override string ToString()
        {
            return Program.title[0].Value;
        }

        /// <summary>
        /// Indicate if the program is at the top channel.
        /// </summary>
        public bool IsTop { get; set; }

        /// <summary>
        /// Indicate if the program is at the bottom channel.
        /// </summary>
        public bool IsBottom { get; set; }
    }

    /// <summary>
    /// An ordered item.
    /// </summary>
    public class OrderedItemContent : ListBoxItem
    {
        /// <summary>
        /// The parent window.
        /// </summary>
        public UCEPGViewer ParentWindow { get; set; }

        /// <summary>
        /// The proggrame linked with this order.
        /// </summary>
        public EPGItemContent LinkedContent { get; set; }

        /// <summary>
        /// The window start when the order was taken.
        /// </summary>
        public DateTime WindowStart { get; set; }

        /// <summary>
        /// The window stop when the order was taken.
        /// </summary>
        public DateTime WindowStop { get; set; }

        /// <summary>
        /// The channel index
        /// </summary>
        public int ChanIdx { get; set; }

        /// <summary>
        /// The inner timer.
        /// </summary>
        private Timer m_timer;

        /// <summary>
        /// Get string representation of the content.
        /// </summary>
        /// <returns>The string to return.</returns>
        public override string ToString()
        {
            return LinkedContent.Program.title[0].Value;
        }

        /// <summary>
        /// Set the timer of the event.
        /// </summary>
        /// <param name="timerElapsed">The event to raise.</param>
        public void SetTimer()
        {
            m_timer = new Timer();
            var ts = Utils.GetEPGDate(LinkedContent.Program.start) - DateTime.Now;
            if (ts.TotalMilliseconds > 0)
            {
                m_timer.Interval = ts.TotalMilliseconds;
                m_timer.AutoReset = false;
                m_timer.Start();
                m_timer.Elapsed += orderedElapse;
            }
        }

        /// <summary>
        /// Called when a program time as arrived.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void orderedElapse(object sender, ElapsedEventArgs e)
        {
            MessageBox.Show("עכשיו מתחיל:" + LinkedContent.Program.title[0].Value + " בערוץ " +
                LinkedContent.Channel.displayname[0].Value);

            ParentWindow.Dispatcher.Invoke(new Action(() =>
            {
                ParentWindow.lstOrders.Items.Remove(this);
                ((StackPanel)LinkedContent.Content).Children[0].Visibility = System.Windows.Visibility.Collapsed;
                LinkedContent.Ordered = null;
                LinkedContent.Background = Brushes.Transparent;
            }));
        }

        /// <summary>
        /// Disable the timer.
        /// </summary>
        public void UnsetTimer()
        {
            m_timer.Stop();
        }
    }
}
