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

    public class Pessoa //entidade
    {
        //existencia de um identificador unico
        public string Cpf { get; set; }
    }
}
