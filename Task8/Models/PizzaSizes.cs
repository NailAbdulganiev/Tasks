using System;
using System.Collections.Generic;

namespace Task8.Models;

public partial class PizzaSizes
{
    public int Id { get; set; }

    public int PizzaId { get; set; }

    public int Size { get; set; }

    public int Price { get; set; }

    public int Weight { get; set; }

    public bool IsDoughTraditional { get; set; }

    public virtual Pizzas Pizza { get; set; } = null!;
}
