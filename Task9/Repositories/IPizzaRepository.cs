using Task9.Models;

namespace Task9.Repositories
{
    public interface IPizzaRepository
    {
        List<PizzaModel> GetAllPizzas();
        PizzaModel? FindById(int id);
        void AddPizza(PizzaModel pizza, IFormFile? imageFile = null);
        void UpdatePizza(PizzaModel pizza, IFormFile? imageFile);
        void DeletePizza(int id);
    }
}
