﻿using Infrastructure.EFCore;
using Listening.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Infrastructure
{
    public class ListeningDbContext : BaseDbContext
    {
        public DbSet<Category> Categories { get; private set; }//不要忘了写set，否则拿到的DbContext的Categories为null
        public DbSet<Album> Albums { get; private set; }
        public DbSet<Episode> Episodes { get; private set; }

        public ListeningDbContext(DbContextOptions<ListeningDbContext> options, IMediator mediator) : base(options, mediator)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            modelBuilder.EnableSoftDeletionGlobalFilter();
        }
    }
}
