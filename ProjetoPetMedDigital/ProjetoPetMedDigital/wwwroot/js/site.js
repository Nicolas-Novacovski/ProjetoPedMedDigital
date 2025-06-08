// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// --- Funcionalidade 1: Confirmação de Exclusão com Pop-up ---
function confirmDelete(event) {
    if (!confirm("Tem certeza que deseja excluir este registro? Esta ação é irreversível.")) {
        event.preventDefault(); // Impede o envio do formulário se o usuário clicar em "Cancelar"
        return false;
    }
    return true;
}

// --- Funcionalidade 2: Autocompletar Endereço ao Digitar CEP ---
function consultarCep(cep) {
    // Remove qualquer caractere não numérico do CEP
    cep = cep.replace(/\D/g, '');

    if (cep.length != 8) {
        // Se o CEP não tem 8 dígitos, não faz a consulta
        document.querySelector('[name="Endereco"]').value = '';
        document.querySelector('[name="Bairro"]').value = '';
        document.querySelector('[name="Cidade"]').value = '';
        return;
    }

    // Reseta os campos de endereço antes de consultar
    // Isso evita que o campo fique com dados antigos se o novo CEP não for encontrado
    document.querySelector('[name="Endereco"]').value = 'Buscando...';
    document.querySelector('[name="Bairro"]').value = 'Buscando...';
    document.querySelector('[name="Cidade"]').value = 'Buscando...';

    fetch(`https://viacep.com.br/ws/${cep}/json/`)
        .then(response => response.json())
        .then(data => {
            if (!data.erro) { // Se não houver erro na resposta da API (CEP encontrado)
                document.querySelector('[name="Endereco"]').value = data.logradouro;
                document.querySelector('[name="Bairro"]').value = data.bairro;
                document.querySelector('[name="Cidade"]').value = data.localidade;
                // Você pode adicionar outros campos como estado (data.uf) se tiver no seu modelo
            } else {
                // CEP não encontrado
                alert("CEP não encontrado. Por favor, digite o endereço manualmente.");
                document.querySelector('[name="Endereco"]').value = '';
                document.querySelector('[name="Bairro"]').value = '';
                document.querySelector('[name="Cidade"]').value = '';
            }
        })
        .catch(error => {
            console.error("Erro ao consultar CEP:", error);
            alert("Erro ao consultar CEP. Tente novamente mais tarde.");
            document.querySelector('[name="Endereco"]').value = '';
            document.querySelector('[name="Bairro"]').value = '';
            document.querySelector('[name="Cidade"]').value = '';
        });
}

// NOTA: Para que a função consultarCep funcione, você precisa garantir
// que seus campos de endereço no HTML (nas Views de Create e Edit de Clientes/CadastroColaboradores)
// tenham os atributos 'name' corretos, por exemplo:
// <input asp-for="CEP" class="form-control" onblur="consultarCep(this.value)" />
// <input asp-for="Endereco" class="form-control" name="Endereco" />
// <input asp-for="Bairro" class="form-control" name="Bairro" />
// <input asp-for="Cidade" class="form-control" name="Cidade" />
// O 'asp-for' já gera o atributo 'name', mas é bom verificar.