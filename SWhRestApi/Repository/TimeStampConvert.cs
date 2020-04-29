using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Repository
{
    public class TimeStampConvert
    {
        public DateTime convertTimeStamp(double timeStamp)
        {
            DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(timeStamp);
            return dateTime;
        }
    }
}