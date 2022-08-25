namespace ML
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Imagen { get; set; }

        public ML.Empresa Empresa { get; set; }
        public List<object> Empleados { get; set; }
    }
}