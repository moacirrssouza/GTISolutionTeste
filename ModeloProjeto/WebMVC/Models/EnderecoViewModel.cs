using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models
{
    public class EnderecoViewModel
    {
        public int EnderecoId { get; set; }

        [Required(ErrorMessage = "CEP é obrigatório")]
        [RegularExpression(@"^\d{5}\-?\d{3}$", ErrorMessage = "CEP inválido")]
        [DisplayName("CEP")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "Logradouro é obrigatório")]
        [DisplayName("Logradouro")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Número é obrigatório")]
        [DisplayName("Número")]
        public string Numero { get; set; }

        [DisplayName("Complemento")]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "Bairro é obrigatório")]
        [DisplayName("Bairro")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Cidade é obrigatória")]
        [DisplayName("Cidade")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "UF é obrigatória")]
        [DisplayName("Estado")]
        public string UF { get; set; }
    }
}