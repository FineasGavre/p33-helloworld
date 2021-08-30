// <copyright file="ClientNotificationHub.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace HelloWorldWebApp.Hubs
{
    /// <summary>
    /// SignalR Hub for Client Notifications.
    /// </summary>
    [Authorize]
    public class ClientNotificationHub : Hub
    {
    }
}
