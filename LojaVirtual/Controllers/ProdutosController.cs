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
            
            produtos.Add(produto);

            return Redirect("/produtos/listar");
        }

        static List<Produto> produtos = new List<Produto>();

        [HttpGet]
        public ActionResult ProdutoCadastrado()
        {
            return View();
        }
    }
}
