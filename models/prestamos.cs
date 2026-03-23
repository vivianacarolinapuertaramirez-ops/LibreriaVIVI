using System;

namespace system_books.Models
{
    public class Prestamo
    {
        public string IdPrestamo { get; set; }
        public string IdUsuario { get; set; }
        public string IdLibro { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaLimite { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public EstadoPrestamo Estado { get; set; }

        public Prestamo()
        {
            Estado = EstadoPrestamo.Activo;
            FechaPrestamo = DateTime.Now;
            FechaLimite = DateTime.Now.AddDays(7);
            FechaDevolucion = null;
        }

        public bool EstaVencido()
        {
            return DateTime.Now > FechaLimite;
        }

        public int DiasTranscurridos()
        {
            return (int)(DateTime.Now - FechaPrestamo).TotalDays;
        }

        public string ResumenCorto()
        {
            return $"{IdUsuario} -> {IdLibro}";
        }

        public string DetalleCompleto()
        {
            return $"Préstamo: {IdPrestamo}, Usuario: {IdUsuario}, Libro: {IdLibro}, Estado: {Estado}, Días: {DiasTranscurridos()}";
        }

        public override string ToString()
        {
            return DetalleCompleto();
        }
    }
}