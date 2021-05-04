using System;

namespace Product.Models
{
    /// <summary>
    /// Defines base class implementation for an event.
    /// </summary>
    public abstract class EventBase
    {
        /// <summary>
        /// Gets the event name.
        /// </summary>
        public string EventName { get; }

        /// <summary>
        /// Gets or sets the time that the event occured.
        /// </summary>
        public DateTimeOffset DateTimeUtc { get; set; }

        /// <summary>
        /// Gets the parent entity id being changed.
        /// </summary>
        public abstract Guid ObjectId { get; }

        /// <summary>
        /// Calculate event changes.
        /// </summary>
        /// <returns>JSON string containing the changes.</returns>
        public abstract string GetChanges();
    }
}
