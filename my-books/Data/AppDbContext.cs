﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using my_books.Data.Models;

namespace my_books.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options ): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book_Author>()
                .HasOne(b => b.Book)
                .WithMany(ba => ba.Book_Authors)
                .HasForeignKey(b => b.BookId);

            modelBuilder.Entity<Book_Author>()
                .HasOne(b => b.Author)
                .WithMany(ba => ba.Book_Authors)
                .HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<Log>().HasKey(n => n.Id);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book_Author> Book_Authors { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
