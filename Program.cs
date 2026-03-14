using System;

struct Libro
{
    public string id;
    public string titulo;
    public string autor;
    public string categoria;
    public int anio;
    public bool disponible;
}

struct Usuario
{
    public string id;
    public string nombre;
    public string contacto;
    public bool activo;
}

class Program
{
    static Libro[] libros = new Libro[100];
    static int contadorLibros = 0;
    static Usuario[] usuarios = new Usuario[100];
    static int contadorUsuarios = 0;

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("¡Bienvenido a Vivi Librería!");
            Console.WriteLine("Tu lugar favorito para libros y conocimiento.");
            Console.WriteLine();
            Console.WriteLine("Menú principal:");
            Console.WriteLine("1. Libros");
            Console.WriteLine("2. Usuarios");
            Console.WriteLine("3. Préstamos");
            Console.WriteLine("4. Búsquedas y reportes");
            Console.WriteLine("5. Guardar / Cargar datos");
            Console.WriteLine("6. Salir");
            Console.Write("Selecciona una opción: ");

            string? opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    MenuLibros();
                    break;
                case "2":
                    MenuUsuarios();
                    break;
                case "3":
                    Console.WriteLine("Has seleccionado: Préstamos");
                    Console.WriteLine("Presiona Enter para continuar...");
                    Console.ReadLine();
                    break;
                case "4":
                    Console.WriteLine("Has seleccionado: Búsquedas y reportes");
                    Console.WriteLine("Presiona Enter para continuar...");
                    Console.ReadLine();
                    break;
                case "5":
                    Console.WriteLine("Has seleccionado: Guardar / Cargar datos");
                    Console.WriteLine("Presiona Enter para continuar...");
                    Console.ReadLine();
                    break;
                case "6":
                    Console.WriteLine("¡Hasta luego!");
                    return;
                default:
                    Console.WriteLine("Opción no válida. Inténtalo de nuevo.");
                    Console.WriteLine("Presiona Enter para continuar...");
                    Console.ReadLine();
                    break;
            }

            Console.Clear();
        }
    }

    static void MenuLibros()
    {
        while (true)
        {
            Console.WriteLine("Submenú de Libros:");
            Console.WriteLine("1. Registrar libro");
            Console.WriteLine("2. Listar libros");
            Console.WriteLine("3. Ver detalle de libro (por ID/ISBN)");
            Console.WriteLine("4. Actualizar libro");
            Console.WriteLine("5. Eliminar libro");
            Console.WriteLine("0. Volver al menú principal");
            Console.Write("Selecciona una opción: ");

            string? subOpcion = Console.ReadLine();

            switch (subOpcion)
            {
                case "1":
                    RegistrarLibro();
                    break;
                case "2":
                    MenuListarLibros();
                    break;
                case "3":
                    VerDetalleLibro();
                    break;
                case "4":
                    MenuActualizarLibro();
                    break;
                case "5":
                    EliminarLibro();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opción no válida. Inténtalo de nuevo.");
                    Console.WriteLine("Presiona Enter para continuar...");
                    Console.ReadLine();
                    break;
            }

            Console.Clear();
        }
    }

    static void RegistrarLibro()
    {
        if (contadorLibros >= 100)
        {
            Console.WriteLine("No se pueden registrar más libros.");
            Console.WriteLine("Presiona Enter para continuar...");
            Console.ReadLine();
            return;
        }

        Console.Write("ID/ISBN: ");
        string? id = Console.ReadLine() ?? "";
        Console.Write("Título: ");
        string titulo = Console.ReadLine() ?? "";
        Console.Write("Autor: ");
        string? autor = Console.ReadLine() ?? "";
        Console.Write("Categoría: ");
        string? categoria = Console.ReadLine() ?? "";
        Console.Write("Año: ");
        int anio = int.Parse(Console.ReadLine() ?? "0");

        libros[contadorLibros] = new Libro
        {
            id = id,
            titulo = titulo,
            autor = autor,
            categoria = categoria,
            anio = anio,
            disponible = true,
        };
        contadorLibros++;

        Console.WriteLine("Libro registrado exitosamente.");
        Console.WriteLine("Presiona Enter para continuar...");
        Console.ReadLine();
    }

    static void MenuListarLibros()
    {
        while (true)
        {
            Console.WriteLine("Listar libros:");
            Console.WriteLine("1. Listar todos");
            Console.WriteLine("2. Listar disponibles");
            Console.WriteLine("3. Listar prestados");
            Console.WriteLine("0. Volver");
            Console.Write("Selecciona una opción: ");

            string? listOpcion = Console.ReadLine();

            switch (listOpcion)
            {
                case "1":
                    ListarLibros(true, true);
                    break;
                case "2":
                    ListarLibros(true, false);
                    break;
                case "3":
                    ListarLibros(false, true);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }

            Console.WriteLine("Presiona Enter para continuar...");
            Console.ReadLine();
        }
    }

    static void ListarLibros(bool disponibles, bool prestados)
    {
        for (int i = 0; i < contadorLibros; i++)
        {
            if ((disponibles && libros[i].disponible) || (prestados && !libros[i].disponible))
            {
                Console.WriteLine(
                    $"ID: {libros[i].id}, Título: {libros[i].titulo}, Autor: {libros[i].autor}, Disponible: {libros[i].disponible}"
                );
            }
        }
    }

    static void VerDetalleLibro()
    {
        Console.Write("Ingresa ID/ISBN del libro: ");
        string? id = Console.ReadLine() ?? "";

        for (int i = 0; i < contadorLibros; i++)
        {
            if (libros[i].id == id)
            {
                Console.WriteLine($"ID: {libros[i].id}");
                Console.WriteLine($"Título: {libros[i].titulo}");
                Console.WriteLine($"Autor: {libros[i].autor}");
                Console.WriteLine($"Categoría: {libros[i].categoria}");
                Console.WriteLine($"Año: {libros[i].anio}");
                Console.WriteLine($"Disponible: {libros[i].disponible}");
                Console.WriteLine("Presiona Enter para continuar...");
                Console.ReadLine();
                return;
            }
        }

        Console.WriteLine("Libro no encontrado.");
        Console.WriteLine("Presiona Enter para continuar...");
        Console.ReadLine();
    }

    static void MenuActualizarLibro()
    {
        Console.Write("Ingresa ID/ISBN del libro a actualizar: ");
        string? id = Console.ReadLine() ?? "";

        int index = -1;
        for (int i = 0; i < contadorLibros; i++)
        {
            if (libros[i].id == id)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
        {
            Console.WriteLine("Libro no encontrado.");
            Console.WriteLine("Presiona Enter para continuar...");
            Console.ReadLine();
            return;
        }

        while (true)
        {
            Console.WriteLine("Actualizar libro:");
            Console.WriteLine("1. Editar título");
            Console.WriteLine("2. Editar autor");
            Console.WriteLine("3. Editar año / categoría");
            Console.WriteLine("0. Volver");
            Console.Write("Selecciona una opción: ");

            string? updateOpcion = Console.ReadLine();

            switch (updateOpcion)
            {
                case "1":
                    Console.Write("Nuevo título: ");
                    libros[index].titulo = Console.ReadLine() ?? "";
                    break;
                case "2":
                    Console.Write("Nuevo autor: ");
                    libros[index].autor = Console.ReadLine() ?? "";
                    break;
                case "3":
                    Console.Write("Nuevo año: ");
                    libros[index].anio = int.Parse(Console.ReadLine() ?? "0");
                    Console.Write("Nueva categoría: ");
                    libros[index].categoria = Console.ReadLine() ?? "";
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }

            Console.WriteLine("Actualización realizada.");
            Console.WriteLine("Presiona Enter para continuar...");
            Console.ReadLine();
        }
    }

    static void EliminarLibro()
    {
        Console.Write("Ingresa ID/ISBN del libro a eliminar: ");
        string id = Console.ReadLine() ?? "";

        for (int i = 0; i < contadorLibros; i++)
        {
            if (libros[i].id == id)
            {
                if (!libros[i].disponible)
                {
                    Console.WriteLine("No se puede eliminar un libro prestado.");
                }
                else
                {
                    // Simular eliminación moviendo el último al lugar
                    libros[i] = libros[contadorLibros - 1];
                    contadorLibros--;
                    Console.WriteLine("Libro eliminado.");
                }
                Console.WriteLine("Presiona Enter para continuar...");
                Console.ReadLine();
                return;
            }
        }

        Console.WriteLine("Libro no encontrado.");
        Console.WriteLine("Presiona Enter para continuar...");
        Console.ReadLine();
    }

    static void MenuUsuarios()
    {
        while (true)
        {
            Console.WriteLine("Submenú de Usuarios:");
            Console.WriteLine("1. Registrar usuario");
            Console.WriteLine("2. Listar usuarios");
            Console.WriteLine("3. Ver detalle de usuario (por ID/documento)");
            Console.WriteLine("4. Actualizar usuario");
            Console.WriteLine("5. Eliminar usuario");
            Console.WriteLine("0. Volver al menú principal");
            Console.Write("Selecciona una opción: ");

            string? subOpcion = Console.ReadLine();

            switch (subOpcion)
            {
                case "1":
                    RegistrarUsuario();
                    break;
                case "2":
                    ListarUsuarios();
                    break;
                case "3":
                    VerDetalleUsuario();
                    break;
                case "4":
                    MenuActualizarUsuario();
                    break;
                case "5":
                    EliminarUsuario();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opción no válida. Inténtalo de nuevo.");
                    Console.WriteLine("Presiona Enter para continuar...");
                    Console.ReadLine();
                    break;
            }

            Console.Clear();
        }
    }

    static void RegistrarUsuario()
    {
        if (contadorUsuarios >= 100)
        {
            Console.WriteLine("No se pueden registrar más usuarios.");
            Console.WriteLine("Presiona Enter para continuar...");
            Console.ReadLine();
            return;
        }

        Console.Write("ID/Documento: ");
        string? id = Console.ReadLine() ?? "";
        Console.Write("Nombre: ");
        string? nombre = Console.ReadLine() ?? "";
        Console.Write("Contacto (teléfono/email): ");
        string? contacto = Console.ReadLine() ?? "";

        usuarios[contadorUsuarios] = new Usuario
        {
            id = id,
            nombre = nombre,
            contacto = contacto,
            activo = true,
        };
        contadorUsuarios++;

        Console.WriteLine("Usuario registrado exitosamente.");
        Console.WriteLine("Presiona Enter para continuar...");
        Console.ReadLine();
    }

    static void ListarUsuarios()
    {
        for (int i = 0; i < contadorUsuarios; i++)
        {
            Console.WriteLine(
                $"ID: {usuarios[i].id}, Nombre: {usuarios[i].nombre}, Contacto: {usuarios[i].contacto}, Activo: {usuarios[i].activo}"
            );
        }
        Console.WriteLine("Presiona Enter para continuar...");
        Console.ReadLine();
    }

    static void VerDetalleUsuario()
    {
        Console.Write("Ingresa ID/Documento del usuario: ");
        string? id = Console.ReadLine() ?? "";

        for (int i = 0; i < contadorUsuarios; i++)
        {
            if (usuarios[i].id == id)
            {
                Console.WriteLine($"ID: {usuarios[i].id}");
                Console.WriteLine($"Nombre: {usuarios[i].nombre}");
                Console.WriteLine($"Contacto: {usuarios[i].contacto}");
                Console.WriteLine($"Activo: {usuarios[i].activo}");
                Console.WriteLine("Presiona Enter para continuar...");
                Console.ReadLine();
                return;
            }
        }

        Console.WriteLine("Usuario no encontrado.");
        Console.WriteLine("Presiona Enter para continuar...");
        Console.ReadLine();
    }

    static void MenuActualizarUsuario()
    {
        Console.Write("Ingresa ID/Documento del usuario a actualizar: ");
        string? id = Console.ReadLine() ?? "";

        int index = -1;
        for (int i = 0; i < contadorUsuarios; i++)
        {
            if (usuarios[i].id == id)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
        {
            Console.WriteLine("Usuario no encontrado.");
            Console.WriteLine("Presiona Enter para continuar...");
            Console.ReadLine();
            return;
        }

        while (true)
        {
            Console.WriteLine("Actualizar usuario:");
            Console.WriteLine("1. Editar nombre");
            Console.WriteLine("2. Editar contacto");
            Console.WriteLine("3. Activar / desactivar");
            Console.WriteLine("0. Volver");
            Console.Write("Selecciona una opción: ");

            string? updateOpcion = Console.ReadLine();

            switch (updateOpcion)
            {
                case "1":
                    Console.Write("Nuevo nombre: ");
                    usuarios[index].nombre = Console.ReadLine() ?? "";
                    break;
                case "2":
                    Console.Write("Nuevo contacto: ");
                    usuarios[index].contacto = Console.ReadLine() ?? "";
                    break;
                case "3":
                    Console.Write("Activar (s/n): ");
                    string activoStr = Console.ReadLine() ?? "";
                    usuarios[index].activo = activoStr.ToLower() == "s";
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }

            Console.WriteLine("Actualización realizada.");
            Console.WriteLine("Presiona Enter para continuar...");
            Console.ReadLine();
        }
    }

    static void EliminarUsuario()
    {
        Console.Write("Ingresa ID/Documento del usuario a eliminar: ");
        string? id = Console.ReadLine() ?? "";

        for (int i = 0; i < contadorUsuarios; i++)
        {
            if (usuarios[i].id == id)
            {
                // Simular verificación de préstamos activos (por ahora, asumir ninguno)
                bool tienePrestamos = false; // En futuro, verificar
                if (tienePrestamos)
                {
                    Console.WriteLine("No se puede eliminar un usuario con préstamos activos.");
                }
                else
                {
                    // Simular eliminación
                    usuarios[i] = usuarios[contadorUsuarios - 1];
                    contadorUsuarios--;
                    Console.WriteLine("Usuario eliminado.");
                }
                Console.WriteLine("Presiona Enter para continuar...");
                Console.ReadLine();
                return;
            }
        }

        Console.WriteLine("Usuario no encontrado.");
        Console.WriteLine("Presiona Enter para continuar...");
        Console.ReadLine();
    }
}
