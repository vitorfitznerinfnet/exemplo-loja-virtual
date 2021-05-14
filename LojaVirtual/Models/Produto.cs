using LojaVirtual.Controllers;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace LojaVirtual.Models
{
    public class Produto //entidade
    {
        //ID materializacao da ideia de unicidade
        public int Id;
        public string Nome; //café
        public decimal Preco; //10
        public int Quantidade; //5
    }
}
