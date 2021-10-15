using System;
using System.Collections.Generic;
using System.Text;

namespace GardenEinfach.Model
{
    public class Post
    {

        public string image { get; set; }

        public List<string> Images { get; set; }
        public string Titel { get; set; }
        public Adress Location { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string PostId { get; set; }

        public string Time { get; set; }
        public MyUser User { get; set; }

        public string Key { get; set; }
    }
}
