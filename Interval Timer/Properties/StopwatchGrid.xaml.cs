using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Diagnostics;

namespace Interval_Timer.Properties
{
    /// <summary>
    /// Interaction logic for StopwatchGrid.xaml
    /// </summary>
    public partial class StopwatchGrid : UserControl
    {
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();
        string currentTime = string.Empty;
        public StopwatchGrid()
        {
            InitializeComponent();
            dt.Tick += new EventHandler(Dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1);
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            if (sw.IsRunning)
            {
                TimeSpan ts = sw.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                Stopwatch.Text = currentTime;
            }
            else
            {
                Stopwatch.Text = "{Binding currentTime, StringFormat={}";
            }
        }

        private int clicker = 0;

        public int Clicker { get => clicker; set => clicker = value; }

        private void StartStop_Click(object sender, RoutedEventArgs e)
        {
            if (Clicker % 2 != 0)
            {
                sw.Stop();
                dt.Stop();
                StartStop.Content = "Resume";
                ResetOrLap.Content = "Reset";
            }
            else
            {
                sw.Start();
                dt.Start();
                StartStop.Content = "Pause";
                ResetOrLap.Content = "Lap";
                RecordedTimes.Add(new List<int> { 0, 0, 0 });
            }
            Clicker++;
        }

        static string NewLap(int NewMinutes, int NewSeconds, int NewMilliseconds)
        {
            if (NewMilliseconds > 99)
            {
                NewMilliseconds = Convert.ToInt32(Convert.ToString(NewMilliseconds).Substring(0, 2));
            }
            var NewLapTime = String.Format("{0:00}:{1:00}:{2:00}",
            NewMinutes, NewSeconds, NewMilliseconds);
            return NewLapTime;
        }

        int NewMinutes = 0;
        int NewSeconds = 0;
        int NewMilliseconds = 0;
        List<List<int>> RecordedTimes = new List<List<int>>();
    public void ResetOrLap_Click(object sender, RoutedEventArgs e)
        {
            
            if (Clicker % 2 == 0)
            {
                Stopwatch.Text = "00:00:00";
                dt.Stop();
                dt.Equals(0);
                sw.Reset();
                LapListing.Children.Clear();
                StartStop.Content = "Start";
                ResetOrLap.Content = "Lap";
            }
            else
            {
                var newTextBlock = new TextBlock();
                var newSeparator = new Separator();
                TimeSpan ts = sw.Elapsed;
                NewMinutes = ts.Minutes - RecordedTimes[RecordedTimes.Count - 1][0];
                NewSeconds = ts.Seconds - RecordedTimes[RecordedTimes.Count - 1][1];
                NewMilliseconds = ts.Milliseconds / 10 - RecordedTimes[RecordedTimes.Count - 1][2];
                var ms = 0;
                ms = NewMinutes * 60000 + NewSeconds * 1000 + NewMilliseconds;
                NewMinutes = ms / 60000;
                NewSeconds = ms / 1000;
                NewMilliseconds = ms - ms / 60000 - ms / 1000;
                newTextBlock.Text = NewLap(NewMinutes, NewSeconds, NewMilliseconds);
                newTextBlock.FontSize = 20;
                newTextBlock.VerticalAlignment = VerticalAlignment.Center;
                newTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                newSeparator.VerticalAlignment = VerticalAlignment.Center;
                newSeparator.HorizontalAlignment = HorizontalAlignment.Center;
                newSeparator.Width = 550;
                LapListing.Children.Add(newTextBlock);
                LapListing.Children.Add(newSeparator);
                RecordedTimes.Add(new List<int> { ts.Minutes, ts.Seconds, ts.Milliseconds / 10 });
            }
        }

        private void ScrollViewer_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}
