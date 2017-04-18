using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWTracker.Classes
{
    class SummonSession
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int ID { get; set; }
        
        //[ForeignKey(typeof(MyClass))]
        //public int MyClassID { get; set; }

        public DateTime Date { get; set; }
    }
}
