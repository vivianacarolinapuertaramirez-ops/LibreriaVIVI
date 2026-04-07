using System;
using System.Collections.Generic;
using System.Linq;
using ViviLibreria.services;

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
        //Instanciamos los servicios
        static LibroService _libroService = new LibroService();
        static UsuarioService _usuarioService = new UsuarioService();
        static PrestamoService _prestamoService = new PrestamoService();
        
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
                
                // 2. Ya no usamos libros.Add(), usamos el servicio
                _libroService.Registrar(nuevo); 
                Console.WriteLine("Libro registrado mediante el Servicio.");
            }
            else if (op == "2")
            {
                Console.WriteLine("\n--- LISTADO ---");
                // 3. Le pedimos los datos al servicio
                var lista = _libroService.ObtenerTodos();
                foreach (var l in lista)
                {
                    Console.WriteLine($"ID: {l.Id} | {l.Titulo} | Disponible: {l.Disponible}");
                }
            }
            else if (op == "3")
            {
                Console.Write("ID a eliminar: ");
                string id = Console.ReadLine() ?? "";
                
                // 4. La lógica de "si existe o si está prestado" ahora la hace el servicio
                if (_libroService.Eliminar(id)) Console.WriteLine("Eliminado con éxito.");
                else Console.WriteLine("No se pudo eliminar.");
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
