using System;
using System.Collections.Generic;
using System.Linq;
using ViviLibreria.models;

namespace ViviLibreria.services
{
    public class PrestamoService
    {
        private List<Prestamo> _prestamos = new List<Prestamo>();

        public bool GenerarPrestamo(string idUsuario, string idLibro, LibroService libroServ, UsuarioService userServ)
        {
            var usuario = userServ.BuscarPorId(idUsuario);
            var libro = libroServ.BuscarPorId(idLibro);

            if (usuario != null && usuario.Activo && libro != null && libro.Disponible)
            {
                var nuevoPrestamo = new Prestamo
                {
                    IdPrestamo = Guid.NewGuid().ToString().Substring(0, 8),
                    IdUsuario = idUsuario,
                    IdLibro = idLibro,
                    FechaPrestamo = DateTime.Now.ToShortDateString(),
                    Estado = "Activo"
                };

                _prestamos.Add(nuevoPrestamo);
                libro.Disponible = false; 
                return true;
            }
            return false;
        }

        public List<Prestamo> ObtenerTodos() => _prestamos;
    }
}