﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftITOFlix.Models
{
    public class Media
    {
        
        public int Id { get; set; }

        [StringLength(200, MinimumLength = 1)]
        [Column(TypeName = "nvarchar(200)")]
        public string Name { get; set; } = "";

        [StringLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        public string? Description { get; set; }

        public bool Passive { get; set; }

        public List<MediaCategory>? MediaCategories { get; set; }
        public List<MediaActor>? MediaActors { get; set; }
        public List<MediaDirector>? MediaDirectors { get; set;}
        public List<MediaRestriction>? MediaRestrictions { get; set; }
        
        [Range(0,10)]
        public float IMDBRating { get; set; }
    }
}

