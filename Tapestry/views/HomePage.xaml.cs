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

namespace Tapestry.views
{
    public partial class HomePage : PhoneApplicationPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void lstCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox l = (ListBox)sender;
            int time = 0;
            switch (l.SelectedIndex)
            {
                case 0: time = 60; break;
                case 1: time = 30; break;
                case 2: time = 10; break;
                case 3: time = 5; break;
                default: time = views.GamePage.TIME_UNLIMITED; break;
            }
            String gameTime = views.GamePage.EXTRA_TIME + "=" + time.ToString();
            NavigationService.Navigate(new Uri("/views/GamePage.xaml?"+gameTime, UriKind.RelativeOrAbsolute));
        }
    }
}