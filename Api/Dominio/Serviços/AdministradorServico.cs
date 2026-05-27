using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.DTOs;
using MinimalApi.Infraestrutura.Db;

namespace MinimalApi.Dominio.Servicos
{
    public class AdministradorServico : IAdministradorServico
    {
        private readonly DbContexto _contexto;

        public AdministradorServico(DbContexto contexto)
        {
            _contexto = contexto;
        }

        public Administrador? BuscaPorId(int id)
        {
         return _contexto.Administradores
        .FirstOrDefault(a => a.Id == id);
        }

        public void Incluir(Administrador adm)
        {
            _contexto.Administradores.Add(adm);
            _contexto.SaveChanges();
        }

        public Administrador? Login(LoginDTO loginDTO)
        {
            var administrador = _contexto.Administradores
                .FirstOrDefault(a =>
                    a.Email.ToLower().Trim() == loginDTO.Email.ToLower().Trim()
                    &&
                    a.Senha.Trim() == loginDTO.Senha.Trim()
                );

            return administrador;
        }

       
        public List<Administrador> Todos()
        {
            return _contexto.Administradores.ToList();
        }
    }
}