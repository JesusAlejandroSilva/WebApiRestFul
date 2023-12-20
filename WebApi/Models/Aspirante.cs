namespace WebApi.Models
{
    public class Aspirante
    {
        public int IdAspirante { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Estado_Prueba { get; set; }
        public string Calificacion { get; set; }
    }
}
