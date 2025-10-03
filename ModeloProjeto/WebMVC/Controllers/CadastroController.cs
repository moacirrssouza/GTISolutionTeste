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
        [HttpGet]
        public ActionResult Index(int? pagina)
        {
            int paginaTamanho = 10;
            int paginaNumero = (pagina ?? 1);
            IEnumerable<ClienteViewModel> clientes = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51456/api/");
                var responseTask = client.GetAsync("cadastro/cliente");
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

        [HttpGet]
        public ActionResult NovoCliente()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NovoCliente(ClienteEnderecoViewModel cliente)
        {
            if (cliente == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (!ModelState.IsValid)
                return View(cliente);

            using (var client = new HttpClient { BaseAddress = new Uri("http://localhost:51456/api/") })
            {
                client.Timeout = TimeSpan.FromSeconds(10); 

                try
                {
                    var response = await client.PostAsJsonAsync("cadastro/cliente", cliente);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction("Index");

                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, "Erro no servidor: " + errorMessage);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao acessar a API: " + ex.Message);
                }
            }

            return View(cliente);
        }

        [HttpGet]
        public async Task<ActionResult> AlterarCliente(int id)
        {
            ClienteViewModel cliente = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51456/api/");
                var response = await client.GetAsync("cadastro/cliente/?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    var clientes = await response.Content.ReadAsAsync<List<ClienteViewModel>>();
                    cliente = clientes.FirstOrDefault();
                }
            }

            if (cliente == null)
                return HttpNotFound();

            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AlterarCliente(ClienteViewModel cliente)
        {
            if (cliente == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51456/api/");
                var response = await client.PostAsJsonAsync("cadastro/cliente", cliente);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");

                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, "Erro na API: " + errorMessage);
            }

            return View(cliente);
        }

        [HttpGet]
        public async Task<ActionResult> DetaheCliente(int? id)
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
                    var readTask = result.Content.ReadAsAsync<List<ClienteViewModel>>();
                    readTask.Wait();

                    var clientes = readTask.Result;
                    cliente = clientes.FirstOrDefault(); 
                }
            }

            if (cliente == null)
                return HttpNotFound();

            return View("DetalhesCliente", cliente);
        }

        [HttpPost]
        public async Task<ActionResult> ExcluirCliente(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:51456/api/");
                var response = await client.DeleteAsync($"cadastro/cliente/{id}");

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }

            return HttpNotFound();
        }
    }
}