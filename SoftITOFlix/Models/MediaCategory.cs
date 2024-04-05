using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SoftITOFlix.Models
{
    public class MediaCategory
    {
        public int MediaId { get; set; }
        [ForeignKey("MediaId")]
        [JsonIgnore]
        public Media? Media { get; set; }

        public short CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}
