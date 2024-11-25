using Api.Data;
using Api.Endpoints.Products;

//using Meziantou.Xunit;
using System.Net.Http.Json;

namespace Testcontainers.Tests
{
    public class CreateProductTestClass : ProductEndpointBaseTestClass
    {

        [Fact]
        public async Task CreateProduct_ReturnsCreatedProduct()
        {
            // Arrange
            var command = new CreateProduct.Command { Name = "Test Product", Price = 10.99m };

            // Act
            var response = await _client.PostAsJsonAsync("/products", command);

            // Assert
            response.EnsureSuccessStatusCode();
            var productId = await response.Content.ReadFromJsonAsync<int>();
            Assert.True(productId > 0);
        }

        [Fact]
        public async Task CreateProduct_ReturnsCreatedProduct2()
        {
            // Arrange
            var command = new CreateProduct.Command { Name = "Test Product", Price = 10.99m };

            // Act
            var response = await _client.PostAsJsonAsync("/products", command);

            // Assert
            response.EnsureSuccessStatusCode();
            var productId = await response.Content.ReadFromJsonAsync<int>();
            Assert.True(productId > 0);
        }
    }

    public class GetProductEndpointTestClass : ProductEndpointBaseTestClass
    {
        [Fact]
        public async Task GetProduct_ReturnsProduct()
        {
            // Arrange
            var command = new CreateProduct.Command { Name = "Test Product", Price = 10.99m };
            var createResponse = await _client.PostAsJsonAsync("/products", command);
            var productId = await createResponse.Content.ReadFromJsonAsync<int>();

            // Act
            var response = await _client.GetAsync($"/products/{productId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var product = await response.Content.ReadFromJsonAsync<Product>();
            Assert.NotNull(product);
            Assert.Equal("Test Product", product.Name);
            Assert.Equal(10.99m, product.Price);
        }

        [Fact]
        public async Task GetProduct_ReturnsProduct2()
        {
            // Arrange
            var command = new CreateProduct.Command { Name = "Test Product", Price = 10.99m };
            var createResponse = await _client.PostAsJsonAsync("/products", command);
            var productId = await createResponse.Content.ReadFromJsonAsync<int>();

            // Act
            var response = await _client.GetAsync($"/products/{productId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var product = await response.Content.ReadFromJsonAsync<Product>();
            Assert.NotNull(product);
            Assert.Equal("Test Product", product.Name);
            Assert.Equal(10.99m, product.Price);
        }
    }

    public class UpdateProductEndpointTestClass : ProductEndpointBaseTestClass
    {
        [Fact]
        public async Task UpdateProduct_ReturnsNoContent()
        {
            // Arrange
            var command = new CreateProduct.Command { Name = "Test Product", Price = 10.99m };
            var createResponse = await _client.PostAsJsonAsync("/products", command);
            var productId = await createResponse.Content.ReadFromJsonAsync<int>();

            var updateCommand = new UpdateProduct.Command { Id = productId, Name = "Updated Product", Price = 20.99m };

            // Act
            var response = await _client.PutAsJsonAsync($"/products/{productId}", updateCommand);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }

    public class DeleteProductEndpointTestClass : ProductEndpointBaseTestClass
    {
        [Fact]
        public async Task DeleteProduct_ReturnsNoContent()
        {
            // Arrange
            var command = new CreateProduct.Command { Name = "Test Product", Price = 10.99m };
            var createResponse = await _client.PostAsJsonAsync("/products", command);
            var productId = await createResponse.Content.ReadFromJsonAsync<int>();

            // Act
            var response = await _client.DeleteAsync($"/products/{productId}");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }


}

