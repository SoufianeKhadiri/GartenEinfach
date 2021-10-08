using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace GardenEinfach.Model
{
    public class MyImage
    {
        public ImageSource Source { get; set; }
        public string FilePath { get; set; }

        public Stream ImageStream { get; set; }
    }
}
