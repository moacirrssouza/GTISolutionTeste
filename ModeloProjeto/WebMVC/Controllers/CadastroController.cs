using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class CadastroController : Controller
    {
        public ActionResult Index(int? pagina)
        {
            int paginaTamanho = 10;
            int paginaNumero = (pagina ?? 1);
            IEnumerable<ClienteViewModel> clientes = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51456/api/");
                var responseTask = client.GetAsync("cadastro");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ClienteViewModel>>();
                    readTask.Wait();
                    clientes = readTask.Result;
                }
                else
                {
                    clientes = Enumerable.Empty<ClienteViewModel>();
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
                }
                return View(clientes.ToPagedList(paginaNumero, paginaTamanho));
            }
        }

        public ActionResult NovoCliente()
        {
            return View();
        }

        public async Task<ActionResult> NovoCliente(ClienteEnderecoViewModel cliente)
        {
            if (cliente == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var client = new HttpClient { BaseAddress = new Uri("http://localhost:51456/api/") })
            {
                var response = await client.PostAsJsonAsync("cadastro/cliente", cliente);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Erro no Servidor. Contacte o Administrador.");
            return View(cliente);
        }

        public ActionResult AlterarCliente(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ClienteViewModel cliente = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51456/api/cadastro/");
                var responseTask = client.GetAsync("?id=" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ClienteViewModel>();
                    readTask.Wait();
                    cliente= readTask.Result;
                }
            }

            return View(cliente);
        }

        public ActionResult AlterarCliente(ClienteViewModel cliente)
        {
            if (cliente == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51456/api/Cadastro");
                var putTask = client.PutAsJsonAsync<ClienteViewModel>("Cadastro", cliente);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(cliente);
        }

        public ActionResult DetahesCliente(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ClienteViewModel cliente = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51456/api/cadastro/cliente/");
                var responseTask = client.GetAsync("?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ClienteViewModel>();
                    readTask.Wait();

                    cliente = readTask.Result;
                }
            }
            return View("DetalheCliente", cliente);
        }

        public ActionResult ExcluirCliente(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ClienteViewModel cliente = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51456/api/");
                var deleteTask = client.DeleteAsync("cadastro/cliente/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(cliente);
        }
    }
}