using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Chapter.Interfaces;
using Chapter.Models;
using Chapter.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Chapter.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    
    [Authorize]
    public class LivroController : ControllerBase
    {
        private readonly ILivroRepository _iLivroRepository;
        public LivroController(ILivroRepository iLivroRepository)
        {
            _iLivroRepository = iLivroRepository;
        }

        /// <summary>
        /// Método que controla acesso para listagem de livros
        /// </summary>
        /// <returns>Status code ok e a lista de livros</returns>
        /// <exception cref="Exception">Erro acesso no acesso a listagem de livros !</exception>
        [HttpGet]
        public IActionResult Listar()
        {
            try {
                return Ok(_iLivroRepository.Ler());

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //GET
        /// <summary>
        /// Método que controla o acesso para busca de um livro por Id
        /// </summary>
        /// <param name="id">id do livro a ser buscado</param>
        /// <returns>Status code Ok e livro buscado</returns>
        /// <exception cref="Exception">Erro na busca do livro por Id !</exception>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try 
            {
                Livro livro = _iLivroRepository.BuscarPorId(id);
                if (livro == null) {
                    return NotFound();
                }
                return Ok(livro);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Método que controla o acesso para cadastro de livros
        /// </summary>
        /// <param name="livro">Livro a ser cadastrado</param>
        /// <returns>Status code Ok</returns>
        /// <exception cref="Exception">Erro no cadastro do livro !</exception>
        //POST
        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Cadastrar(Livro livro)
        {
            try 
            {
                _iLivroRepository.Cadastrar(livro);
                return Ok(livro);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Método que controla o acesso para edição de um livro
        /// </summary>
        /// <param name="id">Id do livro a ser atualizado</param>
        /// <param name="livro">Livro com os novos dados</param>
        /// <returns>Status code 204</returns>
        /// <exception cref="Exception">Erro na edição do livro !</exception>
        //PUT
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Livro livro)
        {
            try 
            {
                _iLivroRepository.Atualizar(id, livro);
                return StatusCode(204);
            }
            catch (Exception e) 
            {
                throw new Exception(e.Message);
            }
        }
        //DELETE
        /// <summary>
        /// Método que controla o acesso para exclusão de um livro
        /// </summary>
        /// <param name="id">Id do livro a ser excluído</param>
        /// <returns>Status code 204</returns>
        /// <exception cref="Exception">Erro da exclusão do livro</exception>
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _iLivroRepository.Deletar(id);
                return StatusCode(204);
            }
            catch(Exception e) 
            {
                throw new Exception(e.Message);   
            }
        }
        /*[HttpGet("titulo/[titulo]")]
        public IActionResult BuscarPorTitulo(string titulo)
        {
            try 
            {
                Livro livro = _iLivroRepository.BuscarPorTitulo(titulo);

                if (livro == null) 
                {
                   return NotFound();
                }
                return Ok(livro);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }*/
    }
}
