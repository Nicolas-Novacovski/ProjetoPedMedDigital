const $theme2 = document.querySelector('#theme');
$theme2.addEventListener('change', function () {
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