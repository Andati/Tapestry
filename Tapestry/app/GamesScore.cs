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
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace Tapestry.app
{
    [Table]
    public class GamesScore
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false, AutoSync = AutoSync.OnInsert)]

        public int GameScoreID { get; set; }

        [Column(CanBeNull = false)]
        public int Score { get; set; }

        [Column(CanBeNull = false)]
        public int Time { get; set; }

        [Column(CanBeNull = false)]
        public bool isTimed { get; set; }
    }

    public class GameScoreDataContext : DataContext
    {
        public GameScoreDataContext(string connectionString) : base(connectionString) { }

        public Table<GamesScore> GameScores
        {
            get
            {
                return this.GetTable<GamesScore>();
            }
        }
    }
}
