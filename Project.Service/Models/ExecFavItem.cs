using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofExecFavItem
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public int type { get; set; }


        [Required]
        public string ClientSecret { get; set; }

    }

        public class ExecFavItems
        {
            public bool result { get; set; }
            public string message { get; set; }
            public string servertime { get; set; }
            public List<ExecFavItem> data { get; set; }
        }

        public class ExecFavItem
        {
            public int Value { get; set; }
        }
    }
