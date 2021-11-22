using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListOfExecLatestLatLng
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string date { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class ExecLatestLatLngLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ExecLatestLatLngList> data { get; set; }
    }

    public class ExecLatestLatLngList
    {
        public string ExecutiveName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}