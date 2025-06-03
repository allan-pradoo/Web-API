using Microsoft.EntityFrameworkCore;
using Web_API.Data;
using Web_API.DTO.Autor;
using Web_API.DTO.Livro;
using Web_API.Models;

namespace Web_API.Services.Livro
{
    public class LivroServicecs : LivroInterface
    {
        private readonly AppDbContext _context;

        public LivroServicecs(AppDbContext context )
        {
            _context = context;
        }
        public async Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro)
        {
            ResponseModel<LivroModel> resposta = new ResponseModel<LivroModel>();
            try
            {

                var livro = await _context.Livros
                    .Include(a => a.Author)
                    .FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);

                if (livro == null)
                {
                    resposta.Mensagem = "Autor não encontrado.";

                    return resposta;
                }

                resposta.Dados = livro;
                resposta.Mensagem = "Livro encontrado com sucesso.";

                return resposta;



            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;

            }
        }

        public async Task<ResponseModel<List<LivroModel>>> BuscarLivroPorIdAutor(int idAutor)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();

            try
            {
                var livro = await _context.Livros
                    .Include(a => a.Author)
                    .Where(livroBanco => livroBanco.Author.Id == idAutor)
                    .ToListAsync();




                if (livro == null)
                {
                    resposta.Mensagem = "Livro não encontrado ou não possui autor associado.";
                    return resposta;

                }

                resposta.Dados = livro;
                resposta.Mensagem = "Livro encontrado com sucesso.";
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;

            }
        }

        public async Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();


            try
            {
              
                var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == livroCriacaoDto.Autor.Id);

                if (autor == null)
                {
                    resposta.Mensagem = "Autor não encontrado.";
                    return resposta;
                }

                var livro = new LivroModel()
                {
                    Tittle = livroCriacaoDto.Titulo,
                    Author = autor
                };


                _context.Livros.Add(livro);
                await _context.SaveChangesAsync();
                
                resposta.Dados = await _context.Livros
                    .Include(a => a.Author)
                    .ToListAsync();

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;


            }
        }

        public async Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();

            try
            {
                var livro = await _context.Livros
                    .Include(a => a.Author)
                    .FirstOrDefaultAsync(livroBanco => livroBanco.Id == livroEdicaoDto.Id);

                var autor = await _context.Autores
                    .FirstOrDefaultAsync(autorBanco => autorBanco.Id == livroEdicaoDto.Autor.Id);

                if (livro == null)
                { 
                    resposta.Mensagem = "Livro não encontrado.";
                    return resposta;
                }

                if (autor == null)
                {
                    resposta.Mensagem = "Autor não encontrado.";
                    return resposta;
                }

                livro.Tittle = livroEdicaoDto.Titulo;
                livro.Author = autor;

                _context.Update(livro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livros
                    .ToListAsync();

                return resposta;




            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();


            try
            {

                var livro = await _context.Livros
                    .Include(a => a.Author)
                    .FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);

                if (livro == null)
                {
                    resposta.Mensagem = "Autor não encontrado.";
                    resposta.Status = false;
                    return resposta;

                }

                _context.Remove(livro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livros.ToListAsync();
                resposta.Mensagem = "Livro excluído com sucesso.";

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> ListarLivros()
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();



            try
            {
                var livros = await _context.Livros
                    .Include(a => a.Author)
                    .ToListAsync();

                resposta.Dados = livros;
                resposta.Mensagem = "Livros listados com sucesso.";


                return resposta;





            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;

            }
        }
    }
}
