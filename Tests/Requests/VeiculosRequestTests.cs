using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Tests.Requests;

[TestClass]
public class VeiculosRequestTests
{
    [TestMethod]
    public async Task Testar_Get_Veiculos()
    {
        // Arrange
        await using var application = new WebApplicationFactory<Program>();

        var client = application.CreateClient();

        // Act
        var response = await client.GetAsync("/veiculos");

        // Assert
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}