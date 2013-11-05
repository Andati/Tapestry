using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Threading;

namespace Tapestry.views
{
    public partial class GamePage : PhoneApplicationPage
    {
        public static String EXTRA_TIME = "time";
        public static int TIME_UNLIMITED = -1;

        private bool hasStarted;
        public GamePage()
        {
            InitializeComponent();
            hasStarted = false;
        }

        private void startGame(int timeout)
        {
            stckStart.Visibility = (hasStarted) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            txtCount.Visibility = (hasStarted) ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
            //start timer

            DispatcherTimer timer = new DispatcherTimer();
            int counter = getTimeout();

            //txtTimer.Text = counter.ToString();

            timer.Tick += delegate(object s, EventArgs args)
            {
                counter--;
                txtTimer.Text = counter.ToString();
                if (counter == 0)
                {
                    timer.Stop();
                    stopGame(txtCount.Text);
                }
            };

            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            hasStarted = true;
        }
        private void stopGame(string count)
        {
            MessageBox.Show("Your score is: " + count);
            hasStarted = false;
        }

        private void stckStart_Tap(object sender, GestureEventArgs e)
        {
            startGame(getTimeout());
        }

        private int getTimeout()
        {
            string timeStr = NavigationContext.QueryString[EXTRA_TIME];
            int time = int.Parse(timeStr);
            return time;
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            int time = getTimeout();
            txtTimer.Text = (time != TIME_UNLIMITED) ? time.ToString() : "tapathon";
        }

        private void mouseEvtExit(object sender, MouseEventArgs e)
        {
            if (hasStarted)
                txtCount.Text = (int.Parse(txtCount.Text) + e.StylusDevice.GetStylusPoints((UIElement)sender).Count).ToString();
        }
    }
}