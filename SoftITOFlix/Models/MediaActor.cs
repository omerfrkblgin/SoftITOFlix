﻿using SoftITOFlix.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftITOFlix.Models
{
    public class MediaActor
    {
        public int MediaId { get; set; }
        [ForeignKey("MediaId")]
        public Media? Media { get; set; }

        public int ActorId { get; set; }
        [ForeignKey("ActorId")]
        public Actor? Actor { get; set; }
    }
}