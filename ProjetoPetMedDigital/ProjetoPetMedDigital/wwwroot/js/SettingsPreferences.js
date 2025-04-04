document.addEventListener('DOMContentLoaded', function () {
    const $html = document.querySelector('html');
    const storedTheme = localStorage.getItem('selectedTheme');
    const fontSize = localStorage.getItem('fontSizePreference');
    const spacing = localStorage.getItem('spacingPreference');

    if (storedTheme === 'dark') {
        $html.classList.add('dark-mode');
    } else if (storedTheme === 'high-contrast') {
        $html.classList.add('high-contrast');
    }

    if (fontSize === 'bigger') {
        $html.classList.add('bigger-font');
    }

    if (spacing === 'bigger') {
        $html.classList.add('bigger-spacing');
    }
});