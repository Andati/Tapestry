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
using Tapestry.app;

namespace Tapestry.views
{
    public class StatsView
    {
        private List<GamesScore> _scores;
        private Challenge _time;
        public List<GamesScore> scores { get { return _scores; } set { _scores = value; } }
        public Challenge time { get { return _time; } set { _time = value; } }
        public string title { get { return _time.str; } }
        public bool isEmpty { get { return _scores.Count < 1; } }
    }

    public partial class StatsPage : PhoneApplicationPage
    {
        public StatsPage()
        {
            InitializeComponent();
            GameScoreDataContext db = new GameScoreDataContext(StringVals.ISO_STORE_CONNECTION_STRING);
            List<StatsView> stats = new List<StatsView>();
            IEnumerable<short> challenges = StringVals.TIME_CHALLENGES.Reverse();
            foreach (short i in challenges)
            {
                var scores = (from score in db.GameScores where (score.Time == i) orderby score.Score descending select score);
                StatsView sv = new StatsView { scores = scores.Take(10).ToList(), time = new Challenge { time = i } };
                stats.Add(sv);
            }
            pvtContainer.DataContext = stats;

            //List<GamesScore> seconds5 = new List<GamesScore>();
            //List<GamesScore> seconds10 = new List<GamesScore>();
            //List<GamesScore> seconds20 = new List<GamesScore>();
            //List<GamesScore> seconds30 = new List<GamesScore>();
            //List<GamesScore> seconds40 = new List<GamesScore>();
            //List<GamesScore> seconds50 = new List<GamesScore>();
            //List<GamesScore> tapathon = new List<GamesScore>();

            //var scores = (from score in db.GameScores where (score.Time == 5 && score.isTimed) orderby score.Score descending select score);
            //seconds5 = scores.Take(10).ToList();

            //if (seconds5.Count != 0)
            //{
            //    noStats5.Visibility = System.Windows.Visibility.Collapsed;
            //}
            //lst5sec.DataContext = seconds5;

            //scores = from score in db.GameScores
            //         where (score.Time == 10 && score.isTimed)
            //         orderby score.Score descending
            //         select score;
            //seconds10 = scores.Take(10).ToList();
            //if (seconds10.Count != 0)
            //{
            //    noStats10.Visibility = System.Windows.Visibility.Collapsed;
            //}
            //lst10sec.DataContext = seconds10;

            //scores = from score in db.GameScores
            //         where (score.Time == 20 && score.isTimed)
            //         orderby score.Score ascending
            //         select score;
            //seconds20 = scores.Take(10).ToList();
            //if (seconds20.Count != 0)
            //{
            //    noStats20.Visibility = System.Windows.Visibility.Collapsed;
            //}
            //lst20sec.DataContext = seconds20;

            //scores = from score in db.GameScores
            //         where (score.Time == 30 && score.isTimed)
            //         orderby score.Score ascending
            //         select score;
            //seconds30 = scores.Take(10).ToList();
            //if (seconds30.Count != 0)
            //{
            //    noStats30.Visibility = System.Windows.Visibility.Collapsed;
            //}
            //lst30sec.DataContext = seconds30;

            //scores = from score in db.GameScores
            //         where (score.Time == 40 && score.isTimed)
            //         orderby score.Score descending
            //         select score;
            //seconds40 = scores.Take(10).ToList();
            //if (seconds40.Count != 0)
            //{
            //    noStats40.Visibility = System.Windows.Visibility.Collapsed;
            //}
            //lst40sec.DataContext = seconds40;

            //scores = from score in db.GameScores
            //         where (score.Time == 50 && score.isTimed)
            //         orderby score.Score descending
            //         select score;
            //seconds50 = scores.Take(10).ToList();
            //if (seconds50.Count != 0)
            //{
            //    noStats50.Visibility = System.Windows.Visibility.Collapsed;
            //}
            //lst50sec.DataContext = seconds50;

            //scores = from score in db.GameScores
            //         where (!score.isTimed)
            //         orderby score.Score descending
            //         select score;
            //tapathon = scores.Take(10).ToList();
            //if (tapathon.Count != 0)
            //{
            //    noStattapathon.Visibility = System.Windows.Visibility.Collapsed;
            //}
            //lsttapathon.DataContext = tapathon;
        }
    }
}