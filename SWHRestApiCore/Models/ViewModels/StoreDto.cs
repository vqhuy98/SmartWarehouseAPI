using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Models.ViewModels
{
    public class StoreDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public DetailDto status { get; set; }

        public StoreDto()
        {
        }

        public StoreDto(string id, string name, string location, DetailDto status)
        {
            this.id = id;
            this.name = name;
            this.location = location;
            this.status = status;
        }
    }
}