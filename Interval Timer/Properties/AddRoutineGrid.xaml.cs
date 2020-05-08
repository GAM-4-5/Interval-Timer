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
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Interval_Timer.Properties
{
    /// <summary>
    /// Interaction logic for AddRoutineGrid.xaml
    /// </summary>
    public partial class AddRoutineGrid : UserControl
    {
        public AddRoutineGrid()
        {
            InitializeComponent();
        }

        public string routineFolderPath = AppDomain.CurrentDomain.BaseDirectory + @"Routines\";
        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        private void NewRoutineButton_Click(object sender, RoutedEventArgs e)
        {
            RoutineInformation routineinformation = RoutineInformation.Instance();
            if (NewRoutineName.Text == "")
            {
                NameError.Visibility = Visibility.Visible;
                NameError.Text = "Routine name is needed";
            }
            else if (NewRoutineName.Text.Length > 19)
            {
                NameError.Visibility = Visibility.Visible;
                NameError.Text = "Routine name should be less than 20 characters";
            }
            else
            {
                var controls = new[] { WarmupHours, WarmupMinutes, WarmupSeconds, LowHours, LowMinutes, LowSeconds, HighHours, HighMinutes, HighSeconds, RepeatTimes, CHours, CMinutes, CSeconds, Sets, RHours, RMinutes, RSeconds };
                int counter = 0;
                int zerocounter = 0;
                foreach (var control in controls)
                {
                    string txt = Convert.ToString(control.Text);
                    Console.WriteLine(txt);
                    Console.WriteLine(String.IsNullOrEmpty(txt));
                    Console.WriteLine(IsDigitsOnly(txt));
                    
                    if (String.IsNullOrEmpty(txt))
                    {
                        ErrorOrPass.Text = "Please fill all fields";
                        ErrorOrPass.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        if (IsDigitsOnly(txt))
                        {
                            counter++;
                            if (Convert.ToInt32(control.Text) == 0)
                            {
                                zerocounter++;
                            }
                        }
                        else
                        {
                            ErrorOrPass.Text = "Please put only numbers";
                            ErrorOrPass.Visibility = Visibility.Visible;
                        }
                    }
                }

                if (counter == 17 && zerocounter < 15)
                {
                    routineinformation.Load();
                    string name = NewRoutineName.Text;
                    int warmup = Convert.ToInt32(WarmupHours.Text) * 3600 + Convert.ToInt32(WarmupMinutes.Text) * 60 + Convert.ToInt32(WarmupSeconds.Text);
                    int lowset = Convert.ToInt32(LowHours.Text) * 3600 + Convert.ToInt32(LowMinutes.Text) * 60 + Convert.ToInt32(LowSeconds.Text);
                    int highset = Convert.ToInt32(HighHours.Text) * 3600 + Convert.ToInt32(HighMinutes.Text) * 60 + Convert.ToInt32(HighSeconds.Text);
                    int sets = Convert.ToInt32(RepeatTimes.Text);
                    bool firsthigh = Convert.ToBoolean(FirstHigh.IsChecked);
                    int cooldown = Convert.ToInt32(CHours.Text) * 3600 + Convert.ToInt32(CMinutes.Text) * 60 + Convert.ToInt32(CSeconds.Text);
                    int repeat = Convert.ToInt32(Sets.Text);
                    int rest = Convert.ToInt32(RHours.Text) * 3600 + Convert.ToInt32(RMinutes.Text) * 60 + Convert.ToInt32(RSeconds.Text);
                    routineinformation.AddRoutine(name, warmup, lowset, highset, sets, firsthigh, cooldown, repeat, rest);
                    routineinformation.Save();
                    routineinformation.Print();
                    Console.WriteLine("Success");
                    ErrorOrPass.Text = "Succesfully added routine";
                    ErrorOrPass.Visibility = Visibility.Visible;
                    NameError.Visibility = Visibility.Hidden;

                }
                else
                {
                    ErrorOrPass.Text = "Unavailable routine";
                    ErrorOrPass.Visibility = Visibility.Visible;
                }
                

            }
        }

        //bool hasBeenClicked = false;

        //private void NewRoutineName_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (!hasBeenClicked)
        //    {
        //        NewRoutineName.Text = "";
        //        hasBeenClicked = true;
        //    }
        //}
    }
}
