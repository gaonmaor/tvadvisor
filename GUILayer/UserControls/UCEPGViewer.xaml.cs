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
using LogicLayer;

namespace GUILayer
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

        /// <summary>
        /// The list of orderedDetails
        /// </summary>
        private List<OrderDetail> lstOrderDetails;

        /// <summary>
        /// A timer for the event saves the orders.
        /// </summary>
        private Timer saveTimer;

        /// <summary>
        /// Indicate if there are orders need to be saved.
        /// </summary>
        private bool needToSave;

        /// <summary>
        /// Indicate if there are orders need to be saved.
        /// </summary>
        public bool NeedToSave 
        {
            get
            {
                return needToSave;
            }
            set
            {
                needToSave = value;
                if (needToSave == true)
                {
                    if (!saveTimer.Enabled)
                    {
                        saveTimer.Interval = 60000;
                        saveTimer.Start();
                    }
                }
                else
                {
                    saveTimer.Stop();
                }
            }
        }

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
            lstOrderDetails = new List<OrderDetail>();
            needToSave = false;
            saveTimer = new Timer();
            saveTimer.Elapsed += new ElapsedEventHandler(saveTimer_Elapsed);
            SearchIndex = -1;
            OrderedBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFC66E"));
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

        /// <summary>
        /// Save all the unsaved orders.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void saveTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (needToSave)
                {
                    NeedToSave = false;
                    SaveOrders();
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
        /// Save the current orders to the database.
        /// </summary>
        private void SaveOrders()
        {
            LogicManager.Instance.SaveOrders(lstOrderDetails);
        }

        /// <summary>
        /// Load the user orders.
        /// </summary>
        /// <param name="userId">The user id.</param>
        private void LoadOrders(int userId)
        {
            List<OrderDetail> retList = LogicManager.Instance.LoadOrders(userId);
            NeedToSave = false;
            foreach (OrderDetail order in retList)
            {
                if (order.Start >= DateTime.Now)
                {
                    programme orderedProgram = null;
                    channel chan = null;
                    try
                    {
                        orderedProgram = (from p in Epg.programme
                                                    where p.channel == order.ChanId && order.Start == Utils.GetEPGDate(p.start)
                                                    select p).First();
                    }
                    catch (Exception)
                    {

                        continue;
                    }

                    try
                    {
                        chan = (from c in Epg.channel where c.id == order.ChanId select c).First();
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                    DateTime windowStart = new DateTime(order.Start.Year, order.Start.Month, order.Start.Day, order.Start.Hour, (order.Start.Minute > 30) ? 30 : 0, 0, DateTimeKind.Local);
                    DateTime windowStop = new DateTime(WindowStart.Ticks, DateTimeKind.Local);
                    WindowStep = 30;
                    windowStop = WindowStop.AddMinutes(WindowStep * 3);

                    OrderedItemContent ordered = new OrderedItemContent()
                    {
                        ParentWindow = epgViewer,
                        LinkedContent = null,
                        Content = Utils.GetEPGDate(orderedProgram.start).ToShortTimeString() + " : " +
                            chan.displayname[0].Value + " : " + orderedProgram.title[0].Value,
                        WindowStart = windowStart,
                        WindowStop = windowStop,
                        ChanIdx = m_chanIdx,
                        OrderedDetail = order
                    };
                    
                    lstOrderDetails.Add(ordered.OrderedDetail);
                    //item.Background = OrderedBrush;
                    int i = 0;
                    for (; i < lstOrders.Items.Count; ++i)
                    {
                        if (((OrderedItemContent)lstOrders.Items[i]).OrderedDetail.Start > order.Start)
                        {
                            break;
                        }
                    }
                    lstOrders.Items.Insert(i, ordered);
                    ordered.SetTimer();
                }
            }
        }

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
                DateTime mid = new DateTime((WindowStart.Ticks + WindowStop.Ticks) / 2, DateTimeKind.Local);
                lblMid.Content = String.Format("{0:00}:{1:00}", mid.Hour, mid.Minute);
                lblStop.Content = String.Format("{0:00}:{1:00}", WindowStop.Hour, WindowStop.Minute);

                int count = 0;
                int chnIdx = 0;
                var min = (from p in Epg.programme select new { Time = Utils.GetEPGDate(p.start), String = p.start }).Aggregate(
                    (curmin, x) => (curmin == null || x.Time <= curmin.Time ? x : curmin));
                var max = (from p in Epg.programme select new { Time = Utils.GetEPGDate(p.start), String = p.start }).Aggregate(
                    (curmin, x) => (curmin == null || x.Time >= curmin.Time ? x : curmin));

                foreach (var chan in Epg.channel)
                {
                    if ((from p in Epg.programme where p.channel == chan.id select p).Count() == 0)
                    {
                        var list = Epg.programme.ToList();                  
                        list.Add(new programme() { title = new title[1] { new title() { Value = "No information"}}, 
                            start = min.String, stop = max.String, channel = chan.id });
                        Epg.programme = list.ToArray();
                    }

                    if (chnIdx < m_chanIdx)
                    {
                        ++chnIdx;
                        continue;
                    }

                    if (count == GUILayer.Properties.Settings.Default.ChannelsInWindow)
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

                        double mult = (lstChannels.ActualWidth - 15) / div;

                        programme[] programs = chanProg.ToArray();
                        ListBoxItem lbiFirst = null;
                        ListBoxItem lbiLast = null;
                        bool loadOrdersMode = false;
                        for (int i = 0; i < programs.Length; ++i)
                        {
                            programme p = programs[i];
                            EPGItemContent lbi = null;
                            foreach (OrderedItemContent o in lstOrders.Items)
                            {
                                if (o.LinkedContent != null)
                                {
                                    if (o.LinkedContent.Program == p)
                                    {
                                        lbi = o.LinkedContent;
                                        loadOrdersMode = false;
                                        break;
                                    }
                                }
                                else
	                            {
                                    loadOrdersMode = true;
	                            }
                            }

                            if (lbi == null)
                            {
                                StackPanel spLBI = new StackPanel() { Orientation = Orientation.Horizontal };
                                spLBI.Children.Add(new Image()
                                {
                                    Visibility = System.Windows.Visibility.Hidden,
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
                                    FlowDirection = System.Windows.FlowDirection.LeftToRight,
                                    IsStop = (i + 1) == programs.Length,
                                    IsTop = count == 0,
                                    IsBottom = count + 1 == GUILayer.Properties.Settings.Default.ChannelsInWindow,
                                    Content = spLBI,
                                };
                            }
                            lbi.Width = (Math.Min(Utils.GetEPGDate(p.stop).Ticks, WindowStop.Ticks) -
                                    Math.Max(Utils.GetEPGDate(p.start).Ticks, WindowStart.Ticks)) * mult;


                            if (loadOrdersMode)
                            {
                                foreach (OrderedItemContent o in lstOrders.Items)
                                {
                                    if (o.OrderedDetail.Start == Utils.GetEPGDate(p.start) &&
                                        o.OrderedDetail.ChanId == p.channel)
                                    {
                                        o.LinkedContent = lbi;
                                        lbi.Ordered = o;
                                        ((StackPanel)lbi.Content).Children[0].Visibility = System.Windows.Visibility.Visible;
                                    }
                                }
                            }
                            lstChannels.Items.Add(lbi);

                            if (selectedChan == p.channel && (
                                ((dir == Dir.Right) && i == 0) ||
                                ((dir == Dir.Left) && i + 1 == programs.Length) ||
                                ((dir == Dir.Up) && count == 0) ||
                                ((dir == Dir.Down) && (count + 2) == GUILayer.Properties.Settings.Default.ChannelsInWindow)))
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
                else if (e.Key == Key.Escape)
                {
                    MainWindow.Instance.Close();
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
                    WindowStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 
                        DateTime.Now.Hour, (DateTime.Now.Minute > 30) ? 30 : 0, 0);
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
                        lstOrderDetails.Remove(item.Ordered.OrderedDetail);
                        lstOrders.Items.Remove(item.Ordered);
                        item.Ordered = null;
                        NeedToSave = true;
                        //item.Background = Brushes.Transparent;
                        ((StackPanel)item.Content).Children[0].Visibility = System.Windows.Visibility.Hidden;
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
                            Content = Utils.GetEPGDate(item.Program.start).ToShortTimeString() + " : " + 
                                item.Channel.displayname[0].Value +  " : " + item.Program.title[0].Value,
                            WindowStart = this.WindowStart,
                            WindowStop = this.WindowStop,
                            ChanIdx = m_chanIdx,
                            OrderedDetail = new OrderDetail(MainWindow.Instance.UserID, 
                            item.Channel.id, Utils.GetEPGDate(item.Program.start), false)
                        };
                        NeedToSave = true;
                        lstOrderDetails.Add(ordered.OrderedDetail);
                        //item.Background = OrderedBrush;
                        item.Ordered = ordered;
                        int i = 0;
                        for (; i < lstOrders.Items.Count; ++i)
                        {
                            if (((OrderedItemContent)lstOrders.Items[i]).OrderedDetail.Start > start)
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
                    AddDetailPanel("Channel:", SelectedChannel.displayname[0].Value);
                AddDetailPanel("Time:", Utils.GetEPGDate(SelectedProgram.stop).ToShortTimeString() + " - " +
                    Utils.GetEPGDate(SelectedProgram.start).ToShortTimeString());
                if (SelectedProgram.category != null)
                    AddDetailPanel("Category:", SelectedProgram.category[0].Value);
                if (SelectedProgram.desc != null && SelectedProgram.desc.Count() > 0 && SelectedProgram.desc[0].Value != null)
                    AddDetailPanel("Description:", SelectedProgram.desc[0].Value);
                if (SelectedProgram.credits != null &&
                    SelectedProgram.credits.actor != null &&
                    SelectedProgram.credits.actor.Length > 0)
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (actor a in SelectedProgram.credits.actor)
                    {
                        builder.Append((a.role != null?"(" + a.role + ") ":"") + a.Value + " ");
                    }
                    AddDetailPanel("Actors:", builder.ToString());
                }
                if (SelectedProgram.country != null &&
                    SelectedProgram.country.Length > 0 &&
                    SelectedProgram.country[0] != null)
                {
                    AddDetailPanel("Country:", SelectedProgram.country[0].Value);
                }
                if (SelectedProgram.rating != null && 
                    SelectedProgram.rating.Length > 0 &&
                    SelectedProgram.rating[0] != null)
                    
                {
                    int rate = -1;
                    if (Int32.TryParse(SelectedProgram.rating[0].value, out rate))
                    {
                        AddRatingPanel("Rating", rate);
                    }
                    else
                    {
                        AddRatingPanel("Rating", 0);
                    }
                }
                else
                {
                    AddRatingPanel("Rating", 0);
                }
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
            Grid g = new Grid();
            g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0, GridUnitType.Auto) });
            g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            Label l = new Label() { Content = title };
            l.FontSize = 18;
            l.FontWeight = FontWeights.Bold;
            l.SetValue(Grid.ColumnProperty, 0);
            g.Children.Add(l);
            TextBlock tb = new TextBlock() { Text = desc, TextWrapping = TextWrapping.Wrap };
            tb.FontSize = 16;
            tb.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            tb.SetValue(Grid.ColumnProperty, 1);
            g.Children.Add(tb);

            grdDetails.Children.Add(new Label()
            {
                Content = g
            });
        }

        /// <summary>
        /// Get stack pannel for the given property.
        /// </summary>
        /// <param name="title">The title of the propery.</param>
        /// <param name="desc">The description of the property.</param>
        private void AddRatingPanel(string title, int rate)
        {
            RatingsControl rc = new RatingsControl() { NumberOfStars = 10, Value = rate };
            rc.RenderTransform = new ScaleTransform(2, 2);
            rc.BackgroundColor = Brushes.White;
            rc.StarForegroundColor = Brushes.Orange;
            rc.StarOutlineColor = Brushes.DarkGray;
            rc.ValueUpdated += new EventHandler(rc_ValueUpdated);

            Grid g = new Grid();
            g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0, GridUnitType.Auto) });
            g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            Label l = new Label() { Content = title };
            l.FontSize = 18;
            l.FontWeight = FontWeights.Bold;
            l.SetValue(Grid.ColumnProperty, 0);
            g.Children.Add(l);

            rc.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            rc.SetValue(Grid.ColumnProperty, 1);
            g.Children.Add(rc);

            grdDetails.Children.Add(new Label()
            {
                Content = g
            });
        }

        void rc_ValueUpdated(object sender, EventArgs e)
        {
            if (SelectedProgram.rating == null ||
                SelectedProgram.rating.Length <= 0)
            {
                SelectedProgram.rating = new rating[1];
                SelectedProgram.rating[0] = new rating();
            }
            SelectedProgram.rating[0].value = ((RatingsControl)sender).Value.ToString();
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
                        (p.desc != null && (p.desc[0].Value != null) && (p.desc[0].Value).Contains(txtSearch.Text)))
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

        /// <summary>
        /// Handle Focuse managment.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void epgViewer_GotFocus(object sender, RoutedEventArgs e)
        {
            if(lstChannels.Items.Count > 0)
            {
                lstChannels.SelectedIndex = 0;
                epgViewer.GotFocus -= epgViewer_GotFocus;
                ((ListBoxItem)(lstChannels.SelectedItem)).Focus();
            }
        }

        /// <summary>
        /// Handle unloading.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void epgViewer_Unloaded(object sender, RoutedEventArgs e)
        {
            SaveOrders();
        }

        #endregion

        private void epgViewer_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                int userId = MainWindow.Instance.UserID;
                LoadOrders(userId);
            }
            catch (Exception)
            {
            }
        }
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
        /// The ordered detail.
        /// </summary>
        public OrderDetail OrderedDetail { get; set; }

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
            var ts = OrderedDetail.Start - DateTime.Now;
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
            MessageBox.Show("Now Starting:\n" + LinkedContent.Program.title[0].Value + "\nAt Channel:\n" +
                LinkedContent.Channel.displayname[0].Value);

            ParentWindow.Dispatcher.Invoke(new Action(() =>
            {
                ParentWindow.lstOrders.Items.Remove(this);
                ((StackPanel)LinkedContent.Content).Children[0].Visibility = System.Windows.Visibility.Hidden;
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
