using System.Text.Json.Serialization;

namespace Web_API.Models
{
    public class AutorModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        [JsonIgnore]

        public ICollection<LivroModel> Books { get; set; }
    }
}
