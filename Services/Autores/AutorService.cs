using Microsoft.EntityFrameworkCore;
using Web_API.Data;
using Web_API.Models;

namespace Web_API.Services.Autores
{
    public class AutorService : AutorInterface
    {

        private readonly AppDbContext _context;

        public AutorService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor)
        {
            ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();
            try 
            {

                var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == idAutor);

                if (autor == null )
                {
                    resposta.Mensagem = "Autor não encontrado.";

                    return resposta;
                }

                resposta.Dados = autor;
                resposta.Mensagem = "Autor encontrado com sucesso.";
                
                return resposta;



            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;

            }
        }

        public async Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro)
        {
            ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();

            try
            {

                var livro = await _context.Livros
                    .Include(a => a.Author)
                    .FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);

                if (livro == null)
                {
                    resposta.Mensagem = "Livro não encontrado ou não possui autor associado.";
                    return resposta;

                }

                resposta.Dados = livro.Author;
                resposta.Mensagem = "Autor do livro encontrado com sucesso.";
                return resposta;

            }
            catch (Exception ex) 
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;

            }





        }

        public async Task<ResponseModel<List<AutorModel>>> ListarAutores()
        {
            
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();



            try
            {
                var autores = await _context.Autores.ToListAsync();

                resposta.Dados = autores;
                resposta.Mensagem = "Autores listados com sucesso.";


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
