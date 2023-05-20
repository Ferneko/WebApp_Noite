using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp_Noite.Models;
using WebApp_Noite.Tabelas;

namespace WebApp_Noite.Controllers
{
    public class ProdutoController : Controller
    {
        public static List<ProdutoModel> db_produto = new List<ProdutoModel>();

        private Contexto db;
        public ProdutoController(Contexto contexto)
        {
            db = contexto;
        }

        public IActionResult Lista(string filtro, string busca)
        {
            if( string.IsNullOrEmpty (busca))
            {
                return View( db.Produtos.Include(a => a.Categoria).ToList() );
            }
            else
            {
                switch (filtro)
                {
                   
                    case "id":
                        return View(db.Produtos.Include(a =>a.Categoria).Where(a => a.Id.ToString() == busca).ToList() );
                        break;

                    case "nome":
                        return View(db.Produtos.Include(a => a.Categoria).Where(a => a.Descricao.Contains(busca) ).ToList() );
                        break;

                    case "qtd":
                        return View(db.Produtos.Include(a => a.Categoria).Where(a => a.Valor.ToString() == busca).ToList() );
                        break;

                    default:
                        return View(
                            db.Produtos.Include(a => a.Categoria).Where(a => a.Id.ToString() == busca
                              ||
                               a.Descricao.Contains(busca) 
                              ||
                               a.Valor.ToString() == busca).ToList()
                              );
                        break;
                }
            }
            
        }

        public IActionResult Cadastrar()
        {
            ProdutoModel model = new ProdutoModel();
            model.TodasCategorias = db.Categorias.ToList();
            return View(model);
        }

        public IActionResult SalvarDados(ProdutoModel produto)
        {
            Produtos novoProduto = new Produtos();
            novoProduto.Descricao = produto.Nome;
            novoProduto.Valor = produto.QtdEstoque;
            novoProduto.Ativo = true;
            novoProduto.CategoriaId = produto.CategoriaId;

            db.Produtos.Add(novoProduto);
            db.SaveChanges();

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
