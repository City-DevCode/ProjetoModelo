using Loja.Models;

namespace Loja.Repositorio
{
    public interface ILoginRepositorio
    {

        LoginUsuario Login(string usuario, string senha);
    }
}
