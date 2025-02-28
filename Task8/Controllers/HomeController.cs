using Microsoft.AspNetCore.Mvc;
using Task8.Models;
using Task8.Repositories;

namespace Task8.Controllers
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

        public IActionResult IndexNew()
        {
            _logger.LogInformation("Запрос на получение списка пицц (IndexNew).");
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
        public IActionResult TestException()
        {
            try
            {

                _logger.LogInformation("Запуск тестового метода исключения в контроллере.");
                var pizza = new PizzaModel();
                pizza.ThrowTestException();
                return Ok("Этот код не выполнится из-за исключения.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в методе TestException контроллера.");
                return StatusCode(500, "Произошла тестовая ошибка.");
            }
        }
    }
}
