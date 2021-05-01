using System;

namespace Product.Application.Event.Common
{
    /// <summary>
    /// Defines base class implementation for an event.
    /// </summary>
    public abstract class EventBase
    {
        /// <summary>
        /// Gets or sets the time that the event occured.
        /// </summary>
        public DateTimeOffset DateTimeUtc { get; set; }

        /// <summary>
        /// Calculate event changes.
        /// </summary>
        /// <returns>JSON string containing the changes.</returns>
        public abstract string GetChanges();
    }
}
