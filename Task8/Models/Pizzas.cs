using System;
using System.Collections.Generic;

namespace Task8.Models;

public partial class Pizzas
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public virtual ICollection<PizzaSizes> PizzaSizes { get; set; } = new List<PizzaSizes>();
}
