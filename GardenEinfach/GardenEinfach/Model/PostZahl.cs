using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace GardenEinfach.Model
{
    public class PostZahl
    {
        //[PrimaryKey, AutoIncrement]
        //public int PlZID { get; set; }
        public string PLZ { get; set; }
        public string Ort { get; set; }
        public string Bundesland { get; set; }
    }
}
