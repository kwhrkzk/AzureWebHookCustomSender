using Microsoft.AspNet.WebHooks;
using Microsoft.AspNet.WebHooks.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AzureWebHookCustomSender.WebHooks
{
    public class MyWebHookManager : WebHookManager
    {
        public MyWebHookManager(IWebHookStore webHookStore, IWebHookSender webHookSender, ILogger logger)
            : base(webHookStore, webHookSender, logger)
        {
        }

        protected override async Task VerifyEchoAsync(WebHook webHook) => await Task.FromResult<object>(null);
    }
}