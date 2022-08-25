using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Empresa
    {
        public Empresa()
        {
            Empleados = new HashSet<Empleado>();
        }

        public int IdEmpresa { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
