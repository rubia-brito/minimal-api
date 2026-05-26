using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinimalApi.Dominio.Entidades;

namespace Tests.Domain.Entidades;

[TestClass]
public sealed class VeiculoTests
{
    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        // Arrange
        var veiculo = new Veiculo();

        // Act
        veiculo.Id = 1;
        veiculo.Nome = "Fiesta 2.0";
        veiculo.Marca = "Ford";
        veiculo.Ano = 2013;

        // Assert
        Assert.AreEqual(1, veiculo.Id);
        Assert.AreEqual("Fiesta 2.0", veiculo.Nome);
        Assert.AreEqual("Ford", veiculo.Marca);
        Assert.AreEqual(2013, veiculo.Ano);
    }
}