using System.Collections.Generic;

namespace Project.Service.Models
{
    public class ShowRooms
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ShowRoom> data { get; set; }
    }

    public class ShowRoom
    {
        public string name { get; set; }
        public string address { get; set; }
        public string area { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string image { get; set; }
    }
}