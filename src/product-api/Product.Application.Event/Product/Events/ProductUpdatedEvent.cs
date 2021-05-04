using System;
using JsonDiffPatchDotNet;
using MediatR;
using Newtonsoft.Json;
using Product.Models;

namespace Product.Application.Event.Product.Events
{
    /// <summary>
    /// Event payload when a new product is created.
    /// </summary>
    public class ProductUpdatedEvent :
        EventBase,
        INotification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductUpdatedEvent"/> class.
        /// </summary>
        public ProductUpdatedEvent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductUpdatedEvent"/> class.
        /// </summary>
        /// <param name="oldProduct">Model before changes.</param>
        /// <param name="newProduct">Model after changes.</param>
        public ProductUpdatedEvent(Models.Product oldProduct, Models.Product newProduct)
        {
            New = oldProduct;
            Old = newProduct;
        }

        /// <summary>
        /// Gets or sets the produc before changes.
        /// </summary>
        public Models.Product Old { get; set; }

        /// <summary>
        /// Gets or sets the updated product.
        /// </summary>
        public Models.Product New { get; set; }

        /// <inheritdoc />
        public override Guid ObjectId { get => New.Id; }

        /// <inheritdoc />
        public override string GetChanges()
        {
            return new JsonDiffPatch().Diff(JsonConvert.SerializeObject(Old), JsonConvert.SerializeObject(New));
        }
    }
}
