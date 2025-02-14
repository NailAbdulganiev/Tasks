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