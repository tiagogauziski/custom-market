using System;
using JsonDiffPatchDotNet;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Product.Models;

namespace Product.Application.Event.Product.Events
{
    /// <summary>
    /// Event payload when a new product is created.
    /// </summary>
    public class ProductDeletedEvent :
        EventBase,
        INotification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDeletedEvent"/> class.
        /// </summary>
        public ProductDeletedEvent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDeletedEvent"/> class.
        /// </summary>
        /// <param name="product">Product model.</param>
        public ProductDeletedEvent(Models.Product product)
        {
            Old = product;
        }

        /// <summary>
        /// Gets or sets the created product.
        /// </summary>
        public Models.Product Old { get; set; }

        /// <inheritdoc />
        public override Guid ObjectId { get => Old.Id; }

        /// <inheritdoc />
        public override string GetChanges()
        {
            JObject left = JObject.FromObject(Old);
            JObject right = new JObject();
            JToken patch = new JsonDiffPatch().Diff(left, right);

            return JsonConvert.SerializeObject(patch);
        }
    }
}
