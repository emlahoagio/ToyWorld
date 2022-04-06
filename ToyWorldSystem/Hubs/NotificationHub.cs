using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToyWorldSystem.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly IDictionary<string, UserConnection> _connections;

        public NotificationHub(IDictionary<string, UserConnection> connections)
        {
            _connections = connections;
        }


    }
}
