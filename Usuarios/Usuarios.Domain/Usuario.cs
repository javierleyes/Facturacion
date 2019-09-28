using Usuarios.Domain.Base;

namespace Usuarios.Domain
{
    public class Usuario : Domain<long>
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string Correo { get; set; }
    }
}
