using Task3.Models;
using System.Collections.Generic;

namespace Task3.Repositories
{
    public interface IPizzaRepository
    {
        List<PizzaModel> GetAllPizzas();
        PizzaModel FindById(int id);
    }
}
