namespace Web_API.Models
{
    public class LivroModel
    {
        public int Id { get; set; }
        public string Tittle { get; set; }

        public AutorModel Author { get; set; }
    }
}
