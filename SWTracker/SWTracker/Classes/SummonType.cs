using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWTracker.Classes
{
    class SummonType
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int SummonTypeID { get; set; }

        public string Type { get; set; }
    }
}
