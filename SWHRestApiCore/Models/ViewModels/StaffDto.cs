using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Models.ViewModels
{
    public class StaffDto
    {

        public long id { get; set; }
        public string name { get; set; }
        public string gmail { get; set; }
        public DetailDto position { get; set; }
        public StoreDto store { get; set; }
        public DetailDto status { get; set; }
        public string picUrl { get; set; }
        public string authToken { get; set; }
        public string deviceToken { get; set; }

        public StaffDto(long id, string name, string gmail, DetailDto position, StoreDto store, DetailDto status, string picUrl, string authToken, string deviceToken)
        {
            this.id = id;
            this.name = name;
            this.gmail = gmail;
            this.position = position;
            this.store = store;
            this.status = status;
            this.picUrl = picUrl;
            this.authToken = authToken;
            this.deviceToken = deviceToken;
        }

        public StaffDto()
        {
        }
    }
}