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
    public class Challenge
    {
        private short _time;
        public short time { get { return _time; } set { _time = value; } }
        public string str { get { return ToString(); } }
        public string image { get { return (_time < 1) ? StringVals.IMG_UNTIMED : StringVals.IMG_TIMED; } }
        public override string ToString()
        {
            return (_time < 1) ?
                StringVals.UNLIMITED_TIME :
                _time.ToString() + " " + StringVals.TIME_UNIT;
        }
    }
}
