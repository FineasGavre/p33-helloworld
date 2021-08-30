// <copyright file="BroadcastService.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldWebApp.Data.Models;
using HelloWorldWebApp.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace HelloWorldWebApp.Services.Impl
{
    /// <summary>
    /// Service for interacting with BroadcastHub.
    /// </summary>
    public class BroadcastService : IBroadcastService
    {
        private readonly IHubContext<ClientNotificationHub> broadcastHub;

        /// <summary>
        /// Initializes a new instance of the <see cref="BroadcastService"/> class.
        /// </summary>
        /// <param name="broadcastHub">DI injected BroadcastHub.</param>
        public BroadcastService(IHubContext<ClientNotificationHub> broadcastHub)
        {
            this.broadcastHub = broadcastHub;
        }

        /// <inheritdoc/>
        public async Task SendEntityAddedNotification(string entityType, string entityId)
        {
            await broadcastHub.Clients.All.SendAsync("EntityAdded", entityType, entityId);
        }

        /// <inheritdoc/>
        public async Task SendEntityRemovedNotification(string entityType, string entityId)
        {
            await broadcastHub.Clients.All.SendAsync("EntityRemoved", entityType, entityId);
        }

        /// <inheritdoc/>
        public async Task SendEntityUpdatedNotification(string entityType, string entityId)
        {
            await broadcastHub.Clients.All.SendAsync("EntityUpdated", entityType, entityId);
        }
    }
}
