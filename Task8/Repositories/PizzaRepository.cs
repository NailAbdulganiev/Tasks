using Microsoft.EntityFrameworkCore;
using Task3.Models;
using Task3.Repositories;
using Task8.Models;

public class PizzaRepository : IPizzaRepository
{
    private readonly PizzaDbContext _context;

    public PizzaRepository(PizzaDbContext context)
    {
        _context = context;
    }

    public List<PizzaModel> GetAllPizzas()
    {
        return _context.Pizzas
            .Include(p => p.PizzaSizes)
            .Select(p => new PizzaModel(p))
            .ToList();
    }

    public PizzaModel? FindById(int id)
    {
        var pizza = _context.Pizzas
            .Include(p => p.PizzaSizes)
            .FirstOrDefault(p => p.Id == id);

        return pizza != null ? new PizzaModel(pizza) : null;
    }
}

