using System;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Tapestry.app;

namespace Tapestry.views
{
    public partial class HomePage : PhoneApplicationPage
    {
        private static Challenge[] challenges = { 
                                                    new Challenge { time = 60 },new Challenge { time = 50 }, 
                                                    new Challenge { time = 40 },new Challenge { time = 30 },
                                                    new Challenge { time = 20 },new Challenge { time = 10 },
                                                    new Challenge { time = 0 }
                                                };

        public HomePage()
        {
            InitializeComponent();
            lstCategory.DataContext = challenges;
        }

        private void listItemTapped(object sender, EventArgs e)
        {
             //ListBox l = (ListBox)sender;
            Challenge c = (Challenge)lstCategory.SelectedItem;
            String gameTime = views.GamePage.EXTRA_TIME + "=" + c.time.ToString();
            NavigationService.Navigate(new Uri("/views/GamePage.xaml?" + gameTime, UriKind.RelativeOrAbsolute));
        }
                
    }
}