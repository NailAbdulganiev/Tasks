using Microsoft.AspNetCore.Mvc;
using Task9.Models;
using Task9.Repositories;

namespace Task9.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPizzaRepository _pizzaRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IPizzaRepository pizzaRepository, ILogger<HomeController> logger)
        {
            _pizzaRepository = pizzaRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Запрос на получение списка пицц (Index).");
            var pizzas = _pizzaRepository.GetAllPizzas();
            return View(pizzas);
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Переход на страницу Privacy.");
            return View();
        }

        [HttpGet]
        public IActionResult GetPizzas()
        {
            try
            {
                _logger.LogInformation("Запрос на получение всех пицц в формате JSON.");
                var pizzas = _pizzaRepository.GetAllPizzas();
                return Json(pizzas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении списка пицц.");
                return StatusCode(500, new { error = "Произошла ошибка при загрузке списка пицц." });
            }
        }
        public IActionResult IndexNew()
        {
            _logger.LogInformation("Запрос на получение списка пицц (IndexNew).");
            var pizzas = _pizzaRepository.GetAllPizzas();
            return View(pizzas);
        }

        public IActionResult Detail(int id)
        {
            _logger.LogInformation("Запрос на просмотр деталей пиццы с ID {Id}.", id);
            var pizza = _pizzaRepository.FindById(id);

            if (pizza == null)
            {
                _logger.LogWarning("Пицца с ID {Id} не найдена.", id);
                return NotFound();
            }

            return View(pizza);
        }

        [HttpGet]
        public IActionResult GetPizzaById(int id)
        {
            try
            {
                _logger.LogInformation("Запрос на получение данных о пицце с ID {Id}.", id);
                var pizza = _pizzaRepository.FindById(id);

                if (pizza == null)
                {
                    _logger.LogWarning("Пицца с ID {Id} не найдена.", id);
                    return NotFound();
                }

                return Json(pizza);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении данных о пицце с ID {Id}.", id);
                return StatusCode(500, new { error = "Произошла ошибка при загрузке данных о пицце." });
            }
        }

        [HttpGet]
        public IActionResult CreateOrEdit(int? id)
        {
            if (id == null)
                return View(new PizzaModel());

            var pizza = _pizzaRepository.FindById(id.Value);
            if (pizza == null)
                return NotFound();

            return View(pizza);
        }

        [HttpPost]
        public IActionResult CreateOrEdit(PizzaModel pizza, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
                return View(pizza);

            if (pizza.Id == 0)
            {
                _pizzaRepository.AddPizza(pizza, imageFile);
            }
            else
            {
                _pizzaRepository.UpdatePizza(pizza, imageFile);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var pizza = _pizzaRepository.FindById(id);
            if (pizza == null)
                return NotFound();

            _pizzaRepository.DeletePizza(id);
            return RedirectToAction("Index");
        }
    }
}
