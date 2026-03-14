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

struct Prestamo
{
    public string idPrestamo;
    public string idUsuario;
    public string idLibro;
    public string fechaPrestamo;
    public string fechaLimite;
    public string fechaDevolucion; // vacío si no devuelto
    public string estado; // "Activo" o "Devuelto"
}

class Program
{
    static Libro[] libros = new Libro[100];
    static int contadorLibros = 0;
    static Usuario[] usuarios = new Usuario[100];
    static int contadorUsuarios = 0;
    static Prestamo[] prestamos = new Prestamo[200];
    static int contadorPrestamos = 0;

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
                    MenuPrestamos();
                    break;
                case "4":
                    MenuBusquedasReportes();
                    break;
                case "5":
                    MenuGuardarCargar();
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
                if (UsuarioTienePrestamosActivos(id))
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

    static bool UsuarioTienePrestamosActivos(string idUsuario)
    {
        for (int i = 0; i < contadorPrestamos; i++)
        {
            if (prestamos[i].idUsuario == idUsuario && prestamos[i].estado == "Activo")
            {
                return true;
            }
        }
        return false;
    }

    static void MenuPrestamos()
    {
        while (true)
        {
            Console.WriteLine("Submenú de Préstamos:");
            Console.WriteLine("1. Crear préstamo");
            Console.WriteLine("2. Listar préstamos");
            Console.WriteLine("3. Ver detalle de préstamo (por ID)");
            Console.WriteLine("4. Registrar devolución");
            Console.WriteLine("5. Eliminar préstamo");
            Console.WriteLine("0. Volver al menú principal");
            Console.Write("Selecciona una opción: ");

            string? subOpcion = Console.ReadLine();

            switch (subOpcion)
            {
                case "1":
                    CrearPrestamo();
                    break;
                case "2":
                    MenuListarPrestamos();
                    break;
                case "3":
                    VerDetallePrestamo();
                    break;
                case "4":
                    RegistrarDevolucion();
                    break;
                case "5":
                    EliminarPrestamo();
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

    static void CrearPrestamo()
    {
        if (contadorPrestamos >= 200)
        {
            Console.WriteLine("No se pueden registrar más préstamos.");
            Console.WriteLine("Presiona Enter para continuar...");
            Console.ReadLine();
            return;
        }

        Console.Write("ID de préstamo: ");
        string idPrestamo = Console.ReadLine() ?? "";

        if (BuscarPrestamoIndex(idPrestamo) != -1)
        {
            Console.WriteLine("Ya existe un préstamo con ese ID.");
            Console.WriteLine("Presiona Enter para continuar...");
            Console.ReadLine();
            return;
        }

        Console.Write("ID/Documento del usuario: ");
        string idUsuario = Console.ReadLine() ?? "";
        int usuarioIndex = BuscarUsuarioIndex(idUsuario);
        if (usuarioIndex == -1 || !usuarios[usuarioIndex].activo)
        {
            Console.WriteLine("Usuario no encontrado o no está activo.");
            Console.WriteLine("Presiona Enter para continuar...");
            Console.ReadLine();
            return;
        }

        if (UsuarioTienePrestamosActivos(idUsuario))
        {
            int contadorActivos = 0;
            for (int i = 0; i < contadorPrestamos; i++)
            {
                if (prestamos[i].idUsuario == idUsuario && prestamos[i].estado == "Activo")
                {
                    contadorActivos++;
                }
            }
            if (contadorActivos >= 3)
            {
                Console.WriteLine(
                    "El usuario ya tiene 3 préstamos activos. No se puede crear otro."
                );
                Console.WriteLine("Presiona Enter para continuar...");
                Console.ReadLine();
                return;
            }
        }

        Console.Write("ID/ISBN del libro: ");
        string idLibro = Console.ReadLine() ?? "";
        int libroIndex = BuscarLibroIndex(idLibro);
        if (libroIndex == -1 || !libros[libroIndex].disponible)
        {
            Console.WriteLine("Libro no encontrado o no está disponible.");
            Console.WriteLine("Presiona Enter para continuar...");
            Console.ReadLine();
            return;
        }

        string fechaPrestamo = DateTime.Now.ToString("yyyy-MM-dd");
        string fechaLimite = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");

        prestamos[contadorPrestamos] = new Prestamo
        {
            idPrestamo = idPrestamo,
            idUsuario = idUsuario,
            idLibro = idLibro,
            fechaPrestamo = fechaPrestamo,
            fechaLimite = fechaLimite,
            fechaDevolucion = "",
            estado = "Activo",
        };
        contadorPrestamos++;
        libros[libroIndex].disponible = false;

        Console.WriteLine("Préstamo registrado exitosamente.");
        Console.WriteLine("Presiona Enter para continuar...");
        Console.ReadLine();
    }

    static void MenuListarPrestamos()
    {
        while (true)
        {
            Console.WriteLine("Listar préstamos:");
            Console.WriteLine("1. Todos");
            Console.WriteLine("2. Activos");
            Console.WriteLine("3. Cerrados (devueltos)");
            Console.WriteLine("0. Volver");
            Console.Write("Selecciona una opción: ");

            string? listOpcion = Console.ReadLine();

            switch (listOpcion)
            {
                case "1":
                    ListarPrestamos(true, true);
                    break;
                case "2":
                    ListarPrestamos(true, false);
                    break;
                case "3":
                    ListarPrestamos(false, true);
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

    static void ListarPrestamos(bool activos, bool cerrados)
    {
        for (int i = 0; i < contadorPrestamos; i++)
        {
            if (
                (activos && prestamos[i].estado == "Activo")
                || (cerrados && prestamos[i].estado == "Devuelto")
            )
            {
                Console.WriteLine(
                    $"ID: {prestamos[i].idPrestamo}, Usuario: {prestamos[i].idUsuario}, Libro: {prestamos[i].idLibro}, Estado: {prestamos[i].estado}"
                );
            }
        }
    }

    static void VerDetallePrestamo()
    {
        Console.Write("Ingresa ID de préstamo: ");
        string id = Console.ReadLine() ?? "";

        int index = BuscarPrestamoIndex(id);
        if (index == -1)
        {
            Console.WriteLine("Préstamo no encontrado.");
            Console.WriteLine("Presiona Enter para continuar...");
            Console.ReadLine();
            return;
        }

        var p = prestamos[index];
        Console.WriteLine($"ID: {p.idPrestamo}");
        Console.WriteLine($"Usuario: {p.idUsuario}");
        Console.WriteLine($"Libro: {p.idLibro}");
        Console.WriteLine($"Fecha préstamo: {p.fechaPrestamo}");
        Console.WriteLine($"Fecha límite: {p.fechaLimite}");
        Console.WriteLine(
            $"Fecha devolución: {(string.IsNullOrEmpty(p.fechaDevolucion) ? "(pendiente)" : p.fechaDevolucion)}"
        );
        Console.WriteLine($"Estado: {p.estado}");
        Console.WriteLine("Presiona Enter para continuar...");
        Console.ReadLine();
    }

    static void RegistrarDevolucion()
    {
        Console.Write("Ingresa ID de préstamo a devolver: ");
        string id = Console.ReadLine() ?? "";

        int index = BuscarPrestamoIndex(id);
        if (index == -1)
        {
            Console.WriteLine("Préstamo no encontrado.");
            Console.WriteLine("Presiona Enter para continuar...");
            Console.ReadLine();
            return;
        }

        if (prestamos[index].estado != "Activo")
        {
            Console.WriteLine("El préstamo ya está devuelto.");
            Console.WriteLine("Presiona Enter para continuar...");
            Console.ReadLine();
            return;
        }

        prestamos[index].estado = "Devuelto";
        prestamos[index].fechaDevolucion = DateTime.Now.ToString("yyyy-MM-dd");

        int libroIndex = BuscarLibroIndex(prestamos[index].idLibro);
        if (libroIndex != -1)
        {
            libros[libroIndex].disponible = true;
        }

        Console.WriteLine("Devolución registrada. El libro ahora está disponible.");
        Console.WriteLine("Presiona Enter para continuar...");
        Console.ReadLine();
    }

    static void EliminarPrestamo()
    {
        Console.Write("Ingresa ID de préstamo a eliminar: ");
        string id = Console.ReadLine() ?? "";

        int index = BuscarPrestamoIndex(id);
        if (index == -1)
        {
            Console.WriteLine("Préstamo no encontrado.");
            Console.WriteLine("Presiona Enter para continuar...");
            Console.ReadLine();
            return;
        }

        bool estabaActivo = prestamos[index].estado == "Activo";
        string idLibro = prestamos[index].idLibro;

        // Si estaba activo, devolver el libro antes de eliminar el préstamo
        if (estabaActivo)
        {
            int libroIndex = BuscarLibroIndex(idLibro);
            if (libroIndex != -1)
            {
                libros[libroIndex].disponible = true;
            }
        }

        // Eliminar préstamo moviendo el último al lugar
        prestamos[index] = prestamos[contadorPrestamos - 1];
        contadorPrestamos--;

        Console.WriteLine("Préstamo eliminado.");
        Console.WriteLine("Presiona Enter para continuar...");
        Console.ReadLine();
    }

    static void MenuBusquedasReportes()
    {
        while (true)
        {
            Console.WriteLine("Submenú de Búsquedas y reportes:");
            Console.WriteLine("1. Buscar libro");
            Console.WriteLine("2. Buscar usuario");
            Console.WriteLine("3. Reportes");
            Console.WriteLine("0. Volver al menú principal");
            Console.Write("Selecciona una opción: ");

            string? subOpcion = Console.ReadLine();

            switch (subOpcion)
            {
                case "1":
                    BuscarLibro();
                    break;
                case "2":
                    BuscarUsuario();
                    break;
                case "3":
                    MenuReportes();
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

    static void BuscarLibro()
    {
        while (true)
        {
            Console.WriteLine("Buscar libro:");
            Console.WriteLine("1. Por título");
            Console.WriteLine("2. Por autor");
            Console.WriteLine("3. Por ID/ISBN");
            Console.WriteLine("4. Por categoría");
            Console.WriteLine("0. Volver");
            Console.Write("Selecciona una opción: ");

            string? opcion = Console.ReadLine();
            if (opcion == null)
                return;

            switch (opcion)
            {
                case "1":
                    Console.Write("Título a buscar: ");
                    BuscarLibrosPorTexto(Console.ReadLine() ?? "", "titulo");
                    break;
                case "2":
                    Console.Write("Autor a buscar: ");
                    BuscarLibrosPorTexto(Console.ReadLine() ?? "", "autor");
                    break;
                case "3":
                    Console.Write("ID/ISBN a buscar: ");
                    BuscarLibrosPorTexto(Console.ReadLine() ?? "", "id");
                    break;
                case "4":
                    Console.Write("Categoría a buscar: ");
                    BuscarLibrosPorTexto(Console.ReadLine() ?? "", "categoria");
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

    static void BuscarLibrosPorTexto(string texto, string campo)
    {
        texto = texto.Trim().ToLower();
        if (texto == "")
        {
            Console.WriteLine("Texto vacío. Intenta de nuevo.");
            return;
        }

        bool encontrado = false;
        for (int i = 0; i < contadorLibros; i++)
        {
            string valor = campo switch
            {
                "titulo" => libros[i].titulo,
                "autor" => libros[i].autor,
                "id" => libros[i].id,
                "categoria" => libros[i].categoria,
                _ => "",
            };

            if (valor.ToLower().Contains(texto))
            {
                encontrado = true;
                Console.WriteLine(
                    $"ID: {libros[i].id}, Título: {libros[i].titulo}, Autor: {libros[i].autor}, Categoría: {libros[i].categoria}, Disponible: {libros[i].disponible}"
                );
            }
        }

        if (!encontrado)
        {
            Console.WriteLine("No se encontraron libros que coincidan.");
        }
    }

    static void BuscarUsuario()
    {
        while (true)
        {
            Console.WriteLine("Buscar usuario:");
            Console.WriteLine("1. Por nombre");
            Console.WriteLine("2. Por ID/documento");
            Console.WriteLine("0. Volver");
            Console.Write("Selecciona una opción: ");

            string? opcion = Console.ReadLine();
            if (opcion == null)
                return;

            switch (opcion)
            {
                case "1":
                    Console.Write("Nombre a buscar: ");
                    BuscarUsuariosPorTexto(Console.ReadLine() ?? "", "nombre");
                    break;
                case "2":
                    Console.Write("ID/documento a buscar: ");
                    BuscarUsuariosPorTexto(Console.ReadLine() ?? "", "id");
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

    static void BuscarUsuariosPorTexto(string texto, string campo)
    {
        texto = texto.Trim().ToLower();
        if (texto == "")
        {
            Console.WriteLine("Texto vacío. Intenta de nuevo.");
            return;
        }

        bool encontrado = false;
        for (int i = 0; i < contadorUsuarios; i++)
        {
            string valor = campo switch
            {
                "nombre" => usuarios[i].nombre,
                "id" => usuarios[i].id,
                _ => "",
            };

            if (valor.ToLower().Contains(texto))
            {
                encontrado = true;
                Console.WriteLine(
                    $"ID: {usuarios[i].id}, Nombre: {usuarios[i].nombre}, Contacto: {usuarios[i].contacto}, Activo: {usuarios[i].activo}"
                );
            }
        }

        if (!encontrado)
        {
            Console.WriteLine("No se encontraron usuarios que coincidan.");
        }
    }

    static void MenuReportes()
    {
        while (true)
        {
            Console.WriteLine("Reportes:");
            Console.WriteLine("1. Préstamos por usuario");
            Console.WriteLine("2. Préstamos por libro");
            Console.WriteLine("3. Préstamos vencidos");
            Console.WriteLine("4. Resumen general");
            Console.WriteLine("0. Volver");
            Console.Write("Selecciona una opción: ");

            string? opcion = Console.ReadLine();
            if (opcion == null)
                return;

            switch (opcion)
            {
                case "1":
                    ReportePrestamosPorUsuario();
                    break;
                case "2":
                    ReportePrestamosPorLibro();
                    break;
                case "3":
                    ReportePrestamosVencidos();
                    break;
                case "4":
                    ReporteResumenGeneral();
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

    static void ReportePrestamosPorUsuario()
    {
        Console.Write("ID/Documento del usuario: ");
        string idUsuario = Console.ReadLine() ?? "";

        bool encontrado = false;
        for (int i = 0; i < contadorPrestamos; i++)
        {
            if (prestamos[i].idUsuario == idUsuario)
            {
                encontrado = true;
                Console.WriteLine(
                    $"ID Préstamo: {prestamos[i].idPrestamo}, Libro: {prestamos[i].idLibro}, Estado: {prestamos[i].estado}, Fecha límite: {prestamos[i].fechaLimite}"
                );
            }
        }

        if (!encontrado)
        {
            Console.WriteLine("No se encontraron préstamos para ese usuario.");
        }
    }

    static void ReportePrestamosPorLibro()
    {
        Console.Write("ID/ISBN del libro: ");
        string idLibro = Console.ReadLine() ?? "";

        bool encontrado = false;
        for (int i = 0; i < contadorPrestamos; i++)
        {
            if (prestamos[i].idLibro == idLibro)
            {
                encontrado = true;
                Console.WriteLine(
                    $"ID Préstamo: {prestamos[i].idPrestamo}, Usuario: {prestamos[i].idUsuario}, Estado: {prestamos[i].estado}, Fecha límite: {prestamos[i].fechaLimite}"
                );
            }
        }

        if (!encontrado)
        {
            Console.WriteLine("No se encontraron préstamos para ese libro.");
        }
    }

    static void ReportePrestamosVencidos()
    {
        bool encontrado = false;
        DateTime hoy = DateTime.Today;

        for (int i = 0; i < contadorPrestamos; i++)
        {
            if (prestamos[i].estado == "Activo")
            {
                if (DateTime.TryParse(prestamos[i].fechaLimite, out DateTime limite))
                {
                    if (limite < hoy)
                    {
                        encontrado = true;
                        Console.WriteLine(
                            $"ID Préstamo: {prestamos[i].idPrestamo}, Usuario: {prestamos[i].idUsuario}, Libro: {prestamos[i].idLibro}, Fecha límite: {prestamos[i].fechaLimite}"
                        );
                    }
                }
            }
        }

        if (!encontrado)
        {
            Console.WriteLine("No hay préstamos vencidos.");
        }
    }

    static void ReporteResumenGeneral()
    {
        int totalLibros = contadorLibros;
        int disponibles = 0;
        int prestados = 0;

        for (int i = 0; i < contadorLibros; i++)
        {
            if (libros[i].disponible)
            {
                disponibles++;
            }
            else
            {
                prestados++;
            }
        }

        Console.WriteLine($"Total libros: {totalLibros}");
        Console.WriteLine($"Disponibles: {disponibles}");
        Console.WriteLine($"Prestados: {prestados}");
    }

    static void MenuGuardarCargar()
    {
        while (true)
        {
            Console.WriteLine("Guardar / Cargar datos:");
            Console.WriteLine("1. Guardar datos");
            Console.WriteLine("2. Cargar datos");
            Console.WriteLine("3. Reiniciar datos (vaciar todo)");
            Console.WriteLine("0. Volver al menú principal");
            Console.Write("Selecciona una opción: ");

            string? opcion = Console.ReadLine();
            if (opcion == null)
                return;

            switch (opcion)
            {
                case "1":
                    GuardarDatos();
                    break;
                case "2":
                    CargarDatos();
                    break;
                case "3":
                    ReiniciarDatos();
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

    static void GuardarDatos()
    {
        var librosLines = new string[contadorLibros];
        for (int i = 0; i < contadorLibros; i++)
        {
            librosLines[i] =
                $"{libros[i].id}|{libros[i].titulo}|{libros[i].autor}|{libros[i].categoria}|{libros[i].anio}|{libros[i].disponible}";
        }

        var usuariosLines = new string[contadorUsuarios];
        for (int i = 0; i < contadorUsuarios; i++)
        {
            usuariosLines[i] =
                $"{usuarios[i].id}|{usuarios[i].nombre}|{usuarios[i].contacto}|{usuarios[i].activo}";
        }

        var prestamosLines = new string[contadorPrestamos];
        for (int i = 0; i < contadorPrestamos; i++)
        {
            prestamosLines[i] =
                $"{prestamos[i].idPrestamo}|{prestamos[i].idUsuario}|{prestamos[i].idLibro}|{prestamos[i].fechaPrestamo}|{prestamos[i].fechaLimite}|{prestamos[i].fechaDevolucion}|{prestamos[i].estado}";
        }

        System.IO.File.WriteAllLines("libros.txt", librosLines);
        System.IO.File.WriteAllLines("usuarios.txt", usuariosLines);
        System.IO.File.WriteAllLines("prestamos.txt", prestamosLines);

        Console.WriteLine("Datos guardados en libros.txt, usuarios.txt y prestamos.txt.");
    }

    static void CargarDatos()
    {
        ReiniciarDatos();

        if (System.IO.File.Exists("libros.txt"))
        {
            var lines = System.IO.File.ReadAllLines("libros.txt");
            for (int i = 0; i < lines.Length && i < libros.Length; i++)
            {
                var parts = lines[i].Split('|');
                if (parts.Length >= 6)
                {
                    libros[i].id = parts[0];
                    libros[i].titulo = parts[1];
                    libros[i].autor = parts[2];
                    libros[i].categoria = parts[3];
                    libros[i].anio = int.TryParse(parts[4], out int a) ? a : 0;
                    libros[i].disponible = bool.TryParse(parts[5], out bool d) ? d : true;
                    contadorLibros++;
                }
            }
        }

        if (System.IO.File.Exists("usuarios.txt"))
        {
            var lines = System.IO.File.ReadAllLines("usuarios.txt");
            for (int i = 0; i < lines.Length && i < usuarios.Length; i++)
            {
                var parts = lines[i].Split('|');
                if (parts.Length >= 4)
                {
                    usuarios[i].id = parts[0];
                    usuarios[i].nombre = parts[1];
                    usuarios[i].contacto = parts[2];
                    usuarios[i].activo = bool.TryParse(parts[3], out bool a) ? a : true;
                    contadorUsuarios++;
                }
            }
        }

        if (System.IO.File.Exists("prestamos.txt"))
        {
            var lines = System.IO.File.ReadAllLines("prestamos.txt");
            for (int i = 0; i < lines.Length && i < prestamos.Length; i++)
            {
                var parts = lines[i].Split('|');
                if (parts.Length >= 7)
                {
                    prestamos[i].idPrestamo = parts[0];
                    prestamos[i].idUsuario = parts[1];
                    prestamos[i].idLibro = parts[2];
                    prestamos[i].fechaPrestamo = parts[3];
                    prestamos[i].fechaLimite = parts[4];
                    prestamos[i].fechaDevolucion = parts[5];
                    prestamos[i].estado = parts[6];
                    contadorPrestamos++;
                }
            }
        }

        Console.WriteLine("Datos cargados (si los archivos existían).");
    }

    static void ReiniciarDatos()
    {
        Console.Write("¿Estás seguro? Esto borrará todos los datos. (s/n): ");
        string confirmar = Console.ReadLine() ?? "";
        if (confirmar.ToLower() != "s")
        {
            Console.WriteLine("Operación cancelada.");
            return;
        }

        contadorLibros = 0;
        contadorUsuarios = 0;
        contadorPrestamos = 0;

        if (System.IO.File.Exists("libros.txt"))
            System.IO.File.Delete("libros.txt");
        if (System.IO.File.Exists("usuarios.txt"))
            System.IO.File.Delete("usuarios.txt");
        if (System.IO.File.Exists("prestamos.txt"))
            System.IO.File.Delete("prestamos.txt");

        Console.WriteLine("Datos reiniciados.");
    }

    static int BuscarUsuarioIndex(string id)
    {
        for (int i = 0; i < contadorUsuarios; i++)
        {
            if (usuarios[i].id == id)
            {
                return i;
            }
        }
        return -1;
    }

    static int BuscarLibroIndex(string id)
    {
        for (int i = 0; i < contadorLibros; i++)
        {
            if (libros[i].id == id)
            {
                return i;
            }
        }
        return -1;
    }

    static int BuscarPrestamoIndex(string id)
    {
        for (int i = 0; i < contadorPrestamos; i++)
        {
            if (prestamos[i].idPrestamo == id)
            {
                return i;
            }
        }
        return -1;
    }
}
