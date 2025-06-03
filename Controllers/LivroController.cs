using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API.DTO.Autor;
using Web_API.DTO.Livro;
using Web_API.Models;
using Web_API.Services.Autores;
using Web_API.Services.Livro;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly LivroInterface _livroInterface;

        public LivroController(LivroInterface livroInterface)
        {
            _livroInterface = livroInterface;
        }


        [HttpGet("ListarLivros")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> ListarLivros()
        {
            var livros = await _livroInterface.ListarLivros();
            return Ok(livros);


        }

        [HttpGet("BuscarLivroPorId/{idLivro}")]

        public async Task<ActionResult<ResponseModel<LivroModel>>> BuscarLivroPorId(int idLivro)
        {
            var livro = await _livroInterface.BuscarLivroPorIdAutor(idLivro);
            return Ok(livro);


        }


        [HttpGet("BuscarLivroPorIdAutor/{idAutor}")]

        public async Task<ActionResult<ResponseModel<LivroModel>>> BuscarAutorPorIdLivro(int idAutor)
        {
            var livro = await _livroInterface.BuscarLivroPorIdAutor(idAutor);
            return Ok(livro);


        }

        [HttpPost("CriarLivro")]

        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
        {
            var livro = await _livroInterface.CriarLivro(livroCriacaoDto);
            return Ok(livro);

        }

        [HttpPut("EditarLivro")]

        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
        {
            var livro = await _livroInterface.EditarLivro(livroEdicaoDto);
            return Ok(livro);

        }

        [HttpDelete("ExcluirLivro")]

        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> ExcluirLivro(int idLivro)
        {
            var autores = await _livroInterface.ExcluirLivro(idLivro);
            return Ok(autores);
        }
    }
}
