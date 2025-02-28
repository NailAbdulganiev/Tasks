using Task8.Models;
using System.Collections.Generic;

namespace Task8.Repositories
{
    public interface IPizzaRepository
    {
        List<PizzaModel> GetAllPizzas();
        PizzaModel? FindById(int id);
    }
}
