using MinimalApi.Dominio.Entidades;
using MinimalApi.DTOs;

namespace MinimalApi.Dominio.Interfaces
{
    public interface IAdministradorServico
  {
    Administrador? Login(LoginDTO loginDTO);

    List<Administrador> Todos();

    void Incluir(Administrador adm);

    Administrador? BuscaPorId(int id);
    }
}