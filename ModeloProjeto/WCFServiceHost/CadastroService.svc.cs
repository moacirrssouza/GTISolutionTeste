using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace WCFServiceHost
{
    public class CadastroService : ICadastroService
    {
        private readonly string connectionString;

        public CadastroService()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ConexaoDemoDB"].ConnectionString;
        }

        public void CriarCliente(Cliente cliente)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                int enderecoId;
                using (SqlCommand cmdEndereco = new SqlCommand(@" INSERT INTO Endereco (CEP, Logradouro, Numero, Complemento, Bairro, Cidade, UF)
                                                                  VALUES (@CEP, @Logradouro, @Numero, @Complemento, @Bairro, @Cidade, @UF);
                                                                  SELECT CAST(SCOPE_IDENTITY() AS INT);", conn))
                {
                    cmdEndereco.Parameters.AddWithValue("@CEP", cliente.Endereco.CEP);
                    cmdEndereco.Parameters.AddWithValue("@Logradouro", cliente.Endereco.Logradouro);
                    cmdEndereco.Parameters.AddWithValue("@Numero", cliente.Endereco.Numero);
                    cmdEndereco.Parameters.AddWithValue("@Complemento", (object)cliente.Endereco.Complemento ?? DBNull.Value);
                    cmdEndereco.Parameters.AddWithValue("@Bairro", cliente.Endereco.Bairro);
                    cmdEndereco.Parameters.AddWithValue("@Cidade", cliente.Endereco.Cidade);
                    cmdEndereco.Parameters.AddWithValue("@UF", cliente.Endereco.UF);
                    enderecoId = (int)cmdEndereco.ExecuteScalar();
                }

                using (SqlCommand cmd = new SqlCommand(@" INSERT INTO Cliente 
                                                        (CPF, Nome, RG, DataExpedicao, OrgaoExpedicao, UF, DataNascimento, Sexo, EstadoCivil, EnderecoId)
                                                        VALUES 
                                                        (@CPF, @Nome, @RG, @DataExpedicao, @OrgaoExpedicao, @UF, @DataNascimento, @Sexo, @EstadoCivil, @EnderecoId);",
                                                        conn))
                {
                    cmd.Parameters.AddWithValue("@CPF", cliente.CPF);
                    cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                    cmd.Parameters.AddWithValue("@RG", cliente.RG);
                    cmd.Parameters.AddWithValue("@DataExpedicao", cliente.DataExpedicao);
                    cmd.Parameters.AddWithValue("@OrgaoExpedicao", cliente.OrgaoExpedicao);
                    cmd.Parameters.AddWithValue("@UF", cliente.UF);
                    cmd.Parameters.AddWithValue("@DataNascimento", cliente.DataNascimento);
                    cmd.Parameters.AddWithValue("@Sexo", cliente.Sexo);
                    cmd.Parameters.AddWithValue("@EstadoCivil", cliente.EstadoCivil);
                    cmd.Parameters.AddWithValue("@EnderecoId", enderecoId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Cliente BuscarClientePorId(int id)
        {
            Cliente cliente = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @" SELECT c.*, 
                                e.Id AS EnderecoId, e.CEP, e.Logradouro, e.Numero, e.Complemento,
                                e.Bairro, e.Cidade, e.UF AS EnderecoUF
                                FROM Cliente c
                                INNER JOIN Endereco e ON c.EnderecoId = e.Id
                                WHERE c.Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cliente = MapCliente(reader);
                        }
                    }
                }
            }
            return cliente;
        }

        public void AlterarCliente(Cliente cliente)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(@"UPDATE Cliente SET
                                                        CPF=@CPF, Nome=@Nome, RG=@RG, DataExpedicao=@DataExpedicao,
                                                        OrgaoExpedicao=@OrgaoExpedicao, UF=@UF,
                                                        DataNascimento=@DataNascimento, Sexo=@Sexo, EstadoCivil=@EstadoCivil
                                                        WHERE Id=@Id", 
                                                        conn))
                {
                    cmd.Parameters.AddWithValue("@Id", cliente.Id);
                    cmd.Parameters.AddWithValue("@CPF", cliente.CPF);
                    cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                    cmd.Parameters.AddWithValue("@RG", cliente.RG);
                    cmd.Parameters.AddWithValue("@DataExpedicao", cliente.DataExpedicao);
                    cmd.Parameters.AddWithValue("@OrgaoExpedicao", cliente.OrgaoExpedicao);
                    cmd.Parameters.AddWithValue("@UF", cliente.UF);
                    cmd.Parameters.AddWithValue("@DataNascimento", cliente.DataNascimento);
                    cmd.Parameters.AddWithValue("@Sexo", cliente.Sexo);
                    cmd.Parameters.AddWithValue("@EstadoCivil", cliente.EstadoCivil);
                    cmd.ExecuteNonQuery();
                }

                using (SqlCommand cmdEndereco = new SqlCommand(@"UPDATE Endereco SET
                                                                CEP=@CEP, Logradouro=@Logradouro, Numero=@Numero, Complemento=@Complemento,
                                                                Bairro=@Bairro, Cidade=@Cidade, UF=@EnderecoUF
                                                                WHERE Id=@EnderecoId",
                                                                conn))
                {
                    cmdEndereco.Parameters.AddWithValue("@EnderecoId", cliente.Endereco.Id);
                    cmdEndereco.Parameters.AddWithValue("@CEP", cliente.Endereco.CEP);
                    cmdEndereco.Parameters.AddWithValue("@Logradouro", cliente.Endereco.Logradouro);
                    cmdEndereco.Parameters.AddWithValue("@Numero", cliente.Endereco.Numero);
                    cmdEndereco.Parameters.AddWithValue("@Complemento", (object)cliente.Endereco.Complemento ?? DBNull.Value);
                    cmdEndereco.Parameters.AddWithValue("@Bairro", cliente.Endereco.Bairro);
                    cmdEndereco.Parameters.AddWithValue("@Cidade", cliente.Endereco.Cidade);
                    cmdEndereco.Parameters.AddWithValue("@EnderecoUF", cliente.Endereco.UF);
                    cmdEndereco.ExecuteNonQuery();
                }
            }
        }

        public void ExcluirCliente(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                int enderecoId;
                using (SqlCommand cmd = new SqlCommand("SELECT EnderecoId FROM Cliente WHERE Id=@Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    enderecoId = (int)cmd.ExecuteScalar();
                }

                using (SqlCommand cmd = new SqlCommand("DELETE FROM Cliente WHERE Id=@Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }

                using (SqlCommand cmd = new SqlCommand("DELETE FROM Endereco WHERE Id=@EnderecoId", conn))
                {
                    cmd.Parameters.AddWithValue("@EnderecoId", enderecoId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Cliente> ListarClientes()
        {
            var clientes = new List<Cliente>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @" SELECT c.*, 
                                   e.Id AS EnderecoId, e.CEP, e.Logradouro, e.Numero, e.Complemento,
                                   e.Bairro, e.Cidade, e.UF AS EnderecoUF
                                   FROM Cliente c
                                   INNER JOIN Endereco e ON c.EnderecoId = e.Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clientes.Add(MapCliente(reader));
                    }
                }
            }
            return clientes;
        }

        private Cliente MapCliente(SqlDataReader reader)
        {
            return new Cliente
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                CPF = reader.GetString(reader.GetOrdinal("CPF")),
                Nome = reader.GetString(reader.GetOrdinal("Nome")),
                RG = reader.GetString(reader.GetOrdinal("RG")),
                DataExpedicao = reader.GetDateTime(reader.GetOrdinal("DataExpedicao")),
                OrgaoExpedicao = reader.GetString(reader.GetOrdinal("OrgaoExpedicao")),
                UF = reader.GetString(reader.GetOrdinal("UF")),
                DataNascimento = reader.GetDateTime(reader.GetOrdinal("DataNascimento")),
                Sexo = reader.GetString(reader.GetOrdinal("Sexo")),
                EstadoCivil = reader.GetString(reader.GetOrdinal("EstadoCivil")),

                Endereco = new Endereco
                {
                    Id = reader.GetInt32(reader.GetOrdinal("EnderecoId")),
                    CEP = reader.GetString(reader.GetOrdinal("CEP")),
                    Logradouro = reader.GetString(reader.GetOrdinal("Logradouro")),
                    Numero = reader.GetString(reader.GetOrdinal("Numero")),
                    Complemento = reader["Complemento"] as string,
                    Bairro = reader.GetString(reader.GetOrdinal("Bairro")),
                    Cidade = reader.GetString(reader.GetOrdinal("Cidade")),
                    UF =  reader.GetString(reader.GetOrdinal("EnderecoUF"))
                }
            };
        }

    }
}