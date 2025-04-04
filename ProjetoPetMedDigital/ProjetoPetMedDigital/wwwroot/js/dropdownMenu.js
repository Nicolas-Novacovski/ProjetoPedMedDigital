document.addEventListener('DOMContentLoaded', function () {
    var dropdownMenu = document.getElementById('dropdownMenu');
    var dropdown = document.querySelector('.dropdown');

    // Verifica se ambos os elementos existem antes de adicionar os eventos
    if (dropdownMenu && dropdown) {
        dropdown.addEventListener('mouseenter', function () {
            dropdownMenu.style.display = 'block';
        });

        dropdown.addEventListener('mouseleave', function () {
            dropdownMenu.style.display = 'none';
        });
    }
});