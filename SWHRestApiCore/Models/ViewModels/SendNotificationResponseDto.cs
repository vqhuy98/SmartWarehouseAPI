using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWHRestApiCore.Models.ViewModels
{
    public class SendNotificationResponseDto
    {
        public long multicast_id { get; set; }
        public int success { get; set; }
        public int failure { get; set; }
        public int canonical_ids { get; set; }
        public Result[] results { get; set; }

        public SendNotificationResponseDto(long multicast_id, int success, int failure, int canonical_ids, Result[] results)
        {
            this.multicast_id = multicast_id;
            this.success = success;
            this.failure = failure;
            this.canonical_ids = canonical_ids;
            this.results = results;
        }

        public SendNotificationResponseDto()
        {
        }
    }

    public class Result
    {
        public string message_id { get; set; }
        public string error { get; set; }

    }
}
