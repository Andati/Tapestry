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
using Tapestry.app;

namespace Tapestry.views
{
    public partial class GamePage : PhoneApplicationPage
    {
        public static readonly string EXTRA_TIME = "time";
        private enum GAME_STATE
        {
            STARTING, //game has not yet started running
            RUNNING, //game is running
            PAUSED, //game was running then paused
            STOPPED //game was running then stopped
        };
        private GAME_STATE gameState;
        private bool isTimed;
        private DateTime startTime;
        private DispatcherTimer timer;

        public GamePage()
        {
            InitializeComponent();
            gameState = GAME_STATE.STARTING;
            isTimed = true; //true by default unless it's a tapathon
            startTime = DateTime.Now;
            timer = null;
        }

        private void startGame(int timeout)
        {
            stckStart.Visibility = (gameState == GAME_STATE.RUNNING) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            txtCount.Visibility = (gameState == GAME_STATE.RUNNING) ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
            if (timeout > 1) //starttimer if is timed game
            {
                //start timer
                timer = new DispatcherTimer();
                int counter = getTimeout();
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
            }
            else
            {
                startTime = DateTime.Now; isTimed = false;
            }
            gameState = GAME_STATE.RUNNING;
        }

        private void stopGame(string count)
        {
            showScores(count);
            gameState = GAME_STATE.STOPPED;
            timer = null;
        }

        private void showScores(string count)
        {
            int time = (isTimed) ? getTimeout() : (int)(DateTime.Now - startTime).TotalSeconds;
            MessageBox.Show(String.Format("You have tapped {0} times within {1} {2}", count, time, StringVals.TIME_UNIT));
        }

        private void stckStart_Tap(object sender, GestureEventArgs e)
        {
            startGame(getTimeout());
            //TODO Remove duplicate code to get initial tap location
            #region duplicatecode
            Random r = new Random();
            byte alpha = (byte)r.Next(130, 256);
            byte red = (byte)r.Next(0, 256);
            byte green = (byte)r.Next(0, 256);
            byte blue = (byte)r.Next(0, 256);
            Point p = e.GetPosition(tapCanvas);
            addTapSpot(p, Color.FromArgb(alpha, red, green, blue));
            #endregion
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
            txtTimer.Text = (time > 1) ? time.ToString() : "tapathon";
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            //TODO save game state, pause game e.t.c
            if (timer != null) timer.Stop();
        }
        private void mouseEvt(object sender, MouseEventArgs e)
        {
            if (gameState == GAME_STATE.STARTING)//start game if not yet started
            { startGame(getTimeout()); }

            if (gameState == GAME_STATE.RUNNING)
            {
                txtCount.Text = (int.Parse(txtCount.Text) + e.StylusDevice.GetStylusPoints((UIElement)sender).Count).ToString();
                Random r = new Random();
                byte alpha = (byte)r.Next(130, 256);
                byte red = (byte)r.Next(0, 256);
                byte green = (byte)r.Next(0, 256);
                byte blue = (byte)r.Next(0, 256);
                Point p = new Point
                {
                    X = e.StylusDevice.GetStylusPoints(tapCanvas).First().X,
                    Y = e.StylusDevice.GetStylusPoints(tapCanvas).First().Y
                };
                addTapSpot(p, Color.FromArgb(alpha, red, green, blue));
            }

        }

        private void addTapSpot(Point p, Color c)
        {
            Ellipse tap = new Ellipse { Width = 30, Height = 30, Fill = new SolidColorBrush(c) };
            tap.SetValue(Canvas.LeftProperty, p.X - tap.Width / 2);
            tap.SetValue(Canvas.TopProperty, p.Y - tap.Height / 2);
            tap.IsHitTestVisible = false;
            tapCanvas.Children.Add(tap);
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (isTimed)
            {
                MessageBox.Show("Bailed out too soon!");
            }
            else
            {
                showScores(txtCount.Text);
            }
            base.OnBackKeyPress(e);            
        }
    }
}