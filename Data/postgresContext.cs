using kanban_websocket_back.Models;
using Microsoft.EntityFrameworkCore;

namespace kanban_websocket_back.Data
{
    public partial class postgresContext : DbContext
    {
        public postgresContext()
        {
        }

        public postgresContext(DbContextOptions<postgresContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Board> Boards { get; set; } = null!;
        public virtual DbSet<Formmodel> Formmodels { get; set; } = null!;
        public virtual DbSet<Kanban> Kanbans { get; set; } = null!;
        public virtual DbSet<Lead> Leads { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#pragma warning disable CS1030 // #warning: "To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263."
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=db.uvkgkuftklgukrauvhnx.supabase.co:5432;Database=postgres;Username=postgres;Password=Kanban202015$");
#pragma warning restore CS1030 // #warning: "To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263."
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum("auth", "aal_level", new[] { "aal1", "aal2", "aal3" })
                .HasPostgresEnum("auth", "code_challenge_method", new[] { "s256", "plain" })
                .HasPostgresEnum("auth", "factor_status", new[] { "unverified", "verified" })
                .HasPostgresEnum("auth", "factor_type", new[] { "totp", "webauthn" })
                .HasPostgresEnum("pgsodium", "key_status", new[] { "default", "valid", "invalid", "expired" })
                .HasPostgresEnum("pgsodium", "key_type", new[] { "aead-ietf", "aead-det", "hmacsha512", "hmacsha256", "auth", "shorthash", "generichash", "kdf", "secretbox", "secretstream", "stream_xchacha20" })
                .HasPostgresExtension("extensions", "pg_stat_statements")
                .HasPostgresExtension("extensions", "pgcrypto")
                .HasPostgresExtension("extensions", "pgjwt")
                .HasPostgresExtension("extensions", "uuid-ossp")
                .HasPostgresExtension("graphql", "pg_graphql")
                .HasPostgresExtension("pgsodium", "pgsodium")
                .HasPostgresExtension("vault", "supabase_vault");

            modelBuilder.Entity<Board>(entity =>
            {
                entity.ToTable("board");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BoardName).HasColumnType("character varying");

                entity.Property(e => e.Color).HasColumnType("character varying");

                entity.Property(e => e.PropsBoard).HasColumnType("json");

                entity.HasOne(d => d.Kanban)
                    .WithMany(p => p.Boards)
                    .HasForeignKey(d => d.KanbanId)
                    .HasConstraintName("board_KanbanId_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Boards)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("board_UserId_fkey");
            });

            modelBuilder.Entity<Formmodel>(entity =>
            {
                entity.ToTable("formmodel");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Properties).HasColumnType("json");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Formmodels)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("formmodel_UserId_fkey");
            });

            modelBuilder.Entity<Kanban>(entity =>
            {
                entity.ToTable("kanban");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.KanbanName).HasColumnType("character varying");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Kanbans)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("kanban_UserId_fkey");
            });

            modelBuilder.Entity<Lead>(entity =>
            {
                entity.ToTable("lead");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Color).HasColumnType("character varying");

                entity.Property(e => e.Comments).HasColumnType("character varying");

                entity.Property(e => e.Description).HasColumnType("character varying");

                entity.Property(e => e.IndexNumber).HasColumnName("Index_Number");

                entity.Property(e => e.PropsLead).HasColumnType("json");

                entity.Property(e => e.Title).HasColumnType("character varying");

                entity.HasOne(d => d.Board)
                    .WithMany(p => p.Leads)
                    .HasForeignKey(d => d.BoardId)
                    .HasConstraintName("lead_BoardId_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Leads)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("lead_UserId_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email).HasColumnType("character varying");

                entity.Property(e => e.Name).HasColumnType("character varying");

                entity.Property(e => e.Password).HasColumnType("character varying");
            });


            modelBuilder.HasSequence<int>("seq_schema_version", "graphql").IsCyclic();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
