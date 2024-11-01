using MySqlX.XDevAPI;
using Loja.Models;
using Newtonsoft.Json;

namespace Loja.Libraries.LoginUsuarios
{
    public class LoginUsuarios
    {

        //injeção de depencia
        private string Key = "Login.Usuario";
        private Sessao.Sessao _sessao;

        //Construtor
        public LoginUsuarios(Sessao.Sessao sessao)
        {
            _sessao = sessao;
        }

        public void Login(LoginUsuario login)
        {
            // Serializar- Com a serialização é possível salvar objetos em arquivos de dados
            string loginJSONString = JsonConvert.SerializeObject(login);
        }

        public LoginUsuario GetCliente()
        {
            /* Deserializar-Já a desserialização permite que os 
            objetos persistidos em arquivos possam ser recuperados e seus valores recriados na memória*/

            if (_sessao.Existe(Key))
            {
                string loginJSONString = _sessao.Consultar(Key);
                return JsonConvert.DeserializeObject<LoginUsuario>(loginJSONString);
            }
            else
            {
                return null;
            }
        }
        //Remove a sessão e desloga o Cliente
        public void Logout()
        {
            _sessao.RemoverTodos();
        }
    }
}
