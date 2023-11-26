using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Portfolio_Backend.Models
{
    public class Language
    {
        public int Id { get; set; } 

        public string Description { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [JsonIgnore]
        public virtual User? User { get; set; }
    }
}
