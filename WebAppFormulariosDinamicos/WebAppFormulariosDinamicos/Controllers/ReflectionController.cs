using Microsoft.AspNetCore.Mvc;
using WebAppFormulariosDinamicos.Models.Reflections;

namespace WebAppFormulariosDinamicos.Controllers
{
    public class ReflectionController : Controller
    {
        public IActionResult DynamicForm()
        {
            //Persona persona = new Persona() { Name="Curtis", Age=22};
            Persona personaConDomicilio = new PersonaDomicilio() { Name="Curtis", Age=22, Domicilio="Suipacha"};


            return View(personaConDomicilio);
        }
    }
}
