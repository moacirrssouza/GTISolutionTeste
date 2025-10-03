namespace WebAPI.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    namespace WebAPI.DTOs
    {
        public class ClienteDTO
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "CPF é obrigatório.")]
            [RegularExpression(@"^(\d{11}|\d{3}\.\d{3}\.\d{3}-\d{2})$",
             ErrorMessage = "CPF deve conter 11 números ou estar no formato 000.000.000-00.")]
            public string CPF { get; set; }

            [Required(ErrorMessage = "Nome é obrigatório.")]
            [MaxLength(100)]
            public string Nome { get; set; }

            [Required(ErrorMessage = "RG é obrigatório.")]
            [MaxLength(20)]
            public string RG { get; set; }

            [Required(ErrorMessage = "Data de expedição é obrigatória.")]
            public DateTime DataExpedicao { get; set; }

            [Required(ErrorMessage = "Órgão expedidor é obrigatório.")]
            [MaxLength(20)]
            public string OrgaoExpedicao { get; set; }

            [Required(ErrorMessage = "UF é obrigatório.")]
            [StringLength(2)]
            public string UF { get; set; }

            [Required(ErrorMessage = "Data de nascimento é obrigatória.")]
            public DateTime DataNascimento { get; set; }

            [Required(ErrorMessage = "Sexo é obrigatório.")]
            [RegularExpression("^(M|F)$", ErrorMessage = "Sexo deve ser 'M' ou 'F'.")]
            public string Sexo { get; set; }

            [Required(ErrorMessage = "Estado civil é obrigatório.")]
            [MaxLength(20)]
            public string EstadoCivil { get; set; }

            public int EnderecoId { get; set; }
        }
    }
}