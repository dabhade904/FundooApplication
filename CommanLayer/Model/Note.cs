using System;
using System.Collections.Generic;
using System.Text;

namespace CommanLayer.Model
{
    public class Note
    {
        public string title { get; set; }
        public string discription { get; set; }
        public DateTime reminder { get; set; }
        public string color { get; set; }
        public string img { get; set; }
        public bool archive { get; set; }
        public bool pin { get; set; }
        public bool trash { get; set; }
        public DateTime time_created { get; set; }
        public DateTime time_edited { get; set; }
    }
}
