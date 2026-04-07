using System.Collections.Generic;
using System.Linq;
using ViviLibreria.models;

namespace ViviLibreria.services
{
    public class UsuarioService
    {
        private List<Usuario> _usuarios = new List<Usuario>();

        public void Registrar(Usuario usuario) => _usuarios.Add(usuario);

        public List<Usuario> ObtenerTodos() => _usuarios;

        public Usuario? BuscarPorId(string id) => _usuarios.FirstOrDefault(u => u.Id == id);
    }
}