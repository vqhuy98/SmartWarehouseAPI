using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWHRestApiCore.Models.ViewModels
{
    public class StoreRequestDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public string statusId { get; set; }

        public StoreRequestDto()
        {
        }

        public StoreRequestDto(string id, string name, string location, string statusId)
        {
            this.id = id;
            this.name = name;
            this.location = location;
            this.statusId = statusId;
        }
    }
}
