using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace WpfApplication6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            this._dt = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(230),
            };
            this._dt.Tick += this.dt_Tick;

            this.InitializeComponent();
        }

        private int _counter;
        private readonly DispatcherTimer _dt;

        private void dt_Tick(object sender, EventArgs e)
        {
            Debug.WriteLine("Called: dt_Tick");
            this.TextBlock.Text = (this._counter++).ToString();
        }


        private void BUTTON_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("Called: BUTTON_PreviewMouseLeftButtonDown");
            this._counter = 0;
            this._dt.Start();
        }

        private void BUTTON_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("Called: BUTTON_PreviewMouseLeftButtonUp");
            this._dt.Stop();
        }

        private void BUTTON_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Called: BUTTON_Click");
        }
    }
}
