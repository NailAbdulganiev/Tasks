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
            _logger.LogInformation("������ �� ��������� ������ ���� (Index).");
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
        public IActionResult IndexNew()
        {
            _logger.LogInformation("������ �� ��������� ������ ���� (IndexNew).");
            var pizzas = _pizzaRepository.GetAllPizzas();
            return View(pizzas);
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
