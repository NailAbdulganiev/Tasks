using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using NLog;
using Task9.Models;

namespace Task9.Repositories
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
                return _context.Pizzas
                    .Include(p => p.PizzaSizes)
                    .ToList()
                    .Select(p => new PizzaModel(p))
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Ошибка при получении списка пицц.");
                throw;
            }
        }

        public PizzaModel? FindById(int id)
        {
            try
            {
                _logger.Info($"Поиск пиццы с ID: {id}.");
                var pizza = _context.Pizzas
                    .Include(p => p.PizzaSizes)
                    .FirstOrDefault(p => p.Id == id);

                return pizza != null ? new PizzaModel(pizza) : null;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Ошибка при поиске пиццы с ID {id}.");
                throw;
            }
        }

        public void AddPizza(PizzaModel pizzaModel, IFormFile? imageFile)
        {
            var newPizza = new Pizza
            {
                Name = pizzaModel.Name,
                Description = pizzaModel.Description,
                ImageUrl = "",
                PizzaSizes = new List<PizzaSize>()
            };

            foreach (var size in pizzaModel.SizeToPrice.Keys)
            {
                newPizza.PizzaSizes.Add(new PizzaSize
                {
                    Size = size,
                    Price = pizzaModel.SizeToPrice[size],
                    Weight = pizzaModel.SizeToWeight.TryGetValue(size, out int weight) ? weight : 0,
                    Pizza = newPizza
                });
            }

            _context.Pizzas.Add(newPizza);
            _context.SaveChanges();

            if (imageFile != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = $"pizza{newPizza.Id}{Path.GetExtension(imageFile.FileName)}";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }

                newPizza.ImageUrl = $"/images/{uniqueFileName}";
                _context.Pizzas.Update(newPizza);
                _context.SaveChanges();
            }
        }

        public void UpdatePizza(PizzaModel pizzaModel, IFormFile? imageFile)
        {
            var existingPizza = _context.Pizzas.Include(p => p.PizzaSizes).FirstOrDefault(p => p.Id == pizzaModel.Id);

            if (existingPizza == null)
                return;

            existingPizza.Name = pizzaModel.Name;
            existingPizza.Description = pizzaModel.Description;

            if (imageFile != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = $"pizza{existingPizza.Id}{Path.GetExtension(imageFile.FileName)}";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }

                existingPizza.ImageUrl = $"/images/{uniqueFileName}";
            }

            existingPizza.PizzaSizes.Clear();
            foreach (var kvp in pizzaModel.SizeToPrice)
            {
                existingPizza.PizzaSizes.Add(new PizzaSize
                {
                    Size = kvp.Key,
                    Price = kvp.Value,
                    Weight = pizzaModel.SizeToWeight.TryGetValue(kvp.Key, out int weight) ? weight : 0
                });
            }

            _context.Pizzas.Update(existingPizza);
            _context.SaveChanges();
        }


        public void DeletePizza(int id)
        {
            try
            {
                _logger.Info($"Удаление пиццы с ID {id}.");
                var pizza = _context.Pizzas.Include(p => p.PizzaSizes).FirstOrDefault(p => p.Id == id);
                if (pizza != null)
                {
                    _context.Pizzas.Remove(pizza);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Ошибка при удалении пиццы ID {id}.");
                throw;
            }
        }
    }
}
