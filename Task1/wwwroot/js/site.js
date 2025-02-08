document.addEventListener("DOMContentLoaded", function () {
    generatePizzaTiles();
});

const pizzas = [
    {
        name: "Новогодняя",
        image: "new-year.jpg",
        description: "Соус 'Гавайский', Сыр моцарелла, Куриная грудка, Мандарины, Миндаль, Кокосовая стружка.",
        prices: { "30": 550, "40": 740, "60": 1420 },
        weight: { "30": 670, "40": 1220, "60": 2500 }
    },
    {
        name: "XXXL",
        image: "xxxl.jpg",
        description: "Сыр моцарелла, Соус '1000 островов', Ветчина, Бекон, Колбаски, Томаты черри, Маслины.",
        prices: { "30": 570, "40": 820 },
        weight: { "30": 740, "40": 1440 }
    },
    {
        name: "Сырная",
        image: "cheese.jpg",
        description: "Сыр моцарелла, Сливочный сыр, Соус 'Кальяри', Сыр фетаки, Сыр пармезан.",
        prices: { "30": 570, "40": 810, "60": 1545 },
        weight: { "30": 550, "40": 1000, "60": 2390 }
    },
    {
        name: "Кальяри",
        image: "cagliari.jpg",
        description: "Соус 'Кальяри', Креветки, Куриная грудка, Сыр моцарелла, Ананас, Соус 'Унаги', Кунжут.",
        prices: { "30": 540, "40": 770, "60": 1620 },
        weight: { "30": 560, "40": 1000, "60": 2370 }
    },
    {
        name: "Цезарь",
        image: "cezar.jpg",
        description: "Ветчина, Куриная грудка, Соус 'Кальяри', Сыр моцарелла, Салат айсберг, Сыр пармезан, Соус 'Спайси'.",
        prices: { "30": 525, "40": 715, "60": 1470 },
        weight: { "30": 640, "40": 1060, "60": 2380}
    },
    {
        name: "Пепперони",
        image: "peperoni.jpg",
        description: "Соус 'Томатный', Сыр моцарелла, Пепперони.",
        prices: { "30": 475, "40": 620, "60": 1260 },
        weight: { "30": 410, "40": 700, "60": 1840 }
    }
];

function generatePizzaTiles() {
    const container = document.querySelector(".pizza-container");
    if (!container) {
        console.error("Контейнер .pizza-container не найден!");
        return;
    }

    container.innerHTML = "";

    pizzas.forEach((pizza, index) => {
        const pizzaHtml = `
            <div class="pizza-tile" id="pizza${index + 1}">
                <img src="/images/${pizza.image}" alt="${pizza.name}">
                <h3>${pizza.name}</h3>
                <p>${pizza.description}</p>
                <div class="size-selector">
                    ${generateSizeOptions(pizza, index)}
                </div>
                <div class="dough-selector">
                    <input type="radio" id="dough_${index}_thin" name="dough_${index}" value="thin" checked>
                    <label for="dough_${index}_thin">Традиционное</label>
                    <input type="radio" id="dough_${index}_thick" name="dough_${index}" value="thick">
                    <label for="dough_${index}_thick">Толстое</label>
                </div>
                <div class="price-weight">
                    <span class="price">${Object.values(pizza.prices)[0]}₽</span>
                    <span class="weight">${Object.values(pizza.weight)[0]} гр</span>
                </div>
                <button>В корзину</button>
            </div>
        `;
        container.innerHTML += pizzaHtml;
    });

    document.querySelectorAll(".size-selector input").forEach(input => {
        input.addEventListener("change", updatePriceAndWeight);
    });
}

function generateSizeOptions(pizza, index) {
    return [30, 40, 60].map(size => {
        const isDisabled = !pizza.prices[size];
        return `
            <input type="radio" id="size_${index}_${size}" name="size_${index}" value="${size}" ${isDisabled ? "disabled" : ""}>
            <label for="size_${index}_${size}" style="${isDisabled ? "color: gray; opacity: 0.5; pointer-events: none; cursor: not-allowed;" : ""}">
                ${size} см
            </label>
        `;
    }).join("");
}


function updatePriceAndWeight() {
    const selectedSize = this.value;
    const pizzaIndex = this.name.split("_")[1];

    const pizza = pizzas[pizzaIndex];
    if (!pizza) return;

    const tile = document.getElementById(`pizza${parseInt(pizzaIndex) + 1}`);
    tile.querySelector(".price").textContent = `${pizza.prices[selectedSize]}₽`;
    tile.querySelector(".weight").textContent = `${pizza.weight[selectedSize]} гр`;
}