using Microsoft.EntityFrameworkCore;
using realationshipss.Entities;


namespace realationshipss.data
{

    public class Datacontext : DbContext
    {
        public Datacontext(DbContextOptions<Datacontext> options) : base(options) { }

        public DbSet<Book> TbBOOK { get; set; } = null!;
        public DbSet<Author> TbAuthors { get; set; } = null!;
        public DbSet<BookAuthor> TbBookAuthor { get; set; } = null!;
        public DbSet<User> TbUser { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>()
            .HasKey(ba => new { ba.BookId, ba.AuthorId});

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.BookId);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new EntityInterceptor());
            base.OnConfiguring(optionsBuilder);
        }

    }
}
