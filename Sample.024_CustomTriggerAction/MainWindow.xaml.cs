using System;
using System.Windows;
using System.Windows.Threading;

namespace Sample._024_CustomTriggerAction
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            txtHour.Text = DateTime.Now.Hour.ToString();
            txtMinute.Text = DateTime.Now.Minute.ToString();
            txtSecond.Text = DateTime.Now.Second.ToString();
        }
    }
}
