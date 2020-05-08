using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
using System.Runtime.CompilerServices;

namespace Interval_Timer
{
    /// <summary>
    /// Interaction logic for CountdownGrid.xaml
    /// </summary>
    public partial class CountdownGrid : UserControl
    {
        RoutineInformation routineinformation = RoutineInformation.Instance();
        private int time;
        private DispatcherTimer timer;

        private int ITime;
        private DispatcherTimer ITimer;

        string allInfo;
        int warmup;
        int highset;
        int lowset;
        int sets;
        bool firstinthigh;
        int repeat;
        int rest;
        int cooldown;
        string[] array;

        public CountdownGrid(string name)
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
            time = routineinformation.GetTimeSeconds(name);
            ITimer = new DispatcherTimer();
            ITimer.Interval = new TimeSpan(0, 0, 1);
            ITimer.Tick += ITimer_Tick;
            if (name != "BstsASmtsIFxfKHHvibF")
            {
                allInfo = routineinformation.GetAll(name);
                array = allInfo.Split();
                warmup = Convert.ToInt32(array[0]);
                highset = Convert.ToInt32(array[1]);
                lowset = Convert.ToInt32(array[2]);
                firstinthigh = Convert.ToBoolean(array[3]);
                sets = Convert.ToInt32(array[4]);
                repeat = Convert.ToInt32(array[5]);
                rest = Convert.ToInt32(array[6]);
                cooldown = Convert.ToInt32(array[7]);
                ITime = warmup;
            }
            
        }


        void Timer_Tick(object sender, EventArgs e)
        {
            if (time > 0)
            { 
                time--;
                Timer.Text = routineinformation.GetTimeFromSeconds(time);
                if (time == 0) { IT.Text = "00:00"; }
            }
            else
            {
                timer.Stop();
                ITimer.Stop();
                ListOfRoutines.Visibility = Visibility.Visible;
                MessageBox.Show("Activity completed");
            }
        }

        int wcounter = 0;
        void ITimer_Tick(object sender, EventArgs e)
        {
            if (time > 0)
            {
                if (warmup > 1) 
                {
                    if (wcounter == 0)
                    {
                        ITime = warmup;
                        IT.Text = routineinformation.GetTimeFromSeconds(ITime);
                    }
                    ITime--;
                    warmup--;
                    IT.Text = routineinformation.GetTimeFromSeconds(ITime);
                    wcounter++;
                } 
                else
                {
                    if (firstinthigh)
                    {
                        if (sets > 0)
                        {
                            if (highset > 0)
                            {
                                ITime = highset;
                                IT.Text = routineinformation.GetTimeFromSeconds(ITime);
                                State.Text = "High interval";
                                highset--;
                                Console.WriteLine($"High set: {highset}");
                            }
                            else if (lowset > 0)
                            {
                                ITime = lowset;
                                IT.Text = routineinformation.GetTimeFromSeconds(ITime);
                                State.Text = "Low interval";
                                lowset--;
                                Console.WriteLine($"Low set: {lowset}");
                            }
                            else if (highset == 0 && lowset == 0)
                            {
                                ITime = Convert.ToInt32(array[1]);
                                IT.Text = routineinformation.GetTimeFromSeconds(ITime);
                                highset = Convert.ToInt32(array[1]);
                                lowset = Convert.ToInt32(array[2]);
                                State.Text = "High interval";
                                sets--;                                
                                highset--;
                                if (sets == 0 && repeat > 1)
                                {
                                    State.Text = "Rest";
                                    ITime = rest;
                                    IT.Text = routineinformation.GetTimeFromSeconds(ITime);
                                    rest--;
                                    highset = Convert.ToInt32(array[1]);
                                }
                                else if (sets == 0 && repeat == 1)
                                {
                                    State.Text = "Cooldown";
                                    ITime = cooldown;
                                    IT.Text = routineinformation.GetTimeFromSeconds(ITime);
                                    cooldown--;
                                }
                                Console.WriteLine($"Sets: {sets}");
                            }
                        }
                        else if (rest > 0 && repeat > 1)
                        {
                            Console.WriteLine($"Rest1 {rest}");
                            ITime = rest;
                            IT.Text = routineinformation.GetTimeFromSeconds(ITime);
                            State.Text = "Rest";
                            rest--;
                            Console.WriteLine($"Rest2 {rest}");
                        }
                        else if (repeat > 1)
                        {
                            State.Text = "High interval";
                            ITime = highset;
                            IT.Text = routineinformation.GetTimeFromSeconds(ITime);
                            lowset = Convert.ToInt32(array[2]);
                            sets = Convert.ToInt32(array[4]);
                            rest = Convert.ToInt32(array[6]);
                            highset--;
                            repeat--;
                        }
                        else
                        {
                            State.Text = "Cooldown";
                            ITime = cooldown;
                            IT.Text = routineinformation.GetTimeFromSeconds(ITime);
                            cooldown--;
                        }
                        
                    }
                    else
                    {
                        if (sets > 0)
                        {
                            if (lowset > 0)
                            {
                                ITime = lowset;
                                IT.Text = routineinformation.GetTimeFromSeconds(ITime);
                                State.Text = "Low interval";
                                lowset--;
                                Console.WriteLine($"Low set: {lowset}");
                            }
                            else if (highset > 0)
                            {
                                ITime = highset;
                                IT.Text = routineinformation.GetTimeFromSeconds(ITime);
                                State.Text = "High interval";
                                highset--;
                                Console.WriteLine($"High set: {highset}");
                            }
                            else if (highset == 0 && lowset == 0)
                            {
                                ITime = Convert.ToInt32(array[2]);
                                IT.Text = routineinformation.GetTimeFromSeconds(ITime);
                                highset = Convert.ToInt32(array[1]);
                                lowset = Convert.ToInt32(array[2]);
                                State.Text = "Low interval";
                                sets--;
                                lowset--;
                                if (sets == 0 && repeat > 1)
                                {
                                    State.Text = "Rest";
                                    ITime = rest;
                                    IT.Text = routineinformation.GetTimeFromSeconds(ITime);
                                    rest--;
                                    lowset = Convert.ToInt32(array[2]);
                                }
                                else if (sets == 0 && repeat == 1)
                                {
                                    State.Text = "Cooldown";
                                    ITime = cooldown;
                                    IT.Text = routineinformation.GetTimeFromSeconds(ITime);
                                    cooldown--;
                                }
                                Console.WriteLine($"Sets: {sets}");
                            }
                        }
                        else if (rest > 0 && repeat > 1)
                        {
                            Console.WriteLine($"Rest1 {rest}");
                            ITime = rest;
                            IT.Text = routineinformation.GetTimeFromSeconds(ITime);
                            State.Text = "Rest";
                            rest--;
                            Console.WriteLine($"Rest2 {rest}");
                        }
                        else if (repeat > 1)
                        {
                            State.Text = "Low interval";
                            ITime = lowset;
                            IT.Text = routineinformation.GetTimeFromSeconds(ITime);
                            highset = Convert.ToInt32(array[1]);
                            sets = Convert.ToInt32(array[4]);
                            rest = Convert.ToInt32(array[6]);
                            lowset--;
                            repeat--;
                        }
                        else
                        {
                            State.Text = "Cooldown";
                            ITime = cooldown;
                            IT.Text = routineinformation.GetTimeFromSeconds(ITime);
                            cooldown--;
                        }
                    }
                }
            }
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            routineinformation.Load();
            Dictionary<string, Routine> dict = routineinformation.GetDict();
            foreach (Routine routine in dict.Values)
            {
                if (!(dict.Count <= 0))
                {
                    ListOfRoutines.Items.Add(routine.Name);
                }
            }
            Timer.Text = routineinformation.GetTimeFromSeconds(time);
            IT.Text = routineinformation.GetTimeFromSeconds(warmup);

        }

        int clicker = 0;
        private void ListOfRoutines_DropDownClosed(object sender, EventArgs e)
        {
            string selected = ListOfRoutines.Text;
            Console.WriteLine(selected);
            routineinformation.Load();
            Dictionary<string, Routine> dict = routineinformation.GetDict();
            foreach (Routine routine in dict.Values)
            {
                if (!(dict.Count <= 0))
                {
                    if (selected == routine.Name)
                    {
                        time = routineinformation.GetTimeSeconds(routine.Name);
                        Timer.Text = routineinformation.GetTimeFromSeconds(time);
                        allInfo = routineinformation.GetAll(routine.Name);
                        array = allInfo.Split();
                        warmup = Convert.ToInt32(array[0]);
                        highset = Convert.ToInt32(array[1]);
                        lowset = Convert.ToInt32(array[2]);
                        firstinthigh = Convert.ToBoolean(array[3]);
                        sets = Convert.ToInt32(array[4]);
                        repeat = Convert.ToInt32(array[5]);
                        rest = Convert.ToInt32(array[6]);
                        cooldown = Convert.ToInt32(array[7]);
                        ITime = warmup;
                        IT.Text = routineinformation.GetTimeFromSeconds(ITime);

                    }
                }
            }
            clicker = 0;
            StartStop.Content = "Start";
        }

        
        private void StartStop_Click(object sender, RoutedEventArgs e)
        {
            if (clicker % 2 != 0)
            {
                timer.Stop();
                ITimer.Stop();
                StartStop.Content = "Resume";
                ListOfRoutines.Visibility = Visibility.Visible;
            }
            else
            {
                timer.Start();
                ITimer.Start();
                StartStop.Content = "Pause";
                ListOfRoutines.Visibility = Visibility.Hidden;
            }
            clicker++;
            
        }

        private void Discard_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
