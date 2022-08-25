using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Empleado
    {
        public static ML.Result Add(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AseguradoraContext context = new DL.AseguradoraContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoAdd '{empleado.Nombre}','{empleado.Telefono}','{empleado.Email}','{empleado.Imagen}',{empleado.Empresa.IdEmpresa}");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se inserto el registro";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AseguradoraContext context = new DL.AseguradoraContext())
                {

                    var query = context.Empleados.FromSqlRaw($"EmpleadoGetAll").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Empleado empleado = new ML.Empleado();
                            empleado.IdEmpleado = obj.IdEmpleado;

                            empleado.Nombre = obj.Nombre;
                            empleado.Telefono = obj.Telefono;
                            empleado.Email = obj.Email;
                            empleado.Imagen = obj.Imagen;

                            empleado.Empresa = new ML.Empresa();
                            empleado.Empresa.IdEmpresa = obj.IdEmpresa.Value;
                            empleado.Empresa.Nombre = obj.EmpresaNombre;


                            result.Objects.Add(empleado);

                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}