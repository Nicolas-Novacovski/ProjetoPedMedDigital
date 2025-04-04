var clientHml = '81ca0262c82e712e50c580c032d99b60';
var clientProd = '8f468c873a32bb0619eaeb2050ba45d1';

function validacaoAmbienteLogin() {
    var currentURL = window.location.href.toLowerCase();
    let authenticationUrl;
    let redirectUri;
    let clientId;
    if (currentURL.includes('localhost')) {
        authenticationUrl = "https://auth-cs-hml.identidadedigital.pr.gov.br/centralautenticacao/api/v1/authorize/jwt?response_type=token";
        redirectUri = encodeURIComponent("https://localhost:5299/login");
        clientId = clientHml;
    }
    else if (currentURL.includes('homolog')) {
        authenticationUrl = "https://auth-cs-hml.identidadedigital.pr.gov.br/centralautenticacao/api/v1/authorize/jwt?response_type=token";
        redirectUri = encodeURIComponent("https://homolog.agrinho.redacao.seed.pr.gov.br/login");
        clientId = clientHml;
    }
    else {
        authenticationUrl = "https://auth-cs.identidadedigital.pr.gov.br/centralautenticacao/api/v1/authorize/jwt?response_type=token";
        redirectUri = encodeURIComponent("https://agrinho.redacao.seed.pr.gov.br/login");
        clientId = clientProd;
    }

    login(authenticationUrl, clientId, redirectUri);
}
function validacaoAmbienteLogout() {
    var currentURL = window.location.href.toLowerCase();
    let authenticationUrl;
    let redirectUri;
    let clientId;
    if (currentURL.includes('localhost')) {
        authenticationUrl = "https://auth-cs-hml.identidadedigital.pr.gov.br/centralautenticacao/api/v1/authorize/jwt?response_type=token";
        redirectUri = encodeURIComponent("https://localhost:5299/Login");
        clientId = clientHml;
    }
    else if (currentURL.includes('homolog')) {
        authenticationUrl = "https://auth-cs-hml.identidadedigital.pr.gov.br/centralautenticacao/api/v1/authorize/jwt?response_type=token";
        redirectUri = encodeURIComponent("https://agrinho.redacao.seed.pr.gov.br/login");
        clientId = clientHml;
    }
    else {
        authenticationUrl = "https://auth-cs.identidadedigital.pr.gov.br/centralautenticacao/api/v1/authorize/jwt?response_type=token";
        redirectUri = encodeURIComponent("https://agrinho.redacao.seed.pr.gov.br/login");
        clientId = clientProd;
    }

    closeSession(authenticationUrl, clientId, redirectUri);
}
