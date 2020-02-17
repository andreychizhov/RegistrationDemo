using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PM.Model.Entities;
using System.Collections.Generic;

namespace PM.Model
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);
                
            modelBuilder.Entity<Location>()
                .ToTable("Locations")
                .HasDiscriminator(x => x.LocationType)
                .HasValue<Country>(LocationType.Country)
                .HasValue<Province>(LocationType.Province);

            modelBuilder.Entity<Country>()
                .HasMany(k => k.Provinces)
                .WithOne(k => k.Country)
                .HasForeignKey(k => k.ParentId);

            modelBuilder.Entity<User>()
                .HasOne(x => x.ClientInfo)
                .WithOne(x => x.User)
                .HasForeignKey<ClientInfo>(x => x.UserId);
            
            modelBuilder.Entity<Country>()
                .HasOne(x => x.ClientInfo)
                .WithOne(x => x.Country)
                .HasForeignKey<ClientInfo>(x => x.CountryId);

            modelBuilder.Entity<Province>()
                .HasOne(x => x.ClientInfo)
                .WithOne(x => x.Province)
                .HasForeignKey<ClientInfo>(x => x.ProvinceId);

            modelBuilder.Entity<ClientInfo>()
                .HasIndex(x => x.CountryId)
                .IsUnique(false);
            
            modelBuilder.Entity<ClientInfo>()
                .HasIndex(x => x.ProvinceId)
                .IsUnique(false);

            int i = 1;
            var usa = new Country { Id = i++, Name = "USA", LocationType = LocationType.Country };

            modelBuilder.Entity<Country>().HasData(usa);
            modelBuilder.Entity<Province>().HasData(new Province { Id = i++, Name = "USA Province 1", LocationType = LocationType.Province, ParentId = usa.Id });
            modelBuilder.Entity<Province>().HasData(new Province { Id = i++, Name = "USA Province 2", LocationType = LocationType.Province, ParentId = usa.Id });
            modelBuilder.Entity<Province>().HasData(new Province { Id = i++, Name = "USA Province 3", LocationType = LocationType.Province, ParentId = usa.Id });
            
            var ireland = new Country { Id = i++, Name = "Ireland", LocationType = LocationType.Country };
            modelBuilder.Entity<Country>().HasData(ireland);
            modelBuilder.Entity<Province>().HasData(new Province { Id = i++, Name = "Ireland Province 1", LocationType = LocationType.Province, ParentId = ireland.Id });
            modelBuilder.Entity<Province>().HasData(new Province { Id = i++, Name = "Ireland Province 2", LocationType = LocationType.Province, ParentId = ireland.Id });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<ClientInfo> ClientInfos { get; set; }
    }
}