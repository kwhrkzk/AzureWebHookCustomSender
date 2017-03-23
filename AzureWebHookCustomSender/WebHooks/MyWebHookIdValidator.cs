using Microsoft.AspNet.WebHooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AzureWebHookCustomSender.WebHooks
{
    public class MyWebHookIdValidator : IWebHookIdValidator
    {
        public async Task ValidateIdAsync(HttpRequestMessage request, WebHook webHook) => await Task.FromResult<object>(null);
    }
}