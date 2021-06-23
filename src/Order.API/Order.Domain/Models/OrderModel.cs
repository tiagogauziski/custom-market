using System.Collections.Generic;

namespace Order.Domain.Models
{
    /// <summary>
    /// Model that represents the order in the system.
    /// </summary>
    public class OrderModel
    {
        /// <summary>
        /// Gets or sets the customer details.
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets the delivery address.
        /// </summary>
        public Address DeliveryAddress { get; set; }

        /// <summary>
        /// Gets or sets the billing address.
        /// </summary>
        public Address BillingAddress { get; set; }

        /// <summary>
        /// Gets or sets the payment details.
        /// </summary>
        public PaymentDetail PaymentDetail { get; set; }

        /// <summary>
        /// Gets or sets the list of items in the order.
        /// </summary>
        public IEnumerable<OrderItem> OrderItems { get; set; }

        /// <summary>
        /// Gets or sets the order status.
        /// </summary>
        public OrderStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the shipping status.
        /// </summary>
        public ShippingStatus ShippingStatus { get; set; }
    }
}
