﻿using ChristmasHamper.Domain.Common;
using ChristmasHamper.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChristmasHamper.Persistence.DbContexts;

public class ChristmasHamperDbContext(DbContextOptions<ChristmasHamperDbContext> options) : DbContext(options)
{
    public DbSet<Organization> Organizations { get; set; } = null!;
    public DbSet<User> Users { get; set; }
    public DbSet<Committee> Committees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ChristmasHamperDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<Auditable>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedOn = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedOn = DateTime.Now;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}

