using Microsoft.AspNet.WebHooks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AzureWebHookCustomSender.WebHooks
{
    /// <summary>
    /// Use a IWebHookFilterProvider implementation to describe the events that users can 
    /// subscribe to. A wildcard is always registered meaning that users can register for 
    /// "all events". It is possible to have 0, 1, or more IWebHookFilterProvider 
    /// implementations.
    /// </summary>
    public class MyWebHookFilterProvider : IWebHookFilterProvider
    {
        private readonly Collection<WebHookFilter> filters = new Collection<WebHookFilter>
        {
            new WebHookFilter { Name = "*", Description = "wildcard"}
        };

        public Task<Collection<WebHookFilter>> GetFiltersAsync()
        {
            return Task.FromResult(this.filters);
        }
    }
}