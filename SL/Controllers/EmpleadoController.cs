using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace SL.Controllers
{
   
    public class EmpleadoController : ControllerBase
    {
        [HttpGet]
        [Route("api/empleado/GetAll")]

        public IActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();
            

            ML.Result result = BL.Empleado.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPost]
        [Route("api/empleado/Add")]
        public IActionResult Post([FromBody] ML.Empleado empleado)
        {

            bool resultEmail = isValidEmailId(empleado.Email);
            bool resultNombre = isValidName(empleado.Nombre);
            bool resultNumber = IsOnlyDigits(empleado.Telefono);

            //if(resultEmail && resultNombre && resultNumber)
            //{
                ML.Result result = BL.Empleado.Add(empleado);
                if (result.Correct)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound(result);
                }
            //}
            //else
            //{
            //    return BadRequest();
            //}
           
           
        }

        public static bool isValidEmailId(string email)
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(email))
                isValid = false;
            else
            {
                try
                {
                    MailAddress m = new MailAddress(email);
                    isValid = true;
                }
                catch (FormatException fx)
                {
                    isValid = false;
                }
            }
            return isValid;
        }

        public static bool isValidName(string nameInput)
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(nameInput))
                isValid = false;
            else
            {

                //process 1
                isValid = Regex.IsMatch(nameInput, @"^[a-zA-Z]+$");

                //process 2
                foreach (char c in nameInput)
                {
                    if (!Char.IsLetter(c))
                        isValid = false;
                }

            }
            return isValid;
        }


        public static bool IsOnlyDigits(string inputString)
        {
            bool isValid = true;

            foreach (char c in inputString)
            {
                if (!Char.IsDigit(c))
                    isValid = false;
            }
            return isValid;
        }


    }
}
