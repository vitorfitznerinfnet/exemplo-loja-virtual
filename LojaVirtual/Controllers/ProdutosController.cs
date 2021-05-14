using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LojaVirtual.Models;
using System.Data.SqlClient;
using System;
using System.Data;

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
        public ActionResult Listar(string pesquisa, string ordenarpelonome)
        {
            var service = new ProdutosService();

            var produtos = service.ListarProdutos(pesquisa, ordenarpelonome);

            return View(produtos);
        }

        [HttpPost]
        public ActionResult ExecutarCadastroDeProduto(string nome, decimal preco)
        {
            var service = new ProdutosService();

            service.CadastrarProduto(nome, preco);

            return Redirect("/produtos/listar");
        }

        [HttpPost]
        [Route("produtos/excluir")]
        public ActionResult ExcluirPost(int identificador)
        {
            var service = new ProdutosService();

            service.ExcluirProduto(identificador);

            return Redirect("/produtos/listar");
        }

        [HttpGet]
        public ActionResult ProdutoCadastrado()
        {
            return View();
        }

        [HttpGet]
        [Route("produtos/editar")]
        public ActionResult Editar(int identificador)
        {
            var service = new ProdutosService();

            var produto = service.BuscarPelo(identificador);

            return View(produto);
        }

        [HttpPost]
        [Route("produtos/editar")]
        [Route("prod/editar")]
        public ActionResult Editar(string nome, decimal preco, int identificador)
        {
            var service = new ProdutosService();

            var produto = service.BuscarPelo(identificador);
            produto.Nome = nome;
            produto.Preco = preco;

            //adicionar operacao de salvar

            return Redirect("/produtos/listar");
        }

        [HttpGet]
        [Route("produtos/excluir")]
        public ActionResult ExcluirGet(int identificador)
        {
            var service = new ProdutosService();

            var produto = service.BuscarPelo(identificador);

            return View("excluir", produto);
        }
    }

    public class ProdutosService
    {
        public Produto BuscarPelo(int identificador)
        {
            var comando = AbrirConexaoECriarComando();

            comando.CommandText = "select * from produto where id = @id";

            comando.Parameters.AddWithValue("@id", identificador);

            var leitor = comando.ExecuteReader();

            var produto = new Produto();

            while (leitor.Read())
            {
                var nome = leitor["Nome"];
                var preco = leitor["Preco"];

                produto.Id = identificador;
                produto.Nome = Convert.ToString(nome);
                produto.Preco = Convert.ToInt32(preco);
            }

            comando.Connection.Close();

            return produto;
        }

        public void CadastrarProduto(string nome, decimal preco)
        {
            Produto produto = new Produto();

            produto.Nome = nome;
            produto.Preco = preco;

            var comando = AbrirConexaoECriarComando();

            comando.CommandText = "insert into produto(nome, preco, quantidade) values(@nome, @preco, @quantidade);";

            comando.Parameters.AddWithValue("@nome", produto.Nome);
            comando.Parameters.AddWithValue("@preco", produto.Preco);
            comando.Parameters.AddWithValue("@quantidade", produto.Quantidade);
            comando.ExecuteNonQuery();

            comando.Connection.Close();
        }

        public List<Produto> ListarProdutos(string pesquisa, string ordenarpelonome)
        {
            SqlCommand comando = AbrirConexaoECriarComando();

            string consulta = "select * from produto";
            comando.CommandText = consulta;

            var leitor = comando.ExecuteReader();

            var produtos = new List<Produto>();

            while (leitor.Read())
            {
                var nome = leitor["Nome"];
                var preco = leitor["Preco"];

                var produto = new Produto();
                produto.Nome = Convert.ToString(nome);
                produto.Preco = Convert.ToInt32(preco);
                produtos.Add(produto);
            }

            var date = DateTime.UtcNow.AddHours(-30);

            comando.Connection.Close();

            if (pesquisa != null)
            {
                var produtosPesquisados = produtos.Where(produto => produto.Nome == pesquisa).ToList();
            }

            if (ordenarpelonome == "asc")
            {
                produtos = produtos.OrderBy(produto => produto.Nome).ToList();
            }

            return produtos;
        }

        public void ExcluirProduto(int id)
        {
            var comando = AbrirConexaoECriarComando();

            comando.CommandText = "delete from produto where id = @id";

            comando.Parameters.AddWithValue("@id", id);

            comando.ExecuteNonQuery();
        }

        public void AtualizarProduto()
        {

        }

        public SqlCommand AbrirConexaoECriarComando()
        {
            var dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LojaVirtualDb;Integrated Security=True;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            dbConnection.Open();
            var comando = dbConnection.CreateCommand();
            return comando;
        }
    }
}
