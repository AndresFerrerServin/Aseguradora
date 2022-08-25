using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace PL.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly IHostingEnvironment _hostingEnvironment;

        public EmpleadoController(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }


        public IActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();
            ML.Result resultEnpleado = new ML.Result();
            resultEnpleado.Objects = new List<object>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["WebAPI"]);

                var responseTask = client.GetAsync("api/empleado/GetAll");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Empleado resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Empleado>(resultItem.ToString());
                        resultEnpleado.Objects.Add(resultItemList);
                    }
                }
                empleado.Empleados = resultEnpleado.Objects;
            }

            return View(empleado);
        }

        [HttpGet]
        public IActionResult Form()
        {
            ML.Empleado empleado = new ML.Empleado();
            ML.Result resultEmpresa = BL.Empresa.GetAll();
            ViewBag.Titulo = "Agregar un Cliente";
            ViewBag.Accion = "Agregar";

            empleado.Empresa = new ML.Empresa();
            empleado.Empresa.Empresas = resultEmpresa.Objects.ToList();

            return View(empleado);
        }


        [HttpPost]
        public ActionResult Form(ML.Empleado empleado)
        {
            //Obtener la imagen
            IFormFile file = Request.Form.Files["IFImagen"];

            //Valido si trae la imagen
            if (file != null)
            {
                //LLama al metodo que tranforma a byte la imagen
                byte[] ImagenBytes = ConvertToBytes(file);
                //Convierte a base 64 y guardalo
                empleado.Imagen = Convert.ToBase64String(ImagenBytes);

            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["WebAPI"]);

                var postTask = client.PostAsJsonAsync<ML.Empleado>("api/empleado/Add", empleado);
                postTask.Wait();

                var result = postTask.Result;


                if (result.IsSuccessStatusCode)
                {

                    ViewBag.Message = "Se ha agregado correctamente";
                    return PartialView("Modal");

                }

                else
                {

                    ViewBag.Message = "No se pudo agregar";
                    return PartialView("Modal");

                }
            }
        }

        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
    }
}
