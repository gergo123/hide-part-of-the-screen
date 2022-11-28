using App;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace HidePartOfTheScreen
{
    public enum WindowType { @default = 0, Center, Secondary, Third, PartyMembers }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Dictionary<WindowType, Window> windows { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            windows = new Dictionary<WindowType, Window>();
            createWindows();
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            if (windows.Count == 2)
            {
                clearWindows();
                createWindows();
            }
            else if (!allWindowsVisible() || windows.Count != 2)
            {
                createWindows();
            }
        }

        private void tryOpenWindow(WindowType type)
        {
            if (windows.TryGetValue(type, out Window? wnd))
            {
                if (!wnd.IsVisible)
                {
                    wnd.Close();
                    windows.Remove(type);
                    windows.Add(type, WindowFactory(type));
                }
            }
            else
            {
                windows.Add(type, WindowFactory(type));
            }
        }

        private Window WindowFactory(WindowType type)
        {
            switch (type)
            {
                case WindowType.Center:
                    return addCenterWindow();
                case WindowType.Secondary:
                    return addSecondaryWindow();
                case WindowType.Third:
                    return addThirdWindow();
            }
            throw new Exception();
        }

        private AlwaysOnTopWindow addCenterWindow()
        {
            AlwaysOnTopWindow window = new AlwaysOnTopWindow();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Topmost = true;
            window.Background = Brushes.Black;
            window.Show();

            return window;
        }

        private AlwaysOnTopWindow addSecondaryWindow()
        {
            AlwaysOnTopWindow window = new AlwaysOnTopWindow();
            window.WindowStartupLocation = WindowStartupLocation.Manual;
            window.Topmost = true;
            window.Background = Brushes.Black;
            window.Height = 320;

            var DisplayWidth = 1920;
            var DisplayHeight = 1080;
            window.Width = 145;
            window.Top = DisplayHeight / 2 - window.Height / 2 - 22;
            var padLeft = DisplayWidth - window.Width * 4 + 47.5;
            window.Left = padLeft;

            window.Show();

            return window;
        }

        private AlwaysOnTopWindow addThirdWindow()
        {
            AlwaysOnTopWindow window = new AlwaysOnTopWindow();
            window.WindowStartupLocation = WindowStartupLocation.Manual;
            window.Topmost = true;
            window.Background = Brushes.Black;
            window.Height = 60;
            window.Width = 90;
            window.ResizeMode = ResizeMode.CanResizeWithGrip;

            var DisplayWidth = 1920;
            var DisplayHeight = 1080;
            window.Top = DisplayHeight / 2 - window.Height * 6.2;

            var padLeft = DisplayWidth / 7;
            window.Left = padLeft;

            window.Show();

            return window;
        }

        private void createWindows()
        {
            tryOpenWindow(WindowType.Center);
            tryOpenWindow(WindowType.Secondary);
            tryOpenWindow(WindowType.Third);
        }

        private bool allWindowsVisible()
        {
            return windows.Select(x => x.Value).All(x => x.IsVisible);
        }

        private void clearWindows()
        {
            foreach (var wnd in windows)
            {
                wnd.Value.Close();
            }
            windows = new Dictionary<WindowType, Window>();
        }

        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            clearWindows();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            clearWindows();
            base.OnClosing(e);
        }
    }
}
