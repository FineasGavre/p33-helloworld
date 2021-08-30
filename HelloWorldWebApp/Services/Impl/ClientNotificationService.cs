// <copyright file="ClientNotificationService.cs" company="PRINCIPAL33">
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
    /// Service for interacting with Client Notification Hub.
    /// </summary>
    public class ClientNotificationService : IClientNotificationService
    {
        private readonly IHubContext<ClientNotificationHub> clientNotificationHub;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientNotificationService"/> class.
        /// </summary>
        /// <param name="clientNotificationHub">DI injected Client Notification Hub.</param>
        public ClientNotificationService(IHubContext<ClientNotificationHub> clientNotificationHub)
        {
            this.clientNotificationHub = clientNotificationHub;
        }

        /// <inheritdoc/>
        public async Task SendEntityAddedNotification(string entityType, string entityId)
        {
            await clientNotificationHub.Clients.All.SendAsync("EntityAdded", entityType, entityId);
        }

        /// <inheritdoc/>
        public async Task SendEntityRemovedNotification(string entityType, string entityId)
        {
            await clientNotificationHub.Clients.All.SendAsync("EntityRemoved", entityType, entityId);
        }

        /// <inheritdoc/>
        public async Task SendEntityUpdatedNotification(string entityType, string entityId)
        {
            await clientNotificationHub.Clients.All.SendAsync("EntityUpdated", entityType, entityId);
        }
    }
}
