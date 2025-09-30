using System;

namespace WCFServiceHost.Validation
{
    public class ValidacaoCadastro
    {
        public void ValidateCliente(Cliente cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente), "Cliente não pode ser nulo.");

            if (string.IsNullOrWhiteSpace(cliente.CPF))
                throw new ArgumentException("CPF é obrigatório.", nameof(cliente.CPF));

            if (cliente.CPF.Length != 11)
                throw new ArgumentException("CPF deve ter 11 dígitos.", nameof(cliente.CPF));

            if (string.IsNullOrWhiteSpace(cliente.Nome))
                throw new ArgumentException("Nome é obrigatório.", nameof(cliente.Nome));

            if (string.IsNullOrWhiteSpace(cliente.RG))
                throw new ArgumentException("RG é obrigatório.", nameof(cliente.RG));

            if (cliente.DataExpedicao == default)
                throw new ArgumentException("Data de expedição inválida.", nameof(cliente.DataExpedicao));

            if (string.IsNullOrWhiteSpace(cliente.OrgaoExpedicao))
                throw new ArgumentException("Órgão expedidor é obrigatório.", nameof(cliente.OrgaoExpedicao));

            if (string.IsNullOrWhiteSpace(cliente.UF) || cliente.UF.Length != 2)
                throw new ArgumentException("UF inválida.", nameof(cliente.UF));

            if (cliente.DataNascimento == default)
                throw new ArgumentException("Data de nascimento inválida.", nameof(cliente.DataNascimento));

            if (string.IsNullOrWhiteSpace(cliente.Sexo))
                throw new ArgumentException("Sexo é obrigatório.", nameof(cliente.Sexo));

            if (string.IsNullOrWhiteSpace(cliente.EstadoCivil.ToString()))
                throw new ArgumentException("Estado civil é obrigatório.", nameof(cliente.EstadoCivil));

            if (cliente.Endereco == null)
                throw new ArgumentException("Endereço é obrigatório.", nameof(cliente.Endereco));

            // Validação do endereço
            if (string.IsNullOrWhiteSpace(cliente.Endereco.CEP))
                throw new ArgumentException("CEP é obrigatório.", nameof(cliente.Endereco.CEP));

            if (string.IsNullOrWhiteSpace(cliente.Endereco.Logradouro))
                throw new ArgumentException("Logradouro é obrigatório.", nameof(cliente.Endereco.Logradouro));

            if (string.IsNullOrWhiteSpace(cliente.Endereco.Numero))
                throw new ArgumentException("Número é obrigatório.", nameof(cliente.Endereco.Numero));

            if (string.IsNullOrWhiteSpace(cliente.Endereco.Bairro))
                throw new ArgumentException("Bairro é obrigatório.", nameof(cliente.Endereco.Bairro));

            if (string.IsNullOrWhiteSpace(cliente.Endereco.Cidade))
                throw new ArgumentException("Cidade é obrigatória.", nameof(cliente.Endereco.Cidade));

            if (string.IsNullOrWhiteSpace(cliente.Endereco.UF.ToString()))
                throw new ArgumentException("UF do endereço inválida.", nameof(cliente.Endereco.UF));
        }
    }
}