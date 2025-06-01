using EFTest.Models;
using Microsoft.EntityFrameworkCore;

namespace EFTest.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Trip> Trips => Set<Trip>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<ClientTrip> ClientTrips => Set<ClientTrip>();
    public DbSet<CountryTrip> CountryTrips => Set<CountryTrip>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClientTrip>().HasKey(ct => new { ct.IdClient, ct.IdTrip });
        modelBuilder.Entity<CountryTrip>().HasKey(ct => new { ct.IdCountry, ct.IdTrip });
    }
}

