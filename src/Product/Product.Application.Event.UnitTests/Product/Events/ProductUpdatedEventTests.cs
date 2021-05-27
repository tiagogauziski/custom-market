using System;
using Product.Application.Event.Product.Events;
using Xunit;

namespace Product.Application.Event.UnitTests.Product.Events
{
    public class ProductUpdatedEventTests
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

            var newModel = new Models.Product()
            {
                Id = oldModel.Id,
                Name = "DifferentName",
                Description = "Test Description"
            };

            var systemEvent = new ProductUpdatedEvent(oldModel, newModel);

            // Act
            var changes = systemEvent.GetChanges();

            // Assert
            Assert.NotEmpty(changes);
        }
    }
}
