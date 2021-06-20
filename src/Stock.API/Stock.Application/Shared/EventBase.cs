using System;

namespace Stock.Application.Shared
{
    /// <summary>
    /// Defines base class implementation for an event.
    /// </summary>
    public abstract class EventBase
    {
        /// <summary>
        /// Gets the event name.
        /// </summary>
        public virtual string EventName => GetType().Name;

        /// <summary>
        /// Gets or sets the time that the event occured.
        /// </summary>
        public DateTimeOffset DateTimeUtc { get; set; } = DateTimeOffset.UtcNow;
    }
}
