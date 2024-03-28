﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SoftITOFlix.Models;

// Add profile data for application users by adding properties to the SoftITOFlixUser class
public class SoftITOFlixUser : IdentityUser<long>
{
    [Column(TypeName = "date")]
    public DateTime BirthDate { get; set; }

    [StringLength(100, MinimumLength = 2)]
    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; set; } = "";

    public bool Passive { get; set; }

    [NotMapped]
    [StringLength(100, MinimumLength = 8)]
    public string Password { get; set; } = "";
}

