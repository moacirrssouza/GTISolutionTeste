using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class EnderecoDTO
    {
        [Required]
        [MaxLength(9)]
        public string CEP { get; set; }

        [Required]
        [MaxLength(200)]
        public string Logradouro { get; set; }

        [Required]
        [MaxLength(10)]
        public string Numero { get; set; }

        [MaxLength(50)]
        public string Complemento { get; set; }

        [Required]
        [MaxLength(50)]
        public string Bairro { get; set; }

        [Required]
        [MaxLength(50)]
        public string Cidade { get; set; }

        [Required]
        [StringLength(2)]
        public string UF { get; set; }

        // Máscara de CEP para exibição
        public string CEPFormatado => string.IsNullOrWhiteSpace(CEP) ? "" : $"{CEP.Substring(0, 5)}-{CEP.Substring(5, 3)}";
    }
}