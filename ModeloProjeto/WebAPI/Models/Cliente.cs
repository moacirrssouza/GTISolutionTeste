using System;

namespace WebAPI.Models
{
    public class Cliente
    {
        /// <summary>
        ///
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RG { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DataExpedicao { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ÓrgaoExpedicao { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UF { get; set; } 

        /// <summary>
        /// 
        /// </summary>
        public DateTime DataNascimento { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Sexo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EstadoCivil { get; set; }
    }
}