using Microsoft.EntityFrameworkCore;
using NLog;
using Task8.Models;

namespace Task8.Repositories
{
    public class PizzaRepository : IPizzaRepository
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly PizzaDbContext _context;

        public PizzaRepository(PizzaDbContext context)
        {
            _context = context;
        }

        public List<PizzaModel> GetAllPizzas()
        {
            try
            {
                _logger.Info("Запрос на получение всех пицц из базы данных.");

                var pizzas = _context.Pizzas
                    .Include(p => p.PizzaSizes).ToList()
                    .Select(p => new PizzaModel(p)).ToList();

                return pizzas;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Ошибка при получении списка пицц из базы данных.");
                throw;
            }
        }

        public PizzaModel? FindById(int id)
        {
            try
            {
                _logger.Info($"Поиск пиццы с ID: {id} в базе данных.");

                var pizza = _context.Pizzas
                    .Include(p => p.PizzaSizes)
                    .FirstOrDefault(p => p.Id == id);

                if (pizza == null)
                {
                    _logger.Warn($"Пицца с ID {id} не найдена в базе данных.");
                    return null;
                }

                return new PizzaModel(pizza);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Ошибка при поиске пиццы с ID {id} в базе данных.");
                throw;
            }
        }
    }
}
