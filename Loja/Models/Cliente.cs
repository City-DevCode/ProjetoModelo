namespace Loja.Models
{
    public class Cliente
    {

        public int Codigo { get; set; }
        public string? Nome { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public List<Cliente>? ListaCliente { get; set; }
    }
}
