using Loja.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace Loja.Repositorio
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        //declarando a varival de da string de conxão

        private readonly string? _conexaoMySQL;

        //metodo da conexão com banco de dados
        public ClienteRepositorio(IConfiguration conf) => _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");

        //Cadastrar Cliente
        public void Cadastrar(Cliente cliente)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))

            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("insert into tbCliente (nome,telefone,email) values (@nome, @telefone, @email)", conexao); // @: PARAMETRO

                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = cliente.Nome;
                cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = cliente.Telefone;
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = cliente.Email;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }

        }


        //lista todos os clientes

        public IEnumerable<Cliente> TodosClientes()
        {
            List<Cliente> Clientlist = new List<Cliente>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from tbCliente", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    Clientlist.Add(
                            new Cliente
                            {
                                //DBNull.value - Significa aceitar campos nulos ou vazios
                                Codigo = dr["codigo"] != DBNull.Value ? Convert.ToInt32(dr["codigo"]) : 0,
                                Nome = dr["nome"] != DBNull.Value ? (string)dr["nome"] : string.Empty,
                                Telefone = dr["telefone"] != DBNull.Value ? (string)dr["telefone"] : string.Empty,
                                Email = dr["email"] != DBNull.Value ? (string)dr["email"] : string.Empty,

                            });
                }
                return Clientlist;

            }
        }

        
        
        //buscar todos os clientes por id
        public Cliente ObterCliente(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new("SELECT * from tbCliente where codigo=@codigo ", conexao);
                cmd.Parameters.AddWithValue("@codigo", id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Cliente cliente = new Cliente();
                // retorna conjunto de resultado ,  é funcionalmente equivalente a chamar ExecuteReader().
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    cliente.Codigo = Convert.ToInt32(dr["codigo"]);
                    cliente.Nome = (string)(dr["nome"]);
                    cliente.Telefone = (string)(dr["telefone"]);
                    cliente.Email = (string)(dr["email"]);

                

                }
                return cliente;
            }
        }

        //Alterar Cliente
        public void Atualizar(Cliente cliente)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("Update tbCliente set nome=@nome, telefone=@telefone, email=@email " +
                                                    " where codigo=@codigo ", conexao);

                cmd.Parameters.Add("@codigo", MySqlDbType.VarChar).Value = cliente.Codigo;
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = cliente.Nome;
                cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = cliente.Telefone;
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = cliente.Email;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }


        //excluir Cliente
        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from tbCliente where codigo=@codigo", conexao);
                cmd.Parameters.AddWithValue("@codigo", Id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }







    }
}
