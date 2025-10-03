using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models
{
    public class ClienteEnderecoViewModel
    {
        [Required(ErrorMessage = "CPF é obrigatório")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}\-\d{2}$", ErrorMessage = "CPF inválido")]
        [DisplayName("CPF")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(10, ErrorMessage = "Nome deve ter até 10 caracteres")]
        [DisplayName("Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "RG é obrigatório")]
        [DisplayName("RG")]
        public string RG { get; set; }

        [Required(ErrorMessage = "Data de Expedição é obrigatória")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Data de Expedição")]
        public DateTime? DataExpedicao { get; set; }

        [Required(ErrorMessage = "Órgao Expediçâo é obrigatória")]
        [DisplayName("Órgão de Expedição")]
        public string OrgaoExpedicao { get; set; }

        [Required(ErrorMessage = "UF é obrigatória")]
        [DisplayName("Estado")]
        public string UF { get; set; }

        [Required(ErrorMessage = "Data de nascimento é obrigatória")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Data de Nascimento")]
        public DateTime? DataNascimento { get; set; }

        [Required(ErrorMessage = "Sexo é obrigatório")]
        [DisplayName("Sexo")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "Estado civil é obrigatório")]
        [DisplayName("Estado Cívil")]
        public string EstadoCivil { get; set; }

        public EnderecoViewModel Endereco { get; set; }
    }
}