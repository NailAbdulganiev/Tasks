using NLog;
using System.Reflection;

namespace Task3.Models
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
