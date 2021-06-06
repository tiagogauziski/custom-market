using System;
using Product.Application.Event.Product.Events;
using Xunit;

namespace Product.Application.Event.UnitTests.Product.Events
{
    public class ProductCreatedEventTests
    {
        [Fact]
        public void GetChanges_ShouldReturnValue()
        {
            // Arrange
            var model = new Models.Product()
            {
                Id = Guid.NewGuid(),
                Name = "Name"
            };

            var systemEvent = new ProductCreatedEvent(model);

            // Act
            string changes = systemEvent.GetChanges();

            // Assert
            Assert.NotEmpty(changes);
        }
    }
}
