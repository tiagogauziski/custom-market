using System;
using Product.Application.Event.Product.Events;
using Xunit;

namespace Product.Application.Event.UnitTests.Product.Events
{
    public class ProductDeletedEventTests
    {
        [Fact]
        public void GetChanges_ShouldReturnValue()
        {
            // Arrange
            var oldModel = new Models.Product()
            {
                Id = Guid.NewGuid(),
                Name = "Name",
                Description = "Description"
            };

            var systemEvent = new ProductDeletedEvent(oldModel);

            // Act
            var changes = systemEvent.GetChanges();

            // Assert
            Assert.NotEmpty(changes);
        }
    }
}
