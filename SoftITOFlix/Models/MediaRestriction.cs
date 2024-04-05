using SoftITOFlix.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SoftITOFlix.Models
{
    public class MediaRestriction
    {
        public int MediaId { get; set; }
        [ForeignKey("MediaId")]
        [JsonIgnore]
        public Media? Media { get; set; }

        public byte RestrictionId { get; set; }
        [ForeignKey("RestrictionId")]
        [JsonIgnore]
        public Restricton? Restricton { get; set; }
    }
}

