document.addEventListener('DOMContentLoaded', (event) => {
    let nome = "NomeAluno";
    let serie = "SerieAluno";
    const nomeArquivo = nome + "_" + serie;
    var element = document.getElementById('pageWrapperContainer');
    element.style.fontSize = '10pt';

    var opt = {
        margin: [0.5, 0.5],
        filename: nomeArquivo,
        image: { type: 'jpeg', quality: 0.98 },
        html2canvas: { scale: 3 },
        jsPDF: { unit: 'in', format: 'a4', orientation: 'portrait' },
        pagebreak: { mode: ['avoid-all', 'css', 'legacy'] }
    };
    html2pdf().set(opt).from(element).save().then(function () {
        setTimeout(function () { window.close(); }, 0050);
    });
});