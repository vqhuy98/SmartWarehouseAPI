using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWHRestApiCore.Models.ViewModels
{
    public class DataMessage
    {
        public List<string> registration_ids { get; set; }
        public Notification notification { get; set; }
        public object data { get; set; }

        public DataMessage(List<string> registration_ids, Notification nofitication, object data)
        {
            this.registration_ids = registration_ids;
            this.notification = nofitication;
            this.data = data;
        }

        public DataMessage()
        {
        }
    }
}
