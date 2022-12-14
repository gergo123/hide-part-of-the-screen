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
using System.Windows.Shapes;

namespace App
{
    /// <summary>
    /// Interaction logic for AlwaysOnTopWindow.xaml
    /// </summary>
    public partial class AlwaysOnTopWindow : Window
    {
        public AlwaysOnTopWindow()
        {
            InitializeComponent();
            //this.MouseDown += delegate { DragMove(); };
            this.MouseDown += AlwaysOnTopWindow_MouseDown;
        }

        private void AlwaysOnTopWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}
