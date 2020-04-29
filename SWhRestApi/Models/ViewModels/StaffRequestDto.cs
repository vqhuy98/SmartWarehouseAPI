using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Models.ViewModels
{
    public class StaffRequestDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string gmail { get; set; }
        public string positionId { get; set; }
        public String storeId { get; set; }
        public String statusId { get; set; }
        public string picUrl { get; set; }
        public string accessToken { get; set; }

        public StaffRequestDto(int id, string name, string gmail, String position, String store, String status, string picUrl, string accessToken)
        {
            this.id = id;
            this.name = name;
            this.gmail = gmail;
            this.positionId = position;
            this.storeId = store;
            this.statusId = status;
            this.picUrl = picUrl;
            this.accessToken = accessToken;
        }

        public StaffRequestDto()
        {
        }
    }
}