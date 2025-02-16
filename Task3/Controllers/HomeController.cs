using Microsoft.AspNetCore.Mvc;
using Task3.Repositories;
using Task3.Models;

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

        public IActionResult IndexNew()
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

        public IActionResult Detail(int id)
        {
            var pizza = _pizzaRepository.FindById(id);

            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }
    }
}
