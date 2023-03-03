using Chapter.Repositories;
using Chapter.Models;
using Chapter.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Chapter.Controllers
{
    [Produces("applications/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroRepository _iLivroRepository;
        public LivroController(ILivroRepository iLivroRepository)
        {
            _iLivroRepository = iLivroRepository;
        }

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
        //POST
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
