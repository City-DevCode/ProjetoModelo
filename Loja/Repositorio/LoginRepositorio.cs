using Loja.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace Loja.Repositorio
{
    public class LoginRepositorio : ILoginRepositorio
    {

        //declarando a varival de da string de conxão

        private readonly string? _conexaoMySQL;

        //metodo da conexão com banco de dados
        public LoginRepositorio(IConfiguration conf) => _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");



        //Login Cliente(metodo )

        public LoginUsuario Login(string usuario, string senha)
        {
            //usando a variavel conexao 
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                //abre a conexão com o banco de dados
                conexao.Open();

                // variavel cmd que receb o select do banco de dados buscando email e senha
                MySqlCommand cmd = new MySqlCommand("select * from tbLogin where usuario = @Usuario and senha = @Senha", conexao);

                //os paramentros do usuario e da senha 
                cmd.Parameters.Add("@Usuario", MySqlDbType.VarChar).Value = usuario;
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = senha;

                //Le os dados que foi pego do email e senha do banco de dados
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                //guarda os dados que foi pego do email e senha do banco de dados
                MySqlDataReader dr;

                //instanciando a model Login
                LoginUsuario login = new LoginUsuario();
                //executando os comandos do mysql e passsando paa a variavel dr
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                //verifica todos os dados que foram pego do banco e pega o email e senha
                while (dr.Read())
                {

                    login.usuario = Convert.ToString(dr["usuario"]);
                    login.senha = Convert.ToString(dr["senha"]);
                }
                return login;
            }
        }

    }
}
