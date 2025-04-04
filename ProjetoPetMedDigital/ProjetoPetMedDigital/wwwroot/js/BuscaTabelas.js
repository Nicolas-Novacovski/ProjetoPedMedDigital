var lightThemeImage = '../src/icons/Filtro.png';
var darkThemeImage = '../src/icons/Filtro_Dark.png';
var highThemeImage = '../src/icons/Filtro_High.png';

var lightThemeSearch = '../src/icons/Search.png';
var darkThemeSearch = '../src/icons/Search_Dark.png';
var highThemeSearch = '../src/icons/Search_High.png';

var selectedTheme1 = localStorage.getItem('selectedTheme');
var searchInput = document.querySelector('#search');
var toggleButton = document.querySelector('#toggle-search');
var table = document.querySelector('.table');
var filters = document.querySelectorAll('thead input');

if (selectedTheme1 === 'dark') {
    filterImage.src = darkThemeImage;
    searchImage.src = darkThemeSearch;
} else if (selectedTheme1 === 'high-contrast') {
    filterImage.src = highThemeImage;
    searchImage.src = highThemeSearch;
}
else {
    filterImage.src = lightThemeImage;
    searchImage.src = lightThemeSearch;
}

toggleButton.addEventListener('click', function () {
    searchInput.style.display = searchInput.style.display === 'none' ? 'block' : 'none';

    searchInput.value = '';

    var rows = document.querySelectorAll('tbody tr');
    rows.forEach(function (row) {
        row.style.display = '';
    });
});
searchInput.addEventListener('input', function () {
    var searchValue = searchInput.value.toLowerCase();

    var rows = document.querySelectorAll('tbody tr');

    rows.forEach(function (row) {
        var rowContainsSearchValue = Array.prototype.some.call(row.children, function (cell) {
            return cell.textContent.toLowerCase().includes(searchValue);
        });

        row.style.display = rowContainsSearchValue ? '' : 'none';
    });
});

filters.forEach(function (input) {
    input.addEventListener('input', function () {
        var columnIndex = Array.prototype.indexOf.call(input.parentNode.parentNode.children, input.parentNode);
        filterTable(table, columnIndex, input.value);
    });
});

function filterTable(table, columnIndex, value) {
    var rows = table.querySelectorAll('tbody tr');

    rows.forEach(function (row) {
        var cellValue = row.children[columnIndex].textContent;

        cellValue = cellValue.toLowerCase();
        value = value.toLowerCase();

        if (cellValue.includes(value)) {
            row.style.display = '';
        } else {
            row.style.display = 'none';
        }
    });
}

var toggleButton = document.querySelector('#toggle-filters');

toggleButton.addEventListener('click', function () {
    var filters = document.querySelectorAll('thead input');
    var filtersVisible = filters[0].style.display !== 'none';
    console.log(filtersVisible)
    filters.forEach(function (input) {
        //console.log('entro')
        input.style.display = input.style.display === 'none' ? 'block' : 'none';
        console.log(input.style.display);
    });

    searchInput.value = '';

    var rows = document.querySelectorAll('tbody tr');
    rows.forEach(function (row) {
        row.style.display = '';
    });
});

var toggleButtonFilter = document.querySelector('#toggle-filters');
toggleButtonFilter.addEventListener('click', function () {
    createPaginationButtons();
    var filters = document.querySelectorAll('thead input');
    console.log(filters);
    var filtersVisible = filters[0].style.display !== 'none';
    filters.forEach(function (input) {
        input.style.display = filtersVisible ? 'none' : 'block';
    });
    searchInput.value = '';
    var rows = document.querySelectorAll('tbody tr');
    rows.forEach(function (row) {
        row.style.display = '';
    });
});

var itemsPerPage = parseInt(document.querySelector("#items-per-page").value);

// Número total de entradas na tabela
var totalItems = parseInt(document.querySelector("table tbody").rows.length);

// Número total de páginas
var totalPages = Math.ceil(totalItems / itemsPerPage);

// Container da paginação
var paginationContainer = document.querySelector("#pagination-container");

// Seta a pagina atual para 1 ao iniciar a pagina
var currentPage = Number(1);
function createPaginationButtons() {
    paginationContainer.innerHTML = "";
    var startPage = Number(0);
    var endPage = Number(0);
    if (totalPages < 5) {
        startPage = 1;
        endPage = totalPages;
    }
    else if (currentPage < 3) {
        startPage = 1;
        endPage = 5;
    } else if (currentPage > totalPages - 2) {
        startPage = totalPages - 4;
        endPage = totalPages;
    } else {
        startPage = currentPage - 2;
        endPage = currentPage + 2;
    }

    var li = document.createElement("li");
    li.classList.add("pageItem");
    var a = document.createElement("a");
    a.classList.add("pageLink");
    a.href = "#";
    a.innerText = "«";
    a.addEventListener("click", function (event) {
        event.preventDefault();
        currentPage = 1;
        showPage(1);
        createPaginationButtons();
    });
    li.appendChild(a);
    paginationContainer.appendChild(li);

    for (var i = startPage; i <= endPage; i++) {
        var li = document.createElement("li");
        li.classList.add("pageItem");
        if (i === currentPage) li.classList.add("active");
        var a = document.createElement("a");
        a.classList.add("pageLink");
        a.href = "#";
        a.innerText = i;
        a.addEventListener("click", function (event) {
            event.preventDefault();
            currentPage = Number(this.innerText);
            showPage(currentPage);
            createPaginationButtons();
        });
        li.appendChild(a);
        paginationContainer.appendChild(li);
    }

    var li = document.createElement("li");
    li.classList.add("pageItem");
    var a = document.createElement("a");
    a.classList.add("pageLink");
    a.href = "#";
    a.innerText = "»";
    a.addEventListener("click", function (event) {
        event.preventDefault();
        currentPage = totalPages;
        showPage(totalPages);
        createPaginationButtons();
    });
    li.appendChild(a);
    paginationContainer.appendChild(li);

}
function updateInfoText() {
    var infoText = document.querySelector("#info-text");
    var startNumber = (currentPage - 1) * itemsPerPage + 1;
    var endNumber = startNumber + itemsPerPage - 1;
    if (endNumber > totalItems) endNumber = totalItems;
    infoText.textContent = `Mostrando ${startNumber} a ${endNumber} de ${totalItems} itens`;
}
function showPage(pageNumber) {
    var pageNumberInt = parseInt(pageNumber, 10);
    var start = (pageNumberInt - 1) * itemsPerPage;
    var end = start + itemsPerPage;

    var rows = document.querySelector("table tbody").rows;
    for (var i = 0; i < rows.length; i++) {
        if (i >= start && i < end) {
            rows[i].style.display = "";
        } else {
            rows[i].style.display = "none";
        }
    }

    // Atualiza o estilo dos botões de paginação
    var pageItems = paginationContainer.querySelectorAll(".pageItem");
    pageItems.forEach(function (pageItem) {
        pageItem.classList.remove("active");
    });

    var pageLinks = paginationContainer.querySelectorAll(".pageLink");
    pageLinks.forEach(function (pageLink) {
        if (parseInt(pageLink.innerText, 10) === pageNumberInt) {
            pageLink.parentElement.classList.add("active");
        }
    });
}
document.querySelector("#items-per-page").addEventListener("change", function () {
    itemsPerPage = parseInt(this.value);
    totalPages = Math.ceil(totalItems / itemsPerPage);
    paginationContainer.querySelector(".pageItem:nth-child(2)").classList.add("active");
    currentPage = 1;
    createPaginationButtons();
    showPage(1);
    updateInfoText();
});

createPaginationButtons();
showPage(1);
updateInfoText(); 

document.querySelector("#items-per-page").addEventListener("change", function () {
    itemsPerPage = parseInt(this.value);
    totalPages = Math.ceil(totalItems / itemsPerPage);
    paginationContainer.querySelector(".pageItem:nth-child(2)").classList.add("active");
    currentPage = 1;
    createPaginationButtons();
    showPage(1);
});