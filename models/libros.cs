using System;

namespace system_books.Models
{
    public class Libro
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Categoria { get; set; }
        public int Anio { get; set; }
        public bool Disponible { get; set; }

        public Libro()
        {
            Disponible = true;
        }

        public string ResumenCorto()
        {
            return $"{Titulo} - {Autor}";
        }

        public string DetalleCompleto()
        {
            return $"ID: {Id}, Título: {Titulo}, Autor: {Autor}, Categoría: {Categoria}, Año: {Anio}, Disponible: {Disponible}";
        }

        public override string ToString()
        {
            return DetalleCompleto();
        }
    }
}