using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LojaVirtual.Models;

namespace LojaVirtual.Controllers
{
    public class ProdutosController : Controller
    {
        [HttpGet]
        [Route("prod/cadastrar")]
        [Route("prod/cadastro")]
        [Route("produtos/cadastrar")]
        [Route("produtos/cadastro")]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpGet]
        [Route("produtos/listar")]
        public ActionResult Listar()
        {
            return View(produtos);
        }

        [HttpPost]
        public ActionResult ExecutarCadastroDeProduto(string nome, decimal preco)
        {
            Produto produto = new Produto();

            produto.Nome = nome;
            produto.Preco = preco;
            produto.Id = produtos.Count + 1;

            produtos.Add(produto);

            return Redirect("/produtos/listar");
        }

        static List<Produto> produtos = new List<Produto>();

        [HttpGet]
        public ActionResult ProdutoCadastrado()
        {
            return View();
        }

        //Criar, Alterar, Buscar, Excluir
        //CRUD      = create, read, update, delete
        //CRUD HTTP = post,   get,  put,    delete
        
        [HttpGet]
        [Route("produtos/editar")]
        public ActionResult Editar(int identificador)
        {
            var produto = produtos.First(produto => produto.Id == identificador);

            return View(produto);
        }

        [HttpPost]
        [Route("produtos/editar")]
        [Route("prod/editar")]
        public ActionResult Editar(string nome, decimal preco, int identificador)
        {
            Produto produto = produtos.First(produto => produto.Id == identificador);
            produto.Nome = nome;
            produto.Preco = preco;

            return Redirect("/produtos/listar");
            //return Redirect("/produtos/editar?identificador=" + identificador);
        }

        [HttpGet]
        [Route("produtos/excluir")]
        public ActionResult ExcluirGet(int identificador)
        {
            var produto = produtos.First(produto => produto.Id == identificador);

            return View("excluir", produto);
        }

        [HttpPost]
        [Route("produtos/excluir")]
        public ActionResult ExcluirPost(int identificador)
        {
            var produto = produtos.First(produto => produto.Id == identificador);

            produtos.Remove(produto);

            return Redirect("/produtos/listar");
        }
    }
}
