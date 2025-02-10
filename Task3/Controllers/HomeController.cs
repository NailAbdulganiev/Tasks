using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Task3.Models;
using Task3.Repositories;

namespace Task3.Controllers
{
    public class HomeController : Controller
    {
        private readonly PizzaRepository _pizzaRepository = new PizzaRepository();

        public IActionResult Index()
        {
            var pizzas = _pizzaRepository.GetAllPizzas();
            return View(pizzas);
        }
    }
}
