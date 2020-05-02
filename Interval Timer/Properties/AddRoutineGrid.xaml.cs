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

        private void NewRoutineButton_Click(object sender, RoutedEventArgs e)
        {
            if (NewRoutineName.Text == "")
            {
                NameError.Visibility = Visibility.Visible;
            }
            else
            {
                NameError.Visibility = Visibility.Hidden;
                string fileName = NewRoutineName.Text + ".txt";
                string filePath = AppDomain.CurrentDomain.BaseDirectory + fileName;
                if (!File.Exists(filePath))
                {
                    File.WriteAllText(filePath, $"Name: { NewRoutineName.Text }");
                    Console.WriteLine("Success");
                }
                else
                {
                    NameError.Text = "Routine name already exists, please choose another name";
                    NameError.Visibility = Visibility.Visible;
                    Console.WriteLine("Already exists.");
                }
                
            }
        }

        bool hasBeenClicked = false;
        private void NewRoutineName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!hasBeenClicked)
            {
                NewRoutineName.Text = "";
                hasBeenClicked = true;
            }
        }
    }
}
