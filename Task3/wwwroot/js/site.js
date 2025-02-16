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


$(document).on('click', '.clickable-image', function () {
    const pizzaId = $(this).data('pizza-id');
    window.location.href = `/Home/Detail/${pizzaId}`;
});

$(document).ready(function () {
    $(document).on("click", ".pizza-title", function () {
        var pizzaTile = $(this).closest(".pizza-tile");

        var pizzaName = $(this).text();
        var pizzaImage = pizzaTile.find(".pizza-image").attr("src");
        var pizzaDescription = pizzaTile.find(".pizza-description").text();
        var pizzaPrice = pizzaTile.find(".price").text();
        var pizzaWeight = pizzaTile.find(".weight").text();

        $("#pizzaModalLabel").text(pizzaName);
        $("#modalPizzaImage").attr("src", pizzaImage);
        $("#modalPizzaDescription").text(pizzaDescription);
        $("#modalPizzaPrice").text(pizzaPrice);
        $("#modalPizzaWeight").text(pizzaWeight);

        $("#pizzaModal").modal("show");
    });
});
