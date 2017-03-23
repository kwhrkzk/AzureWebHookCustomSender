using Microsoft.AspNet.WebHooks;
using Microsoft.AspNet.WebHooks.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AzureWebHookCustomSender.Controllers
{
    public class WebhookController : ApiController
    {
        public async Task<IHttpActionResult> Post(dynamic req)
        {
            await this.NotifyAsync("*", new { url = req.url });
            return Ok();
        }
    }
}
