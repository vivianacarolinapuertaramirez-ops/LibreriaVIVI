using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.IO;
using System.Text.Json;
using LibreriaVIVI.services; 
using LibreriaVIVI.models;   

namespace LibreriaVIVI
{
    class Program
    {
        static LibroService _libroService = new LibroService();
        static UsuarioService _usuarioService = new UsuarioService();
        static PrestamoService _prestamoService = new PrestamoService();

        static string pathLibros = "libros.json";
        static string pathUsuarios = "usuarios.json";
        static string pathPrestamos = "prestamos.json";
        
        static void Main(string[] args)
        {
            CargarDatos();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("====================================");
                Console.WriteLine("    SISTEMA BIBLIOTECARIO VIVI      ");
                Console.WriteLine("====================================");
                Console.WriteLine("1. Libros (Inventario)");
                Console.WriteLine("2. Usuarios (Socios)");
                Console.WriteLine("3. Préstamos y Devoluciones");
                Console.WriteLine("4. Reportes y KPIs");
                Console.WriteLine("5. Guardar y Salir");
                Console.Write("\nSeleccione una opción: ");

                string opcion = Console.ReadLine() ?? "";
                
                if (opcion == "5") 
                {
                    GuardarDatos();
                    Console.WriteLine("\n¡Sincronización completada! Hasta luego.");
                    break;
                }

                switch (opcion)
                {
                    case "1": MenuLibros(); break;
                    case "2": MenuUsuarios(); break;
                    case "3": MenuPrestamos(); break;
                    case "4": MenuReportes(); break;
                    default: Error(); break;
                }
            }
        }

        // --- GESTIÓN DE LIBROS (Con ISBN y Género) ---
        static void MenuLibros()
        {
            Console.Clear();
            Console.WriteLine(">> SECCIÓN DE INVENTARIO");
            Console.WriteLine("1. Registrar nuevo libro\n2. Ver catálogo completo\n0. Volver");
            string op = Console.ReadLine() ?? "";

            if (op == "1")
            {
                var n = new Libro();
                Console.Write("ISBN/ID: "); n.Id = Console.ReadLine() ?? "";
                Console.Write("Título: "); n.Titulo = Console.ReadLine() ?? "";
                Console.Write("Autor: "); n.Autor = Console.ReadLine() ?? "";
                Console.Write("Género/Categoría: "); n.Categoria = Console.ReadLine() ?? "";
                _libroService.Registrar(n); 
                Ok("Libro indexado exitosamente.");
            }
            else if (op == "2")
            {
                Console.Clear();
                Console.WriteLine("{0,-15} {1,-25} {2,-15} {3,-10}", "ISBN", "TÍTULO", "GÉNERO", "ESTADO");
                Console.WriteLine(new string('-', 65));
                
                foreach (var l in _libroService.ObtenerTodos())
                {
                    string estado = l.Disponible ? "Disponible" : "Prestado";
                    Console.WriteLine("{0,-15} {1,-25} {2,-15} {3,-10}", l.Id, l.Titulo, l.Categoria, estado);
                }
                Ok($"\nTotal de ejemplares: {_libroService.TotalLibros()}");
            }
        }

        // --- GESTIÓN DE PRÉSTAMOS Y DEVOLUCIONES ---
        static void MenuPrestamos()
        {
            Console.Clear();
            Console.WriteLine(">> PRÉSTAMOS Y DEVOLUCIONES");
            Console.WriteLine("1. Registrar Préstamo\n2. Registrar Devolución\n3. Ver Historial Activo\n0. Volver");
            string op = Console.ReadLine() ?? "";

            if (op == "1")
            {
                Console.Write("ID del Socio: "); string u = Console.ReadLine() ?? "";
                Console.Write("ISBN del Libro: "); string l = Console.ReadLine() ?? "";
                if (_prestamoService.GenerarPrestamo(u, l, _libroService, _usuarioService)) 
                    Ok("Préstamo registrado. El libro ya no aparece como disponible.");
                else 
                    Error("No se pudo procesar (ID inválido o libro ya prestado).");
            }
            else if (op == "2")
            {
                Console.Write("Ingrese el ID del Préstamo o ISBN del libro a devolver: ");
                string id = Console.ReadLine() ?? "";
                
                // Buscamos el préstamo en la lista
                var prestamo = _prestamoService.ObtenerTodos().FirstOrDefault(p => p.IdLibro == id || p.IdPrestamo == id);
                
                if (prestamo != null)
                {
                    var libro = _libroService.ObtenerTodos().FirstOrDefault(l => l.Id == prestamo.IdLibro);
                    if (libro != null) libro.Disponible = true; // Liberamos el libro
                    
                    _prestamoService.ObtenerTodos().Remove(prestamo); // Quitamos el préstamo de activos
                    Ok("Devolución exitosa. El libro está disponible nuevamente.");
                }
                else Error("No se encontró un préstamo activo con ese ID/ISBN.");
            }
            else if (op == "3")
            {
                Console.Clear();
                Console.WriteLine("--- PRÉSTAMOS EN CURSO ---");
                var lista = _prestamoService.ObtenerTodos();
                if (!lista.Any()) { Ok("No hay libros fuera de la biblioteca."); return; }

                foreach (var p in lista)
                {
                    Console.WriteLine($"Ticket: {p.IdPrestamo} | Libro: {p.IdLibro} | Socio: {p.IdUsuario}");
                }
                Ok("");
            }
        }

        // --- PERSISTENCIA JSON ---
        static void GuardarDatos()
        {
            try {
                var options = new JsonSerializerOptions { WriteIndented = true };
                File.WriteAllText(pathLibros, JsonSerializer.Serialize(_libroService.ObtenerTodos(), options));
                File.WriteAllText(pathUsuarios, JsonSerializer.Serialize(_usuarioService.ObtenerTodos(), options));
                File.WriteAllText(pathPrestamos, JsonSerializer.Serialize(_prestamoService.ObtenerTodos(), options));
            } catch (Exception ex) { Console.WriteLine("Error al guardar: " + ex.Message); }
        }

        static void CargarDatos()
        {
            try {
                if (File.Exists(pathLibros))
                    JsonSerializer.Deserialize<List<Libro>>(File.ReadAllText(pathLibros))?.ForEach(l => _libroService.Registrar(l));
                
                if (File.Exists(pathUsuarios))
                    JsonSerializer.Deserialize<List<Usuario>>(File.ReadAllText(pathUsuarios))?.ForEach(u => _usuarioService.Registrar(u));

                if (File.Exists(pathPrestamos))
                {
                    var prestamos = JsonSerializer.Deserialize<List<Prestamo>>(File.ReadAllText(pathPrestamos));
                    foreach (var p in prestamos ?? new List<Prestamo>())
                    {
                        // Registramos y bloqueamos el libro automáticamente
                        _prestamoService.GenerarPrestamo(p.IdUsuario, p.IdLibro, _libroService, _usuarioService);
                    }
                }
            } catch { }
        }

        static void MenuUsuarios()
        {
            Console.Clear();
            Console.WriteLine(">> SOCIOS\n1. Registrar\n2. Listar\n0. Volver");
            string op = Console.ReadLine() ?? "";
            if (op == "1")
            {
                var u = new Usuario { Id = Guid.NewGuid().ToString().Substring(0,5) };
                Console.WriteLine($"Generando ID automático: {u.Id}");
                Console.Write("Nombre: "); u.Nombre = Console.ReadLine() ?? "";
                _usuarioService.Registrar(u);
                Ok("Socio registrado.");
            }
            else if (op == "2")
            {
                _usuarioService.ObtenerTodos().ForEach(u => Console.WriteLine($"ID: {u.Id} | {u.Nombre}"));
                Ok("");
            }
        }

        static void MenuReportes()
        {
            Console.Clear();
            Console.WriteLine(">> KPIs BIBLIOTECA");
            Console.WriteLine("1. Libros Disponibles\n2. Promedio de Días de Préstamo\n0. Volver");
            string op = Console.ReadLine() ?? "";
            if (op == "1")
            {
                var disp = _libroService.ObtenerPorDisponibilidad(true);
                disp.ForEach(l => Console.WriteLine($"- {l.Titulo} (ISBN: {l.Id})"));
                Ok($"Total disponibles: {disp.Count}");
            }
            else if (op == "2")
            {
                Ok($"El promedio actual es de: {_prestamoService.PromedioDiasPrestamo():F1} días.");
            }
        }

        static void Ok(string m) { if(!string.IsNullOrEmpty(m)) Console.WriteLine($"\n[OK]: {m}"); Console.WriteLine("\nPresione una tecla..."); Console.ReadKey(); }
        static void Error(string m = "Operación fallida") { Console.WriteLine($"\n[!] {m}"); Thread.Sleep(1500); }
    }
}