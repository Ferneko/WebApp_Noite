using Microsoft.AspNetCore.Mvc;
using WebApp_Noite.Models;

namespace WebApp_Noite.Controllers
{
    public class ProdutoController : Controller
    {
        public static List<ProdutoModel> db_produto = new List<ProdutoModel>();

        public IActionResult Lista()
        {
            return View( db_produto );
        }

        public IActionResult Cadastrar()
        {
            ProdutoModel model = new ProdutoModel();
            return View(model);
        }

        public IActionResult SalvarDados(ProdutoModel produto)
        {
            if(produto.Id == 0)
            {
                Random rand = new Random();
                produto.Id = rand.Next(1,999);
                db_produto.Add(produto);
            }
            else
            {
                int item = db_produto.FindIndex(a => a.Id == produto.Id);
                db_produto[item] = produto;
            }
            return RedirectToAction("Lista");
        }

        public IActionResult Excluir(int id)
        {
            ProdutoModel item = db_produto.Find(a => a.Id == id);
            if (item != null)
            {
                db_produto.Remove(item);
            }
            return RedirectToAction("Lista");

        }

        public IActionResult Editar(int id)
        {
            ProdutoModel item = db_produto.Find(cliente => cliente.Id == id);
            if (item != null)
            {
                return View(item);
            }
            else
            {
                return RedirectToAction("Lista");
            }
        }
    }
}
