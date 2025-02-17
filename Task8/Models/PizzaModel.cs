using Task8.Models;

public class PizzaModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public Dictionary<int, int> SizeToPrice { get; set; } = new();
    public Dictionary<int, int> SizeToWeight { get; set; } = new();
    public bool IsDoughTraditional { get; set; }

    public PizzaModel(Pizzas pizza)
    {
        Id = pizza.Id;
        Name = pizza.Name;
        Description = pizza.Description;
        ImageUrl = pizza.ImageUrl;

        // Заполняем словари
        if (pizza.PizzaSizes != null)
        {
            foreach (var size in pizza.PizzaSizes)
            {
                if (size.IsDoughTraditional)
                {
                    SizeToPrice[size.Size] = size.Price;
                    SizeToWeight[size.Size] = size.Weight;
                }
            }
        }
    }
    public PizzaModel() { }
}
