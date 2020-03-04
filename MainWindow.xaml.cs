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

namespace DesktopApp
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void button_minimize_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void button_kuchnia_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new Pages.Cook();
        }

        private void button_dostawy_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new Pages.Delivery();
        }

        private void WrapPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PageMovement.Clicked(Mouse.GetPosition(this));

        }

        private void WrapPanel_MouseMove(object sender, MouseEventArgs e)
        {
            PageMovement.Move(e.GetPosition(this), Application.Current.MainWindow);
        }

        private void WrapPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            PageMovement.UnClicked();
        }
    }
}
