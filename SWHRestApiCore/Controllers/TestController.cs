using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using SWhRestApi.Models.ViewModels;
using SWhRestApi.Repository;
using SWHRestApiCore.Models.ViewModels;

namespace SWHRestApiCore.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        String token = "c0CQeN8VQ1E:APA91bGI5brsEXHnPhgA9zhVJANjQ7EnNRWspH1d2vRvdzadu2yEUda9qrg4pETdJT5y11OOWj_P40D8UT68QhpPm-mx0MyOumyB4vHh68nylf3JMubO_Q_agmJKZC2iX_WxJrEUDFX3";
        String serverKey = "AAAAylW3cKg:APA91bHfIdu52ENnGpxY7saK8iUrWs7mXXa5CxIIbOPMjz1MXPqG7gEjBZGRPjWi31KscmRob1E4Q4CfkwS8qkheSnyaYtMKvJRmOLWym0rNhaWgFm48hEtJOt5P7A7vczjdfzR4tRJY";
        String url = "https://fcm.googleapis.com/fcm/send"; 
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "NV")]
        [HttpGet("NV")]
        public bool testNV()
        {
            return true;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "QL")]
        [HttpGet("QL")]
        public bool testQL()
        {
            return true;
        }

        [HttpGet("notification")]
        public async Task<Object> testNotification()
        {
            DetailDto dto = new DetailDto("123","huy");
            
            DataMessage notif = new DataMessage()
            {
                registration_ids = new List<string>() { token},
                data = dto ,
                notification = new Notification("title", "text")
            };

            /*HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "key=" + serverKey);
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("sender", "id=869021479080");
            string data = JsonConvert.SerializeObject(notif);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage respone = await httpClient.PostAsync(url, content);

            if (respone.IsSuccessStatusCode)
            {
                String result = await respone.Content.ReadAsStringAsync();
                return result;
            }

            return respone.ToString();*/

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Headers.TryAddWithoutValidation("Authorization", "key =" +serverKey);
            String jsonMessage = JsonConvert.SerializeObject(notif);
            request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");
            HttpResponseMessage result;
            using (var client = new HttpClient())
            {
                result = await client.SendAsync(request);
                if (result.IsSuccessStatusCode)
                {
                    try
                    {
                        string data = await result.Content.ReadAsStringAsync();
                        SendNotificationResponseDto routes_list = JsonConvert.DeserializeObject<SendNotificationResponseDto>(data);
                        return routes_list;
                    }catch(Exception e)
                    {
                        return e.Message;
                    }
                }

                string results = await result.Content.ReadAsStringAsync();
                return results.ToString();
            }

        }
    }

}
