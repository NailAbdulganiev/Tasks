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