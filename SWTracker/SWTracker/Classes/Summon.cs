using SQLite;
using SQLiteNetExtensions.Attributes;
using SWTracker.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWTracker.Classes
{
    public class Summon
    {
        DBConnection db = new DBConnection();


        [PrimaryKey, AutoIncrement, Column("_id")]
        public int SummonID { get; set; }

        [ForeignKey(typeof(SummonSession))]
        public int SummonSessionID { get; set; }

        public string Name { get; set; }
        public int Stars { get; set; }

        public DateTime Date { get; set; }

        //public SummonSession SummonSession {
        //    get {
        //        return db.getSummonSession("", SummonSessionID).Result;
        //    }
        //    set { }
        //}
    }
}
