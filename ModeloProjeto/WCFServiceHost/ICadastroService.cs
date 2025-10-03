using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WCFServiceHost
{
    [ServiceContract]
    public interface ICadastroService
    {
        [OperationContract]
        void CriarCliente(Cliente cliente);

        [OperationContract]
        Cliente BuscarClientePorId(int id);

        [OperationContract]
        void AlterarCliente(Cliente cliente);

        [OperationContract]
        void ExcluirCliente(int id);

        [OperationContract]
        List<Cliente> ListarClientes();
    }

    [DataContract]
    public class Endereco
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string CEP { get; set; }

        [DataMember]
        public string Logradouro { get; set; }

        [DataMember]
        public string Numero { get; set; }

        [DataMember]
        public string Complemento { get; set; }

        [DataMember]
        public string Bairro { get; set; }

        [DataMember]
        public string Cidade { get; set; }

        [DataMember]
        public string UF { get; set; }
    }

    [DataContract]
    public class Cliente
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string CPF { get; set; }

        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public string RG { get; set; }

        [DataMember]
        public DateTime DataExpedicao { get; set; }

        [DataMember]
        public string OrgaoExpedicao { get; set; }

        [DataMember]
        public string UF { get; set; }

        [DataMember]
        public DateTime DataNascimento { get; set; }

        [DataMember]
        public string Sexo { get; set; }

        [DataMember]
        public string EstadoCivil { get; set; }

        [DataMember]
        public Endereco Endereco { get; set; }
    }
}