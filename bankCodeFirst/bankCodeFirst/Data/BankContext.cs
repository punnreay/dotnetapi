using bankCodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace bankCodeFirst.Data
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AccountStatus> AccountStatuses { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<TransactionTypes> TransactionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            var etbuilder = modelBuilder.Entity<AccountStatus>();
            etbuilder.ToTable("accountstatuses").HasKey(e => e.Id);
            etbuilder.HasIndex(e => e.Code).IsUnique(true);

            etbuilder.Property(e => e.Id).IsRequired(true).HasColumnType("varchar").HasMaxLength(36).IsUnicode(false);
            etbuilder.Property(e => e.Code).IsRequired(true).HasColumnType("varchar").HasMaxLength(10).IsUnicode(false);
            etbuilder.Property(e => e.Text).IsRequired(false).HasColumnType("nvarchar").HasMaxLength(50).IsUnicode(true);


            var accountetb = modelBuilder.Entity<Account>();
            accountetb.ToTable("accounts").HasKey(e => e.Id);
            accountetb.HasIndex(e => e.Number).IsUnique(true);

            accountetb.Property(e => e.Id).IsRequired(true).HasColumnType("varchar").HasMaxLength(36).IsUnicode(false);
            accountetb.Property(e => e.Number).IsRequired(true).HasColumnType("varchar").HasMaxLength(10).IsUnicode(false);
            accountetb.Property(e => e.Holder).IsRequired(true).HasColumnType("nvarchar").HasMaxLength(50).IsUnicode(true);
            accountetb.Property(e => e.Balance).IsRequired(true).HasColumnType("decimal");
            accountetb.Property(e => e.AccountStatusId).IsRequired(true).HasColumnType("varchar").HasMaxLength(36).IsUnicode(false);
            accountetb.HasOne(e => e.AccountStatus).WithMany(p => p.Accounts).HasForeignKey(e => e.AccountStatusId)
                .OnDelete(DeleteBehavior.NoAction);

            var tranTypeBuilder = modelBuilder.Entity<TransactionTypes>();
            tranTypeBuilder.ToTable("transactiontypes").HasKey(e => e.Id);
            tranTypeBuilder.HasIndex(e => e.Code).IsUnique(true);

            tranTypeBuilder.Property(e => e.Id).IsRequired(true).HasColumnType("varchar").HasMaxLength(36).IsUnicode(false);
            tranTypeBuilder.Property(e => e.Code).IsRequired(true).HasColumnType("varchar").HasMaxLength(10).IsUnicode(false);
            tranTypeBuilder.Property(e => e.Text).IsRequired(false).HasColumnType("nvarchar").HasMaxLength(50).IsUnicode(true);


            var tranetb = modelBuilder.Entity<Transactions>();
            tranetb.ToTable("transactions").HasKey(e => e.Id);


            tranetb.Property(e => e.Id).IsRequired(true).HasColumnType("varchar").HasMaxLength(36).IsUnicode(false);
            tranetb.Property(e => e.Amount).IsRequired(true).HasColumnType("decimal");

            tranetb.Property(e => e.Note).IsRequired(false).HasColumnType("nvarchar").HasMaxLength(100).IsUnicode(false);

           //define reletionship account => Transactions
           tranetb.Property(e => e.AccountId).IsRequired(true).HasColumnType("varchar").HasMaxLength(36).IsUnicode(false);
           tranetb.HasOne(e => e.Accounts).WithMany(p => p.Transactions).HasForeignKey(e => e.AccountId)
                .OnDelete(DeleteBehavior.NoAction);

            tranetb.Property(e => e.TransactionTypeId).IsRequired(true).HasColumnType("varchar").HasMaxLength(36).IsUnicode(false);
            tranetb.HasOne(e => e.TransactionTypes).WithMany(p => p.Transactions).HasForeignKey(e => e.TransactionTypeId)
                 .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
