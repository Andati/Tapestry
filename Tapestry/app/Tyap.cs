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
    public class Tyap
    {
        public long _timestamp;
        public Color _color;
        public Point _position;

        public long timestamp { get { return _timestamp; } set { _timestamp = value; } }
        //TODO save as hex or bytes
        public Color color { get { return _color; } set { _color = value; } }
        public Point position { get { return _position; } set { _position = value; } }
    }
}
