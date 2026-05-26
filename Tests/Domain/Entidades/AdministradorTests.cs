using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinimalApi.Dominio.Entidades;

namespace Tests.Dominio.Entidades;

[TestClass]
public sealed class AdministradorTests
{
    [TestMethod]
    public void Deve_Preencher_Propriedades_Do_Administrador()
    {
        // Arrange
        var administrador = new Administrador
        {
            Id = 1,
            Email = "teste@teste.com",
            Senha = "teste",
            Perfil = "Adm"
        };

        // Assert
        Assert.AreEqual(1, administrador.Id);
        Assert.AreEqual("teste@teste.com", administrador.Email);
        Assert.AreEqual("teste", administrador.Senha);
        Assert.AreEqual("Adm", administrador.Perfil);
    }
}