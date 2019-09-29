﻿using Usuarios.Domain.Base;

namespace Usuarios.Domain
{
    public class Usuario : Domain<long>
    {
        public string Name { get; set; }
        public string Last_Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }
}
