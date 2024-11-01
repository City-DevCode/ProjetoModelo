using Loja.Models;
using Loja.Repositorio;
using Loja.Libraries.LoginUsuarios;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MySqlX.XDevAPI;


namespace Loja.Controllers
{
    public class HomeController : Controller
    {
        //DECLARANDO OS OBJETOS QUE SERÃO UTILIZADOS NO PROJETO
        private readonly ILogger<HomeController> _logger;
        private ILoginRepositorio? _loginRepositorio;
        private IClienteRepositorio? _clienteRepositorio;
        private LoginUsuarios _loginUsuarios;



        //CRIANDO O CONSTRUTOR COM OS OBJETOS CRIADOS
        public HomeController(ILogger<HomeController> logger , ILoginRepositorio loginRepositorio, LoginUsuarios loginUsuarios, IClienteRepositorio clienteRepositorio)
        {
            _logger = logger;
            _loginRepositorio = loginRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _loginUsuarios = loginUsuarios;


        }
        //PÁGINA INDEX

        public IActionResult Index()
        {
            return View();
        }


        //PÁGINA PAINEL CLIENTE 
        public IActionResult PainelCliente()
        {
            //RETORNA A PÁGINA COM TODOS OS USUARIOS CADASTRADOS
            return View(_clienteRepositorio?.TodosClientes());
        }


        //PÁGINA LOGIN (GET)
        public IActionResult Login()
        {

            return View();
        }

      
        //PÁGINA LOGIN (POST)

        [HttpPost]
        public IActionResult Login(LoginUsuario login)
        {
            //CHAMANDO A MODEL LOGIN PASSANDO UM NOME PARA ELA  E RECEBER O METODO LOGIN DO REPOSOTORIO
            LoginUsuario loginDB = _loginRepositorio?.Login(login.usuario, login.senha);

            //VERIFICA SE O USUARIO E SENHA ESTIVEREM VAZIAS
            if (loginDB.usuario != null && loginDB.senha != null)
            {
               //CASO NÃO ESTEJAM VAZAIOS O LOGIN QUE VEM DO LIBRRIES VALIDA E
                _loginUsuarios.Login(loginDB);
                //CHAMA A PÁGINA PAINEL CLIENTE
                return new RedirectResult(Url.Action(nameof(PainelCliente)));
            }
            //CASO OS CAMPOS ESTEJAM VAZIOS OU COM USUARIO E SENHA INVÁLIDOS 
            else
            {
                //É PASSADO PARA A VIEWBAG UMA MENSAGEM DE ERROR
                ViewData["msg"] = "Usuário inválido, verifique e-mail e senha";
                //RETORNA A VIEW
                return View();
            }
        }

        //CRUD - DO PROJETO

        //METODO CADASTRAR CLIENTE (GET)
        public IActionResult CadastrarCliente()
        {
          
            return View();
        }
        //METODO CADASTRAR CLIENTE (POST)

        [HttpPost]
        public IActionResult CadastrarCliente(Cliente cliente)
        {
            //VAI LA NO REPOSOTORIO ICLIENTERESPOSITORIO E VERIFICA SE EXISTE O METODO CADASTRAR 
            _clienteRepositorio?.Cadastrar(cliente);

            //AO CADASTRAR É DIRECIONADO PARA O PAINEL CLIENTE
            return RedirectToAction(nameof(PainelCliente));
        }


        //METODO EDITAR CLIENTE(GET)

        public IActionResult EditarCliente(int id)
        {

            // VAI NO CLIENTEREPOSIRIO E VERIFICA SE TEM O METODO TODOSCLIENTES
            var listaCliente = _clienteRepositorio?.TodosClientes();
            // INSTANCIANDO O OBJETO(MODEL) CLIENTE
            Cliente ObjCliente = new Cliente

            {
                //METODO QUE LISTA CLIENTES ESTA NA MODEL
                ListaCliente = (List<Cliente>)listaCliente

            };

            //RETORNA O CLIENTE COM SEU ID
            return View(_clienteRepositorio.ObterCliente(id));
        }

        //METODO EDITAR CLIENTE(POST)

        [HttpPost]
        public IActionResult EditarCliente(Cliente cliente)
        {

            // CARREGA A LISTA DO CLIENTE
            var listaCliente = _clienteRepositorio.TodosClientes();

            //VAI NA CLIENTE REPOSITORIO E VERIFICA SE EXISTE O METODO ATUALIZAR
            _clienteRepositorio.Atualizar(cliente);
            //CASO A EDIÇÃO ESTEJA TUDO OK DIRECIONA PARA O PAINEL CLIENTE
            return RedirectToAction(nameof(PainelCliente));
        }



        // METODO EXCLUIR CLIENTE
        public IActionResult ExcluirCliente(int id)
        {
            //VAI NA CLIENTE REPOSITORIO E VERIFICA SE EXISTE O METODO EXCLUIR
            _clienteRepositorio.Excluir(id);
            //CASO A EXCLUSÃO ESTEJA TUDO OK DIRECIONA PARA O PAINEL CLIENTE
            return RedirectToAction(nameof(PainelCliente));
        }

       
    


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
