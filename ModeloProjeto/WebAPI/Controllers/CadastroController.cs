using WebAPI.Models;
using WebAPI.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class CadastroController : ApiController
    {
        [HttpGet]
        [Route("api/cadastro/cliente")]
        public IHttpActionResult ListarClientes(bool incluirEndereco = false)
        {
            using (var ctx = new AppDbContext())
            {
                var query = ctx.Cliente.AsQueryable();
                if (incluirEndereco)
                    query = query.Include(c => c.Endereco);

                var clientes = query.Select(clt => new ClienteEnderecoDTO
                    {
                        Id = clt.Id,
                        CPF = clt.CPF,
                        Nome = clt.Nome,
                        RG = clt.RG,
                        DataExpedicao = clt.DataExpedicao,
                        OrgaoExpedicao = clt.OrgaoExpedicao,
                        UF = clt.UF,
                        DataNascimento = clt.DataNascimento,
                        Sexo = clt.Sexo,
                        EstadoCivil = clt.EstadoCivil,
                        Endereco = new EnderecoDTO()
                        {
                            CEP = clt.Endereco.CEP,
                            Logradouro = clt.Endereco.Logradouro,
                            Numero = clt.Endereco.Numero,
                            Complemento = clt.Endereco.Complemento,
                            Bairro = clt.Endereco.Bairro,
                            Cidade = clt.Endereco.Cidade,
                            UF = clt.Endereco.UF
                        } 
                    })
                    .ToList();

                if (!clientes.Any())
                    return NotFound();

                return Ok(clientes);
            }
        }

        [HttpGet]
        [Route("api/cadastro/cliente/{id}")]
        public IHttpActionResult BuscarClientePorId(int? id)
        {
            if (id == null)
                return BadRequest("O Id do cliente é inválido");

            ClienteEnderecoDTO cliente = null;
            using (var ctx = new AppDbContext())
            {
                cliente = ctx.Cliente.Include("Endereco").ToList()
                            .Where(c => c.Id == id)
                            .Select(c => new ClienteEnderecoDTO()
                            {
                                Id = c.Id,
                                CPF = c.CPF,
                                Nome = c.Nome,
                                RG = c.RG,
                                DataExpedicao = c.DataExpedicao,
                                OrgaoExpedicao = c.OrgaoExpedicao,
                                UF = c.UF,
                                DataNascimento = c.DataNascimento,
                                Sexo = c.Sexo,
                                EstadoCivil = c.EstadoCivil,
                                Endereco = c.Endereco == null ? null : new EnderecoDTO()
                                {
                                    CEP = c.Endereco.CEP,
                                    Logradouro = c.Endereco.Logradouro,
                                    Numero = c.Endereco.Numero,
                                    Complemento = c.Endereco.Complemento,
                                    Bairro = c.Endereco.Bairro,
                                    Cidade = c.Endereco.Cidade,
                                    UF = c.Endereco.UF
                                }
                            }).FirstOrDefault();
            }

            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpPost]
        [Route("api/cadastro/cliente")]
        public IHttpActionResult NovoCliente(ClienteEnderecoDTO cliente)
        {
            if (cliente == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            using (var ctx = new AppDbContext())
            {
                var novoCliente = new Cliente
                {
                    CPF = cliente.CPF,
                    Nome = cliente.Nome,
                    RG = cliente.RG,
                    DataExpedicao = cliente.DataExpedicao,
                    OrgaoExpedicao = cliente.OrgaoExpedicao,
                    UF = cliente.UF,
                    DataNascimento = cliente.DataNascimento,
                    Sexo = cliente.Sexo,
                    EstadoCivil = cliente.EstadoCivil,
                    Endereco = new Endereco
                    {
                        CEP = cliente.Endereco.CEP,
                        Logradouro = cliente.Endereco.Logradouro,
                        Numero = cliente.Endereco.Numero,
                        Complemento = cliente.Endereco.Complemento,
                        Bairro = cliente.Endereco.Bairro,
                        Cidade = cliente.Endereco.Cidade,
                        UF = cliente.Endereco.UF
                    }
                };

                ctx.Cliente.Add(novoCliente);
                ctx.SaveChanges();

                cliente.Id = novoCliente.Id;
                return Ok(new { message = $"Cliente cadastrado com sucesso" });
            }
        }

        [HttpPut]
        [Route("api/cadastro/cliente/{id}")]
        public IHttpActionResult AtualizarCliente(Cliente cliente)
        {
            if (!ModelState.IsValid || cliente == null)
                return BadRequest("Os dados do cliente são inválidos");

            using (var ctx = new AppDbContext())
            {
                var clienteSelecionado = ctx.Cliente.Where(c => c.Id == cliente.Id)
                                         .FirstOrDefault<Cliente>();

                if (clienteSelecionado != null)
                {
                    clienteSelecionado.CPF = cliente.CPF;
                    clienteSelecionado.Nome = cliente.Nome;
                    clienteSelecionado.RG = cliente.RG;
                    clienteSelecionado.DataExpedicao = cliente.DataExpedicao;
                    clienteSelecionado.OrgaoExpedicao = cliente.OrgaoExpedicao;
                    clienteSelecionado.UF = cliente.UF;
                    clienteSelecionado.DataNascimento = cliente.DataNascimento;
                    clienteSelecionado.Sexo = cliente.Sexo;
                    clienteSelecionado.EstadoCivil = cliente.EstadoCivil;

                    ctx.Entry(clienteSelecionado).State = EntityState.Modified;

                    var enderecoSelecionado = ctx.Endereco.Where(e =>
                                                e.Id == clienteSelecionado.Endereco.Id)
                                                .FirstOrDefault<Endereco>();

                    if (enderecoSelecionado != null)
                    {
                        enderecoSelecionado.CEP = cliente.Endereco.CEP;
                        enderecoSelecionado.Logradouro = cliente.Endereco.Logradouro;
                        enderecoSelecionado.Numero = cliente.Endereco.Numero;
                        enderecoSelecionado.Complemento = cliente.Endereco.Complemento;
                        enderecoSelecionado.Bairro = cliente.Endereco.Bairro;
                        enderecoSelecionado.Cidade = cliente.Endereco.Cidade;
                        enderecoSelecionado.UF = cliente.Endereco.UF;

                        ctx.Entry(enderecoSelecionado).State = EntityState.Modified;
                    }

                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok($"Cliente {cliente.Nome} atualizado com sucesso");
        }

        [HttpDelete]
        [Route("api/cadastro/cliente/{id}")]
        public IHttpActionResult ExcluirCliente(int id)
        {
            using (var ctx = new AppDbContext())
            {
                var clienteSelecionado = ctx.Cliente
                    .Include(c => c.Endereco)
                    .FirstOrDefault(c => c.Id == id);

                if (clienteSelecionado == null)
                    return NotFound();

                if (clienteSelecionado.Endereco != null)
                {
                    ctx.Endereco.Remove(clienteSelecionado.Endereco);
                }

                ctx.Cliente.Remove(clienteSelecionado);
                ctx.SaveChanges();
            }

            return Ok(new { message = $"Cliente {id} foi deletado com sucesso" });
        }
    }
}