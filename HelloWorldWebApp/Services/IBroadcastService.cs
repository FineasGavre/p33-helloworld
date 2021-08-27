// <copyright file="IBroadcastService.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System.Threading.Tasks;

namespace HelloWorldWebApp.Services
{
    /// <summary>
    /// Interface for the Broadcast Service.
    /// </summary>
    public interface IBroadcastService
    {
        /// <summary>
        /// Sends a notification to all clients when an entity is added.
        /// </summary>
        /// <param name="entityType">Type of entity.</param>
        /// <param name="entityId">Entity id.</param>
        /// <returns>Completed Task.</returns>
        Task SendEntityAddedNotification(string entityType, string entityId);

        /// <summary>
        /// Sends a notification to all clients when an entity is removed.
        /// </summary>
        /// <param name="entityType">Type of entity.</param>
        /// <param name="entityId">Entity id.</param>
        /// <returns>Completed Task.</returns>
        Task SendEntityRemovedNotification(string entityType, string entityId);

        /// <summary>
        /// Sends a notification to all clients when an entity is removed.
        /// </summary>
        /// <param name="entityType">Type of entity.</param>
        /// <param name="entityId">Entity id.</param>
        /// <returns>Completed Task.</returns>
        Task SendEntityUpdatedNotification(string entityType, string entityId);
    }
}