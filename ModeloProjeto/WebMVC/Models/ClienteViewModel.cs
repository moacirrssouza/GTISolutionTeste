using System;

namespace WebMVC.Models
{
    public class ClienteViewModel
    {
        public int Id { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string RG { get; set; }
        public DateTime DataExpedicao { get; set; }
        public string OrgaoExpedicao { get; set; }
        public string UF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string EstadoCivil { get; set; }
        public virtual EnderecoViewModel Endereco { get; set; }
        public int EnderecoId { get; set; }
    }
}