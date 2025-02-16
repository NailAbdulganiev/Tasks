using Task3.Models;

namespace Task3.Repositories
{
    public class PizzaRepository : IPizzaRepository
    {
        private readonly List<PizzaModel> pizzas = new List<PizzaModel>()
        {
            new PizzaModel
            {
                Id = 0,
                Name = "Новогодняя",
                Description = "Соус 'Гавайский', Сыр моцарелла, Куриная грудка, Мандарины консервированные, Стружка миндаля, Кокосовая стружка.",
                ImageUrl = "/images/new-year.jpg",
                SizeToPrice = new Dictionary<int, int>{ { 30, 550 }, { 40, 740 }, { 60, 1420 } },
                SizeToWeight = new Dictionary<int, int> { { 30, 670 }, { 40, 1220 }, { 60, 2500 } },
            },
            new PizzaModel
            {
                Id = 1,
                Name = "XXXL",
                Description = "Сыр моцарелла, Соус '1000 островов', Куриный рулет, Ветчина, Колбаски охотничьи, Бекон, Сервелат, Огурцы маринованные, Томаты черри, Маслины, Лук маринованный.",
                ImageUrl = "/images/xxxl.jpg",
                SizeToPrice = new Dictionary<int, int>{ { 30, 570 }, { 40, 820 } },
                SizeToWeight = new Dictionary<int, int> { { 30, 740 }, { 40, 1440 } },
            },
            new PizzaModel
            {
                Id = 2,
                Name = "Сырная",
                Description = "Сыр моцарелла, Сливочный сыр, Соус 'Кальяри', Сыр фетаки, Сыр с голубой плесенью, Сыр пармезан.",
                ImageUrl = "/images/cheese.jpg",
                SizeToPrice = new Dictionary<int, int>{ { 30, 570 }, { 40, 810 }, { 60, 1545 } },
                SizeToWeight = new Dictionary<int, int> { { 30, 550 }, { 40, 1000 }, { 60, 2390 } },
            },
            new PizzaModel
            {
                Id = 3,
                Name = "Кальяри",
                Description = "Соус 'Кальяри', Креветки, Куриная грудка, Сыр моцарелла, Ананас, Соус 'Унаги', Кунжут.",
                ImageUrl = "/images/cagliari.jpg",
                SizeToPrice = new Dictionary<int, int>{ { 30, 540 }, { 40, 770 }, { 60, 1620 } },
                SizeToWeight = new Dictionary<int, int> { { 30, 560 }, { 40, 1000 }, { 60, 2370 } },
            },
            new PizzaModel
            {
                Id = 4,
                Name = "Цезарь",
                Description = "Ветчина, Куриная грудка, Соус 'Кальяри', Сыр моцарелла, Салат айсберг, Сыр пармезан, Соус 'Спайси'.",
                ImageUrl = "/images/cezar.jpg",
                SizeToPrice = new Dictionary<int, int>{ { 30, 525 }, { 40, 715 }, { 60, 1470 } },
                SizeToWeight = new Dictionary<int, int> { { 30, 640 }, { 40, 1060 }, { 60, 2380 } },
            },
            new PizzaModel
            {
                Id = 5,
                Name = "Пепперони",
                Description = "Соус 'Томатный', Сыр моцарелла, Пепперони.",
                ImageUrl = "/images/peperoni.jpg",
                SizeToPrice = new Dictionary<int, int>{ { 30, 475 }, { 40, 620 }, { 60, 1260 } },
                SizeToWeight = new Dictionary<int, int> { { 30, 410 }, { 40, 700 }, { 60, 1840 } },
            }
        };

        public List<PizzaModel> GetAllPizzas()
        {
            return pizzas;
        }

        public PizzaModel FindById(int id)
        {
            return pizzas.FirstOrDefault(p => p.Id == id);
        }
    }
}
