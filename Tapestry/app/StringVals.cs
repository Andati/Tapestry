using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Tapestry.app
{
    public class StringVals
    {
        public static readonly string UNLIMITED_TIME = "tapathon";
        public static readonly string TIME_UNIT = "seconds";
        public static readonly string IMG_TIMED = "/images/timed.png";
        public static readonly string IMG_UNTIMED = "/images/untimed.png";
        public static readonly string ISO_STORE_CONNECTION_STRING = @"isostore:/GamesScore.sdf";
        public static readonly short[] TIME_CHALLENGES = { 60, 30, 20, 10,5, 0 };
        public static readonly string ABOUT_TEXT = "media/about.txt";
    }
}
