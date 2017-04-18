using System;
using SQLite;

namespace SWTracker
{
    public class MyClass
    {
        public MyClass()
        { }

        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public int testNum { get; set; }
    
    }
}

