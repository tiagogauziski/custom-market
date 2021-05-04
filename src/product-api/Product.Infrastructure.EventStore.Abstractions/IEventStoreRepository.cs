﻿using System.Threading;
using System.Threading.Tasks;
using Product.Models;

namespace Product.Infrastructure.EventStore.Abstractions
{
    /// <summary>
    /// Abstraction of repository to save events into data store.
    /// </summary>
    public interface IEventStoreRepository
    {
        /// <summary>
        /// Save an event into data store.
        /// </summary>
        /// <param name="systemEvent">Event being stored.</param>
        /// <param name="cancellation">Cancellation token.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        Task SaveEventAsync(EventBase systemEvent, CancellationToken cancellation);
    }
}
