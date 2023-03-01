using Chapter.Repositories;
using Chapter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Chapter.Controllers
{
    [Produces("applications/json")]
    [Route("api/[controller]")]
    public class LivroController : ControllerBase
    {
        private readonly LivroRepository _livroRepository;
        public LivroController(LivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            try {
                return Ok(_livroRepository.Ler());

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //GET
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try 
            {
                Livro livro = _livroRepository.BuscarPorId(id);
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
        //POST
        [HttpPost]
        public IActionResult Cadastrar(Livro livro)
        {
            try 
            {
                _livroRepository.Cadastrar(livro);
                return Ok(livro);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //PUT
        [HttpPut]
        public IActionResult Atualizar(int id, Livro livro)
        {
            try 
            {
                _livroRepository.Atualizar(id, livro);
                return StatusCode(204);
            }
            catch (Exception e) 
            {
                throw new Exception(e.Message);
            }
        }
        //DELETE
        [HttpDelete]
        public IActionResult Deletar(int id)
        {
            try
            {
                _livroRepository.Deletar(id);
                return StatusCode(204);
            }
            catch(Exception e) 
            {
                throw new Exception(e.Message);   
            }
        }
    }
}
