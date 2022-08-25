using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Empleado
    {
        public int IdEmpleado { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Imagen { get; set; }
        public int? IdEmpresa { get; set; }

        public string EmpresaNombre { get; set; }

        public virtual Empresa? IdEmpresaNavigation { get; set; }
    }
}
