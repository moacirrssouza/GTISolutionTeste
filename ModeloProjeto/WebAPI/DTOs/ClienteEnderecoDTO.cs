using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class ClienteEnderecoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF deve conter 11 números.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório.")]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "RG é obrigatório.")]
        [MaxLength(20)]
        public string RG { get; set; }

        [Required]
        public DateTime DataExpedicao { get; set; }

        [Required]
        [MaxLength(20)]
        public string OrgaoExpedicao { get; set; }

        [Required]
        [StringLength(2)]
        public string UF { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }

        [Required]
        [RegularExpression("^(M|F)$", ErrorMessage = "Sexo deve ser 'M' ou 'F'.")]
        public string Sexo { get; set; }

        [Required]
        [MaxLength(20)]
        public string EstadoCivil { get; set; }

        [Required]
        public EnderecoDTO Endereco { get; set; }
    }
}