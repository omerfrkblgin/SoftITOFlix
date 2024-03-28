﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SoftITOFlix.Models;

namespace SoftITOFlix.Data
{
    public class SoftITOFlixContext : IdentityDbContext<SoftITOFlixUser,SoftITOFlixRole,long>
    {
        public SoftITOFlixContext (DbContextOptions<SoftITOFlixContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<MediaCategory>().HasKey(m => new { m.MediaId, m.CategoryId });
            builder.Entity<MediaDirector>().HasKey(m => new { m.MediaId, m.DirectorId });
            builder.Entity<MediaRestriction>().HasKey(m => new { m.MediaId, m.RestrictionId });
            builder.Entity<MediaActor>().HasKey(m => new { m.MediaId, m.ActorId });
            builder.Entity<UserFavorite>().HasKey(u => new { u.UserId, u.MediaId });
            builder.Entity<UserWatched>().HasKey(u => new { u.UserId, u.EpisodeId });
            builder.Entity<UserPlan>().HasKey(u => new { u.UserId, u.PlanId });
        }

        public DbSet<SoftITOFlix.Models.Category> Categories { get; set; } = default!;
        public DbSet<SoftITOFlix.Models.Director> Directors { get; set; } = default!;
        public DbSet<SoftITOFlix.Models.Episode> Episodes { get; set; } = default!;
        public DbSet<SoftITOFlix.Models.Actor> Actors { get; set; } = default!;
        public DbSet<SoftITOFlix.Models.Media> Medias { get; set; } = default!;
        public DbSet<SoftITOFlix.Models.MediaActor> MediaActors { get; set; } = default!;
        public DbSet<SoftITOFlix.Models.MediaCategory> MediaCategories { get; set; } = default!;
        public DbSet<SoftITOFlix.Models.MediaDirector> MediaDirectors { get; set; } = default!;
        public DbSet<SoftITOFlix.Models.MediaRestriction> MediaRestrictions { get; set; } = default!;
        public DbSet<SoftITOFlix.Models.Plan> Plans { get; set; } = default!;
        public DbSet<SoftITOFlix.Models.Restricton> Restrictons { get; set; } = default!;
        public DbSet<SoftITOFlix.Models.UserFavorite> UserFavorites { get; set; } = default!;
        public DbSet<SoftITOFlix.Models.UserPlan> UserPlans { get; set; } = default!;
        public DbSet<SoftITOFlix.Models.UserWatched> UserWatches { get; set; } = default!;

        
    }
}