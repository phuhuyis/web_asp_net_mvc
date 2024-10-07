using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace G03Trinh_eQLBHDabiLocal.Entity
{
    public partial class EFDbContext : DbContext
    {
        public EFDbContext()
            : base("name=EFDbContext")
        {
        }

        public virtual DbSet<account> account { get; set; }
        public virtual DbSet<bill> bill { get; set; }
        public virtual DbSet<bill_info> bill_info { get; set; }
        public virtual DbSet<cart> cart { get; set; }
        public virtual DbSet<category> category { get; set; }
        public virtual DbSet<contact> contact { get; set; }
        public virtual DbSet<customer> customer { get; set; }
        public virtual DbSet<product> product { get; set; }
        public virtual DbSet<bank> bank { get; set; }
        public virtual DbSet<slide> slide { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<account>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<account>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<account>()
                .HasMany(e => e.customer)
                .WithRequired(e => e.account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<bill>()
                .HasMany(e => e.bill_info)
                .WithRequired(e => e.bill1)
                .HasForeignKey(e => e.bill)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<category>()
                .Property(e => e.metatitle)
                .IsUnicode(false);

            modelBuilder.Entity<category>()
                .HasMany(e => e.product)
                .WithRequired(e => e.category1)
                .HasForeignKey(e => e.category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.bill)
                .WithRequired(e => e.customer1)
                .HasForeignKey(e => e.customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.cart)
                .WithRequired(e => e.customer1)
                .HasForeignKey(e => e.customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.contact)
                .WithRequired(e => e.customer1)
                .HasForeignKey(e => e.customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<product>()
                .Property(e => e.price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<product>()
                .HasMany(e => e.bill_info)
                .WithRequired(e => e.product1)
                .HasForeignKey(e => e.product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<product>()
                .HasMany(e => e.cart)
                .WithRequired(e => e.product1)
                .HasForeignKey(e => e.product)
                .WillCascadeOnDelete(false);
        }
    }
}
