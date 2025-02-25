using System;
using System.Collections.Generic;

namespace Task8.Models;

public partial class PizzaSize
{
    public int Id { get; set; }

    public int PizzaId { get; set; }

    public int Size { get; set; }

    public int Price { get; set; }

    public int Weight { get; set; }

    public virtual Pizza Pizza { get; set; } = null!;
}
