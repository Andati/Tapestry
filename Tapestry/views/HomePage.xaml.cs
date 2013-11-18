using System;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Tapestry.app;
using System.Windows;
using System.Windows.Resources;
using System.IO;
using System.Threading;

namespace Tapestry.views
{
    public partial class HomePage : PhoneApplicationPage
    {
        private static Challenge[] challenges;

        public HomePage()
        {
            InitializeComponent();
            int len = StringVals.TIME_CHALLENGES.Length;
            challenges = new Challenge[len];
            for (int i = 0; i < len; i++)
            {
                challenges[i] = new Challenge { time = StringVals.TIME_CHALLENGES[i] };
            }
            lstCategory.DataContext = challenges;
            //TODO Execute in another thread
            loadAbout();
        }

        private void loadAbout()
        {
            StreamResourceInfo resourceStream = null;
            string val = string.Empty;
            try
            {
                resourceStream = Application.GetResourceStream(new Uri(StringVals.ABOUT_TEXT, UriKind.Relative));
                if (resourceStream != null)
                {
                    using (Stream myFileStream = resourceStream.Stream)
                    {
                        if (myFileStream.CanRead)
                        {
                            StreamReader myStreamReader = new StreamReader(myFileStream);
                            val = myStreamReader.ReadToEnd();
                            myStreamReader.Close();
                        }
                        myFileStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
#endif
            }
            txtAbout.Text = val;
        }

        private void listItemTapped(object sender, EventArgs e)
        {
            //ListBox l = (ListBox)sender;
            Challenge c = (Challenge)lstCategory.SelectedItem;
            String gameTime = views.GamePage.EXTRA_TIME + "=" + c.time.ToString();
            NavigationService.Navigate(new Uri("/views/GamePage.xaml?" + gameTime, UriKind.RelativeOrAbsolute));
        }

        private void ShowMyStatics(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/views/StatsPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}