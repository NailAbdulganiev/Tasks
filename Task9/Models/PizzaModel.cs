using NLog;
using System.Reflection;

namespace Task9.Models
{
    public class PizzaModel
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public Dictionary<int, int> SizeToPrice { get; set; } = new Dictionary<int, int>();
        public Dictionary<int, int> SizeToWeight { get; set; } = new Dictionary<int, int>();
        public string[] DoughTypes { get; set; } = ["Традиционное", "Толстое"];

        public PizzaModel(Pizza pizza)
        {
            Id = pizza.Id;
            Name = pizza.Name;
            Description = pizza.Description;
            ImageUrl = pizza.ImageUrl;
            SizeToPrice = new Dictionary<int, int>();
            SizeToWeight = new Dictionary<int, int>();

            if (pizza.PizzaSizes != null)
            {
                foreach (var size in pizza.PizzaSizes)
                {
                    SizeToPrice[size.Size] = size.Price;
                    SizeToWeight[size.Size] = size.Weight;
                }
            }
        }

        public PizzaModel() { }

        public void ThrowTestException()
        {
            try
            {
                throw new Exception("Тестовое исключение в PizzaModel.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Ошибка в PizzaModel: {MethodName}", MethodBase.GetCurrentMethod()?.Name);
                throw;
            }
        }
    }
}
