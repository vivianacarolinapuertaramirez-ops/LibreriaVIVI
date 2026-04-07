using System.Collections.Generic;
using System.Linq;
using ViviLibreria.models;

namespace ViviLibreria.services
{
    public class LibroService
    {
        private List<Libro> _libros = new List<Libro>();

        public void Registrar(Libro libro) => _libros.Add(libro);

        public List<Libro> ObtenerTodos() => _libros;

        public Libro? BuscarPorId(string id) => _libros.FirstOrDefault(l => l.Id == id);

        public bool Eliminar(string id)
        {
            var libro = BuscarPorId(id);
            if (libro != null && libro.Disponible)
            {
                _libros.Remove(libro);
                return true;
            }
            return false;
        }
    }
}