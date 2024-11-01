using Loja.Models;

namespace Loja.Repositorio
{
    public interface IClienteRepositorio
    {

        //METODO CADASTRA CLIENTE
        void Cadastrar(Cliente cliente);

        // METODO BUSCAS TODOS OS CLIENTES
        IEnumerable<Cliente> TodosClientes();

        //METODO BUSCAR CLIENTE POR ID
        Cliente ObterCliente(int id);

        //METODO EDITAR CLIENTE
        void Atualizar(Cliente cliente);

        //METODO EXCLUIR CLIENTE
        void Excluir(int id);
    }
}
