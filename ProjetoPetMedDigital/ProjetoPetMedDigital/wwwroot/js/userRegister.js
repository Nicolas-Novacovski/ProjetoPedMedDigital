const cpfInput1 = document.querySelector('#cadastrarCPFSubstituto');

cpfInput1.addEventListener('input', (event) => {
    let value = event.target.value;
    value = value.replace(/\D/g, '');
    if (value.length > 11) {
        value = value.slice(0, 11);
    }
    value = value.replace(/(\d{3})(\d)/, '$1.$2');
    value = value.replace(/(\d{3})(\d)/, '$1.$2');
    value = value.replace(/(\d{3})(\d{1,2})/, '$1-$2');
    event.target.value = value;
});

cpfInput1.addEventListener('blur', (event) => {
    const value = event.target.value.replace(/\D/g, '');
    if (value.length === 11) {
        if (!validarCPF(value)) {
            alert('CPF inválido!');
        } else if (todosNumerosIguais(value)) {
            alert('CPF inválido!');
        }
    }
});

function validarCPF(cpf) {
    cpf = cpf.replace(/\D/g, ''); // Remove caracteres não numéricos
    if (cpf.length !== 11) {
        return false; // CPF deve ter 11 dígitos
    }

    // Calcula os dígitos verificadores
    let soma = 0;
    for (let i = 0; i < 9; i++) {
        soma += parseInt(cpf.charAt(i)) * (10 - i);
    }
    const resto = soma % 11;
    const digito1 = resto < 2 ? 0 : 11 - resto;

    soma = 0;
    for (let i = 0; i < 10; i++) {
        soma += parseInt(cpf.charAt(i)) * (11 - i);
    }
    const resto2 = soma % 11;
    const digito2 = resto2 < 2 ? 0 : 11 - resto2;

    return parseInt(cpf.charAt(9)) === digito1 && parseInt(cpf.charAt(10)) === digito2;
}

function todosNumerosIguais(cpf) {
    const primeiroDigito = cpf.charAt(0);
    for (let i = 1; i < cpf.length; i++) {
        if (cpf.charAt(i) !== primeiroDigito) {
            return false;
        }
    }
    return true;
}

const telefoneInput = document.querySelector('#cadastrarTelSubstituto');

let telefoneChanged = false; // Variável para rastrear se o telefone foi modificado

telefoneInput.addEventListener('input', (event) => {
    let value = event.target.value;
    value = value.replace(/\D/g, ''); // Remove caracteres não numéricos

    if (value.length > 11) {
        value = value.slice(0, 11); // Limita o número de caracteres a 11
    }

    if (value.length === 11) {
        value = value.replace(/(\d{2})(\d{5})(\d{4})/, '($1) $2-$3'); // Formata como (XX) XXXXX-XXXX
    } else if (value.length === 10) {
        value = value.replace(/(\d{2})(\d{4})(\d{4})/, '($1) $2-$3'); // Formata como (XX) XXXX-XXXX
    }

    event.target.value = value;
    telefoneChanged = true; // Marca o telefone como modificado quando algo é digitado
});

telefoneInput.addEventListener('blur', (event) => {
    if (telefoneChanged && event.target.value.length < 10) {
        alert('Número de telefone deve ter pelo menos 10 dígitos.');
    }
});

const emailInput = document.querySelector('#cadastrarEmailSubstituto');
let emailChanged = false; // Variável para rastrear se o e-mail foi modificado

emailInput.addEventListener('input', (event) => {
    emailChanged = true; // Marca o e-mail como modificado quando algo é digitado
});

emailInput.addEventListener('blur', (event) => {
    if (emailChanged) { // Verifica se o e-mail foi modificado antes de validar
        const value = event.target.value;
        if (!validarEmail(value)) {
            alert('E-mail inválido');
        }
    }
});

function validarEmail(email) {
    // Utilize uma expressão regular simples para validar o e-mail
    const regex = /^[a-zA-Z0-9._-]+@@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    return regex.test(email);
}