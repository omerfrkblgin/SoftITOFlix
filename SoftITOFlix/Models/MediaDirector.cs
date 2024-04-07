using SoftITOFlix.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SoftITOFlix.Models
{
    public class MediaDirector
    {
        public int MediaId { get; set; }
        [ForeignKey("MediaId")]
        [JsonIgnore]
        public Media? Media { get; set; }

        public int DirectorId { get; set; }
        [ForeignKey("DirectorId")]
        [JsonIgnore]
        public Director? Director { get; set; }
    }
}