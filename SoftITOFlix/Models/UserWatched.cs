using System.ComponentModel.DataAnnotations.Schema;

namespace SoftITOFlix.Models
{
    public class UserWatched
    {
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public SoftITOFlixUser? SoftITOFlixUser { get; set; }

        public long EpisodeId { get; set; }
        [ForeignKey("EpisodeId")]
        public Episode? Episode { get; set; }
    }
}
