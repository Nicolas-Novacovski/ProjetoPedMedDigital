const $html = document.querySelector('html');
const $theme = document.querySelector('#theme');
const $fontSize = document.querySelector('#fontSize');
const $spacing = document.querySelector('#spacing');
const $btnRadio1 = document.querySelector('#btnradio1');
const $btnRadio2 = document.querySelector('#btnradio2');
const $btnRadio3 = document.querySelector('#btnradio3');
const $btnRadio4 = document.querySelector('#btnradio4');
const $btnRadio5 = document.querySelector('#btnradio5');
const $btnRadio6 = document.querySelector('#btnradio6');
const $btnRadio7 = document.querySelector('#btnradio7');


$theme.addEventListener('change', function () {
    let selectedTheme = '';

    if ($btnRadio1.checked) {
        $html.classList.remove('dark-mode');
        $html.classList.remove('high-contrast');
        localStorage.removeItem('selectedTheme');
        selectedTheme = '';
        window.location.reload();
    } else if ($btnRadio2.checked) {
        $html.classList.add('dark-mode');
        $html.classList.remove('high-contrast');
        localStorage.setItem('selectedTheme', 'dark');
        selectedTheme = 'dark';
        window.location.reload();
    } else if ($btnRadio3.checked) {
        $html.classList.remove('dark-mode');
        $html.classList.add('high-contrast');
        localStorage.setItem('selectedTheme', 'high-contrast');
        selectedTheme = 'high-contrast';
        window.location.reload();
    }
});
//-------------*********-------------


// Função para definir o tema com base no localStorage
function setThemeFromStorage() {
    const storedTheme = localStorage.getItem('selectedTheme');

    if (storedTheme === 'dark') {
        $btnRadio2.checked = true;
        $html.classList.add('dark-mode');
    } else if (storedTheme === 'high-contrast') {
        $btnRadio3.checked = true;
        $html.classList.add('high-contrast');
    }

}
// Função para definir o tamanho da fonte com base no localStorage
function setFontSizeFromStorage() {
    const storedFontSize = localStorage.getItem('fontSizePreference');

    //console.log('fontSize ', storedFontSize);
    if (storedFontSize === 'bigger') {
        $html.classList.add('bigger-font');
        $btnRadio5.checked = true;
    }
}

// Função para definir o espaçamento com base no localStorage
function setSpacingFromStorage() {
    const storedSpacing = localStorage.getItem('spacingPreference');

    //console.log('spacing ', storedSpacing);
    if (storedSpacing === 'bigger') {
        $html.classList.add('bigger-spacing');
        $btnRadio7.checked = true;
    }
}


//-------------*********-------------


// Carregar o tema armazenado ao carregar a página
document.addEventListener('DOMContentLoaded', setThemeFromStorage);

$theme.addEventListener('change', function () {
    if ($btnRadio1.checked) {
        $html.classList.remove('dark-mode');
        $html.classList.remove('high-contrast');
        localStorage.removeItem('selectedTheme');
    } else if ($btnRadio2.checked) {
        $html.classList.add('dark-mode');
        $html.classList.remove('high-contrast');
        localStorage.setItem('selectedTheme', 'dark');
    } else if ($btnRadio3.checked) {
        $html.classList.remove('dark-mode');
        $html.classList.add('high-contrast');
        localStorage.setItem('selectedTheme', 'high-contrast');
    }
});

// Carregar o tamanho de fonte armazenado ao carregar a página
document.addEventListener('DOMContentLoaded', setFontSizeFromStorage);

$fontSize.addEventListener('change', function () {
    if ($btnRadio4.checked) {
        $html.classList.remove('bigger-font');
        localStorage.setItem('fontSizePreference', 'normal');
    } else {
        $html.classList.add('bigger-font');
        localStorage.setItem('fontSizePreference', 'bigger');
    }
});

// Carregar o espaçamento armazenado ao carregar a página
document.addEventListener('DOMContentLoaded', setSpacingFromStorage);

$spacing.addEventListener('change', function () {
    if ($btnRadio6.checked) {
        $html.classList.remove('bigger-spacing');
        localStorage.setItem('spacingPreference', 'normal');
    } else {
        $html.classList.add('bigger-spacing');
        localStorage.setItem('spacingPreference', 'bigger');
    }
});