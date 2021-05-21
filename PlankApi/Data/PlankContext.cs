using Microsoft.EntityFrameworkCore;
using PlankApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace PlankApi.Data
{
    public class PlankContext : IdentityDbContext
    {
        public PlankContext(DbContextOptions<PlankContext> options)
             : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Plank>()
                .HasMany(p => p.Tags)
                .WithOne()
                .IsRequired()
                .HasForeignKey("PlankId"); //Shadow property
            builder.Entity<Plank>().Property(r => r.Titel).IsRequired().HasMaxLength(50);
            builder.Entity<Plank>().Property(r => r.materiaal).HasMaxLength(50);
            builder.Entity<Plank>().Property(r => r.prijs).IsRequired();
            builder.Entity<Plank>().Property(r => r.hoogte).IsRequired();
            builder.Entity<Plank>().Property(r => r.breedte).IsRequired();
            builder.Entity<Plank>().Property(r => r.dikte).IsRequired();
            builder.Entity<Plank>().Property(r => r.aantalInStock).IsRequired(); 
            builder.Entity<Plank>().Property(r => r.beschrijving).IsRequired();
            builder.Entity<Tag>().Property(r => r.Name).IsRequired().HasMaxLength(50);

            builder.Entity<Customer>().Property(c => c.LastName).IsRequired().HasMaxLength(50);
            builder.Entity<Customer>().Property(c => c.FirstName).IsRequired().HasMaxLength(50);
            builder.Entity<Customer>().Property(c => c.Email).IsRequired().HasMaxLength(100);
            builder.Entity<Customer>().Ignore(c => c.FavoritePlanken);

            builder.Entity<CustomerFavorite>().HasKey(f => new { f.CustomerId, f.PlankId });
            builder.Entity<CustomerFavorite>().HasOne(f => f.Customer).WithMany(u => u.Favorites).HasForeignKey(f => f.CustomerId);
            builder.Entity<CustomerFavorite>().HasOne(f => f.Plank).WithMany().HasForeignKey(f => f.PlankId);


            //Another way to seed the database
            builder.Entity<Plank>().HasData(
                 new Plank { Id = 1, Titel = "Heart cutting board", aantalInStock=100, dikte=1.5, hoogte=20, breedte=18, materiaal="Wood", prijs=16.95,beschrijving= "Nice little heart-shaped breakfast board. It will delight lovers of beautiful objects as well as lovers at large. Don't forget to order the pair!" },
                 new Plank { Id = 2, Titel = "Monster cutting board", aantalInStock = 150, dikte = 1.5, hoogte = 19, breedte = 19, materiaal = "Wood", prijs = 18.95,beschrijving= "Cranky kids in the morning... it's over! You'll put a smile back on their faces from breakfast with our cool breadboards. Don't you find them monstrously adorable?" },
                 new Plank { Id = 3, Titel = "Pear cutting board", aantalInStock = 250, dikte = 1.5, hoogte = 19, breedte = 19, materiaal = "Wood", prijs = 18.95,beschrijving="Beautiful fruits on a beautiful table, what could be simpler thanks to this adorable little breakfast board?" }
  );

            builder.Entity<Tag>().HasData(
                    //Shadow property can be used for the foreign key, in combination with anaonymous objects
                    new { Id = 1, Name = "Fruits", PlankId = 3 },
                    new { Id = 2, Name = "Kids", PlankId = 2 },
                    new { Id = 3, Name = "Funny", PlankId = 2 },
                    new { Id = 4, Name = "Valentine", PlankId = 1 },
                    new { Id = 5, Name = "Love", PlankId = 1 }
                 );
        }

        public DbSet<Plank> Planken { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
