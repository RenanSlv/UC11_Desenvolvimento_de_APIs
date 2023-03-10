using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Chapter.Interfaces;
using Chapter.Models;
using Chapter.Repositories;

namespace Chapter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _iUsuarioRepository;
        public UsuarioController(IUsuarioRepository iUsuarioRepository)
        {
            _iUsuarioRepository = iUsuarioRepository;
        }

        /// <summary>
        /// Método que controla o acesso a listagem de usuários
        /// </summary>
        /// <returns>Lista dos usuários cadastrados</returns>
        /// <exception cref="Exception">Erro ao listar usuários !</exception>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_iUsuarioRepository.Listar());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// <summary>
        /// Método que controla o acesso a buscar um usuário por id
        /// </summary>
        /// <param name="id">id do usuário a ser buscado</param>
        /// <returns>Status code Ok e usuário buscado</returns>
        /// <exception cref="Exception">Erro ao buscar usuário por Id !</exception>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                Usuario usuarioBuscado = _iUsuarioRepository.BuscarPorId(id);

                if (usuarioBuscado == null)
                {
                    return NotFound();
                }
                return Ok(usuarioBuscado);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Método que controla o acesso para cadastro de um usuário
        /// </summary>
        /// <param name="usuario">Usuário a ser cadastrado</param>
        /// <returns>Status code created</returns>
        /// <exception cref="Exception">Erro ao cadastrar usuário !</exception>
        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            try
            {
                _iUsuarioRepository.Cadastrar(usuario);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Método que controla o acesso a atualização de um usuário
        /// </summary>
        /// <param name="id">Id do usuário a ser atualizado</param>
        /// <param name="usuario"> Usuário com os novos dados</param>
        /// <returns>Status code ok e mensagem de sucesso</returns>
        /// <exception cref="Exception">Erro na edição do usuário !</exception>
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Usuario usuario)
        {
            try 
            {
                _iUsuarioRepository.Atualizar(id, usuario);
                return Ok("Usuario Atualizado.");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            } 
        }
        /// <summary>
        /// Método que controla o acesso para excluir um usuário
        /// </summary>
        /// <param name="id">Id do usuário a ser excluído</param>
        /// <returns>Status code ok e mensagem de sucesso</returns>
        /// <exception cref="Exception">Erro ao deletar usuário !</exception>
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try 
            {
                _iUsuarioRepository.Deletar(id);
                return Ok("Usuario Deletado.");
            }
            catch (Exception e) 
            {
                throw new Exception(e.Message);
            }
        }


    }
}
