using Microsoft.AspNetCore.Mvc;
using Task3.Repositories;

namespace Task3.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPizzaRepository _pizzaRepository;

        public HomeController(IPizzaRepository pizzaRepository)
        {
            _pizzaRepository = pizzaRepository;
        }

        public IActionResult Index()
        {
            var pizzas = _pizzaRepository.GetAllPizzas();
            return View(pizzas);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetPizzas()
        {
            try
            {
                var pizzas = _pizzaRepository.GetAllPizzas();
                return Json(pizzas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}