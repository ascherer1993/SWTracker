using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWTracker.Classes
{
    class Summon
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int SummonID { get; set; }

        [ForeignKey(typeof(SummonSession))]
        public int SummonSessionID { get; set; }

        public string Name { get; set; }
        public int Stars { get; set; }

        public DateTime Date { get; set; }
    }
}
