using System.Collections.Generic;
using Order.Application.Order.Commands.Contracts;

namespace Order.Application.Order.Commands
{
    public class CreateOrderCommand
    {
        /// <summary>
        /// Gets or sets the customer details.
        /// </summary>
        public CustomerContract Customer { get; set; }

        /// <summary>
        /// Gets or sets the delivery address.
        /// </summary>
        public AddressContract DeliveryAddress { get; set; }

        /// <summary>
        /// Gets or sets the billing address.
        /// </summary>
        public AddressContract BillingAddress { get; set; }

        /// <summary>
        /// Gets or sets the payment details.
        /// </summary>
        public PaymentDetailContract PaymentDetail { get; set; }

        /// <summary>
        /// Gets or sets the list of items in the order.
        /// </summary>
        public IEnumerable<OrderItemContract> OrderItems { get; set; }

    }
}
