using Repository.Models;
using System.Data.Entity;

namespace Repository
{
	public class DriversDbContext : DbContext
	{
		public DriversDbContext() : base("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=DriversDatabase;Integrated Security=SSPI;")
		{
			Database.SetInitializer(new DropCreateDatabaseAlways<DriversDbContext>());
		}

		public DbSet<Driver> Drivers { get; set; }

		public DbSet<Car> Cars { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Driver>()
				.HasKey(d => d.Id)
				.Property(d => d.Id)
				.HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

			modelBuilder.Entity<Driver>()
				.Property(d => d.FirstName).HasMaxLength(400).IsRequired();

			modelBuilder.Entity<Driver>()
				.Property(d => d.LastName).HasMaxLength(400).IsRequired();

			modelBuilder.Entity<Driver>()
				.Property(d => d.MiddleName).HasMaxLength(400);

			modelBuilder.Entity<Driver>()
				.HasIndex(d => new { d.FirstName, d.LastName, d.MiddleName, d.Birthday }).IsUnique();

			modelBuilder.Entity<Car>()
				.HasKey(c => c.Id)
				.Property(c => c.Id)
				.HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

			modelBuilder.Entity<Car>()
				.Property(c => c.Brand).IsRequired();

			modelBuilder.Entity<Car>()
				.Property(c => c.RegistrationNumber).HasMaxLength(400).IsRequired();

			modelBuilder.Entity<Car>()
				.HasIndex(c => c.RegistrationNumber).IsUnique();

			modelBuilder.Entity<Driver>()
				.HasMany(d => d.Cars)
				.WithMany(c => c.Drivers);

			base.OnModelCreating(modelBuilder);
		}
	}
}