using Web_API.DTO.Vinculo;
using Web_API.Models;

namespace Web_API.DTO.Livro
{
    public class LivroEdicaoDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public AutorVinculoDto Autor { get; set; }
    }
}
