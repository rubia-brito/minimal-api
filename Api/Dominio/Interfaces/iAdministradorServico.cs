using MinimalApi.Dominio.Entidades;
using MinimalApi.DTOs;

namespace MinimalApi.Dominio.Interfaces
{
    public interface IAdministradorServico
    {
        Administrador? Login(LoginDTO loginDTO);

        List<Administrador> Todos();

        List<Administrador> Todos(int v);

        void Incluir(Administrador adm);

        int BuscaPorId(int id);
    }
}