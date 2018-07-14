using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Hubs
{
    public sealed class HubAccessor
    {
        private static HubAccessor instance = null;
        private static readonly object objLock = new object();
        private IHubContext context;
        public static HubAccessor Instance {
            get
            {
                lock(objLock)
                {
                    if (instance == null)
                    {
                        instance = new HubAccessor(GlobalHost.ConnectionManager.GetHubContext<BooksCounterHub>());
                    }
                }
                return instance;
            }
        }

        private HubAccessor(IHubContext context)
        {
            this.context = context;
        }

        public void UpdateBookCounter(int id, int count)
        {
            context.Clients.All.updateBookCounter(id, count);
        }
    }
}
