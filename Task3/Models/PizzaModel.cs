using System.Diagnostics.Contracts;
using System.Reflection;

namespace Task3.Models
{
    public class PizzaModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public Dictionary<int, int> SizeToPrice { get; set; } = new Dictionary<int, int>();
        public Dictionary<int, int> SizeToWeight { get; set; } = new Dictionary<int, int>();
        public string[] DoughTypes { get; set; } = new string[] { "Традиционное", "Толстое" };

    }
}
