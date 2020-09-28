using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace aspnetapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbsDemoController : ControllerBase
    {
        // GET: api/absdemo
        [HttpGet]
        public string Get()
        {
            try
            {
                // Get the host details
                var hostname = Dns.GetHostName(); // get container id
                var ipAddress = Dns.GetHostEntry(hostname).AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);

                /*
                 * Runtime defers the Time Zone management to the underlying operating system which in turn causes issue.
                */

                // Get the EST time
                var timeUtc = DateTime.UtcNow;
                //TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                //DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);

                var response = "Hello from Abs demo API!" + Environment.NewLine + "This API is running at " + hostname + " with IP address of " + ipAddress + "." +
                    Environment.NewLine + "The call to this API was made at " + timeUtc + " UTC.";
                return response;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // GET: api/absdemo/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/absdemo
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/absdemo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
