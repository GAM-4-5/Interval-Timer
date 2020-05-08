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
using System.Data;

namespace Interval_Timer.Properties
{
    /// <summary>
    /// Interaction logic for MyRoutinesGrid.xaml
    /// </summary>
    public partial class MyRoutinesGrid : UserControl
    {
        public MyRoutinesGrid()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RoutineInformation routineinformation = RoutineInformation.Instance();
            routineinformation.Load();

            routineinformation.Print();
            Dictionary<string, Routine> dict = routineinformation.GetDict();
            foreach (Routine routine in dict.Values)
            {
                if (!(dict.Count <= 0))
                {
                    Console.WriteLine($"{routine.Name}, {routineinformation.GetTime(routine.Name)}");
                    string name = routine.Name.PadRight(20);
                    MyRoutinesList.Items.Add($"{name} {routineinformation.GetTime(routine.Name)}");
                }
            }
        }

        private void MyRoutinesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = MyRoutinesList.SelectedIndex;
            string name = Convert.ToString(MyRoutinesList.SelectedItems[0]);
            name = name.Substring(0, 20);
            switch (index)
            {
                default:
                    Console.WriteLine(name);
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new CountdownGrid(name.TrimEnd()));
                    break;


            }
        }
    }
}
