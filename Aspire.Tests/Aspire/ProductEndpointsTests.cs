using System.Net.Http.Json;
using Api.Data;
using Api.Endpoints.Products;
using Aspire.Tests;
using Xunit.Abstractions;

namespace Aspire.Tests;


public class CreateProductTests : ProductEndpointsTestsBase
{

    [Fact]
    public async Task CreateProduct_ReturnsCreatedProduct()
    {
        var command = new CreateProduct.Command { Name = "Test Product", Price = 10.99m };
        var response = await _client.PostAsJsonAsync("/products", command);
        response.EnsureSuccessStatusCode();
        var productId = await response.Content.ReadFromJsonAsync<int>();
        Assert.True(productId > 0);
    }
}

public class GetProductTests : ProductEndpointsTestsBase
{

    [Fact]
    public async Task GetProduct_ReturnsProduct()
    {
        var command = new CreateProduct.Command { Name = "Test Product", Price = 10.99m };
        var createResponse = await _client.PostAsJsonAsync("/products", command);
        var productId = await createResponse.Content.ReadFromJsonAsync<int>();

        var response = await _client.GetAsync($"/products/{productId}");
        response.EnsureSuccessStatusCode();
        var product = await response.Content.ReadFromJsonAsync<Product>();
        Assert.NotNull(product);
        Assert.Equal("Test Product", product.Name);
        Assert.Equal(10.99m, product.Price);
    }
}

public class UpdateProductTests : ProductEndpointsTestsBase
{

    [Fact]
    public async Task UpdateProduct_ReturnsNoContent()
    {
        var command = new CreateProduct.Command { Name = "Test Product", Price = 10.99m };
        var createResponse = await _client.PostAsJsonAsync("/products", command);
        var productId = await createResponse.Content.ReadFromJsonAsync<int>();

        var updateCommand = new UpdateProduct.Command { Id = productId, Name = "Updated Product", Price = 20.99m };
        var response = await _client.PutAsJsonAsync($"/products/{productId}", updateCommand);
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}

public class DeleteProductTests : ProductEndpointsTestsBase
{

    [Fact]
    public async Task DeleteProduct_ReturnsNoContent()
    {
        var command = new CreateProduct.Command { Name = "Test Product", Price = 10.99m };
        var createResponse = await _client.PostAsJsonAsync("/products", command);
        var productId = await createResponse.Content.ReadFromJsonAsync<int>();

        var response = await _client.DeleteAsync($"/products/{productId}");
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}
