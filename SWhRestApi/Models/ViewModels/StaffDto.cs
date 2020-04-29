using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Models.ViewModels
{
    public class StaffDto
    {

        public string id { get; set; }
        public string name { get; set; }
        public string gmail { get; set; }
        public DetailDto position { get; set; }
        public StoreDto store { get; set; }
        public DetailDto status { get; set; }
        public string picUrl { get; set; }
        public string accessToken { get; set; }

        public StaffDto(string id, string name, string gmail, DetailDto position, StoreDto store, DetailDto status, string picUrl, string accessToken)
        {
            this.id = id;
            this.name = name;
            this.gmail = gmail;
            this.position = position;
            this.store = store;
            this.status = status;
            this.picUrl = picUrl;
            this.accessToken = accessToken;
        }

        public StaffDto()
        {
        }
    }
}