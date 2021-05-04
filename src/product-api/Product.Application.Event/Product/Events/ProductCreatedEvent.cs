﻿using System;
using MediatR;
using Newtonsoft.Json;
using Product.Models;

namespace Product.Application.Event.Product.Events
{
    /// <summary>
    /// Event payload when a new product is created.
    /// </summary>
    public class ProductCreatedEvent :
        EventBase,
        INotification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCreatedEvent"/> class.
        /// </summary>
        public ProductCreatedEvent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCreatedEvent"/> class.
        /// </summary>
        /// <param name="product">Product model.</param>
        public ProductCreatedEvent(Models.Product product)
        {
            New = product;
        }

        /// <summary>
        /// Gets or sets the created product.
        /// </summary>
        public Models.Product New { get; set; }

        /// <inheritdoc />
        public override Guid ObjectId { get => New.Id; }

        /// <inheritdoc />
        public override string GetChanges()
        {
            return JsonConvert.SerializeObject(New);
        }
    }
}
