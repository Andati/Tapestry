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
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using System.IO;

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

        /// <summary>
        /// Save points to file using gamescoreid as filename in isolatedstorage
        /// CSV format : x, y, color, timestamp, shape, opacity
        /// Please use this method with a saved gamescore with a valid ID
        /// </summary>
        public void savePointsToFile(List<Tyap> tyaps)
        {
            //TODO make this method async
            if (this.GameScoreID < 1) { throw new Exception("Invalid gamescoreid"); }
            using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string path = String.Format(StringVals.ISO_STORE_FILENAME_FORMAT, StringVals.ISO_STORE_FOLDER_POINTS, GameScoreID);
                using (IsolatedStorageFileStream isfs = iso.OpenFile(path, System.IO.FileMode.CreateNew))
                {
                    using (StreamWriter sw = new StreamWriter(isfs))
                    {
                        sw.Write(Newtonsoft.Json.JsonConvert.SerializeObject(tyaps));
                        sw.Flush();
                        sw.Close();
                    }
                    isfs.Close();
                }
            }
        }
        public List<Tyap> readPointsFromFile()
        {
            //TODO make this method async
            List<Tyap> tyaps = null;
            using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string path = String.Format(StringVals.ISO_STORE_FILENAME_FORMAT, StringVals.ISO_STORE_FOLDER_POINTS, GameScoreID);
                if (iso.FileExists(path))
                {
                    using (IsolatedStorageFileStream isfs = iso.OpenFile(path, FileMode.Open))
                    {
                        using (StreamReader sr = new StreamReader(isfs))
                        {
                            string text = sr.ReadToEnd();
                            //TODO seriously dangerous, catch exceptions
                            tyaps = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Tyap>>(text);
                        }
                    }
                }
            }
            return tyaps;
        }
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
