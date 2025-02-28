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
            _logger.LogInformation("������ �� ��������� ������ ���� (Index).");
            var pizzas = _pizzaRepository.GetAllPizzas();
            return View(pizzas);
        }

        public IActionResult IndexNew()
        {
            _logger.LogInformation("������ �� ��������� ������ ���� (IndexNew).");
            var pizzas = _pizzaRepository.GetAllPizzas();
            return View(pizzas);
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("������� �� �������� Privacy.");
            return View();
        }

        [HttpGet]
        public IActionResult GetPizzas()
        {
            try
            {
                _logger.LogInformation("������ �� ��������� ���� ���� � ������� JSON.");
                var pizzas = _pizzaRepository.GetAllPizzas();
                return Json(pizzas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "������ ��� ��������� ������ ����.");
                return StatusCode(500, new { error = "��������� ������ ��� �������� ������ ����." });
            }
        }

        public IActionResult Detail(int id)
        {
            _logger.LogInformation("������ �� �������� ������� ����� � ID {Id}.", id);
            var pizza = _pizzaRepository.FindById(id);

            if (pizza == null)
            {
                _logger.LogWarning("����� � ID {Id} �� �������.", id);
                return NotFound();
            }

            return View(pizza);
        }

        [HttpGet]
        public IActionResult GetPizzaById(int id)
        {
            try
            {
                _logger.LogInformation("������ �� ��������� ������ � ����� � ID {Id}.", id);
                var pizza = _pizzaRepository.FindById(id);

                if (pizza == null)
                {
                    _logger.LogWarning("����� � ID {Id} �� �������.", id);
                    return NotFound();
                }

                return Json(pizza);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "������ ��� ��������� ������ � ����� � ID {Id}.", id);
                return StatusCode(500, new { error = "��������� ������ ��� �������� ������ � �����." });
            }
        }

        [HttpGet]
        public IActionResult TestException()
        {
            try
            {

                _logger.LogInformation("������ ��������� ������ ���������� � �����������.");
                var pizza = new PizzaModel();
                pizza.ThrowTestException();
                return Ok("���� ��� �� ���������� ��-�� ����������.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "������ � ������ TestException �����������.");
                return StatusCode(500, "��������� �������� ������.");
            }
        }
    }
}
