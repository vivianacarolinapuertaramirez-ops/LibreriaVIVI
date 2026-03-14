using System;

class Program
{
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
                    Console.WriteLine("Has seleccionado: Libros");
                    Console.WriteLine("Presiona Enter para continuar...");
                    Console.ReadLine();
                    break;
                case "2":
                    Console.WriteLine("Has seleccionado: Usuarios");
                    Console.WriteLine("Presiona Enter para continuar...");
                    Console.ReadLine();
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
}
