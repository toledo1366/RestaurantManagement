using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DesktopApp
{
    static class PageMovement
    {
        private static bool clicked = false;
        private static Point point = new Point();

        public static void Clicked(Point p)
        {
            clicked = true;
            point = p;

        }
        public static void Move(Point e, System.Windows.Window t)
        {
            if (clicked)
            {
                Application.Current.MainWindow.Left += (e.X - point.X);
                Application.Current.MainWindow.Top += (e.Y - point.Y);
            }
        }
        public static void UnClicked()
        {
            clicked = false;
        }

    }
}
