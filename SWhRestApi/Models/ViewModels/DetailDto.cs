using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Models.ViewModels
{
    public class DetailDto
    {
        public string id { get; set; }
        public string name { get; set; }

        public DetailDto(string id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public DetailDto(int id)
        {
        }
    }
}