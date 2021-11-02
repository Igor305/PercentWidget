using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataAccessLayer.AppContext.Avrora37
{
    public partial class Avrora37Context : DbContext
    {
        public Avrora37Context()
        {
        }

        public Avrora37Context(DbContextOptions<Avrora37Context> options)
            : base(options)
        {
        }

        public virtual DbSet<ExecutionPlanDateHistory> ExecutionPlanDateHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=SQL31,1433;Initial Catalog=Avrora37; Integrated Security=false; User ID=rpitvInfo; Password=QQQqqq123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("RpiTvInfo")
                .HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<ExecutionPlanDateHistory>(entity =>
            {
                entity.HasKey(e => e.Dates)
                    .HasName("PK__Executio__BFFD859286F63F4B");

                entity.ToTable("ExecutionPlanDate_History", "dbo");

                entity.Property(e => e.Dates).HasColumnType("smalldatetime");

                entity.Property(e => e.ChainFactDay).HasColumnType("numeric(21, 9)");

                entity.Property(e => e.ChainFactToDate).HasColumnType("numeric(21, 9)");

                entity.Property(e => e.ChainPlanDay).HasColumnType("numeric(21, 9)");

                entity.Property(e => e.ChainPlanToDate).HasColumnType("numeric(21, 9)");

                entity.Property(e => e.ExecutionPlanDayPercent).HasColumnType("numeric(21, 9)");

                entity.Property(e => e.ExecutionPlanDayUah)
                    .HasColumnType("numeric(21, 9)")
                    .HasColumnName("ExecutionPlanDayUAH");

                entity.Property(e => e.ExecutionPlanToDatePercent).HasColumnType("numeric(21, 9)");

                entity.Property(e => e.ExecutionPlanToDateUah)
                    .HasColumnType("numeric(21, 9)")
                    .HasColumnName("ExecutionPlanToDateUAH");
            });

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
