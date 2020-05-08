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
using System.Windows.Controls.Primitives;
using Interval_Timer.Properties;

namespace Interval_Timer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {

        }



        private void Shutdown_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            MoveCursorMenu(index);

            switch (index)
            {
                case 0:
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new AddRoutineGrid());
                    break;
                case 1:
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new MyRoutinesGrid());
                    break;
                case 2:
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new FinishedRoutinesGrid());
                    break;
                case 3:
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new StopwatchGrid());
                    break;
                case 4:
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new CountdownGrid("BstsASmtsIFxfKHHvibF"));
                    break;

            }
        }
            private void MoveCursorMenu(int index)
        {
            TransitioningContentSlide.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, 60 * index, 0, 0);
            GridCursor.Width = 5;
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("We are still in beta development, this is currently unavailable");
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("We are still in beta development, this is currently unavailable");
        }
    }
}