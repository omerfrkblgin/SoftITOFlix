using SoftITOFlix.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SoftITOFlix.Models
{
    public class MediaActor
    {
        public int MediaId { get; set; }
        [ForeignKey("MediaId")]
        [JsonIgnore]
        public Media? Media { get; set; }

        public int ActorId { get; set; }
        [ForeignKey("ActorId")]
        [JsonIgnore]
        public Actor? Actor { get; set; }
    }
}