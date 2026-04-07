using System;
using System.Collections.Generic;
using System.Linq;

namespace ViviLibreria
{
    // --- MODELOS ---
    // Cambiamos struct por class para usar las ventajas de C# moderno
    class Libro
    {
        public string Id { get; set; } = "";
        public string Titulo { get; set; } = "";
        public string Autor { get; set; } = "";
        public string Categoria { get; set; } = "";
        public int Anio { get; set; }
        public bool Disponible { get; set; } = true;
    }

    class Usuario
    {
        public string Id { get; set; } = "";
        public string Nombre { get; set; } = "";
        public string Contacto { get; set; } = "";
        public bool Activo { get; set; } = true;
    }

    class Prestamo
    {
        public string IdPrestamo { get; set; } = "";
        public string IdUsuario { get; set; } = "";
        public string IdLibro { get; set; } = "";
        public string FechaPrestamo { get; set; } = "";
        public string FechaLimite { get; set; } = "";
        public string Estado { get; set; } = "Activo"; // Activo o Devuelto
    }

    class Program
    {
        // Listas dinámicas: no necesitan tamaño fijo ni contadores manuales
        static List<Libro> libros = new List<Libro>();
        static List<Usuario> usuarios = new List<Usuario>();
        static List<Prestamo> prestamos = new List<Prestamo>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine("   BIENVENIDO A VIVI LIBRERÍA   ");
                Console.WriteLine("================================");
                Console.WriteLine("1. Libros\n2. Usuarios\n3. Préstamos\n4. Salir");
                Console.Write("\nSeleccione una opción: ");

                string opcion = Console.ReadLine() ?? "";
                if (opcion == "4") break;

                switch (opcion)
                {
                    case "1": MenuLibros(); break;
                    case "2": MenuUsuarios(); break;
                    case "3": MenuPrestamos(); break;
                }
            }
        }

        // --- GESTIÓN DE LIBROS ---
        static void MenuLibros()
        {
            Console.Clear();
            Console.WriteLine("1. Registrar libro\n2. Listar libros\n3. Eliminar\n0. Volver");
            string op = Console.ReadLine() ?? "";

            if (op == "1")
            {
                Libro nuevo = new Libro();
                Console.Write("ID/ISBN: "); nuevo.Id = Console.ReadLine() ?? "";
                Console.Write("Título: "); nuevo.Titulo = Console.ReadLine() ?? "";
                Console.Write("Autor: "); nuevo.Autor = Console.ReadLine() ?? "";
                Console.Write("Año: "); nuevo.Anio = int.Parse(Console.ReadLine() ?? "0");
                
                libros.Add(nuevo); // La lista crece sola
                Console.WriteLine("Libro registrado.");
            }
            else if (op == "2")
            {
                Console.WriteLine("\n--- LISTADO ---");
                foreach (var l in libros)
                {
                    Console.WriteLine($"ID: {l.Id} | {l.Titulo} | Disponible: {l.Disponible}");
                }
            }
            else if (op == "3")
            {
                Console.Write("ID a eliminar: ");
                string id = Console.ReadLine() ?? "";
                // LINQ: busca el libro y lo remueve si existe y está disponible
                var libro = libros.FirstOrDefault(l => l.Id == id);
                if (libro != null && libro.Disponible) {
                    libros.Remove(libro);
                    Console.WriteLine("Eliminado.");
                } else {
                    Console.WriteLine("No encontrado o está prestado.");
                }
            }
            Console.ReadLine();
        }

        // --- GESTIÓN DE USUARIOS ---
        static void MenuUsuarios()
        {
            Console.Clear();
            Console.WriteLine("1. Registrar Usuario\n2. Listar\n0. Volver");
            string op = Console.ReadLine() ?? "";

            if (op == "1")
            {
                Usuario u = new Usuario();
                Console.Write("ID: "); u.Id = Console.ReadLine() ?? "";
                Console.Write("Nombre: "); u.Nombre = Console.ReadLine() ?? "";
                usuarios.Add(u);
            }
            else if (op == "2")
            {
                usuarios.ForEach(u => Console.WriteLine($"{u.Id} - {u.Nombre}"));
            }
            Console.ReadLine();
        }

        // --- GESTIÓN DE PRÉSTAMOS ---
        static void MenuPrestamos()
        {
            Console.Clear();
            Console.WriteLine("1. Crear Préstamo\n0. Volver");
            if (Console.ReadLine() == "1")
            {
                Console.Write("ID Usuario: "); string idU = Console.ReadLine() ?? "";
                Console.Write("ID Libro: "); string idL = Console.ReadLine() ?? "";

                // Validación rápida usando las Listas
                var user = usuarios.FirstOrDefault(u => u.Id == idU && u.Activo);
                var book = libros.FirstOrDefault(l => l.Id == idL && l.Disponible);

                if (user != null && book != null)
                {
                    prestamos.Add(new Prestamo {
                        IdPrestamo = Guid.NewGuid().ToString().Substring(0,5),
                        IdUsuario = idU,
                        IdLibro = idL,
                        FechaPrestamo = DateTime.Now.ToShortDateString()
                    });
                    book.Disponible = false;
                    Console.WriteLine("Préstamo realizado con éxito.");
                }
                else Console.WriteLine("Error: Usuario o Libro no válidos.");
            }
            Console.ReadLine();
        }
    }
}
