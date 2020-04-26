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

        int clicker = 0;
        private void StartStop_Click(object sender, RoutedEventArgs e)
        {
            if (clicker % 2 != 0)
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
            }
            clicker++;
        }

        private void ResetOrLap_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch.Text = "0:00:00";
        }
    }
}
