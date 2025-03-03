document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll('.size-selector input[type="radio"]').forEach(radio => {
        radio.addEventListener('change', function () {
            const pizzaTile = this.closest('.pizza-tile');
            const priceElement = pizzaTile.querySelector('.price');
            const weightElement = pizzaTile.querySelector('.weight');

            priceElement.textContent = `${this.dataset.price}₽`;
            weightElement.textContent = `${this.dataset.weight} гр`;
        });
    });
});

$(document).ready(function () {
    function ShowPizza(pizza) {
        $("#pizzaModalLabel").text(pizza.name);
        $("#modalPizzaImage").attr("src", pizza.imageUrl);
        $("#modalPizzaDescription").text(pizza.description);
        $("#modalPizzaPrice").text(pizza.price);
        $("#modalPizzaWeight").text(pizza.weight);
        $("#modalPizzaDough").text(pizza.dough);

        $("#pizzaModal").modal("show");
    }


    $(document).on("click", ".pizza-title, .clickable-image", function () {
        var pizzaTile = $(this).closest(".pizza-tile");
        var pizzaId = pizzaTile.attr("data-pizza-id");

        if (!pizzaId) {
            return;
        }

        var selectedSize = pizzaTile.find(".size-selector input:checked").val();
        var selectedDough = pizzaTile.find(".dough-selector input:checked").val();

        $.ajax({
            url: "/Home/GetPizzaById",
            type: "GET",
            data: { id: pizzaId },
            dataType: "json",
            success: function (pizza) {
                if (!selectedSize || !(selectedSize in pizza.sizeToPrice)) {
                    selectedSize = Object.keys(pizza.sizeToPrice)[0];
                }

                var updatedPizza = {
                    name: pizza.name,
                    imageUrl: pizza.imageUrl,
                    description: pizza.description,
                    price: pizza.sizeToPrice[selectedSize] + "₽",
                    weight: pizza.sizeToWeight[selectedSize] + " гр",
                    dough: selectedDough || pizza.doughTypes[0]
                };

                ShowPizza(updatedPizza);
            },
            error: function () {
                console.error("Ошибка при загрузке данных о пицце.");
                alert("Ошибка при загрузке данных о пицце.");
            }
        });
    });
});
