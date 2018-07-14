using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace BooksStore.Hubs
{
    public class BooksCounterHub : Hub
    {
        public void Hello(string msg)
        {
            Clients.All.hello(msg);
        }
    }
}