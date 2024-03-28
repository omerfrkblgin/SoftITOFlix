using SoftITOFlix.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftITOFlix.Models
{
    public class MediaRestriction
    {
        public int MediaId { get; set; }
        [ForeignKey("MediaId")]
        public Media? Media { get; set; }

        public byte RestrictionId { get; set; }
        [ForeignKey("RestrictionId")]
        public Restricton? Restricton { get; set; }
    }
}

