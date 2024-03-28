using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SoftITOFlix.Models
{
    public class Plan
    {
        [Key]
        public short Id { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; } = "";

        [Range(0, float.MaxValue)]
        public float Price { get; set; }

        [StringLength(20, MinimumLength = 2)]
        [Column(TypeName = "varchar(20)")]
        public string Resolution { get; set; } = "";

    }
}
